using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Services.Helpers
{
    // GraphQL client for the HRIS API (D:\HRIS, default http://127.0.0.1:4001).
    //
    // Deliberately a SIBLING of ApiService<T>, not an extension of it:
    //  - ApiService<T>'s base URL is global/immutable (Program.ApiBaseUrl) and the
    //    HRIS service lives on a different port;
    //  - ServiceBase<T> hardcodes the REST {success,data,message} envelope, while
    //    GraphQL responds {data,errors} - and returns HTTP 200 even on business
    //    errors, so callers must check HasErrors, never an HTTP status or Success flag.
    //
    // Auth is the suite convention: the ERP login token from CacheData.SessionToken,
    // sent as a raw Authorization header (no Bearer prefix). The HRIS API validates
    // it with the same SECRET_KEY, so no second login exists.
    //
    // Uses ONE shared HttpClient (ApiService<T>'s per-request client is a known
    // socket-exhaustion antipattern - do not copy it).
    public class GraphQLError
    {
        [JsonProperty("message")] public string Message { get; set; }
    }

    public class GraphQLResponse<T>
    {
        [JsonProperty("data")] public T Data { get; set; }
        [JsonProperty("errors")] public List<GraphQLError> Errors { get; set; }

        [JsonIgnore] public bool HasErrors => Errors != null && Errors.Count > 0;
        [JsonIgnore]
        public string ErrorMessage => HasErrors
            ? string.Join("; ", Errors.Select(e => e.Message))
            : null;
    }

    public class HrisUploadResult
    {
        [JsonProperty("file_name")] public string FileName { get; set; }
        [JsonProperty("original_name")] public string OriginalName { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("size")] public int Size { get; set; }
    }

    public static class HrisApiService
    {
        private static readonly HttpClient _client = new HttpClient();

        private static string BaseUrl =>
            Program.HrisApiBaseUrl?.TrimEnd('/')
            ?? throw new InvalidOperationException(
                "HRIS API URL is not configured (HrisApiBaseUrl key in App.config).");

        public static async Task<GraphQLResponse<T>> ExecuteAsync<T>(string query, object variables = null)
        {
            var payload = new Dictionary<string, object> { { "query", query } };
            if (variables != null) payload["variables"] = variables;

            var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + "/graphql")
            {
                Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
            };
            AttachToken(request);

            var response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            // Auth middleware failures (401/403) answer with the suite's REST
            // envelope {success,message}, not a GraphQL body - normalize them.
            if (!response.IsSuccessStatusCode)
            {
                return new GraphQLResponse<T>
                {
                    Errors = new List<GraphQLError>
                    {
                        new GraphQLError { Message = ExtractEnvelopeMessage(content)
                            ?? $"HRIS API error (HTTP {(int)response.StatusCode})" }
                    }
                };
            }

            return JsonConvert.DeserializeObject<GraphQLResponse<T>>(content);
        }

        // Multipart upload to POST /files; reference the returned file_name via the
        // addEmployeeFile mutation afterwards.
        public static async Task<HrisUploadResult> UploadFileAsync(string filePath)
        {
            using (var form = new MultipartFormDataContent())
            using (var stream = File.OpenRead(filePath))
            {
                form.Add(new StreamContent(stream), "file", Path.GetFileName(filePath));

                var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + "/files") { Content = form };
                AttachToken(request);

                var response = await _client.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();
                var parsed = JsonConvert.DeserializeObject<HrisFileEnvelope>(content);
                if (parsed == null || !parsed.Success || parsed.Data == null)
                {
                    throw new InvalidOperationException(parsed?.Message ?? "File upload failed.");
                }
                return parsed.Data;
            }
        }

        private static void AttachToken(HttpRequestMessage request)
        {
            if (!string.IsNullOrEmpty(CacheData.SessionToken))
            {
                request.Headers.Add("Authorization", CacheData.SessionToken);
            }
        }

        private static string ExtractEnvelopeMessage(string content)
        {
            try
            {
                var envelope = JsonConvert.DeserializeObject<HrisFileEnvelope>(content);
                return envelope?.Message;
            }
            catch
            {
                return null;
            }
        }

        private class HrisFileEnvelope
        {
            [JsonProperty("success")] public bool Success { get; set; }
            [JsonProperty("data")] public HrisUploadResult Data { get; set; }
            [JsonProperty("message")] public string Message { get; set; }
        }
    }
}

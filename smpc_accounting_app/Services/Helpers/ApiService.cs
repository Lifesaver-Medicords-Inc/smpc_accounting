using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Services.Helpers
{

    public static class ApiService<T> where T : class
    {
        static string baseUrl => Program.ApiBaseUrl ?? "http://127.0.0.1:3000/api";
        static private async Task<T> SendRequestAsync(string url, HttpMethod method, string body = null)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpContent content = null;
                    // If no content is provided, create an empty StringContent with Content-Type set to "application/json"
                    if (content == null && method != HttpMethod.Get)
                    {
                        content = new StringContent(body, Encoding.UTF8, "application/json");
                    }

                    // Create the HttpRequestMessage with the specified method (GET, POST, PUT, DELETE)
                    var requestMessage = new HttpRequestMessage(method, baseUrl + url)
                    {
                        Content = content
                    };

                    // ERP_API's RequireAuth middleware rejects every request with no
                    // token ("Missing authentication token") - the token is only ever
                    // delivered via a Set-Cookie header at login (utils.CreateAuthToken),
                    // never in the JSON body, and this class never read it or resent it.
                    // Same fix as smpc_inventory_app's RequestToApi.cs, which already
                    // works this way.
                    if (!string.IsNullOrEmpty(CacheData.SessionToken))
                    {
                        requestMessage.Headers.Add("Authorization", CacheData.SessionToken);
                    }

                    // Perform the HTTP request asynchronously
                    HttpResponseMessage response = await client.SendAsync(requestMessage);

                    if (string.IsNullOrEmpty(CacheData.SessionToken)
                        && response.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
                    {
                        string token = ExtractToken(setCookieValues.First());
                        if (!string.IsNullOrEmpty(token))
                        {
                            CacheData.SessionToken = token;
                        }
                    }

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Optionally, you can parse the responseContent into an object of type T
                        T result = JsonConvert.DeserializeObject<T>(responseContent);

                        // Display the response content (for debugging purposes)
                        //MessageBox.Show(responseContent, " Response");

                        return result; // Return the parsed result
                    }

                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Optionally, you can parse the responseContent into an object of type T
                        T result = JsonConvert.DeserializeObject<T>(responseContent);

                        // Display the response content (for debugging purposes)
                        //MessageBox.Show(responseContent, " Response");

                        return result; // Return the
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //// POST Method
        static internal async Task<T> Post(string url, HttpContent data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);

            return await SendRequestAsync(url, HttpMethod.Post, jsonContent);
        }

        static internal async Task<T> Post(string url, Dictionary<string, dynamic> data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);

            return await SendRequestAsync(url, HttpMethod.Post, jsonContent);
        }

        // PUT Method
        static internal async Task<T> Put(string url, HttpContent data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);

            return await SendRequestAsync(url, HttpMethod.Put, jsonContent);
        }
        static internal async Task<T> Put(string url, Dictionary<string, object> data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);

            return await SendRequestAsync(url, HttpMethod.Put, jsonContent);
        }

        // GET Method
        public static async Task<T> Get(string url)
        {
            return await SendRequestAsync(url, HttpMethod.Get);
        }

        //DELETE Method
        static internal async Task<T> Delete(string url, HttpContent data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);

            return await SendRequestAsync(url, HttpMethod.Delete, jsonContent);
        }

        static internal async Task<T> Delete(string url, Dictionary<string, object> data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);

            return await SendRequestAsync(url, HttpMethod.Delete, jsonContent);
        }

        // Same parsing as smpc_inventory_app.Services.Helpers.RequestToApi<T>.ExtractToken -
        // the cookie header looks like "Authorization=<jwt>; expires=...; SameSite=Lax"
        // (utils.CreateAuthToken), so pull out just the value between "Authorization="
        // and the next ";" (or end of string if there isn't one).
        private static string ExtractToken(string cookieString)
        {
            int tokenStartIndex = cookieString.IndexOf("Authorization=") + "Authorization=".Length;
            if (tokenStartIndex < "Authorization=".Length) return null; // "Authorization=" not found

            int tokenEndIndex = cookieString.IndexOf(";", tokenStartIndex);
            return tokenEndIndex == -1
                ? cookieString.Substring(tokenStartIndex)
                : cookieString.Substring(tokenStartIndex, tokenEndIndex - tokenStartIndex);
        }
    }
}

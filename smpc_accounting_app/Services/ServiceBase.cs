using smpc_accounting_app.Services.Helpers;
using smpc_sales_app.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services
{
    abstract class ServiceBase<T> where T:class
    {
        private readonly string url ;

        public ServiceBase(string _url)
        {
            this.url = _url;
        }

        // GET
        public   virtual async Task<DataTable> GetAsDatatable()
        {
            try
            {
                var response = await ApiService<ApiResponseModel<List<T>>>.Get(url);

                DataTable data = JsonHelper.ToDataTable(response.data);

                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<T> GetAsModel()
        {
            try
            {
                var response = await ApiService<ApiResponseModel<T>>.Get(url);

                return response.data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<List<T>> GetAsList()
        {
            try
            {
                var response = await ApiService<ApiResponseModel<List<T>>>.Get(url);

                return response.data ?? new List<T>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<DataTable> GetAsDatatable(Func<DataTable, DataTable> filter)
        {
            var response = await ApiService<ApiResponseModel<List<T>>>.Get(url);

            DataTable data = JsonHelper.ToDataTable(response.data);

            return filter(data);
        }

        // ---------- MODEL → DICTIONARY CONVERTER ----------
        private Dictionary<string, dynamic> ModelToDictionary(T model)
        {
            return model
                .GetType()
                .GetProperties()
                .ToDictionary(
                    p => p.Name,
                    p => p.GetValue(model)
                );
        }

        // POST
        public async Task<ApiResponseModel> Insert(Dictionary<string, dynamic> data)
        {
            var response = await ApiService<ApiResponseModel>.Post(url, data);
            return response;
        }

        public async Task<ApiResponseModel> Insert(T model)
        {
            var data = ModelToDictionary(model);
            var response = await ApiService<ApiResponseModel>.Post(url, data);
            return response;
        }

        // DELETE
        public   async Task<bool> Delete(Dictionary<string, dynamic> data)
        {
            var response = await ApiService<ApiResponseModel<T>>.Delete(url, data);
            bool isSuccess = response.success;

            return isSuccess;
        }

        public async Task<bool> Delete(T model)
        {
            var data = ModelToDictionary(model);
            var response = await ApiService<ApiResponseModel<T>>.Delete(url, data);
            return response.success;
        }

        // UPDATE
        public async Task<ApiResponseModel> Update(Dictionary<string, dynamic> data)
        {
            var response = await ApiService<ApiResponseModel>.Put(url, data);

            return response;
        }

        public async Task<ApiResponseModel> Update(T model)
        {
            var data = ModelToDictionary(model);
            var response = await ApiService<ApiResponseModel>.Put(url, data);
            return response;
        }
    }
}
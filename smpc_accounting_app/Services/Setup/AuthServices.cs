using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;

namespace smpc_accounting_app.Services.Setup
{
    class AuthServices
    {
        static string url = "/login";

        // Login
        public static async Task<ApiResponseModel<CurrentUserModel>> Login(Dictionary<string, dynamic> data)
        {
            var response = await ApiService<ApiResponseModel<CurrentUserModel>>.Post(url, data);
            return response;
        }

        // Logout
        public static async Task<ApiResponseModel> Logout(Dictionary<string, dynamic> data)
        {
            var response = await ApiService<ApiResponseModel>.Post(url, data);
            return response;
        }
    }
}

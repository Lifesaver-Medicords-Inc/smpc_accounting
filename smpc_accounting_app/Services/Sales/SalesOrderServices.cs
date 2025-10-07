using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Sales
{
    class SalesOrderServices : ServiceBase<SalesOrderDR>
    {
        public SalesOrderServices() : base(ApiEndPoints.SALES_ORDER_DR) { }
       


        public  async Task<SalesOrderList> GetSalesOrderId(string id)
        { 
            var response = await ApiService<ApiResponseModel<SalesOrderList>>.Get(ApiEndPoints.SALES_ORDER_DR + id );

            return response.data;
        }

    }
}

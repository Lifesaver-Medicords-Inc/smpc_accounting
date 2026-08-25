using System.Collections.Generic;
using System.Threading.Tasks;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Services.Transactions
{
    // ApiResponseModel<object> has no "message" field, so a validation error
    // from CreateCreditMemo/ApproveCreditMemo (utils.RespondError's {success,
    // message} shape) would be silently unreadable through it - this small
    // dedicated shape exposes the message the server actually sent.
    public class CreditMemoActionResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }


    // Custom fetch/create/approve methods, not the generic ServiceBase<T> ones -
    // ERP_API's GetCreditMemo/CreateCreditMemo wrap their payload as
    // {"credit_memo": ...} rather than returning/accepting T directly (same
    // convention as every other new document built this phase), so
    // ServiceBase<CreditMemoModel>.GetAsList()/Insert() would deserialize the
    // wrong shape.
    class CreditMemoService : ServiceBase<CreditMemoModel>
    {
        public CreditMemoService() : base(ApiEndPoints.CREDIT_MEMO) { }

        public async Task<List<CreditMemoModel>> GetCreditMemos()
        {
            var response = await ApiService<ApiResponseModel<CreditMemoGetResponse>>.Get(ApiEndPoints.CREDIT_MEMO);
            return response?.data?.credit_memo ?? new List<CreditMemoModel>();
        }

        public async Task<CreditMemoActionResponse> CreateCreditMemo(CreditMemoModel payload)
        {
            var response = await ApiService<CreditMemoActionResponse>.Post(ApiEndPoints.CREDIT_MEMO, new Dictionary<string, dynamic>
            {
                { "credit_memo", payload }
            });

            return response;
        }

        // §14.99 - COO only, customer-side Credit Memos only. The server enforces
        // both; this just calls the endpoint.
        public async Task<CreditMemoActionResponse> ApproveCreditMemo(int creditMemoId)
        {
            var response = await ApiService<CreditMemoActionResponse>.Post(
                $"{ApiEndPoints.CREDIT_MEMO}/{creditMemoId}/approve",
                new Dictionary<string, dynamic>()
            );

            return response;
        }
    }
}

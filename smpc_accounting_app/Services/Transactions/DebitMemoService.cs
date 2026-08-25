using System.Collections.Generic;
using System.Threading.Tasks;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Services.Transactions
{
    // Same reasoning as CreditMemoActionResponse: ApiResponseModel<object> has
    // no "message" field, so a validation error from CreateDebitMemo
    // (utils.RespondError's {success, message} shape - e.g. the §14.43
    // "unapplied_amount must reach 0" rejection) would be silently
    // unreadable through it.
    public class DebitMemoActionResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    // Custom fetch/create methods, not the generic ServiceBase<T> ones -
    // ERP_API's GetDebitMemo/CreateDebitMemo wrap the payload as
    // {"debit_memo": ..., "debit_memo_details": [...]} rather than
    // returning/accepting T directly, same convention as every other new
    // document built this phase. There is no ApproveDebitMemo - the Go route
    // group has no approve endpoint at all (§14.57, commits entirely on
    // save).
    class DebitMemoService : ServiceBase<DebitMemoModel>
    {
        public DebitMemoService() : base(ApiEndPoints.DEBIT_MEMO) { }

        public async Task<List<DebitMemoModel>> GetDebitMemos()
        {
            var response = await ApiService<ApiResponseModel<DebitMemoGetResponse>>.Get(ApiEndPoints.DEBIT_MEMO);
            var header = response?.data?.debit_memo ?? new List<DebitMemoModel>();
            var details = response?.data?.debit_memo_details ?? new List<DebitMemoDetailsModel>();

            // The Go response splits header and details into two parallel
            // arrays (same shape as Sales Return/Purchase Return) - nest the
            // details back onto their own header here so callers only ever
            // deal with one shape.
            foreach (var dm in header)
            {
                dm.debit_memo_details = details.FindAll(d => d.debit_memo_id == dm.id);
            }

            return header;
        }

        public async Task<DebitMemoActionResponse> CreateDebitMemo(DebitMemoBody payload)
        {
            var response = await ApiService<DebitMemoActionResponse>.Post(ApiEndPoints.DEBIT_MEMO, new Dictionary<string, dynamic>
            {
                { "debit_memo", payload.debit_memo },
                { "debit_memo_details", payload.debit_memo_details }
            });

            return response;
        }
    }
}

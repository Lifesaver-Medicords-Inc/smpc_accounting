using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Transactions
{
    class JournalEntryService : ServiceBase<JournalEntryList>
    {
        public JournalEntryService() : base(ApiEndPoints.JOURNAL_ENTRY) { }

        // CREATE
        public async Task<object> CreateJERecord(JournalEntryPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.JOURNAL_ENTRY, new Dictionary<string, dynamic>
                {
                    { "journal_entry", payload.journal_entry },
                    { "journal_entry_details", payload.journal_entry_details }
                }
            );

            return response.data;
        }

        // UPDATE
        public async Task<object> UpdateJERecord(JournalEntryPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Put(ApiEndPoints.JOURNAL_ENTRY, new Dictionary<string, dynamic>
                {
                    { "journal_entry", payload.journal_entry },
                    { "journal_entry_details", payload.journal_entry_details }
                } 
            );

            return response.data;
        }

        // DELETE
        public async Task<object> DeleteJERecord(JournalEntryPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Delete(ApiEndPoints.JOURNAL_ENTRY, new Dictionary<string, dynamic>
                {
                    { "journal_entry", payload.journal_entry },
                    { "journal_entry_details", payload.journal_entry_details }
                }
            );

            return response.data;
        }
    }
}

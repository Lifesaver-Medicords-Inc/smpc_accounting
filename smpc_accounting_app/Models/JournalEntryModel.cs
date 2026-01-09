using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class JournalEntryModel
    {
        public int id { get; set; }
        public string journal_name { get; set; }
        public string journal_description { get; set; }
        public string doc_no { get; set; }
        public string period { get; set; }
        public string currency { get; set; }
        public string created_by { get; set; }
    }

    public class JournalEntryDetailsModel
    {
        public int id { get; set; }
        public int journal_entry_id { get; set; }
        public string inserted_date { get; set; }
        public string posting_date { get; set; }
        public string account_title { get; set; }
        public string posting_ref { get; set; }
        public int posting_ref_id { get; set; }
        public float debit { get; set; }
        public float credit { get; set; }
        public string line_memo { get; set; }
        public string origin { get; set; }
        public int origin_no { get; set; }
        public string created_by { get; set; }
    }

    public class JournalEntryList
    {
        public List<JournalEntryModel> journal_entry { get; set; }
        public List<JournalEntryDetailsModel> journal_entry_details { get; set; }
    }

    public class JournalEntryPayload
    {
        public JournalEntryModel journal_entry { get; set; }
        public List<JournalEntryDetailsModel> journal_entry_details { get; set; }
    }
}

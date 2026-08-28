using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class ChartOfAccountsModel
    {
       public int id { get; set; }
       public string code { get; set; }
       public string name { get; set; }
       public string account_class { get; set; }
       public int class_id { get; set; }
       public string group { get; set; }
       public int group_id { get; set; }
       // OPERATING (default/blank) | FINANCING - feeds the Cash Flow
       // Statement's Financing section.
       public string cash_flow_category { get; set; }
    }
}

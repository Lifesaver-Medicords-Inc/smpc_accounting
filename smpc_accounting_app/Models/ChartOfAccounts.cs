using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class ChartOfAccounts
    {
       public int id { get; set; }
       public string code { get; set; }
       public string name { get; set; }
       public string class_name { get; set; }
       public string group_name { get; set; }
       public int class_id { get; set; }
       public int group_id { get; set; }
    }
}

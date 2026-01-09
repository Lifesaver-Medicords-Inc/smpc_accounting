using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class ChartClassModel
    {
        public int id { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string type { get; set; }
    }

    class ChartOfAccountsViewModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string type { get; set; }
    }
}

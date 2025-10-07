using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class GeneralSetup
    {
        public int id { get; set; }
        
        public string code { get; set; }
        
        public string name { get; set; }

        public bool? is_selected { get; set; }
    }
}

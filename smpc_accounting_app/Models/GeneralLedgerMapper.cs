using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class GeneralLedgerMapper
    {
        [Column("id")]
        public int id { get; set; }

        [Column("pseudo_account")]
        public string pseudo_account { get; set; }

        [Column("account_id")]
        public string account_id { get; set; } 
    }
}

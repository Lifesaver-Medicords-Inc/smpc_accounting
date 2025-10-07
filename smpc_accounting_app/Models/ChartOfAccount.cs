using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class ChartOfAccount
    {

        [Column("id")]
        public int id { get; set; }


        [Column("account_code")]
        public string account_code { get; set; }


        [Column("account_name")]
        public string account_name { get; set; }


        [Column("short_name")]
        public string short_name { get; set; }



        [Column("account_type")]
        public string account_type { get; set; }

    }
}

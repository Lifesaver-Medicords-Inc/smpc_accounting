using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class ChartClass
    {

        [Column("id")]
        public int Id { get; set; }


        [Column("code")]
        public string code { get; set; }


        [Column("name")]
        public string name { get; set; }

        [Column("type")]
        public string type { get; set; }
    }
}

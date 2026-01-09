using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class TaxSetupList
    {
        [Column("tax")]
        public List<TaxSetupModel> Tax { get; set; }

        [Column("tax_details")]
        public List<TaxDetails> Tax_Details { get; set; }

        [Column("tax_view")]
        public List<TaxView> Tax_View { get; set; }
    }
}

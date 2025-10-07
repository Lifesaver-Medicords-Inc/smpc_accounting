using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class TaxSetup
    {
        [Column("id")]
        public int id { get; set; }

        [Column("code")]
        public string code { get; set;}

        [Column("tax_desc")]
        public string tax_desc { get; set; }

        [Column("coa_sales_id")]
        public string coa_sales_id { get; set; }

        [Column("coa_purchase_id")]
        public string coa_purchase_id { get; set; }

        [Column("input_tax_creditable")]
        public bool? input_tax_creditable { get; set; }

        [Column("remarks")]
        public string remarks { get; set; }



    }
    class TaxDetails
    {
        public int id { get; set; }

        public int tax_code_id { get; set; }

        public string valid_from { get; set; }

        public string valid_to { get; set; }
        public string valid_status { get; set; }

        public decimal tax_rate { get; set; }


        public TaxDetails(int id, int tax_code_id, string valid_from , string valid_to, string valid_status, decimal tax_rate)
        {
            this.id = id;
            this.tax_code_id = tax_code_id;
            this.valid_from = valid_from;
            this.valid_to = valid_to;
            this.valid_status = valid_status;
            this.tax_rate = tax_rate;
        }

    }
    class TaxView
    {
       
        [Column("code")]
        public string Code { get; set; }

        [Column("tax_desc")]
        public string Tax_Desc { get; set; }

        [Column("output_tax_account")]
        public string Output_Tax_Account { get; set; }

        [Column("input_tax_account")]
        public string Input_Tax_Account { get; set; }

        [Column("tax_rate")]
        public decimal Tax_Rate { get; set; }

        [Column("effective_period")]
        public string Effective_Period { get; set; }



    }

    class TaxClassification
    {
        public int id { get; set; }
        public string code { get; set; }
        public bool? input_tax_creditable { get; set; }
        public string tax_desc { get; set; }
        public decimal tax_rate { get; set; }
        public int account_id { get; set; }
        public string status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class Taxation
    {
        private decimal net_amount;
        private string tax_code;
        private int vat_rate;

        public Taxation(decimal netAmount, string taxCode ,int vatRate)
        {
            this.net_amount = netAmount;
            this.tax_code = taxCode;
            this.vat_rate = vatRate; 
        }

        public TaxResult ComputeTaxValue()
        {
            var taxValue = new TaxResult();
            switch (this.tax_code)
            {
                case "VAT":
                    taxValue.vat_amount = this.net_amount * (this.vat_rate / 100);
                    taxValue.vatable = this.net_amount + taxValue.vat_amount;

                    break;
                case "ZERO-RATED":

                case "NON-VAT":
                    taxValue.vatable = this.net_amount;
                    taxValue.vat_amount = this.vat_rate;
              break;
            }

            return taxValue;
        } 
    }

    class TaxResult
    {
        public decimal vatable { get; set; }
        public decimal vat_amount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace smpc_accounting_app.Models
{
    public class ExchangeRateModel
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        public CurrencyRateModel rates { get; set; }
    }

    public class CurrencyRateModel
    {
        public double ADA { get; set; }
        public double JPY { get; set; }
        public double PHP { get; set; }
        public double USD { get; set; }
    }
}

using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Setup
{ 
    class ExpandedTaxService : ServiceBase<ChartGroup>
    {
        public ExpandedTaxService() : base(ApiEndPoints.CHART_GROUP_SETUP) { }
    }
} 
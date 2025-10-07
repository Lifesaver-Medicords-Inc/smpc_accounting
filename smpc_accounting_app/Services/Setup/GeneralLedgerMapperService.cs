using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
using smpc_sales_app.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Setup
{ 
    class GeneralLedgerMapperService : ServiceBase<GeneralLedgerMapper>
    {
        public GeneralLedgerMapperService() : base(ApiEndPoints.GENERAL_LEDGER_MAPPER_SETUP) { }  
    }
} 
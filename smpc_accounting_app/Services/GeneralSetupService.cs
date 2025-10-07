using smpc_accounting_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services
{
    class GeneralSetupService : ServiceBase<GeneralSetup>
    {
        public GeneralSetupService(string url) : base(url) { }
    }
}

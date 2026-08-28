using smpc_accounting_app.Models;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Services.Setup
{
    class FixedAssetService : ServiceBase<FixedAssetModel>
    {
        public FixedAssetService() : base(ApiEndPoints.FIXED_ASSET_SETUP) { }
    }
}

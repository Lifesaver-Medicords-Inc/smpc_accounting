using smpc_accounting_app.Models;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Services.Setup
{
    class AssetCategoryService : ServiceBase<AssetCategoryModel>
    {
        public AssetCategoryService() : base(ApiEndPoints.ASSET_CATEGORY_SETUP) { }
    }
}

namespace smpc_accounting_app.Models
{
    // PP&E register category level (LAND, BUILDING, MACHINERY, ...) - not in
    // the spec at all, see ERP_API's accounting_asset_category_model.go.
    class AssetCategoryModel
    {
        public int id { get; set; }

        public string code { get; set; }

        public string name { get; set; }
    }
}

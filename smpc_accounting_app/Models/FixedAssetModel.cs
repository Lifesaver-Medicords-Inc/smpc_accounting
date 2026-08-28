namespace smpc_accounting_app.Models
{
    // One PP&E item. Depreciation (straight-line) is computed server-side on
    // every read, never stored here - see ERP_API's fixed_asset_service.go.
    class FixedAssetModel
    {
        public int id { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public int category_id { get; set; }

        public string category_name { get; set; }

        public double cost { get; set; }

        // MM/dd/yyyy, same free-text date convention as every other date
        // field in this codebase.
        public string acquired_date { get; set; }

        // 0 marks a non-depreciable asset (e.g. Land).
        public double useful_life_years { get; set; }

        public double salvage_value { get; set; }

        // "ACTIVE" | "DISPOSED"
        public string status { get; set; }

        public string disposed_date { get; set; }
    }
}

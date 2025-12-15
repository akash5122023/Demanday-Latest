namespace AdvanceCRM.Settings.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using System;
    using System.ComponentModel;

    [ColumnsScript("Settings.ProductModule")]
    [BasedOnRow(typeof(ProductModuleRow), CheckNames = true)]
    public class ProductModuleColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public int Id { get; set; }

        [EditLink, Width(200)]
        public string DisplayName { get; set; }

        [Width(160)]
        public string Name { get; set; }

        [Width(300)]
        public string Description { get; set; }

        [DisplayFormat("#,##0.00"), AlignRight, Width(120)]
        public decimal? Price { get; set; }

        [Width(100)]
        public string Currency { get; set; }

        [QuickFilter, AlignCenter, Width(80)]
        public bool IsActive { get; set; }

        [Width(160)]
        [DisplayName("Modified On"), DisplayFormat("g")]
        public DateTime? ModifiedOn { get; set; }

        [Width(160)]
        public string ModifiedBy { get; set; }
    }
}

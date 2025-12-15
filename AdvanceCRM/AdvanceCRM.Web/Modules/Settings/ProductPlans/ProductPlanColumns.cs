namespace AdvanceCRM.Settings.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using System;
    using System.ComponentModel;

    [ColumnsScript("Settings.ProductPlan")]
    [BasedOnRow(typeof(ProductPlanRow), CheckNames = true)]
    public class ProductPlanColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }

        [EditLink]
        public String Name { get; set; }

        [DisplayFormat("#,##0.00"), AlignRight]
        public Decimal PricePerUser { get; set; }

        [AlignCenter]
        public Int32 TrialDays { get; set; }

        [AlignCenter]
        public Int32 UserLimit { get; set; }

        [AlignCenter]
        [DisplayName("Non Operational Users")]
        public Int32 NonOperationalUsers { get; set; }

        public String Currency { get; set; }

        [QuickFilter]
        public Boolean IsActive { get; set; }

        [Width(100)]
        public Int32 SortOrder { get; set; }

        [DisplayName("Badge Label")]
        public PlanBadgeLabel? BadgeLabel { get; set; }

        [DisplayName("Highlight Badge")]
        public Boolean BadgeHighlight { get; set; }
    }
}

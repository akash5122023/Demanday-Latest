namespace AdvanceCRM.Settings.Columns
{
    using Serenity.ComponentModel;
    using System;

    [ColumnsScript("Settings.CouponCode")]
    [BasedOnRow(typeof(CouponCodeRow), CheckNames = true)]
    public class CouponCodeColumns
    {
        [EditLink, Width(120)]
        public string Code { get; set; }

        [Width(90)]
        public string DiscountType { get; set; }

        [Width(120), AlignRight]
        public decimal DiscountValue { get; set; }

        [Width(120)]
        public int? MaxUsageCount { get; set; }

        [Width(100)]
        public int? UsedCount { get; set; }

        [Width(110)]
        public DateTime? ExpiryDate { get; set; }

        [Width(80)]
        public bool IsActive { get; set; }
    }
}

namespace AdvanceCRM.Settings.Forms
{
    using Serenity.ComponentModel;
    using System;
    using System.ComponentModel; // for CategoryAttribute

    [FormScript("Settings.CouponCode")]
    [BasedOnRow(typeof(CouponCodeRow), CheckNames = true)]
    public class CouponCodeForm
    {
    [Category("Coupon Details"), HalfWidth]
        public string Code { get; set; }

        [HalfWidth, LookupEditor(typeof(CouponDiscountTypeLookup))]
        public string DiscountType { get; set; }

        [HalfWidth, DecimalEditor(MinValue = "0", Decimals = 2)]
        public decimal DiscountValue { get; set; }

        [HalfWidth]
        public int? MaxUsageCount { get; set; }

        [HalfWidth]
        public DateTime? ExpiryDate { get; set; }

        [HalfWidth]
        public bool IsActive { get; set; }
    }
}

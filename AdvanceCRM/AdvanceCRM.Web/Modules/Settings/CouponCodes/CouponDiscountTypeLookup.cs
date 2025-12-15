namespace AdvanceCRM.Settings
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System.Collections; // for IEnumerable

    // Static lookup for coupon discount types (Flat / Percent)
    [LookupScript("Settings.CouponDiscountType", Permission = "?")]
    public class CouponDiscountTypeLookup : LookupScript
    {
        public CouponDiscountTypeLookup()
        {
            // Tell framework which property names to treat as Id/Text in returned objects
            IdField = "Id";
            TextField = "Text";
        }

        // Provide list of items (Id/Text pairs). Serenity will JSON serialize these.
        protected override IEnumerable GetItems()
        {
            return new[]
            {
                new { Id = "Flat", Text = "Flat" },
                new { Id = "Percent", Text = "Percent" }
            };
        }
    }
}

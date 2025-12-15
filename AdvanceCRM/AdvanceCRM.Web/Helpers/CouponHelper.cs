using System;
using AdvanceCRM.Settings;

namespace AdvanceCRM.Web.Helpers
{
    public static class CouponHelper
    {
        public const string CouponInvalid = "CouponInvalid";
        public const string CouponInactive = "CouponInactive";
        public const string CouponExpired = "CouponExpired";
        public const string CouponUsageLimitReached = "CouponUsageLimitReached";
        public const string CouponAmountInvalid = "CouponAmountInvalid";

        public static string ValidateCouponForCheckout(CouponCodeRow coupon, DateTime utcNow)
        {
            if (coupon == null)
                return CouponInvalid;

            if (coupon.IsActive != true)
                return CouponInactive;

            if (coupon.ExpiryDate.HasValue && coupon.ExpiryDate.Value < utcNow)
                return CouponExpired;

            if (coupon.MaxUsageCount.HasValue && coupon.MaxUsageCount.Value >= 0 &&
                coupon.UsedCount.HasValue && coupon.UsedCount.Value >= coupon.MaxUsageCount.Value)
            {
                return CouponUsageLimitReached;
            }

            return null;
        }

        public static int CalculateDiscountMinorUnits(CouponCodeRow coupon, int baseAmountMinorUnits)
        {
            if (coupon == null || baseAmountMinorUnits <= 0)
                return 0;

            var discountValue = coupon.DiscountValue ?? 0m;
            if (discountValue <= 0m)
                return 0;

            var discountType = coupon.DiscountType?.Trim();
            if (string.Equals(discountType, "Flat", StringComparison.OrdinalIgnoreCase))
            {
                var discountMinorUnits = (int)Math.Round(discountValue * 100m, MidpointRounding.AwayFromZero);
                if (discountMinorUnits < 0)
                    discountMinorUnits = 0;

                return Math.Min(discountMinorUnits, baseAmountMinorUnits);
            }

            if (string.Equals(discountType, "Percent", StringComparison.OrdinalIgnoreCase))
            {
                var percentage = Math.Clamp(discountValue, 0m, 100m);
                var discountMinorUnits = (int)Math.Round(baseAmountMinorUnits * (percentage / 100m), MidpointRounding.AwayFromZero);
                if (discountMinorUnits < 0)
                    discountMinorUnits = 0;

                return Math.Min(discountMinorUnits, baseAmountMinorUnits);
            }

            return 0;
        }

        public static CouponValidationResult Validate(string couponCode, Func<string, CouponCodeRow> couponResolver, DateTime utcNow)
        {
            if (couponResolver == null)
                throw new ArgumentNullException(nameof(couponResolver));

            if (string.IsNullOrWhiteSpace(couponCode))
                return new CouponValidationResult(null, null);

            var trimmed = couponCode.Trim();
            var coupon = couponResolver(trimmed);
            var validationError = ValidateCouponForCheckout(coupon, utcNow);

            return validationError == null
                ? new CouponValidationResult(coupon, null)
                : new CouponValidationResult(null, validationError);
        }

        public static decimal ApplyDiscount(CouponCodeRow coupon, decimal baseAmount)
        {
            if (coupon == null || baseAmount <= 0)
                return Math.Max(0, baseAmount);

            var baseMinorUnits = (int)Math.Round(baseAmount * 100m, MidpointRounding.AwayFromZero);
            var discountMinorUnits = CalculateDiscountMinorUnits(coupon, baseMinorUnits);
            var finalMinorUnits = Math.Max(0, baseMinorUnits - discountMinorUnits);

            return finalMinorUnits / 100m;
        }

        public readonly struct CouponValidationResult
        {
            public CouponValidationResult(CouponCodeRow coupon, string error)
            {
                Coupon = coupon;
                Error = error;
            }

            public CouponCodeRow Coupon { get; }

            public string Error { get; }

            public bool IsValid => Coupon != null && string.IsNullOrEmpty(Error);
        }
    }
}

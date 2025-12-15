namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[CouponCodes]")]
    [ReadPermission("Settings:ProductPlans")]
    [ModifyPermission("Settings:ProductPlans")]
    public sealed class CouponCodeRow : Row<CouponCodeRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => Fields.Id[this];
            set => Fields.Id[this] = value;
        }

        [DisplayName("Code"), Size(50), NotNull, QuickSearch, NameProperty]
        public String Code
        {
            get => Fields.Code[this];
            set => Fields.Code[this] = value;
        }

        [DisplayName("Discount Type"), Size(10), NotNull]
        public String DiscountType
        {
            get => Fields.DiscountType[this];
            set => Fields.DiscountType[this] = value;
        }

        [DisplayName("Discount Value"), Scale(2), NotNull]
        public Decimal? DiscountValue
        {
            get => Fields.DiscountValue[this];
            set => Fields.DiscountValue[this] = value;
        }

        [DisplayName("Max Usage Count")]
        public Int32? MaxUsageCount
        {
            get => Fields.MaxUsageCount[this];
            set => Fields.MaxUsageCount[this] = value;
        }

        [DisplayName("Used Count"), DefaultValue(0)]
        public Int32? UsedCount
        {
            get => Fields.UsedCount[this];
            set => Fields.UsedCount[this] = value;
        }

        [DisplayName("Is Active"), NotNull]
        public Boolean? IsActive
        {
            get => Fields.IsActive[this];
            set => Fields.IsActive[this] = value;
        }

        [DisplayName("Expiry Date")]
        public DateTime? ExpiryDate
        {
            get => Fields.ExpiryDate[this];
            set => Fields.ExpiryDate[this] = value;
        }

        public CouponCodeRow()
            : base(Fields)
        {
        }

        public CouponCodeRow(RowFields fields)
            : base(fields)
        {
        }

       

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Code;
            public StringField DiscountType;
            public DecimalField DiscountValue;
            public Int32Field MaxUsageCount;
            public Int32Field UsedCount;
            public BooleanField IsActive;
            public DateTimeField ExpiryDate;
        }
    }
}

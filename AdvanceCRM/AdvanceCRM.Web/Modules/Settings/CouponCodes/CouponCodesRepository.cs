namespace AdvanceCRM.Settings.Repositories
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Data;
    using MyRow = CouponCodeRow;

    public class CouponCodesRepository : BaseRepository
    {
        public CouponCodesRepository(IRequestContext context) : base(context) { }

        private static MyRow.RowFields fld => MyRow.Fields;

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyDeleteHandler(Context).Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyListHandler(Context).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            public MySaveHandler(IRequestContext context) : base(context) { }

            protected override void BeforeSave()
            {
                base.BeforeSave();
                if (IsCreate)
                {
                    // Initialize used count to 0 if not set
                    if (!Row.UsedCount.HasValue)
                        Row.UsedCount = 0;
                }

                // Normalize code to upper-case (coupon codes typically case-insensitive)
                if (!string.IsNullOrWhiteSpace(Row.Code))
                    Row.Code = Row.Code.Trim();

                // Validate discount value
                if (Row.DiscountValue <= 0)
                    throw new ValidationError("DiscountInvalid", "DiscountValue", "Discount must be greater than zero.");

                var type = Row.DiscountType?.Trim();
                if (string.Equals(type, "Percent", StringComparison.OrdinalIgnoreCase))
                {
                    if (Row.DiscountValue > 100)
                        throw new ValidationError("DiscountTooHigh", "DiscountValue", "Percentage discount cannot exceed 100%.");
                }
                else if (!string.Equals(type, "Flat", StringComparison.OrdinalIgnoreCase))
                {
                    throw new ValidationError("DiscountTypeInvalid", "DiscountType", "Discount type must be Flat or Percent.");
                }

                if (Row.MaxUsageCount.HasValue && Row.MaxUsageCount.Value < 0)
                    Row.MaxUsageCount = null; // treat negative as unlimited
            }
        }

        private class MyDeleteHandler : DeleteRequestHandler<MyRow>
        {
            public MyDeleteHandler(IRequestContext context) : base(context) { }
        }

        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow>
        {
            public MyRetrieveHandler(IRequestContext context) : base(context) { }
        }

        private class MyListHandler : ListRequestHandler<MyRow>
        {
            public MyListHandler(IRequestContext context) : base(context) { }
        }
    }
}

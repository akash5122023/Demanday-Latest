using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.EnquiryContactsRow>;
using MyRow = AdvanceCRM.Demanday.EnquiryContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IEnquiryContactsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class EnquiryContactsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IEnquiryContactsListHandler
    {
        public EnquiryContactsListHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ApplyFilters(SqlQuery query)
        {
            base.ApplyFilters(query);

            if (EnquiryContactsAccess.SeesAll(Context))
                return;

            // A regular employee: only their own records. No identifiable user -> nothing.
            var userId = EnquiryContactsAccess.CurrentUserId(Context);
            query.Where(MyRow.Fields.OwnerId == (userId ?? -1));
        }
    }
}

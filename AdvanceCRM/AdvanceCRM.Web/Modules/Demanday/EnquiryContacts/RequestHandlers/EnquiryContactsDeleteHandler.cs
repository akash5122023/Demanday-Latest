using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.EnquiryContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IEnquiryContactsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class EnquiryContactsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IEnquiryContactsDeleteHandler
    {
        public EnquiryContactsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
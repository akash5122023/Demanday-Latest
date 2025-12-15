using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.EnquiryContactsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.EnquiryContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IEnquiryContactsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class EnquiryContactsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IEnquiryContactsSaveHandler
    {
        public EnquiryContactsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
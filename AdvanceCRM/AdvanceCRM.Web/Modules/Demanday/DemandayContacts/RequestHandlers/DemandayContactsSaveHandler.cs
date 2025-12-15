using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayContactsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayContactsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayContactsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayContactsSaveHandler
    {
        public DemandayContactsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
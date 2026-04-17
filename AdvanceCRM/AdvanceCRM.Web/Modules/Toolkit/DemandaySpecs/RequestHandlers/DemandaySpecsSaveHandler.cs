using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.DemandaySpecsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.DemandaySpecsRow;

namespace AdvanceCRM.Toolkit
{
    public interface IDemandaySpecsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandaySpecsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandaySpecsSaveHandler
    {
        public DemandaySpecsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
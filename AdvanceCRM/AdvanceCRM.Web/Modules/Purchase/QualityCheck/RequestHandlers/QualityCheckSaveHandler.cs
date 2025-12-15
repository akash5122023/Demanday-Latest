using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Purchase.QualityCheckRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Purchase.QualityCheckRow;

namespace AdvanceCRM.Purchase.QualityCheck
{
    public interface IQualityCheckSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class QualityCheckSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IQualityCheckSaveHandler
    {
        public QualityCheckSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
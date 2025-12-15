using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Purchase.QualityCheckRow>;
using MyRow = AdvanceCRM.Purchase.QualityCheckRow;

namespace AdvanceCRM.Purchase.QualityCheck
{
    public interface IQualityCheckRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class QualityCheckRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IQualityCheckRetrieveHandler
    {
        public QualityCheckRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
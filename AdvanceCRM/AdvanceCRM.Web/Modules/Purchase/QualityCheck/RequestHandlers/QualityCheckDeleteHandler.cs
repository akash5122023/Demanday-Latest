using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Purchase.QualityCheckRow;

namespace AdvanceCRM.Purchase.QualityCheck
{
    public interface IQualityCheckDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class QualityCheckDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IQualityCheckDeleteHandler
    {
        public QualityCheckDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
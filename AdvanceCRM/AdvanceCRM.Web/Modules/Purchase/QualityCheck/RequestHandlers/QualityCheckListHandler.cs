using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Purchase.QualityCheckRow>;
using MyRow = AdvanceCRM.Purchase.QualityCheckRow;

namespace AdvanceCRM.Purchase.QualityCheck
{
    public interface IQualityCheckListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class QualityCheckListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IQualityCheckListHandler
    {
        public QualityCheckListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
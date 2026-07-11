using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.EBBCheck.EBBCheckRow>;
using MyRow = AdvanceCRM.EBBCheck.EBBCheckRow;

namespace AdvanceCRM.EBBCheck
{
    public interface IEBBCheckListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class EBBCheckListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IEBBCheckListHandler
    {
        public EBBCheckListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

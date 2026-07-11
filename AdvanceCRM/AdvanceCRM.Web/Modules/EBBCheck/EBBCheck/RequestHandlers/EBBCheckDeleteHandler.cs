using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.EBBCheck.EBBCheckRow;

namespace AdvanceCRM.EBBCheck
{
    public interface IEBBCheckDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class EBBCheckDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IEBBCheckDeleteHandler
    {
        public EBBCheckDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.EBBCheck.EBBCheckRow>;
using MyRow = AdvanceCRM.EBBCheck.EBBCheckRow;

namespace AdvanceCRM.EBBCheck
{
    public interface IEBBCheckRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class EBBCheckRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IEBBCheckRetrieveHandler
    {
        public EBBCheckRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.TeleMarketingEmailTeam.TeleMarketingEmailTeamRow>;
using MyRow = AdvanceCRM.TeleMarketingEmailTeam.TeleMarketingEmailTeamRow;

namespace AdvanceCRM.TeleMarketingEmailTeam
{
    public interface ITeleMarketingEmailTeamRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class TeleMarketingEmailTeamRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, ITeleMarketingEmailTeamRetrieveHandler
    {
        public TeleMarketingEmailTeamRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

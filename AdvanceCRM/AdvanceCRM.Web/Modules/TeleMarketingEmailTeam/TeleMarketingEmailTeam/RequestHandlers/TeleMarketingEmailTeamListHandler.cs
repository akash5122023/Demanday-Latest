using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.TeleMarketingEmailTeam.TeleMarketingEmailTeamRow>;
using MyRow = AdvanceCRM.TeleMarketingEmailTeam.TeleMarketingEmailTeamRow;

namespace AdvanceCRM.TeleMarketingEmailTeam
{
    public interface ITeleMarketingEmailTeamListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class TeleMarketingEmailTeamListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, ITeleMarketingEmailTeamListHandler
    {
        public TeleMarketingEmailTeamListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

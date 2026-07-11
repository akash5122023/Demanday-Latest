using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.TeleMarketingEmailTeam.TeleMarketingEmailTeamRow;

namespace AdvanceCRM.TeleMarketingEmailTeam
{
    public interface ITeleMarketingEmailTeamDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class TeleMarketingEmailTeamDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, ITeleMarketingEmailTeamDeleteHandler
    {
        public TeleMarketingEmailTeamDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

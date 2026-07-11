using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.EmailTeam.EmailTeamRow>;
using MyRow = AdvanceCRM.EmailTeam.EmailTeamRow;

namespace AdvanceCRM.EmailTeam
{
    public interface IEmailTeamListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class EmailTeamListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IEmailTeamListHandler
    {
        public EmailTeamListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

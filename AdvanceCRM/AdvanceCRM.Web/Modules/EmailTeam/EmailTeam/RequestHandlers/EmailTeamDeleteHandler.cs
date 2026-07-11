using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.EmailTeam.EmailTeamRow;

namespace AdvanceCRM.EmailTeam
{
    public interface IEmailTeamDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class EmailTeamDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IEmailTeamDeleteHandler
    {
        public EmailTeamDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.EmailTeam.EmailTeamRow>;
using MyRow = AdvanceCRM.EmailTeam.EmailTeamRow;

namespace AdvanceCRM.EmailTeam
{
    public interface IEmailTeamRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class EmailTeamRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IEmailTeamRetrieveHandler
    {
        public EmailTeamRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}

namespace AdvanceCRM.Common
{
    using AdvanceCRM.Common.Endpoints;
    using Serenity.Data;
    using Serenity.Services;
    using Microsoft.AspNetCore.Hosting;
    using Serenity.Abstractions;
    using Serenity.Extensions.DependencyInjection;

    public class CommonService : ICommonService
    {
        public void StartCleanupTask(string directoryPath)
        {
            CommonController.StartCleanupTask(directoryPath);
        }

        public StandardResponse SendIntractWa(IUnitOfWork uow, SendIntractSMSRequest request)
        {
            var controller = new CommonController(
                Dependency.Resolve<ISqlConnections>(),
                Dependency.Resolve<IWebHostEnvironment>());
            return controller.SendIntractWa(uow,request);
        }
    }
}

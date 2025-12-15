namespace AdvanceCRM.Common
{
    using Serenity.Services;
    using Serenity.Data;

    public interface ICommonService
    {
        void StartCleanupTask(string directoryPath);
        StandardResponse SendIntractWa(IUnitOfWork uow, SendIntractSMSRequest request);
    }
}

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface IParentControlService
    {
        IConfigService ConfigService { get; }
        IDeviceService DeviceService { get; }
        IScheduleService ScheduleService { get; }

        ISessionService SessionService { get; }

        bool IsConnected { get; }
        bool IsOfflineConfigured { get; }
        string InfoData { get; }

        bool ValidApiConfiguration();
    }
}

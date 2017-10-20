namespace ParentControl.Core.Contracts.Services
{
    public interface IParentControlService
    {
        IConfigService ConfigService { get; }
        IDeviceService DeviceService { get; }
        IScheduleService ScheduleService { get; }

        ISessionService SessionService { get; }

        bool IsConnected { get; }
        string InfoData { get; }
    }
}

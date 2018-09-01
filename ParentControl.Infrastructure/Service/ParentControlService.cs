using System;
using Castle.Windsor;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Contracts.Services;

namespace ParentControl.Infrastructure.Service
{
    public class ParentControlService : IParentControlService
    {
        private IWindsorContainer _container;
        public ParentControlService()
        {
            Bootstrap bootstrap = new Bootstrap();
            _container = bootstrap.Container;
        }

        public bool ValidApiConfiguration()
        {
            var config = _container.Resolve<IConfiguration>();

            if (string.IsNullOrEmpty(config.ApiAddress) && string.IsNullOrEmpty(config.Login) && string.IsNullOrEmpty(config.Password))
            {
                return false;
            }

            return true;
        }

        public bool ValidateApiConnection()
        {
            var owin = _container.Resolve<IHttpService>();
            var config = _container.Resolve<IConfiguration>();

            try
            {
                owin.Authenticate(config.Login, config.Password);
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public IDeviceService DeviceService => _container.Resolve<IDeviceService>();

        public IScheduleService ScheduleService => _container.Resolve<IScheduleService>();

        public ITimesheetService TimesheetService => _container.Resolve<ITimesheetService>();

        public ISessionService SessionService => _container.Resolve<ISessionService>();

        public IConfigService ConfigService => _container.Resolve<IConfigService>();

        public ILocalSessionTracker LocalSessionTracker => _container.Resolve<ILocalSessionTracker>();

        public bool IsConnected => _container.Resolve<IHttpService>() != null && _container.Resolve<IHttpService>().IsConnected;

        public string InfoData => $"{_container.Resolve<IConfigService>().FullPath}{Environment.NewLine}Token:{_container.Resolve<IHttpService>().Token}";

        public bool IsOfflineConfigured => ConfigService?.Config?.Timesheets != null && ConfigService?.Config?.AllowOnNoTimesheet == false;
    }
}

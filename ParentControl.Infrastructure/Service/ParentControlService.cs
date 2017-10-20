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
            Initializer();
        }

        private void Initializer()
        {
            var config = _container.Resolve<IConfigService>();
            var owin = _container.Resolve<IHttpService>();
            if(config.Config.AuthenticationData == null)
            {
                return;
            }
            try
            {
                owin.Authenticate(config.Config.AuthenticationData.Username, config.Config.AuthenticationData.Password);
            }
            catch(Exception ex)
            {

            }
        }

        public IDeviceService DeviceService
        {
            get { return _container.Resolve<IDeviceService>(); }
        }

        public IScheduleService ScheduleService
        {
            get { return _container.Resolve<IScheduleService>(); }
        }

        public ISessionService SessionService
        {
            get { return _container.Resolve<ISessionService>(); }
        }

        public IConfigService ConfigService
        {
            get { return _container.Resolve<IConfigService>(); }
        }

        public ISessionTrackerService SessionTrackerService
        {
            get { return _container.Resolve<ISessionTrackerService>(); }
        }

        public bool IsConnected
        {
            get
            {
                return _container.Resolve<IHttpService>() != null ? _container.Resolve<IHttpService>().IsConnected : false;
            }
        }

        public string InfoData
        {
            get
            {
                return
                    $"{_container.Resolve<IConfigService>().FullPath}{Environment.NewLine}Token:{_container.Resolve<IHttpService>().Token}";
            }
        }
    }
}

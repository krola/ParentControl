using Castle.Windsor;
using ParentControl.Infrastructure.Configuration;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Service;

namespace ParentControl.Infrastructure
{
    public class Bootstrap
    {
        private IWindsorContainer _container;

        public Bootstrap()
        {
            _container = new WindsorContainer();
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            _container.Register(Castle.MicroKernel.Registration.Component.For<IConfiguration>().ImplementedBy<Configuration.Configuration>());
            _container.Register(Castle.MicroKernel.Registration.Component.For<IConfigService>().ImplementedBy<ConfigService>());
            _container.Register(Castle.MicroKernel.Registration.Component.For<IHttpService>().ImplementedBy<HttpService>());
            _container.Register(Castle.MicroKernel.Registration.Component.For<IDeviceService>().ImplementedBy<DeviceService>());
            _container.Register(Castle.MicroKernel.Registration.Component.For<ITimesheetService>().ImplementedBy<TimesheetService>());
            _container.Register(Castle.MicroKernel.Registration.Component.For<IScheduleService>().ImplementedBy<ScheduleService>());
            _container.Register(Castle.MicroKernel.Registration.Component.For<ISessionService>().ImplementedBy<SessionService>());
            _container.Register(Castle.MicroKernel.Registration.Component.For<ILocalSessionTracker>().ImplementedBy<LocalSessionTracker>());
        }

        public IWindsorContainer Container
        {
            get { return _container; }
        }
    }
}

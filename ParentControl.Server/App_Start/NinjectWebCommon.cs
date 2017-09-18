using Ninject;
using ParentControl.Server.Contracts;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Repository;
using ParentControl.Server.Services;

namespace ParentControl.Server.App_Start
{ 

    public static class NinjectWebCommon 
    {
       
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                //kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                //kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAppContext>().To<AppContext>().InSingletonScope();
            kernel.Bind<IAuthRepository>().To<AuthRepository>().InSingletonScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            kernel.Bind<IDeviceRepository>().To<DeviceRepository>().InSingletonScope();
            kernel.Bind<ITimesheetRepository>().To<TimesheetRepository>().InSingletonScope();
            kernel.Bind<IScheduleRepository>().To<ScheduleRepository>().InSingletonScope();
            kernel.Bind<ISessionRepository>().To<SessionRepository>().InSingletonScope();
            
        }        
    }
}

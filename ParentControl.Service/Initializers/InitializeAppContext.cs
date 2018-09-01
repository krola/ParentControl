using ParentControl.Infrastructure.Service;
using ParentControl.Service.Contracts;

namespace ParentControl.Service.Initializers
{
    class InitializeAppContext : BaseInitializer
    {
        public InitializeAppContext(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "InitializeAppContext";

        protected override bool CanSkip => false;

        protected override void Do()
        {
            Context.ParentControlService = new ParentControlService();
            Context.JobManager = new Jobs.JobManager();
        }
    }
}

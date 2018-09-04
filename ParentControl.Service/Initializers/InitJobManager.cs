using ParentControl.Service.Manager;

namespace ParentControl.Service.Initializers
{
    class InitJobManager : BaseInitializer
    {
        public InitJobManager(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "InitJobManager";

        protected override bool CanSkip => false;

        protected override void Do()
        {
            Context.JobManager = new Jobs.JobManager();
        }
    }
}

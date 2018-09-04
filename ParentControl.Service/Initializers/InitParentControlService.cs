using ParentControl.Infrastructure.Service;

namespace ParentControl.Service.Initializers
{
    class InitParentControlService : BaseInitializer
    {
        public InitParentControlService(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "InitParentControlService";

        protected override bool CanSkip => false;

        protected override void Do()
        {
            Context.Initialized = true;
            Context.ParentControlService = new ParentControlService();
        }
    }
}

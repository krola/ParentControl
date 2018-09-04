using ParentControl.Service.Manager;

namespace ParentControl.Service.Initializers
{
    class InitWebsocketHandler : BaseInitializer
    {
        public InitWebsocketHandler(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "InitWebsocketHandler";

        protected override bool CanSkip => false;

        protected override void Do()
        {
            Context.WebsocketHandler = new WebsocketHandler();
            Context.WebsocketHandler.Initialize();
        }
    }
}

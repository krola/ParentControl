using ParentControl.Service.Manager;

namespace ParentControl.Service.Initializers
{
    class InitWebsocketHandler : BaseInitializer
    {
        public InitWebsocketHandler(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "InitWebsocketHandler";

        protected override bool CanSkip => true;

        protected override void Do()
        {
            Context.WebsocketHandler = new WebsocketHandler();
            Context.WebsocketHandler.Initialize();
        }

        protected override bool Valid()
        {
            if(Context.Mode == Constants.Mode.Offline)
            {
                return false;
            }

            return true;
        }
    }
}

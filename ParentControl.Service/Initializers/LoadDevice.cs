using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Initializers
{
    class LoadDevice : BaseInitializer
    {
        public LoadDevice(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "Loading Device";

        protected override bool CanSkip => true;

        protected override void Do()
        {
            var deviceService = Context.ParentControlService.DeviceService;
            try
            {
                var deviceId = Context.ParentControlService.ConfigService.Config.Device?.Id;
                if (deviceId == null)
                {
                    throw new Exception("No device configured");
                }

                Context.Device = deviceService.GetDevices()
                    .FirstOrDefault(d => d.Id == deviceId);

                if (Context.Device == null)
                {
                    throw new Exception("No device loaded");
                }

                PrintMessage($"Device loaded: {Context.Device.Id} -> {Context.Device.Name}");
            }
            catch (Exception e)
            {
                var message = $"Error {e.Message}";
                throw new Exception(message);
            }
        }

        protected override bool Valid()
        {
            if(Context.Mode == Constants.Mode.Online)
            {
                return true;
            }

            return false;
        }
    }
}

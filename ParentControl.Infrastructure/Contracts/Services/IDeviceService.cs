using System.Collections.Generic;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface IDeviceService 
    {
        IEnumerable<DTO.Device> GetDevices();

        void RegisterDevice(string deviceName);
    }
}

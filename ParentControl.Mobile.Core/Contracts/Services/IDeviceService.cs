using System.Collections.Generic;
using Device = ParentControl.Core.Services.Model.Device;
using System.Threading.Tasks;

namespace ParentControl.Core.Contracts.Services
{
    public interface IDeviceService 
    {
        Task<IEnumerable<DTO.Device>> GetDevicesAsync();

        string GenerateDeviceId();
        string GenerateDeviceId(string username);

        void RegisterDevice(Device device);
    }
}

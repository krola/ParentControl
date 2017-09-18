using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.DTO;
using ParentControl.Infrastructure.Service.Model;
using Device = ParentControl.Infrastructure.Service.Model.Device;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface IDeviceService 
    {
        IEnumerable<DTO.Device> GetDevices();

        string GenerateDeviceId();
        string GenerateDeviceId(string username);

        void RegisterDevice(Device device);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using ParentControl.Server.Models;

namespace ParentControl.Server.Contracts.Repositories
{
    public interface IDeviceRepository
    {
        DeviceModel GetDeviceForUser(UserModel user);

        IList<DeviceModel> GetDevicesForUser(UserModel user);

        DeviceModel GetDeviceById(int id);
        DeviceModel GetDeviceByDeviceId(string deviceId);

        void RegisterDevice(DeviceModel device);
    }
}

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using ParentControl.Server.Contracts;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Exceptions;
using ParentControl.Server.Models;
using ParentControl.Server.Services;

namespace ParentControl.Server.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private IAppContext _appContext;

        public DeviceRepository(IAppContext appContext)
        {
            _appContext = appContext;
        }
        public DeviceModel GetDeviceForUser(UserModel user)
        {
            return _appContext.Devices.FirstOrDefault(d => d.User.Id == user.Id);
        }

        public IList<DeviceModel> GetDevicesForUser(UserModel user)
        {
            return _appContext.Devices.Where(d => d.User.Id == user.Id).ToList();
        }

        public DeviceModel GetDeviceById(int id)
        {
            return _appContext.Devices.Find(id);
        }

        public DeviceModel GetDeviceByDeviceId(string deviceId)
        {
            return _appContext.Devices.FirstOrDefault(d => d.DeviceId == deviceId);
        }

        public void RegisterDevice(DeviceModel device)
        {
            if (_appContext.Devices.Any(d => d.DeviceId == device.DeviceId && d.User.Id == device.User.Id))
            {
                throw new DeviceAlreadyExists();
            }

            _appContext.Devices.Add(device);

            _appContext.SaveChanges();
        }
    }
}
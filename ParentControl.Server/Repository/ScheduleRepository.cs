using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParentControl.Server.Contracts;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Exceptions;
using ParentControl.Server.Models;

namespace ParentControl.Server.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private IAppContext _appContext;

        public ScheduleRepository(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public ScheduleModel CreateSchedule(string name, DeviceModel device)
        {
            if (_appContext.Schedules.Any(s => s.Name == name && s.Device.Id == device.Id))
            {
                throw new ScheduleAlreadyExist();
            }

            var schedule = new ScheduleModel()
            {
                Device = device,
                Name = name
            };

            _appContext.Schedules.Add(schedule);

            _appContext.SaveChanges();

            return schedule;
        }

        public ScheduleModel FindScheduleById(int id)
        {
            return _appContext.Schedules.Find(id);
        }

        public ScheduleModel FindScheduleByDevice(DeviceModel device)
        {
            return _appContext.Schedules.FirstOrDefault(s => s.Device.Id == device.Id);
        }
    }
}
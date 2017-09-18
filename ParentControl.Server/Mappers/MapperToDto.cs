using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParentControl.DTO;
using ParentControl.Server.Models;

namespace ParentControl.Server.Mappers
{
    public static class MapperToDto
    {
        public static Device MapToDTO(this DeviceModel deviceModel)
        {
            return new Device()
            {
                DeviceId = deviceModel.DeviceId,
                Name = deviceModel.Name
            };
        }

        public static Schedule MapToDTO(this ScheduleModel scheduleModel)
        {
            return new Schedule()
            {
                Name = scheduleModel.Name,
                Id = scheduleModel.Id,
                AllowWitoutTimesheet = scheduleModel.AllowWithNoTimesheet
            };
        }

        public static Timesheet MapToDTO(this TimesheetModel timesheetModel)
        {
            return new Timesheet()
            {
                Time = timesheetModel.Time,
                DateTo = timesheetModel.DateTo,
                DateFrom = timesheetModel.DateFrom,
                ScheduleId = timesheetModel.Schedule.Id,
                CreateTime = timesheetModel.CreateTime
            };
        }

        public static Session MapToDTO(this SessionModel sessionModel)
        {
            return new Session()
            {
                SessionEnd = sessionModel.EndTime,
                SessionStart = sessionModel.StarTime,
                SessionId = sessionModel.UniqueId,
                DeviceID = sessionModel.Device.DeviceId
            };
        }
    }
}
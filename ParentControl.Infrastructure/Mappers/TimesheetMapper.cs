using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Infrastructure.Service.Model;

namespace ParentControl.Infrastructure.Mappers
{
    public static class TimesheetMapper
    {
        public static Timesheet MapToTimesheetServiceModel(this DTO.Timesheet dto)
        {
            return new Timesheet()
            {
                From = dto.DateFrom,
                To = dto.DateTo,
                TotalTime = dto.Time
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Core.Services.Model;

namespace ParentControl.Core.Mappers
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

using ParentControl.DTO;
using Timesheet = ParentControl.DTO.Timesheet;
using System.Threading.Tasks;

namespace ParentControl.Core.Contracts.Services
{
    public interface IScheduleService
    {
        Task<Schedule> GetDeviceScheduleAsync(string device);

        Task AddTimesheetAsync(Timesheet timesheet);

        void RemoveSchedule(Schedule schedule);
    }
}

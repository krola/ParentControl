using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.DTO;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Service.Model;

namespace ParentControl.Infrastructure.Service
{
    internal class TimesheetService : BaseService, ITimesheetService
    {
        public TimesheetService(IHttpService httpService) : base(httpService)
        {
        }

        private const string ApiEndpoint = "/api/Timesheet";

        public IEnumerable<Timesheet> GetTimesheetFor(int scheduleId)
        {
            try
            {
                var result = HttpService.GetRequest(ApiEndpoint, new RequestParameter[] {
                    new RequestParameter
                    {
                        Key = "ScheduleId",
                        Value = scheduleId.ToString()
                    }
                });
                return JsonConvert.DeserializeObject<IEnumerable<Timesheet>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

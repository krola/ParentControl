using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.DTO;
using ParentControl.Core.Services.Model;
using Timesheet = ParentControl.Core.Services.Model.Timesheet;

namespace ParentControl.Core.Contracts
{
    public interface IConfigService
    {
        void SaveAuthentication(AuthenticationData authenticationData);

        Config Config { get; }

        void SaveServerAddress(string server);

        void SaveDevice(string deviceId, string deviceName);

        void SaveTimesheets(List<Timesheet> timesheets, bool allowWitoutTimesheet);
        string FullPath { get; }
    }
}

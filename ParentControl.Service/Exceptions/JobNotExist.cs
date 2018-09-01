using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Exceptions
{
    class JobNotExist : Exception
    {
        public string JobID { get; set; }

        public JobNotExist(string jobId)
        {
            JobID = jobId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Infrastructure.Communication.NamedPipes
{
    [Serializable]
    public class TimerPipeModel
    {
        public TimeSpan TimeLeft { get; set; }
    }
}

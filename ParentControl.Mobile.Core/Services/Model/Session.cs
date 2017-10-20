using System;

namespace ParentControl.Core.Services.Model
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
    }
}

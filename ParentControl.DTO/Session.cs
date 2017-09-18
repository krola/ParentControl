using System;

namespace ParentControl.DTO
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }

        public string DeviceID { get; set; }
    }
}

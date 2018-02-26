using System;

namespace ParentControl.DTO
{
    public class Session
    {
        public Guid Id { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public Device Device { get; set; }
    }
}

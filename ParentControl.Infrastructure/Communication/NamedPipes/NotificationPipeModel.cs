using ParentControl.Infrastructure.Constants;

namespace ParentControl.Infrastructure.Communication.NamedPipes
{
    public class NotificationPipeModel
    {
        public NotificationAnwser NotificationType { get; set; }
        public string Text { get; set; }
    }
}

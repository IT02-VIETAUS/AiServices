using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiServices.Domain.Entities.HrSchema;
using AiServices.Domain.Enums.Notifications;

namespace AiServices.Domain.Entities.Notifications
{
    public class UserNotificationSetting
    {
        public Guid UserId { get; set; }             // PK = UserId
        public string Channels { get; set; } = "inapp";     // "inapp,email,telegram"
        public NotificationSeverity MinSeverity { get; set; } = NotificationSeverity.Info;

        public TimeSpan? QuietFrom { get; set; }     // giờ im lặng (local)
        public TimeSpan? QuietTo { get; set; }
        public string? Locale { get; set; }          // "vi-VN"
    
        public Employee User { get; set; } = default!;
    }
}

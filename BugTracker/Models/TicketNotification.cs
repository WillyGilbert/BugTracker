using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public NotificationType Type { get; set; }
        public string ModifiedUser { get; set; }

    }
    public enum NotificationType
    {
        AssignedBy,
        ModifiedBy
    }
}
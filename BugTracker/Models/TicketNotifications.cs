using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketNotifications
    {
        public int Id { get; set; }
        public Tickets Tiket { get; set; }
        public int TiketId { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
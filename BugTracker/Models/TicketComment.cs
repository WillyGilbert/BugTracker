using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public int TiketsId { get; set; }
        public virtual Ticket Tiket { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
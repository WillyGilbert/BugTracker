using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketComments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public Tickets Tiket { get; set; }
        public int TiketsId { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
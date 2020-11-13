using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketAttachments
    {
        public int Id { get; set; }
        public Tickets Tikets { get; set; }
        public int TicketId { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
        public string FileUrl { get; set; }
    }
}
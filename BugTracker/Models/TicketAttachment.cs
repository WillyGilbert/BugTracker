using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string FileUrl { get; set; }
    }
}
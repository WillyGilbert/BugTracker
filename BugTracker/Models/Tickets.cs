using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Tickets
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Projects Project { get; set; }
        public int ProjectId { get; set; }
        public TicketTypes TicketType { get; set; }
        public int TicketTypesId { get; set; }
        public TicketPriorities TicketPriority { get; set; }
        public int TicketPriorityId { get; set; }
        public TicketStatuses TicketStatus { get; set; }
        public int TicketStatusId { get; set; }
        public ApplicationUser OwnerUser { get; set; }
        public int OwnerUserId { get; set; }
        public ApplicationUser AssignedToUser { get; set; }
        public int AssignedToUserId { get; set; }
        public virtual ICollection<TicketAttachments> TicketAttachments { get; set; }
        public virtual ICollection<TicketComments> TicketComments { get; set; }
        public virtual ICollection<TicketHistories> TicketHistories { get; set; }
        public virtual ICollection<TicketNotifications> TicketNotifications { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Updated { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int TicketTypeId { get; set; }
        public virtual TicketType TicketType { get; set; }
        public int TicketPriorityId { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public int TicketStatusId { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public string OwnerUserId { get; set; }
        [ForeignKey("OwnerUserId")]
        public virtual ApplicationUser OwnerUser { get; set; }

        public string AssignedToUserId { get; set; }
        [ForeignKey("AssignedToUserId")]
        public virtual ApplicationUser AssignedToUser { get; set; }

        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }
    }
}
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public static class TicketHelper
    {
        public static List<Ticket> GetTickets()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType");
            return tickets.ToList();
        }
    }
}
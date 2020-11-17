using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Net;

namespace BugTracker.DAL
{
    public static class TicketHelper
    {
        //all tickets
        public static List<Ticket> GetTickets()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType");
            return tickets.ToList();
        }

        //public static List<Ticket> GetTickets(string userId)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    List<Ticket> tickets = new List<Ticket>();
        //    List<Ticket> allTickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").ToList();      

        //    if (UserHelper.UserInRole(userId, "Admin"))            
        //    {
        //        tickets = allTickets;
        //    }
        //    else if(UserHelper.UserInRole(userId, "ProjectManager"))
        //    {
        //        var projectsByManager = db.ProjectUsers.Where(pu => pu.UserId == userId);
        //        tickets = allTickets.Where(t => projectsByManager.Any(up => up.ProjectId == t.ProjectId)).ToList();
        //    }
        //    else if (UserHelper.UserInRole(userId, "Developer"))
        //    {
        //        tickets = allTickets.Where(t => t.AssignedToUserId == userId).ToList();
        //    }
        //    else if (UserHelper.UserInRole(userId, "Submitter"))
        //    {
        //        tickets = allTickets.Where(t => t.OwnerUserId == userId).ToList();
        //    }

        //    return tickets;
        //}

        public static List<Ticket> GetTickets(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //List<Ticket> tickets = new List<Ticket>();
            List<Ticket> allTickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").ToList();
            return allTickets;           
        }

        //sort tickets by title
        public static List<Ticket> SortTicketsByTitle(List<Ticket> tickets)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            //var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").OrderBy(t => t.Title);
            var sortedTickets = tickets.OrderBy(t => t.Title);
            return sortedTickets.ToList();
        }

        //ticket created by submitter
        //public static List<Ticket> GetTicketsBySubmitter(string userId)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").Where(t => t.OwnerUserId == userId);
        //    return tickets.ToList();
        //}

        //ticket assigned to developer
        //public static List<Ticket> GetTicketsByDeveloper(string userId)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").Where(t => t.AssignedToUserId == userId);
        //    return tickets.ToList();
        //}

        //ticket that belong to projects of p.manager
        //public static List<Ticket> GetTicketsByManager(string userId)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var projectsByManager = db.ProjectUsers.Where(pu => pu.UserId == userId);
        //    var ticketsByManager = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").Where(t => projectsByManager.Any(up => up.ProjectId == t.ProjectId));
        //    return ticketsByManager.ToList();
        //}

        public static Ticket GetTicket(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Ticket ticket = db.Tickets.Find(Id);
            db.Dispose();
            if (ticket == null)
            {
                return null;
            }
            return ticket;
        }

        public static void Create(string userId, string title, string description, int projectId, int ticketTypeId, int ticketPriorityId, int ticketStatusId)        
        {
            ApplicationDbContext db = new ApplicationDbContext();            
            Ticket ticket = new Ticket
            {                
                Title = title,
                Description = description,
                ProjectId = projectId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                TicketTypeId = ticketTypeId,
                TicketPriorityId = ticketPriorityId,
                TicketStatusId = ticketStatusId,
                OwnerUserId = userId
            };
            db.Tickets.Add(ticket);
            db.SaveChanges();

            var dbEntityEntry = db.Entry(ticket);
            foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
            {
                string newValue = (string)dbEntityEntry.CurrentValues.GetValue<object>(property);
                TicketHistoryHelper.SetTicketHistory(ticket.Id, property, "NULL", newValue, userId);
            }


            db.Dispose();
        }

        public static void Edit(int id, string userId, string title, string description, int ticketTypeId, int ticketPriorityId, int ticketStatusId, string assignedUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Ticket ticket = GetTicket(id);
            //var user = db.Users.Find(userId);
            if (ticket != null)
            {
                ticket.Title = title;
                ticket.Description = description;
                //ticket.OwnerUserId = userId;
                //ticket.ProjectId = ticket.ProjectId;
                ticket.TicketTypeId = ticketTypeId;
                ticket.TicketPriorityId = ticketPriorityId;
                ticket.TicketStatusId = ticketStatusId;
                ticket.Updated = DateTime.Now;
                ticket.AssignedToUserId = assignedUserId;


                Ticket Newticket = db.Tickets.Find(id);
                var dbEntityEntry = db.Entry(Newticket);
                foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
                {
                    string oldVAlue = (string)dbEntityEntry.OriginalValues.GetValue<object>(property);
                    string newValue = (string)dbEntityEntry.CurrentValues.GetValue<object>(property);
                    if (newValue != null && !oldVAlue.Equals(newValue))
                    {
                        dbEntityEntry.Property(property).IsModified = true;
                        TicketHistoryHelper.SetTicketHistory(id, property, oldVAlue, newValue, userId);
                    }
                }

                db.SaveChanges();
                db.Dispose();
            }
            
        }

        public static void EditBySubmitter(int id, string title, string description, int projectId, TicketType ticketType, TicketPriority ticketPriority)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Ticket ticket = GetTicket(id);
            ticket.Title = title;
            ticket.Description = description;
            ticket.ProjectId = projectId;
            ticket.TicketType = ticketType;
            ticket.TicketPriority = ticketPriority;            
            ticket.Updated = DateTime.Now;
            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void EditByManager(int id, string title, string description, string userId, TicketType ticketType, TicketPriority ticketPriority, TicketStatus ticketStatus)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Ticket ticket = GetTicket(id);
            ticket.Title = title;
            ticket.Description = description;
            ticket.AssignedToUserId = userId;
            ticket.TicketType = ticketType;
            ticket.TicketPriority = ticketPriority;
            ticket.TicketStatus = ticketStatus;
            ticket.Updated = DateTime.Now;
            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }       
    }
}
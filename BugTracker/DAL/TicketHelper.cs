﻿using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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

        //ticket created by submitter
        public static List<Ticket> GetTicketsBySubmitter(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").Where(t => t.OwnerUserId == userId);
            return tickets.ToList();
        }

        //ticket assigned to developer
        public static List<Ticket> GetTicketsByDeveloper(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").Where(t => t.AssignedToUserId == userId);
            return tickets.ToList();
        }

        //ticket that belong to projects of p.manager
        //public static List<Ticket> GetTicketsByManager(string userId)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();            
        //    var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType").Where(t => t.ProjectId == db.ProjectUsers.Where(p => p.UserId == userId).Select(p => p.ProjectId));
        //    return tickets.ToList();
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

        public static void Create(string userId, string title, string description, int projectId, TicketType ticketType, TicketPriority ticketPriority, TicketStatus ticketStatus)        
        {
            ApplicationDbContext db = new ApplicationDbContext();            
            Ticket ticket = new Ticket
            {                
                Title = title,
                Description = description,
                ProjectId = projectId,
                Created = DateTime.Now,
                TicketType = ticketType,
                TicketPriority = ticketPriority,
                TicketStatus = ticketStatus,
                OwnerUserId = userId
            };
            db.Tickets.Add(ticket);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, string title, string description, int projectId, TicketType ticketType, TicketPriority ticketPriority, TicketStatus ticketStatus)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Ticket ticket = GetTicket(id);
            ticket.Title = title;
            ticket.Description = description;
            ticket.ProjectId = projectId;
            ticket.TicketType = ticketType;
            ticket.TicketPriority = ticketPriority;
            ticket.TicketStatus = ticketStatus;
            ticket.Updated = DateTime.Now;
            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Ticket ticket = GetTicket(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
        }
    }
}
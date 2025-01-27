﻿using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Globalization;

namespace BugTracker.DAL
{
    public static class TicketHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        //all tickets
        public static List<Ticket> GetTickets()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tickets = db.Tickets.Include("Project").Include("TicketPriority").Include("TicketStatus").Include("TicketType");
            return tickets.ToList();
        }
        public static List<Ticket> GetTickets(string userId, string role)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Ticket> tickets = new List<Ticket>();
            List<Ticket> allTickets = db.Tickets.Include("Project")
                .Include("TicketPriority")
                .Include("TicketStatus")
                .Include("TicketType")
                .ToList();

            foreach (var ticket in allTickets)
            {
                if (ticket.AssignedToUser == null) ticket.AssignedToUser = new ApplicationUser();
                if (ticket.AssignedToUserId == null) ticket.AssignedToUserId = "";
            }

            if (role == "Admin") tickets = allTickets;
            if (role == "ProjectManager")
            {
                var projectsByManager = db.ProjectUsers.Where(pu => pu.UserId == userId);
                return allTickets.Where(t => projectsByManager.Any(up => up.ProjectId == t.ProjectId)).ToList();
            }
            if (role == "Developer") return allTickets.Where(t => t.AssignedToUserId == userId).ToList();
            var a = allTickets.Where(t => t.OwnerUserId == userId).ToList();
            if (role == "Submitter") return tickets = allTickets.Where(t => t.OwnerUserId == userId).ToList();

            return allTickets;
        }

        public static List<Ticket> GetTicketsByProjectId(string userId, string role, int projectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Ticket> tickets = new List<Ticket>();
            List<Ticket> allTickets = db.Tickets.Include("Project")
                .Include("TicketPriority")
                .Include("TicketStatus")
                .Include("TicketType")
                .ToList();

            if (role == "Developer") return allTickets.Where(t => t.AssignedToUserId == userId).Where(t=>t.ProjectId == projectId).ToList();
            if (role == "Submitter") return tickets = allTickets.Where(t => t.OwnerUserId == userId).Where(t => t.ProjectId == projectId).ToList();
            return allTickets;
        }

        public static List<Ticket> SortTickets(List<Ticket> tickets, string sortOption)
        {
            List<Ticket> sortedTickets = new List<Ticket>();

            switch (sortOption)
            {
                case "Creation Date":
                    sortedTickets = tickets.OrderBy(t => t.Created).ToList();
                    break;
                case "Title":
                    sortedTickets = tickets.OrderBy(t => t.Title).ToList();
                    break;
                case "Submitter":
                    sortedTickets = tickets.OrderBy(t => t.OwnerUser.UserName).ToList();
                    break;
                case "Developer":
                    sortedTickets = tickets.Where(t => t.AssignedToUser != null).OrderBy(t => t.AssignedToUser.UserName).ToList();
                    break;
                case "Type":
                    sortedTickets = tickets.OrderBy(t => t.TicketType.Name).ToList();
                    break;
                case "Priority":
                    sortedTickets = tickets.OrderBy(t => t.TicketPriority.Name).ToList();
                    break;
                case "Status":
                    sortedTickets = tickets.OrderBy(t => t.TicketStatus.Name).ToList();
                    break;
                case "Project":
                    sortedTickets = tickets.OrderBy(t => t.Project.Name).ToList();
                    break;
                default:
                    sortedTickets = tickets.OrderBy(t => t.Created).ToList();
                    break;
            }

            db.Dispose();
            return sortedTickets;
        }

        public static List<Ticket> FilterTickets(List<Ticket> tickets, string filterOption, string filterString)
        {
            List<Ticket> filteredTickets = new List<Ticket>();
            var capFilterString = char.ToUpper(filterString[0]) + filterString.Substring(1);

            switch (filterOption)
            {
                case "Creation Date":                    
                    filteredTickets = tickets.Where(t => t.Created >= Convert.ToDateTime(filterString)).ToList();
                    break;                
                case "Type":
                    filteredTickets = tickets.Where(t => t.TicketType.Name == capFilterString).ToList();
                    break;
                case "Priority":
                    filteredTickets = tickets.Where(t => t.TicketPriority.Name == capFilterString).ToList();
                    break;
                case "Status":
                    filteredTickets = tickets.Where(t => t.TicketStatus.Name == capFilterString).ToList();
                    break;               
                default:
                    filteredTickets = tickets;
                    break;
            }

            db.Dispose();
            return filteredTickets;
        }

        public static Ticket GetTicket(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Ticket ticket = db.Tickets.Find(Id);
            //db.Dispose();
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

            Ticket newTicket = db.Tickets.OrderByDescending(p => p.Created).FirstOrDefault();

            var dbEntityEntry = db.Entry(newTicket);
            foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
            {
                string newValue = "";
                if (dbEntityEntry.CurrentValues.GetValue<object>(property) != null)
                {
                    dbEntityEntry.CurrentValues.GetValue<object>(property).ToString();
                    TicketHistoryHelper.SetTicketHistory(ticket.Id, property, "New Record", newValue, HttpContext.Current.User.Identity.GetUserId());
                }
            }

            db.Dispose();
        }

        public static void Edit(int id, string userId, string title, string description, int ticketTypeId, int ticketPriorityId, int ticketStatusId, string assignedUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Ticket ticket = db.Tickets.Find(id);
            if (ticket != null)
            {
                if (title != null) ticket.Title = title;
                if (description != null) ticket.Description = description;
                //ticket.OwnerUserId = userId;
                //ticket.ProjectId = ticket.ProjectId;
                if (ticketTypeId != 0) ticket.TicketTypeId = ticketTypeId;
                if (ticketPriorityId != 0) ticket.TicketPriorityId = ticketPriorityId;
                if (ticketStatusId != 0) ticket.TicketStatusId = ticketStatusId;
                ticket.Updated = DateTime.Now;
                if (assignedUserId != null) ticket.AssignedToUserId = assignedUserId;

                var entityEntry = db.Entry(ticket);
                foreach (var property in entityEntry.OriginalValues.PropertyNames)
                {
                    string oldVAlue = "";
                    string newValue = "";

                    if (entityEntry.OriginalValues.GetValue<object>(property) != null)
                    {
                        oldVAlue = entityEntry.OriginalValues.GetValue<object>(property).ToString();
                    }

                    if (entityEntry.CurrentValues.GetValue<object>(property) != null)
                    {
                        newValue = entityEntry.CurrentValues.GetValue<object>(property).ToString();
                    }

                    if (newValue != null && !oldVAlue.Equals(newValue))
                    {
                        entityEntry.Property(property).IsModified = true;
                        TicketHistoryHelper.SetTicketHistory(id, property, oldVAlue, newValue, HttpContext.Current.User.Identity.GetUserId());
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

        // Assign ticket to user
        public static void Assign(int Id, string assignedToUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var ticket = db.Tickets.Find(Id);
            ticket.AssignedToUserId = assignedToUserId;
            db.SaveChanges();
            db.Dispose();
        }
    }
}
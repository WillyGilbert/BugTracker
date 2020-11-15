using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketStatusHelper
    {
        public static List<TicketStatus> GetTicketStatuses()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketStatus = db.TicketStatuses;
            return TicketStatus.ToList();
        }

        public static TicketStatus GetTicketStatus(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketStatus TicketStatus = db.TicketStatuses.Find(Id);
            db.Dispose();
            if (TicketStatus == null)
            {
                return null;
            }
            db.Dispose();
            return TicketStatus;
        }

        public static void Create(string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketStatus ticketStatus = new TicketStatus
            {
                Name = name
            };
            db.TicketStatuses.Add(ticketStatus);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketStatus ticketStatus = GetTicketStatus(id);
            ticketStatus.Name = name;
            db.Entry(ticketStatus).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketStatus ticketStatus = GetTicketStatus(id);
            db.TicketStatuses.Remove(ticketStatus);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
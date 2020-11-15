using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketPriorityHelper
    {
        public static List<TicketPriority> GetTicketPrioritys()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketPriority = db.TicketPriorities;
            return TicketPriority.ToList();
        }

        public static TicketPriority GetTicketPriority(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketPriority TicketPriority = db.TicketPriorities.Find(Id);
            db.Dispose();
            if (TicketPriority == null)
            {
                return null;
            }
            db.Dispose();
            return TicketPriority;
        }

        public static void Create(string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketPriority ticketPriority = new TicketPriority
            {
                Name = name
            };
            db.TicketPriorities.Add(ticketPriority);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketPriority ticketPriority = GetTicketPriority(id);
            ticketPriority.Name = name;
            db.Entry(ticketPriority).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketPriority ticketPriority = GetTicketPriority(id);
            db.TicketPriorities.Remove(ticketPriority);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
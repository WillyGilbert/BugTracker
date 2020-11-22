using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketTypeHelper
    {
        public static List<TicketType> GetTicketTypes()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketType = db.TicketTypes;
            return TicketType.ToList();
        }

        public static TicketType GetTicketType(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketType TicketType = db.TicketTypes.Find(Id);
            db.Dispose();
            if (TicketType == null)
            {
                return null;
            }
            db.Dispose();
            return TicketType;
        }

        public static void Create(string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketType ticketType = new TicketType
            {
                Name = name
            };
            db.TicketTypes.Add(ticketType);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketType ticketType = GetTicketType(id);
            ticketType.Name = name;
            db.Entry(ticketType).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketType ticketType = GetTicketType(id);
            db.TicketTypes.Remove(ticketType);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
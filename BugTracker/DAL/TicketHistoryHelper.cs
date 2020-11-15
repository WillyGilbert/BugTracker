using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketHistoryHelper
    {
        public static List<TicketHistory> GetTicketHistorys()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketHistory = db.TicketHistories.Include(t => t.Ticket).Include(t => t.User);
            return TicketHistory.ToList();
        }

        public static TicketHistory GetTicketHistory(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketHistory TicketHistory = db.TicketHistories.Find(Id);
            db.Dispose();
            if (TicketHistory == null)
            {
                return null;
            }
            db.Dispose();
            return TicketHistory;
        }

        public static void Create(int ticketId, string property, string oldValue, string newValue, DateTime changed, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketHistory ticketHistory = new TicketHistory
            {
                TicketId = ticketId,
                Property = property,
                OldValue = oldValue,
                NewValue = newValue,
                Changed = changed,
                UserId = userId
            };
            db.TicketHistories.Add(ticketHistory);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, int ticketId, string property, string oldValue, string newValue, DateTime changed, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketHistory ticketHistory = GetTicketHistory(id);
            ticketHistory.TicketId = ticketId;
            ticketHistory.Property = property;
            ticketHistory.OldValue = oldValue;
            ticketHistory.NewValue = newValue;
            ticketHistory.Changed = changed;
            ticketHistory.UserId = userId;
            db.Entry(ticketHistory).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketHistory ticketHistory = GetTicketHistory(id);
            db.TicketHistories.Remove(ticketHistory);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
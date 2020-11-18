using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{

    public class TicketNotificationHelper
    {
        public static List<TicketNotification> GetTicketNotifications()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketNotification = db.TicketNotifications.Include(t => t.Ticket).Include(t => t.User);
            return TicketNotification.ToList();
        }

        public static void  AddNotification(int id,string userId, NotificationType type, string userName )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tickNotification = new TicketNotification { TicketId = id, UserId = userId,  Type = type, ModifiedUser = userName };
            db.TicketNotifications.Add(tickNotification);
            db.SaveChanges();
        }

        public static TicketNotification GetTicketNotification(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketNotification TicketNotification = db.TicketNotifications.Find(Id);
            db.Dispose();
            if (TicketNotification == null)
            {
                return null;
            }
            db.Dispose();
            return TicketNotification;
        }

        public static void Create(int ticketId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketNotification ticketNotification = new TicketNotification
            {
                TicketId = ticketId,
                UserId = userId
            };
            db.TicketNotifications.Add(ticketNotification);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, int ticketId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketNotification ticketNotification = GetTicketNotification(id);
            ticketNotification.TicketId = ticketId;
            ticketNotification.UserId = userId;
            db.Entry(ticketNotification).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }
        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketNotification ticketNotification = GetTicketNotification(id);
            db.TicketNotifications.Remove(ticketNotification);
            db.SaveChanges();
            db.Dispose();
        }
        //Developers must be notified each time they are assigned to a ticket

        public static List<TicketNotification> GetAllNotificationForDeveloper(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var ticketNotifications = db.TicketNotifications.ToList();
            var result = ticketNotifications.Where(n => n.UserId == userId).ToList();
            //var result = new List<TicketNotification>();
            //var user = db.Users.Find(userId);

            //if (user == null)
            //{
            //    return result;
            //}
            //result.AddRange(db.TicketNotifications.Where(x => x.UserId == userId).ToList());
            return result;

        }
        //Developers must be notified each time a ticket to which they are assigned is modified by another user
        //(including the addition of comments and attachments)




        // Add or delete notification
        private void AddDeleteNotification(bool addOrDel, int ticketId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (addOrDel)
            {
                bool existedNote = db.TicketNotifications
                    .Any(p => p.TicketId == ticketId && p.UserId == userId);

                if (!existedNote)
                {
                    var addNote = new TicketNotification()
                    {
                        TicketId = ticketId,
                        UserId = userId,
                    };

                    db.TicketNotifications.Add(addNote);

                }

            }
            else
            {
                var deleteNote = db.TicketNotifications
                        .Where(p => p.TicketId == ticketId && p.UserId == userId)
                        .FirstOrDefault();
                if (deleteNote != null)
                {
                    db.TicketNotifications.Remove(deleteNote);
                }

            }
            db.SaveChanges();
            db.Dispose();
        }


        // Notification
        //public static List<TicketNotification> GetNotificationOfUser(string userId)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var notes = db.TicketNotifications.Include(n => n.Ticket).Include(u => u.User).ToList();

        //    return notes.Where(n =>
        //        n.NotificationType == NotificationType.Normal ||
        //        n.NotificationType == NotificationType.NextToExpire
        //    ).ToList();
        //}
        public static string CountUserNotifications(string userId)
        {
            return GetAllNotificationForDeveloper(userId).Count().ToString();
        }
    }
}
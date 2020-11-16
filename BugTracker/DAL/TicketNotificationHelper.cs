﻿using BugTracker.Models;
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
    }
}
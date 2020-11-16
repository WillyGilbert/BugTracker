using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketAttachmentHelper
    {
        public static List<TicketAttachment> GetTicketAttachments()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketAttachment = db.TicketAttachments.Include(t => t.Ticket).Include(t => t.User);
            return TicketAttachment.ToList();
        }

        public static TicketAttachment GetTicketAttachment(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketAttachment TicketAttachment = db.TicketAttachments.Find(Id);
            db.Dispose();
            if (TicketAttachment == null)
            {
                return null;
            }
            db.Dispose();
            return TicketAttachment;
        }

        public static void Create(int ticketId, string filePath, string description, DateTime created, string userId, string fileUrl)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketAttachment ticketAttachment = new TicketAttachment
            {
                TicketId = ticketId,
                FilePath = filePath,
                Description = description,
                Created = created,
                UserId = userId,
                FileUrl = fileUrl
            };
            db.TicketAttachments.Add(ticketAttachment);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, int ticketId, string filePath, string description, DateTime created, string userId, string fileUrl)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketAttachment ticketAttachment = GetTicketAttachment(id);
            ticketAttachment.TicketId = ticketId;
            ticketAttachment.FilePath = filePath;
            ticketAttachment.Description = description;
            ticketAttachment.Created = created;
            ticketAttachment.UserId = userId;
            ticketAttachment.FileUrl = fileUrl;
            db.Entry(ticketAttachment).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketAttachment ticketAttachment = GetTicketAttachment(id);
            db.TicketAttachments.Remove(ticketAttachment);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
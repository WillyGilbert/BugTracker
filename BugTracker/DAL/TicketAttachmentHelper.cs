using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketAttachmentHelper
    {
        public static List<TicketAttachment> GetTicketAttachments(int ticketId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketAttachment = db.TicketAttachments.Include(t => t.Ticket).Include(t => t.User).Where(t => t.TicketId == ticketId);
            return TicketAttachment.ToList();
        }

        public static Message Create(int ticketId, HttpPostedFileBase filePath, string description, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Message message = FileUploaderHelper.Uploader(ticketId, filePath);
            string fileUrl = message.FileName;
            string fileName = Path.GetFileName(fileUrl);

            if (message.Code == MessageType.Successful)
            {
                TicketAttachment ticketAttachment = new TicketAttachment
                {
                    TicketId = ticketId,
                    FilePath = fileName,
                    Description = description,
                    Created = DateTime.Now,
                    UserId = userId,
                    FileUrl = fileUrl
                };
                db.TicketAttachments.Add(ticketAttachment);
                db.SaveChanges();
                db.Dispose();
                return message;
            }
            else
            {
                return message;
            }
        }

        public static TicketAttachment GetTicketAttachment(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketAttachment TicketAttachment = db.TicketAttachments.Find(Id);
            if (TicketAttachment == null)
            {
                return null;
            }
            return TicketAttachment;
        }
        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            db.TicketAttachments.Remove(ticketAttachment);
            db.SaveChanges();
            db.Dispose();
        }

    }
}
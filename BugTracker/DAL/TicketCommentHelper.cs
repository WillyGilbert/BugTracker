using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public static class TicketCommentHelper
    {
        public static List<TicketComment> GetTicketComments()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketComment = db.TicketComments.Include(t => t.Ticket).Include(t => t.User);
            return TicketComment.ToList();
        }
        public static List<TicketComment> GetTicketCommentsByTicket(int? ticketId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketComment = db.TicketComments.Include(t => t.Ticket).Include(t => t.User).Where(t => t.TicketId == ticketId);
            return TicketComment.ToList();
        }
        public static TicketComment GetTicketComment(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketComment ticketComment = db.TicketComments.Find(Id);
            if (ticketComment == null)
            {
                return null;
            }

            return ticketComment;
        }
        public static void Create(string comment, int ticketId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketComment ticketComment = new TicketComment
            {
                Comment = comment,
                Created = DateTime.Now,
                TicketId = ticketId,
                UserId = userId
            };
            db.TicketComments.Add(ticketComment);
            db.SaveChanges();
            db.Dispose();
        }
        public static void Edit(int id, string comment)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketComment ticketComment = db.TicketComments.Find(id);
            ticketComment.Comment = comment;
            ticketComment.Created = DateTime.Now;
            db.Entry(ticketComment).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }
        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketComment ticketComment = db.TicketComments.Find(id);
            db.TicketComments.Remove(ticketComment);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
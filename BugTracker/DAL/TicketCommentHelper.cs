﻿using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketCommentHelper
    {
        public static List<TicketComment> GetTicketComments()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var TicketComment = db.TicketComments.Include(t => t.Ticket).Include(t => t.User);
            return TicketComment.ToList();
        }

        public static TicketComment GetTicketComment(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketComment TicketComment = db.TicketComments.Find(Id);
            db.Dispose();
            if (TicketComment == null)
            {
                return null;
            }
            db.Dispose();
            return TicketComment;
        }

        public static void Create(string comment, DateTime created, int ticketId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketComment ticketComment = new TicketComment
            {
                Comment = comment,
                Created = created,
                TicketId = ticketId,
                UserId = userId
            };
            db.TicketComments.Add(ticketComment);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, string comment, DateTime created, int ticketId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketComment ticketComment = GetTicketComment(id);
            ticketComment.Comment = comment;
            ticketComment.Created = created;
            ticketComment.TicketId = ticketId;
            ticketComment.UserId = userId;
            db.Entry(ticketComment).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TicketComment ticketComment = GetTicketComment(id);
            db.TicketComments.Remove(ticketComment);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
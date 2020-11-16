using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.DAL;
using BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    public class TicketCommentsController : Controller
    {

        // GET: TicketComments
        public ActionResult Index()
        {
            var ticketComments = TicketCommentHelper.GetTicketComments();
            return View(ticketComments.ToList());
        }

        // GET: TicketComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComment = TicketCommentHelper.GetTicketComment(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            return View(ticketComment);
        }

        // GET: TicketComments/Create
        public ActionResult Create(Ticket ticket)
        {
            ViewBag.TicketId = ticket.Id;
            //ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title");
            //ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email");
            return View();
        }

        // POST: TicketComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comment,Created,TicketId,UserId")] TicketComment ticketComment)
        {
            if (ModelState.IsValid)
            {                
                TicketCommentHelper.Create(ticketComment.Comment, ticketComment.TicketId, User.Identity.GetUserId());
                return RedirectToAction("Index");
            }

            //ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketComment.TicketId);
            //ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketComment.UserId);
            return View(ticketComment);
        }

        // GET: TicketComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComment = TicketCommentHelper.GetTicketComment(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketComment.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketComment.UserId);
            return View(ticketComment);
        }

        // POST: TicketComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment,Created,TicketId,UserId")] TicketComment ticketComment)
        {
            if (ModelState.IsValid)
            {
                TicketCommentHelper.Edit(ticketComment.Id, ticketComment.Comment, ticketComment.Created, ticketComment.TicketId, ticketComment.UserId);
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketComment.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketComment.UserId);
            return View(ticketComment);
        }

        // GET: TicketComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComment = TicketCommentHelper.GetTicketComment(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            return View(ticketComment);
        }

        // POST: TicketComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketCommentHelper.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

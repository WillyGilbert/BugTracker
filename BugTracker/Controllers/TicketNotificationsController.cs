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

namespace BugTracker.Controllers
{
    public class TicketNotificationsController : Controller
    {
        // GET: TicketNotifications
        public ActionResult Index()
        {
            return View(TicketNotificationHelper.GetTicketNotifications());
        }

        // GET: TicketNotifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = TicketNotificationHelper.GetTicketNotification(id);
            if (ticketNotification == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title");
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email");
            return View();
        }

        // POST: TicketNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,UserId")] TicketNotification ticketNotification)
        {
            if (ModelState.IsValid)
            {
                TicketNotificationHelper.Create(ticketNotification.TicketId, ticketNotification.UserId);
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketNotification.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = TicketNotificationHelper.GetTicketNotification(id);
            if (ticketNotification == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketNotification.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // POST: TicketNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,UserId")] TicketNotification ticketNotification)
        {
            if (ModelState.IsValid)
            {
                TicketNotificationHelper.Edit(ticketNotification.Id, ticketNotification.TicketId, ticketNotification.UserId);
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketNotification.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = TicketNotificationHelper.GetTicketNotification(id);
            if (ticketNotification == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotification);
        }

        // POST: TicketNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketNotificationHelper.Delete(id);
            return RedirectToAction("Index");
        }
        // Notification****************
        [HttpPost]
        public ActionResult Notify (string userId, int? ticketId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if(!string.IsNullOrEmpty(userId) && ticketId.HasValue)
            {
                Ticket ticket = db.Tickets.FirstOrDefault(n => n.Id == ticketId);
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
              
            }
            return View();
        }
    }
}

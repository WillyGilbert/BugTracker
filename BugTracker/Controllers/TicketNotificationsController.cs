﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using PagedList;

namespace BugTracker.Controllers
{
    public class TicketNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketNotifications
        public ActionResult Index(string userId, int? page)
        {
            var ticketNotifications = db.TicketNotifications.Include(t => t.Ticket).Include(t => t.User).Where(t => t.UserId == userId).OrderByDescending(t => t.Id).ToList();
            return View(PaginateList(ticketNotifications, page));
        }

        public IPagedList<TicketNotification> PaginateList(List<TicketNotification> ticketNotifications, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.TicketsPage = pageNumber;
            return (ticketNotifications.ToPagedList(pageNumber, pageSize));
        }
        // GET: TicketNotifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = db.TicketNotifications.Find(id);
            if (ticketNotification == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: TicketNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,UserId,Type,ModifiedUser")] TicketNotification ticketNotification)
        {
            if (ModelState.IsValid)
            {
                db.TicketNotifications.Add(ticketNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotification.TicketId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = db.TicketNotifications.Find(id);
            if (ticketNotification == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotification.TicketId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // POST: TicketNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,UserId,Type,ModifiedUser")] TicketNotification ticketNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotification.TicketId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = db.TicketNotifications.Find(id);
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
            TicketNotification ticketNotification = db.TicketNotifications.Find(id);
            db.TicketNotifications.Remove(ticketNotification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

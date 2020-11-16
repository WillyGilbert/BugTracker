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
    public class TicketStatusController : Controller
    {
        // GET: TicketStatus
        public ActionResult Index()
        {
            return View(TicketStatusHelper.GetTicketStatuses());
        }

        // GET: TicketStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketStatus ticketStatus = TicketStatusHelper.GetTicketStatus(id);
            if (ticketStatus == null)
            {
                return HttpNotFound();
            }
            return View(ticketStatus);
        }

        // GET: TicketStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TicketStatus ticketStatus)
        {
            if (ModelState.IsValid)
            {
                TicketStatusHelper.Create(ticketStatus.Name);
                return RedirectToAction("Index");
            }

            return View(ticketStatus);
        }

        // GET: TicketStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketStatus ticketStatus = TicketStatusHelper.GetTicketStatus(id);
            if (ticketStatus == null)
            {
                return HttpNotFound();
            }
            return View(ticketStatus);
        }

        // POST: TicketStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TicketStatus ticketStatus)
        {
            if (ModelState.IsValid)
            {
                TicketStatusHelper.Edit(ticketStatus.Id, ticketStatus.Name);
                return RedirectToAction("Index");
            }
            return View(ticketStatus);
        }

        // GET: TicketStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketStatus ticketStatus = TicketStatusHelper.GetTicketStatus(id);
            if (ticketStatus == null)
            {
                return HttpNotFound();
            }
            return View(ticketStatus);
        }

        // POST: TicketStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketStatusHelper.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

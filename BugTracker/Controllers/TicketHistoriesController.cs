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
    public class TicketHistoriesController : Controller
    {

        // GET: TicketHistories
        public ActionResult Index()
        {
            return View(TicketHistoryHelper.GetTicketHistorys());
        }

        // GET: TicketHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketHistory ticketHistory = TicketHistoryHelper.GetTicketHistory(id);
            if (ticketHistory == null)
            {
                return HttpNotFound();
            }
            return View(ticketHistory);
        }

        // GET: TicketHistories/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title");
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email");
            return View();
        }

        // POST: TicketHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,Property,OldValue,NewValue,Changed,UserId")] TicketHistory ticketHistory)
        {
            if (ModelState.IsValid)
            {
                TicketHistoryHelper
                    .Create(ticketHistory.TicketId, 
                    ticketHistory.Property, 
                    ticketHistory.OldValue, 
                    ticketHistory.NewValue, 
                    ticketHistory.Changed, 
                    ticketHistory.UserId);
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketHistory.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketHistory.UserId);
            return View(ticketHistory);
        }

        // GET: TicketHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketHistory ticketHistory = TicketHistoryHelper.GetTicketHistory(id);
            if (ticketHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketHistory.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketHistory.UserId);
            return View(ticketHistory);
        }

        // POST: TicketHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,Property,OldValue,NewValue,Changed,UserId")] TicketHistory ticketHistory)
        {
            if (ModelState.IsValid)
            {
                TicketHistoryHelper.Edit(ticketHistory.Id,
                    ticketHistory.TicketId,
                    ticketHistory.Property,
                    ticketHistory.OldValue,
                    ticketHistory.NewValue,
                    ticketHistory.Changed,
                    ticketHistory.UserId);
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketHistory.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketHistory.UserId);
            return View(ticketHistory);
        }

        // GET: TicketHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketHistory ticketHistory = TicketHistoryHelper.GetTicketHistory(id);
            if (ticketHistory == null)
            {
                return HttpNotFound();
            }
            return View(ticketHistory);
        }

        // POST: TicketHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketHistoryHelper.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

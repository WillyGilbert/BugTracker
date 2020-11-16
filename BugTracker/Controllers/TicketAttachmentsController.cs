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
    public class TicketAttachmentsController : Controller
    {

        // GET: TicketAttachments
        public ActionResult Index()
        {
            var ticketAttachments = TicketAttachmentHelper.GetTicketAttachments();
            return View(ticketAttachments.ToList());
        }

        // GET: TicketAttachments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = TicketAttachmentHelper.GetTicketAttachment(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title");
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email");
            return View();
        }

        // POST: TicketAttachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,FilePath,Description,Created,UserId,FileUrl")] TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                TicketAttachmentHelper.Create(ticketAttachment.TicketId, 
                    ticketAttachment.FilePath, 
                    ticketAttachment.Description, 
                    ticketAttachment.Created, 
                    ticketAttachment.UserId, 
                    ticketAttachment.FileUrl);
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketAttachment.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketAttachment.UserId);
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = TicketAttachmentHelper.GetTicketAttachment(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketAttachment.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketAttachment.UserId);
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,FilePath,Description,Created,UserId,FileUrl")] TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                TicketAttachmentHelper.Edit(ticketAttachment.Id, 
                    ticketAttachment.TicketId, 
                    ticketAttachment.FilePath, 
                    ticketAttachment.Description, 
                    ticketAttachment.Created, 
                    ticketAttachment.UserId, 
                    ticketAttachment.FileUrl);
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(TicketHelper.GetTickets(), "Id", "Title", ticketAttachment.TicketId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", ticketAttachment.UserId);
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = TicketAttachmentHelper.GetTicketAttachment(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketAttachmentHelper.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

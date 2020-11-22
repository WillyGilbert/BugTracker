using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.DAL;
using BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    public class TicketAttachmentsController : Controller
    {
        // GET: TicketAttachments
        public ActionResult Index(int ticketId)
        {
            var ticketAttachments = TicketAttachmentHelper.GetTicketAttachments(ticketId);
            ViewBag.TicketId = ticketId;
            return View(ticketAttachments.ToList());
        }

        // GET: TicketAttachments/Create
        public ActionResult Create(int ticketId)
        {
            ViewBag.TicketId = ticketId;
            return View();
        }

        // POST: TicketAttachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int ticketId, HttpPostedFileBase file, string description)
        {
            Message message = TicketAttachmentHelper.Create(ticketId, file, description, User.Identity.GetUserId());
            if (message.Code == MessageType.Successful && description.Length > 0)
            {
                return RedirectToAction("Index", new { ticketId });
            }
            if (description.Length > 0)
            {
                ViewBag.MessageDescription = "";
            }
            else
            {
                ViewBag.MessageDescription = "Please add the description";
            }
            ViewBag.Message = message.Description;
            return View();
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
        public ActionResult DeleteConfirmed(int id, string file, int ticketId)
        {
            TicketAttachmentHelper.Delete(id);
            FileUploaderHelper.Delete(file);
            return RedirectToAction("Index", new { ticketId });
        }

        //Attachament Management
        public FileResult Download(string file)
        {
            var path = FileUploaderHelper.Download(file);
            return File(path, "application/force- download", Path.GetFileName(path));
        }

        private List<string> GetFiles()
        {
            return FileUploaderHelper.GetFiles();
        }

    }
}

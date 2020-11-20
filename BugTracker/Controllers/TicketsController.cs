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
using BugTracker.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;

namespace BugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        SortViewModel sortModel = new SortViewModel();
        FilterViewModel filterModel = new FilterViewModel();

        [Authorize]
        public ActionResult Index(string userId, string role)
        {
            ViewBag.SelectSort = new SelectList(sortModel.Options);
            ViewBag.SelectFilter = new SelectList(filterModel.Options);
            var tickets = TicketHelper.GetTickets(userId, role).ToList();

            return View(tickets);
        }

        //[Authorize]
        //public ActionResult Index(string userId, string role,int? page)
        //{            
        //    ViewBag.SelectSort = new SelectList(sortModel.Options);
        //    ViewBag.SelectFilter = new SelectList(filterModel.Options);
        //    var tickets = TicketHelper.GetTickets(userId, role).ToList();
            
        //    return View(PaginateList(tickets, page));           
        //}

        [HttpPost]
        public ActionResult Index(string selectSort, string UserId, string role, int? page, string searchString, string selectFilter, string filterString)
        {            
            ViewBag.SelectSort = new SelectList(sortModel.Options);
            ViewBag.SelectFilter = new SelectList(filterModel.Options);
            var tickets = TicketHelper.GetTickets(UserId, role);

            if (selectSort != null)
                tickets = TicketHelper.SortTickets(tickets, selectSort);
            
            if (!String.IsNullOrEmpty(filterString))
                tickets = TicketHelper.FilterTickets(tickets, selectFilter, filterString);

            if (!String.IsNullOrEmpty(searchString))            
                tickets = TicketHelper.GetTickets(UserId, role).Where(t => t.Title.Contains(searchString)
                                       || t.Description.Contains(searchString)).ToList();
            
            return View(PaginateList(tickets, page));
        }
    
        public IPagedList<Ticket> PaginateList(List<Ticket> tickets, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.TicketsPage = pageNumber;
            return (tickets.ToPagedList(pageNumber, pageSize));
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = TicketHelper.GetTicket(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create(int projectId)
        {
            //ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "Email");
            //ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "Email");
            //ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.ProjectId = projectId;
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        public ActionResult Create(string title, string description, int projectId, int TicketTypeId, int TicketPriorityId, int TicketStatusId)
        {
            //ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "Email", ticket.AssignedToUserId);
            //ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "Email", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");

            if (ModelState.IsValid)
            {               
                TicketHelper.Create(User.Identity.GetUserId(), title, description, projectId, TicketTypeId, TicketPriorityId, TicketStatusId);
                return RedirectToAction("ShowMyProjects", "Projects");
            }

            return RedirectToAction("ShowMyProjects", "Projects");
            //return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = TicketHelper.GetTicket(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
                        
            string roleId = db.Roles.FirstOrDefault(r => r.Name == "Developer").Id;
            var developers = db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId));

            ViewBag.AssignedToUserId = new SelectList(developers, "Id", "Email", ticket.AssignedToUserId);      
            //ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "Email", ticket.OwnerUserId);
            //ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(ticket).State = EntityState.Modified;
                //db.SaveChanges();
                TicketHelper.Edit(ticket.Id, ticket.OwnerUserId, ticket.Title, ticket.Description, ticket.TicketTypeId, ticket.TicketPriorityId, ticket.TicketStatusId, ticket.AssignedToUserId);

                var modifiedTicket = db.Tickets.Find(ticket.Id);
                TicketNotificationHelper.AddNotification(modifiedTicket.Id, modifiedTicket.AssignedToUserId, NotificationType.ModifiedBy, User.Identity.GetUserName());
                db.SaveChanges();
                return RedirectToAction("Index", "Projects", new { userId = User.Identity.GetUserId() });
            }
                        
            string roleId = db.Roles.FirstOrDefault(r => r.Name == "Developer").Id;
            var developers = db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId));

            ViewBag.AssignedToUserId = new SelectList(developers, "Id", "Email", ticket.AssignedToUserId);      
            //ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        public ActionResult Assign(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = TicketHelper.GetTicket(Id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            //var users = db.Users.Where(u => UserHelper.UserInRole(u.Id, "Developer") == true);
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.Id = Id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(int Id, string AssignedToUserId)
        {
            TicketHelper.Assign(Id, AssignedToUserId);
            //var users = db.Users.Where(u => UserHelper.UserInRole(u.Id, "Developer") ==true);
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.Id = Id;

            TicketNotificationHelper.AddNotification(Id, AssignedToUserId, NotificationType.AssignedBy, User.Identity.GetUserName());

            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Projects", new { userId = User.Identity.GetUserId() });
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

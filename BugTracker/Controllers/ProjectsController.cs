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
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        SortViewModel sortModel = new SortViewModel();

        // GET: Projects
        public ActionResult Index(bool? myproject, int? page)
        {
            //SortViewModel sortModel = new SortViewModel();
            ViewBag.SelectFilter = new SelectList(sortModel.Options);
            
            if (myproject == true)            
                return View(PaginateList(ProjectHelper.GetMyProjects(User.Identity.GetUserId()), page));       
            else            
                return View(PaginateList(ProjectHelper.GetProjects(), page));           
        }

        [HttpPost]
        public ActionResult Index(string SelectFilter, int? page, string searchString)
        {
            //SortViewModel sortModel = new SortViewModel();
            ViewBag.SelectFilter = new SelectList(sortModel.Options);            
            var projects = ProjectHelper.GetProjects();

            if (SelectFilter == "Creation Date")           
                projects = ProjectHelper.SortTicketsByTitle(projects);            
            else if (SelectFilter == "Title")            
                projects = ProjectHelper.SortTicketsByTitle(projects);


            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    tickets = TicketHelper.GetTickets(UserId).Where(t => t.Title.Contains(searchString)
            //                           || t.Description.Contains(searchString)).ToList();
            //}
                        
            return View(PaginateList(projects, page));
        }

        public IPagedList<Project> PaginateList(List<Project> projects, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.TicketsPage = pageNumber;
            return (projects.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ShowMyProjects()
        {
            return View(ProjectHelper.GetMyProjects(User.Identity.GetUserId()));
        }

        public ActionResult ShowAllUsers(int projectId)
        {
            ViewBag.ProjectId = projectId;
            var users = ProjectHelper.UsersOfTheProject(projectId);
            return View(users);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveUserFromProject(string userId, int projectId)
        {
            if (ProjectHelper.RemoveUserFromProject(userId, projectId))
            {
                return RedirectToAction("ShowAllUsers", new { projectId });
            }
            return RedirectToAction("ShowAllUsers", new { projectId });
        }
        public ActionResult AssignUserToProject(int projectId)
        {
            var project = ProjectHelper.GetProject(projectId);
            if (project == null)
            {
                return HttpNotFound();
            }
            var users = ProjectHelper.UsersOutOfTheProject(projectId);
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = project.Name;
            ViewBag.UserId = new SelectList(users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignUserToProject(string Id, int projectId)
        {

            var project = ProjectHelper.GetProject(projectId);
            if (project == null)
            {
                return HttpNotFound();
            }

            if (ProjectHelper.AssignUserToProject(Id, projectId))
            {
                return RedirectToAction("ShowAllUsers", new { projectId });
            }

            var users = ProjectHelper.UsersOutOfTheProject(projectId);
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = project.Name;
            ViewBag.UserId = new SelectList(users, "Id", "UserName");

            return RedirectToAction("ShowAllUsers", new { projectId });
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

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
using System.Security.Claims;

namespace BugTracker.Controllers
{
    //[Authorize(Roles ="Admin,ProjectManager")]
    public class ProjectsController : Controller
    {

        // GET: Projects
        //[Authorize(Roles = "Admin,ProjectManager,Developer,Submitter")]
        public ActionResult Index()
        {
            return View(ProjectHelper.GetProjects());
        }

        //[Authorize(Roles = "Admin,ProjectManager,Developer,Submitter")]
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
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Project project = ProjectHelper.GetProject(id);
        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(project);
        //}

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
                ProjectHelper.Create(project.Name);
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
            Project project = ProjectHelper.GetProject(id);
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
                ProjectHelper.Edit(project.Id, project.Name);
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
            Project project = ProjectHelper.GetProject(id);
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
            ProjectHelper.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveUserFromProject(string userId, int projectId)
        {
            if(ProjectHelper.RemoveUserFromProject(userId, projectId))
            {
                return RedirectToAction("ShowAllUsers",new { projectId});
            }
            return RedirectToAction("ShowAllUsers", new { projectId });
        }
        public ActionResult AssignUserToProject(int projectId)
        {
            var project = ProjectHelper.GetProject(projectId);
            if(project == null)
            {
                return HttpNotFound();
            }
            var users = ProjectHelper.UsersOutOfTheProject(projectId);
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

            ViewBag.ProjectName = project.Name;
            ViewBag.UserId = new SelectList(users, "Id", "UserName");

            return RedirectToAction("ShowAllUsers", new { projectId });
        }
    }
}

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
    public class ProjectUsersController : Controller
    {
        // GET: ProjectUsers
        public ActionResult Index()
        {
            return View(ProjectUserHelper.GetProjectUsers());
        }

        // GET: ProjectUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUser projectUser = ProjectUserHelper.GetProjectUser(id);
            if (projectUser == null)
            {
                return HttpNotFound();
            }
            return View(projectUser);
        }

        // GET: ProjectUsers/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(ProjectHelper.GetProjects(), "Id", "Name");
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email");
            return View();
        }

        // POST: ProjectUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,UserId")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                ProjectUserHelper.Create(projectUser.ProjectId, projectUser.UserId);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(ProjectHelper.GetProjects(), "Id", "Name", projectUser.ProjectId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", projectUser.UserId);
            return View(projectUser);
        }

        // GET: ProjectUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUser projectUser = ProjectUserHelper.GetProjectUser(id);
            if (projectUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(ProjectHelper.GetProjects(), "Id", "Name", projectUser.ProjectId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", projectUser.UserId);
            return View(projectUser);
        }

        // POST: ProjectUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,UserId")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                ProjectUserHelper.Edit(projectUser.Id, projectUser.ProjectId, projectUser.UserId);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(ProjectHelper.GetProjects(), "Id", "Name", projectUser.ProjectId);
            ViewBag.UserId = new SelectList(UserHelper.GetAllUsers(), "Id", "Email", projectUser.UserId);
            return View(projectUser);
        }

        // GET: ProjectUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUser projectUser = ProjectUserHelper.GetProjectUser(id);
            if (projectUser == null)
            {
                return HttpNotFound();
            }
            return View(projectUser);
        }

        // POST: ProjectUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectUserHelper.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

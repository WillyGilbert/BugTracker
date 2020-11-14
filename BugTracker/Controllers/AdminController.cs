using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowAllUsers()
        {
            var users = UserManager.ShowAllUsers();
            return View(users);
        }
        public ActionResult ShowAllRoles()
        {
            var roles = UserManager.ShowAllRoles();
            return View(roles);
        }
        public ActionResult ShowAllRolesOfTheUser(string userId)
        {
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.UserId = userId;
            var roles = UserManager.ShowAllRolesForAUser(userId);
            return View(roles);
        }
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(string roleName)
        {
            UserManager.CreateRole(roleName);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("ShowAllRoles");
        }

        public ActionResult AddUserToRole(string userId)
        {
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRole(string roleName, string userId)
        {
            var r = UserManager.AddUserToRole(userId, roleName);
            if (r.Succeeded)
            {
                db.SaveChanges();
            }

            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name", "Name");
            db.Dispose();
            return RedirectToAction("ShowAllRolesOfTheUser", new { userId });
        }

        public ActionResult RemoveRole(string roleName, string userId)
        {
            ViewBag.RoleName = roleName;
            ViewBag.UserId = userId;
            ViewBag.UserName = db.Users.Find(userId).UserName;
            return View();
        }
        [HttpPost, ActionName("RemoveRole")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveRoleConfirmed(string roleName, string userId)
        {
            ViewBag.RoleName = roleName;
            ViewBag.UserId = userId;
            ViewBag.UserName = db.Users.Find(userId).UserName;
            if (UserManager.DeleteUserFromRole(roleName, userId))
            {
                db.SaveChanges();
            }

            return RedirectToAction("ShowAllRolesOfTheUser", new { userId });
        }
        //public ActionResult SetSalary(string userId)
        //{
        //    ViewBag.UserId = userId;
        //    ViewBag.UserName = db.Users.Find(userId).UserName;
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SetSalary(string userId, double salary)
        //{
        //    ViewBag.UserId = userId;
        //    ViewBag.UserName = db.Users.Find(userId).UserName;
        //    UserManager.SetSalary(userId, salary);
        //    db.SaveChanges();
        //    db.Dispose();
        //    return RedirectToAction("ShowAllUsers");
        //}
        public ActionResult DeleteRole(string roleName)
        {
            if (!UserManager.IsRoleExist(roleName))
            {
                return HttpNotFound();
            }
            ViewBag.RoleName = roleName;
            return View();
        }
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string roleName)
        {
            UserManager.DeleteRole(roleName);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("ShowAllRoles");
        }
    }
}
using BugTracker.DAL;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{

    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));
        //RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowAllUsers()
        {
            var users = UserHelper.ShowAllUsers();
            return View(users);
        }
        public ActionResult ShowAllRoles()
        {
            var roles = UserHelper.ShowAllRoles();
            return View(roles);
        }
        public ActionResult ShowAllRolesOfTheUser(string userId)
        {
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.UserId = userId;
            var roles = UserHelper.ShowAllRolesForAUser(userId);
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
            UserHelper.CreateRole(roleName);
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
            var r = UserHelper.AddUserToRole(userId, roleName);
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
            if (UserHelper.DeleteUserFromRole(roleName, userId))
            {
                db.SaveChanges();
            }

            return RedirectToAction("ShowAllRolesOfTheUser", new { userId });
        }
 
        public ActionResult DeleteRole(string roleName)
        {
            if (!UserHelper.IsRoleExist(roleName))
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
            UserHelper.DeleteRole(roleName);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("ShowAllRoles");
        }
    }
}
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BugTracker.DAL
{
    public static class UserHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        //Create a new role in the database
        public static void DefineNewRole(string roleName)
        {
            roleManager.Create(new IdentityRole(roleName));
        }
        public static List<ApplicationUser> GetAllUsers()
        {
            var getAllUsers = db.Users.ToList();
            return getAllUsers;
        }

        //Check if specified roleName exist or not
        public static bool CheckRole(string roleName)
        {
            return roleManager.RoleExists(roleName);
        }

        //Gives a list of user from the array of string containing userId.
        // returns list of users
        public static List<ApplicationUser> GetAllUsersFromIds(string[] userIds)
        {
            var users = new List<ApplicationUser>();
            if (userIds.Length != 0)
            {
                foreach (var id in userIds)
                {
                    users.Add(db.Users.Find(id));
                }
            }
            return users;
        }

        //Gives a user object who have the same specified userId
        public static ApplicationUser GetUserFromId(string userId)
        {
            ApplicationUser user = new ApplicationUser();
            if (!string.IsNullOrEmpty(userId))
            {
                user = db.Users.Find(userId);
            }
            return user;
        }

        //Gives a list of user who have specified role.
        //To get all user in particular role
        public static List<ApplicationUser> GetUsersFromRole(string roleName)
        {
            var users = db.Users.ToList();
            var listOfUser = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if (userManager.IsInRole(user.Id, roleName))
                {
                    listOfUser.Add(user);
                }
            }
            return listOfUser;

        }
        public static List<string> ShowAllRoles()
        {
            return db.Roles.Select(r => r.Name).ToList();
        }
        public static List<string> ShowAllRolesForAUser(string userId)
        {
            if (userId != null)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var roles = userManager.GetRoles(userId).ToList();
                return roles;
            }
            return null;
        }

        public static bool IsRoleExist(string roleName)
        {
            return roleManager.RoleExists(roleName);
        }

        public static bool UserInRole(string userId, string roleName)
        {
            var a = userManager;
            return userManager.IsInRole(userId, roleName);
        }

        public static bool CreateRole(string roleName)
        {
            roleName = roleName.ToLower();
            bool result = false;
            if (roleManager.RoleExists(roleName))
            {
                return true;
            }
            else
            {
                result = roleManager.Create(new IdentityRole(roleName)).Succeeded;
            }

            return result;
        }

        public static bool DeleteRole(string roleName)
        {
            roleName = roleName.ToLower();
            bool result = false;
            if (roleManager.RoleExists(roleName))
            {
                var users = roleManager.FindByName(roleName).Users.ToList().Select(x => x.UserId).ToList();
                users.ForEach(uId =>
                {
                    userManager.RemoveFromRole(uId, roleName);
                });
                result = roleManager.Delete(roleManager.FindByName(roleName)).Succeeded;
            }
            return result;
        }
        public static IdentityResult AddUserToRole(string userId, string roleName)
        {
            return userManager.AddToRole(userId, roleName);
        }


        public static bool DeleteUserFromRole(string roleName, string userId)
        {
            roleName = roleName.ToLower();
            bool result = false;
            if (roleManager.RoleExists(roleName) && userManager.FindById(userId) != null)
            {
                if (userManager.IsInRole(userId, roleName))
                {
                    result = userManager.RemoveFromRole(userId, roleName).Succeeded;
                }
            }
            return result;
        }
    }
}
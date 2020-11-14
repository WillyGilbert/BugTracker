using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helper
{
    public class UserHelper
    {
        ApplicationDbContext db;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        public UserHelper()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(db));
            db = new ApplicationDbContext();

        }

        //public void SeedRolesInDatabase()
        //{
        //    roleManager.Create(new IdentityRole("Admin"));
        //    roleManager.Create(new IdentityRole("ProjectManager"));
        //    roleManager.Create(new IdentityRole("Developer"));
        //    roleManager.Create(new IdentityRole("Submitter"));
        //}


        //Create a new role in the database
        public void DefineNewRole(string roleName)
        {
            roleManager.Create(new IdentityRole(roleName));
        }

        // Get a role name from a role Id.
        public string GetRole(string roleId)
        {
            return roleManager.FindById(roleId).Name;
        }

        // Get a list of allrole
        public List<string> GetAllRoles()
        {
            var allRoles = roleManager.Roles.Select(r => r.Name).ToList();
            return allRoles;
        }

        //Check if specified roleName exist or not
        public bool CheckRole(string roleName)
        {
            return roleManager.RoleExists(roleName);
        }

        //Assign user to the specified role
        public void AssignRole(string userId, string roleName)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleName))
            {
                userManager.AddToRole(userId, roleName);
            }
            else
            {
                throw new NullReferenceException("userId and roleName can not be null");
            }
        }

        //Gives a list of user from the array of string containing userId.
        // returns list of users
        public List<ApplicationUser> GetAllUsersFromIds(string[] userIds)
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
        public ApplicationUser GetUserFromId(string userId)
        {
            ApplicationUser user = new ApplicationUser();
            if (!string.IsNullOrEmpty(userId))
            {
                user = db.Users.Find(userId);
            }
            return user;
        }

        //Get a user role from userManager for a specific user
        public string GetUserRole(string userId)
        {
            string userRole = userManager.GetRoles(userId).ToList().First();
            return userRole;
        }

        //Gives a list of user who have specified role.
        //To get all user in particular role
        public List<ApplicationUser> GetUsersFromRole(string roleName)
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

        //Administrator and Project Manager must be able to Assign user to project
        public bool AssignUserInRole(string userId, string role)
        {
            var user = userManager.FindById(userId);
            if (user == null)
            {
                return false;
            }
            if (userManager.IsInRole(userId, role))
            {
                return true;
            }
            var result = userManager.AddToRole(userId, role).Succeeded;
            return result;
        }

        //Administrator and projectManager must be able to unAssign user to project
        public bool unAssignUserToRole(string userId, string role)
        {
            var user = userManager.FindById(userId);
            if (user == null)
            {
                return false;
            }
            if (!userManager.IsInRole(userId, role))
            {
                return true;
            }
            var result = userManager.RemoveFromRole(userId, role).Succeeded;
            return result;
        }
    }
}
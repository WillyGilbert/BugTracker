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
        //Check if specified roleName exist or not
        public bool CheckRole(string roleName)
        {
            return roleManager.RoleExists(roleName);
        }
        //Assign user to the specified role
        public void AssignRole(string userId, string roleName)
        {
            if(!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleName))
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
        public List<ApplicationUser>GetAllUsersFromIds(string[] userIds)
        {
            var users = new List<ApplicationUser>();
            if(userIds.Length != 0)
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
        public List<ApplicationUser>GetUsersFromRole(string roleName)
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
    }
}
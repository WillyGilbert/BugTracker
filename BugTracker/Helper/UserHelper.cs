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
    }
}
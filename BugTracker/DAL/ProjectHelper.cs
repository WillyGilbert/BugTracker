using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public static class ProjectHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        public static List<Project> GetProjects()
        {

            var Project = db.Projects;
            return Project.ToList();
        }

        public static List<ApplicationUser> UsersOfTheProject(int projectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            // zvar projects = db.Users.Find(projectId).ProjectUsers();
            var project = db.Projects.Find(projectId);
            var users = db.Users.Where(u => u.ProjectUsers.Any(pu => pu.ProjectId == projectId)).ToList();
            return users;
        }

        public static List<ApplicationUser> UsersOutOfTheProject(int projectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var users = db.Users.Where(u => !u.ProjectUsers.Any(pu => pu.ProjectId == projectId)).ToList();
            return users;
        }

        public static Project GetProject(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project Project = db.Projects.Find(Id);
            
            if (Project == null)
            {
                return null;
            }
            db.Dispose();
            return Project;
        }
        public static bool AssignUserToProject(string userId, int projectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = UserHelper.GetUserFromId(userId);
            if (user == null)
            {
                return false;
            }

            var project = GetProject(projectId);
            if (project == null)
            {
                return false;
            }

            if (db.ProjectUsers.Any(p => p.UserId == userId && p.ProjectId == projectId))
            {
                return false;
            }

            ProjectUser projectUser = new ProjectUser { UserId = userId, ProjectId = projectId };
            db.ProjectUsers.Add(projectUser);
            db.SaveChanges();
            db.Dispose();

            return true;
        }
        public static bool RemoveUserFromProject(string userId, int projectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = UserHelper.GetUserFromId(userId);
            if (user == null)
            {
                return false;
            }
            var project = GetProject(projectId);
            if (project == null)
            {
                return false;
            }
            ProjectUser projectUser = db.ProjectUsers.FirstOrDefault(p => p.UserId == userId && p.ProjectId == projectId);
            if(projectUser == null)
            {
                return false;
            }
            db.ProjectUsers.Remove(projectUser);
            db.SaveChanges();
            db.Dispose();
            return true;
        }




        public static void Create(string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project Project = new Project
            {
                Name = name
            };
            db.Projects.Add(Project);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int id, string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project Project = GetProject(id);
            Project.Name = name;
            db.Entry(Project).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project Project = GetProject(id);
            db.Projects.Remove(Project);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
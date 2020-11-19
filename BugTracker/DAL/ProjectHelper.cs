﻿using BugTracker.Models;
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
            ApplicationDbContext db = new ApplicationDbContext();
            var Project = db.Projects;
            return Project.ToList();
        }

        public static List<Project> GetMyProjects(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var Project = db.Projects.Where(p => p.ProjectUsers.Any(pu => pu.UserId == userId));
            return Project.ToList();
        }

        //sort tickets by title
        public static List<Project> SortTicketsByTitle(List<Project> projects)
        {
            projects.ForEach(p =>
            {
                p.Tickets = p.Tickets.OrderBy(t => t.Title).ToList();
            });

            db.Dispose();
            return projects;            
        }

        //sort tickets by creation date
        public static List<Project> SortTicketsByDate(List<Project> projects)
        {
            projects.ForEach(p =>
            {
                p.Tickets = p.Tickets.OrderByDescending(t => t.Created).ToList();
            });

            db.Dispose();
            return projects;
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
            Project project = db.Projects.Find(Id);

            if (project == null)
            {
                return null;
            }
            db.Dispose();
            return project;
        }
        public static ProjectUser GetProjectUser(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProjectUser projectUser = db.ProjectUsers.FirstOrDefault(pu=> pu.ProjectId == Id);
            
            if (projectUser == null)
            {
                return null;
            }
            db.Dispose();
            return projectUser;
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
            var projectToDelete = db.Projects.Find(id);
            if(projectToDelete != null)
            {
                db.Projects.Remove(projectToDelete);
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}
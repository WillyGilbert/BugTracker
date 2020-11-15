using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class ProjectHelper
    {
        public static List<Project> GetProjects()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var Project = db.Projects;
            return Project.ToList();
        }

        public static Project GetProject(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project Project = db.Projects.Find(Id);
            db.Dispose();
            if (Project == null)
            {
                return null;
            }
            db.Dispose();
            return Project;
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
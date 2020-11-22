using BugTracker.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.DAL
{
    public class ProjectUserHelper
    {
        public static List<ProjectUser> GetProjectUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var ProjectUser = db.ProjectUsers.Include(p => p.Project).Include(p => p.User);
            return ProjectUser.ToList();
        }
        public static ProjectUser GetProjectUser(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProjectUser ProjectUser = db.ProjectUsers.Find(Id);
            db.Dispose();
            if (ProjectUser == null)
            {
                return null;
            }
            db.Dispose();
            return ProjectUser;
        }
        public static void Create(int projectId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProjectUser ProjectUser = new ProjectUser
            {
                ProjectId = projectId,
                UserId = userId
            };
            db.ProjectUsers.Add(ProjectUser);
            db.SaveChanges();
            db.Dispose();
        }
        public static void Edit(int id, int projectId, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProjectUser ProjectUser = GetProjectUser(id);
            ProjectUser.ProjectId = projectId;
            ProjectUser.UserId = userId;
            db.Entry(ProjectUser).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }
        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProjectUser ProjectUser = GetProjectUser(id);
            db.ProjectUsers.Remove(ProjectUser);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
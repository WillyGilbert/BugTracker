using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProjectUsers> ProjectUsers { get; set; }
    }
}
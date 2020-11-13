using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class ProjectUsers
    {
        public int Id { get; set; }
        public Projects Project { get; set; }
        public int ProjectsId { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class FilterViewModel
    {
        public List<string> Options { get; set; }
        public FilterViewModel()
        {
            Options = new List<string>() { "Creation Date", "Type", "Priority", "Status" };
        }
    }
}
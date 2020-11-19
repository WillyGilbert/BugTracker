using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class SortViewModel
    {
        public List<string> Options { get; set; }
        public SortViewModel()
        {
            Options = new List<string>() { "Creation Date", "Title" };
        }
    }
}
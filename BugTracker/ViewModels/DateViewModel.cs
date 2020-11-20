using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class DateViewModel
    {
        public DateTime Date { get; set; }
        public string DisplayedDate
        {
            get
            {
                return Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
    }
}
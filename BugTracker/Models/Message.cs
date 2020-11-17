using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Message
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public MessageType Code { get; set; }
    }

    public enum MessageType
    {
        Successful,
        NoFileSelected,
        ERROR,
        EmptyFile
    }
}
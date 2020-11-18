using BugTracker.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Notification = TicketNotificationHelper.CountUserNotifications(User.Identity.GetUserId());
            ////ViewBag.NotificationToManager = TicketNotificationHelper.CountManagerNotifications(User.Identity.GetUserId());
            //@ViewBag.Unread = TicketNotificationHelper.CountUnopenedUserNotifications(User.Identity.GetUserId());
            ////@ViewBag.UnreadManager = TicketNotificationHelper.CountUnopenedManagerNotifications(User.Identity.GetUserId());
            //TicketNotificationHelper.SetNotificationsByType();
            //ProjectHelper.SetProjectCompleted();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
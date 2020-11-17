using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BugTracker.DAL
{
    public class EmailNotification
    {
        private readonly string SmtpHost = ConfigurationManager.AppSettings["SmtpHost"];
        private readonly int SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
        private readonly string SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
        private readonly string SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
        private readonly string SmtpFrom = ConfigurationManager.AppSettings["SmtpFrom"];

        public void Send(string receiver, string subject, string body)
        {
            MailMessage message = new MailMessage(SmtpFrom, receiver)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            SmtpClient smtpClient = new SmtpClient(SmtpHost, SmtpPort)
            {
                Credentials = new NetworkCredential(SmtpUsername, SmtpPassword),

                EnableSsl = true
            };

            smtpClient.Send(message);
        }

    }




    public static class NotificationService
    {
        public static void Create(Ticket ticket)
        {
            TicketNotification notification = new TicketNotification
            {

                Ticket = ticket,
                TicketId = ticket.Id,




            };
            SendNotification(ticket.AssignedToUser, notification);

        }

        public static void SendNotification(ApplicationUser user, TicketNotification notification)
        {
            EmailNotification emailservice = new EmailNotification();
            emailservice.Send(user.Email, "BugTracker Notification" + notification.Ticket.Title + "in Project" + notification.Ticket.Project.Name, notification.Ticket.Description);
        }


        //public static void SendEmail(Ticket ticket, ApplicationUser user, string message)
        //{

        //    var emailService = new EmailNotification();
        //    var subject = $"Change in your Ticket -{ticket.Id}";
        //    var body = $"Ticket:{ticket.Title},{message}";

        //    emailService.Send(user.Email, subject, body);
        //}



    }
}
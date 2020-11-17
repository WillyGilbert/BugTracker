using System;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;

namespace BugTracker.Models
{
    public static class FileUploaderHelper
    {
        public static Message Uploader(int ticketId, HttpPostedFileBase file)
        {
            Message message = new Message();
            if (file != null && file.ContentLength > 0)
                try
                {
                    int accumulator = 0;
                    string path = Path.Combine(HttpContext.Current.Server.MapPath("~/root"), 
                        Path.GetFileName(ticketId + "_" + file.FileName));
                    while (File.Exists(path))
                    {
                        var NameOfFile = ticketId + "_" + file.FileName.Substring(0, file.FileName.LastIndexOf("."));
                        string NewFileName = NameOfFile + "_" + accumulator++ + file.FileName.Substring(file.FileName.LastIndexOf("."));
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/root"), 
                            Path.GetFileName(NewFileName));
                    }

                    if (!File.Exists(path)) file.SaveAs(path);

                    message.FileName = path;
                    message.Description = "File uploaded successfully";
                    message.Code = MessageType.Successful;
                    return message;
                }
                catch (Exception ex)
                {
                    message.FileName = file.FileName;
                    message.Description = "ERROR:" + ex.Message.ToString();
                    message.Code = MessageType.ERROR;
                    return message;
                }
            else
            {
                message.Description = "You have not specified a file.";
                message.Code = MessageType.NoFileSelected;
                return message;
            }
        }
        public static string Delete(string file)
        {
            var path = "~/root/" + file;
            if (File.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                try
                {
                    File.Delete(HttpContext.Current.Server.MapPath(path));
                    return "File deleted successfully";
                }
                catch (System.IO.IOException ex)
                {
                    return "ERROR:" + ex.Message.ToString();
                }
            }
            return "The file doesn't exist";
        }
        public static void DeleteAllFilesPerTicket(int ticketId)
        {
            System.IO.DirectoryInfo directory = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/root"));

            foreach (FileInfo file in directory.GetFiles())
            {
                if (Int32.Parse(file.Name.Substring(0, file.Name.IndexOf("_"))) == ticketId)
                {
                    file.Delete();
                }
            }
        }
        public static string Download(string file)
        {
            return "~/root/" + file;
        }

        public static List<string> GetFiles()
        {
            var dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/root"));
            FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name.Substring(file.Name.IndexOf("_") + 1));
            }
            return items;
        }
    }
}
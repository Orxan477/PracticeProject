using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace PracticeProject.Utilities
{
    public static class Helper
    {
        public static void RemoveFile(string root,string folder, string file)
        {
            string image = Path.Combine(root, folder, file);
            if (File.Exists(image))
            {
                File.Delete(image);
            }
        }
    }
    public static class Email
    {
        public static void SendEmailAsync(string fromMail,string password,string sendMail,string body,string subject)
        {
            using(var client=new SmtpClient("smtp.googlemail.com", 587))
            {
                client.Credentials = new NetworkCredential(fromMail, password);
                client.EnableSsl = true;
                var message = new MailMessage(fromMail, sendMail);
                message.Body = body;
                message.Subject = subject;
                message.IsBodyHtml = true;
                client.Send(message);
            }
        }
    }
    public enum UserRoles
    {
        Admin,
        Member
    } 
}

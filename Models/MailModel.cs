using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Web.Models
{
    public class MailModel
    {
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public void SendMail()
        {
            to = "leoshop993@gmail.com";
            MailMessage mail = new MailMessage(System.Configuration.ConfigurationManager.AppSettings["Email"].ToString(), to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Timeout = 1000000;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential nc = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Email"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Password"].ToString());
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mail);
        }
    }
}
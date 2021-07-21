using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SendMailController : Controller
    {
        
        // GET: SendMail
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sendmail(Web.Models.MailModel objMail)
        {
            if(ModelState.IsValid)
            {
                string from = "minhdao933@gmail.com";
                using(MailMessage mail = new MailMessage(from,objMail.to))
                {
                    mail.Subject = objMail.subject;
                    mail.Body = objMail.body;
                    mail.From = new MailAddress(objMail.from);
                    mail.IsBodyHtml = false;
                    using(SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential networkCredentail = new NetworkCredential(from, "minhtue139");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredentail;
                        smtp.Port = 587;
                        smtp.Send(mail);
                        ViewBag.Message = "Sent";
                    }
                    return View("Contact", objMail);
                }    
            }
            else
            {
                return View();
            }
        }
    }
}
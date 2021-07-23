using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web.Models;

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
        public ActionResult Contact(MailModel mail)
        {
            mail.SendMail();
            return View();
        }
    }
}
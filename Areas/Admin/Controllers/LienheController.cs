using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public class LienheController : Controller
    {
        // GET: Admin/Lienhe
        public ActionResult Lienhe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Lienhe(SendMailKH mail)
        {
            mail.SendMail();
            return View();
        }
    }
}
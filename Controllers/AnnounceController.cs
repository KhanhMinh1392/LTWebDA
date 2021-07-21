using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AnnounceController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Announce
        public ActionResult SoluongQT()
        {
            return View();
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}
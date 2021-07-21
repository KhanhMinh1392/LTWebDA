using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class TimKiemController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: TimKiem
        [HttpGet]
        public ActionResult KQTim(string sTukhoa)
        {
            var lstSP = db.Products.Where(n => n.TENSP.Contains(sTukhoa));
            return View(lstSP.OrderBy(n => n.TENSP));
        }

        public ActionResult KQTimkiemRS(string sTen)
        {
            var lstRS = db.Restocks.Where(x => x.TENSP.Contains(sTen));
            return View(lstRS.OrderBy(x => x.TENSP));
        }
    }
}
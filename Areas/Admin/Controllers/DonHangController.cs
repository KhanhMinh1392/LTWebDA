using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Admin/DonHang
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            ChiTietDonDatHang pr = db.ChiTietDonDatHangs.SingleOrDefault(n => n.DONDATHANG_ID == id);
            if (pr == null)
            {
                return HttpNotFound();
            }
            return View(pr);
        }
    }
}
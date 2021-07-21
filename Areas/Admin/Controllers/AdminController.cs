using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using PagedList;

namespace Web.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Customer(int? page)
        {
            var listKhachhang = new List<KhachHang>();
            listKhachhang = db.KhachHangs.ToList();
            int pagesize = 5;
            int pagenumber = (page ?? 1);
            return View(listKhachhang.ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Donhang()
        {
            return View();
        }
        public ActionResult Sanpham(int? page)
        {
            var listsanpham = new List<Product>();
            listsanpham = db.Products.ToList();
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            return View(listsanpham.ToPagedList(pagenumber,pagesize));
        }
        public ActionResult Staff()
        {
            var lstStaff = new List<NhanVien>();
            lstStaff = db.NhanViens.ToList();
            return View(lstStaff);
        }
    }
}
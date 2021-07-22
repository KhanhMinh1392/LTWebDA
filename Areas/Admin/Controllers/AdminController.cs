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
        public double ThongkeDH()
        {
            double ddh = db.DonDatHangs.Count();
            return ddh;
        }
        public double ThongkeKH()
        {
            double tkkh = db.KhachHangs.Count();
            return tkkh;
        }
        public double ThongkeNV()
        {
            double nv = db.NhanViens.Count();
            return nv;
        }
        public double ThongkeSP()
        {
            double pr = db.Products.Count();
            return pr;
        }
        public decimal ThongKeDoanhThu()
        {
            decimal TongDT = decimal.Parse(db.ChiTietDonDatHangs.Sum(n => n.SOLUONG * n.GIA).ToString());
            return TongDT;
        }
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("Login", "LoginAdmin");
            }
            ViewBag.Songuoitruycap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.Songuoidangonl = HttpContext.Application["SoNguoiDangOnl"].ToString();
            ViewBag.Tongdonghang = ThongkeDH();
            ViewBag.Tongkhachhang = ThongkeKH();
            ViewBag.Tongnhanvien = ThongkeNV();
            ViewBag.Tongsanpham = ThongkeSP();
            ViewBag.TongDT = ThongKeDoanhThu();
            return View();
        }
        public ActionResult Customer(int? page)
        {
            var listKhachhang = new List<KhachHang>();
            listKhachhang = db.KhachHangs.ToList();
            int pagesize = 5;
            int pagenumber = (page ?? 1);
            //============================
            ViewBag.Tongnhanvien = ThongkeNV();
            ViewBag.Tongkhachhang = ThongkeKH();
            ViewBag.Songuoitruycap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.Songuoidangonl = HttpContext.Application["SoNguoiDangOnl"].ToString();
            return View(listKhachhang.ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Donhang(int? page)
        {
            var lstDH = new List<DonDatHang>();
            lstDH = db.DonDatHangs.ToList();
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            //============================
            ViewBag.Tongnhanvien = ThongkeNV();
            ViewBag.Tongkhachhang = ThongkeKH();
            ViewBag.Songuoitruycap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.Songuoidangonl = HttpContext.Application["SoNguoiDangOnl"].ToString();
            return View(lstDH.ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Sanpham(int? page)
        {
            var listsanpham = new List<Product>();
            listsanpham = db.Products.ToList();
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            //============================
            ViewBag.Tongnhanvien = ThongkeNV();
            ViewBag.Tongkhachhang = ThongkeKH();
            ViewBag.Songuoitruycap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.Songuoidangonl = HttpContext.Application["SoNguoiDangOnl"].ToString();
            return View(listsanpham.ToPagedList(pagenumber,pagesize));
        }
        public ActionResult Staff(int? page)
        {
            var lstStaff = new List<NhanVien>();
            lstStaff = db.NhanViens.ToList();
            int pagesize = 6;
            int pagenumber = (page ?? 1);
            //============================
            ViewBag.Tongnhanvien = ThongkeNV();
            ViewBag.Tongkhachhang = ThongkeKH();
            ViewBag.Songuoitruycap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.Songuoidangonl = HttpContext.Application["SoNguoiDangOnl"].ToString();
            return View(lstStaff.ToPagedList(pagenumber, pagesize));
        }
    }
}
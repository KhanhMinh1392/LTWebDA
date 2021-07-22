using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Order
        public List<Cart> LayGioHang()
        {
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<Cart>();
                Session["Cart"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult DatHang(KhachHang kh, string ghichu, string diachi)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang khachang = new KhachHang();
            if (Session["username"] == null)
            {
                khachang = kh;
                db.KhachHangs.Add(khachang);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("XemGioHang");
            }
            DonDatHang ddh = new DonDatHang();
            ddh.NGAYLAPDON = DateTime.Now;
            ddh.KHACHHANG_ID = int.Parse(khachang.KHACHHANG_ID.ToString());
            ddh.DATHANHTOAN = false;
            ddh.DAHUY = false;
            ddh.DAXOA = false;
            ddh.GHICHU = ghichu;
            ddh.DIACHI = diachi;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            List<Cart> lstGH = LayGioHang();
            foreach (var item in lstGH)
            {
                ChiTietDonDatHang cthd = new ChiTietDonDatHang();
                cthd.DONDATHANG_ID = ddh.DONDATHANG_ID;
                cthd.PRODUCT_ID = item.PRODUCT_ID;
                cthd.TENSP = item.TENSP;
                cthd.SOLUONG = item.sl;
                cthd.GIA = (double)item.GIA;
                cthd.THANHTIEN = int.Parse(item.ThanhTien.ToString());
                db.ChiTietDonDatHangs.Add(cthd);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("Success","Announce");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
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
        public ActionResult ThemGioHang(int id, string strURL)
        {
            Product sp = db.Products.SingleOrDefault(n => n.PRODUCT_ID == id);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGioHang = LayGioHang();
            Cart spCheck = lstGioHang.SingleOrDefault(n => n.PRODUCT_ID == id);
            if (spCheck != null)
            {
                spCheck.sl++;
                spCheck.ThanhTien = spCheck.sl * spCheck.GIA;
                return Redirect(strURL);
            }
            Cart itemGH = new Cart(id);
            lstGioHang.Add(itemGH);
            return Redirect(strURL);
        }
        public double TinhTongSoLuong()
        {
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.sl);
        }
        public decimal TinhTongTien()
        {
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }
        public ActionResult Cart()
        {
            List<Cart> lstGiohang = LayGioHang();

            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return View(lstGiohang);
        }
        public ActionResult CartPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();

            return PartialView();
        }
        //xóa giỏ hàng
        public ActionResult XoaGioHang(int? id)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product pr =db.Products.SingleOrDefault(n => n.PRODUCT_ID == id);

            if (pr == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGioHang = LayGioHang();
            Cart spCheck = lstGioHang.SingleOrDefault(n => n.PRODUCT_ID == id);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstGioHang.Remove(spCheck);
            return RedirectToAction("Cart","Cart");
        }
        // sửa giỏ hàng
        public ActionResult EditGioHang(int id)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product pr = db.Products.SingleOrDefault(n => n.PRODUCT_ID == id);
            if(pr==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGH = LayGioHang();
            Cart spCheck = lstGH.SingleOrDefault(n => n.PRODUCT_ID==id);
            if(spCheck == null)
            {
                return RedirectToAction("Index","Home");
            }
            ViewBag.GioHang = lstGH;
            return View(spCheck);
        }
        [HttpPost]
        public ActionResult UpdateGH(Cart itemGh)
        {
            Product spcheck = db.Products.Single(n => n.PRODUCT_ID == itemGh.PRODUCT_ID);
            if (spcheck.SL > itemGh.sl)
            {
                List<Cart> lstGH = LayGioHang();
                Cart update = lstGH.Find(n => n.PRODUCT_ID == itemGh.PRODUCT_ID);
                update.sl = itemGh.sl;
                update.ThanhTien = update.sl * update.GIA;
            }
            else
            {
                return RedirectToAction("SoluongQT", "Announce");
            }
            return RedirectToAction("Cart");
        }
        public ActionResult Check()
        {
            List<Cart> lstGiohang = LayGioHang();

            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return View(lstGiohang);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public ActionResult DatHang(KhachHang kh, string ghichu, string diachi,SendMailDonHang send)
        {
            KhachHang khachang = new KhachHang();
            if (Session["username"] == null)
            {
                khachang = kh;
                db.KhachHangs.Add(khachang);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Cart","Cart");
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
                ///////////////////////////////////////////////////
                send.tensp = cthd.TENSP;
                send.size = item.Size;
                send.sl = item.sl;
                send.total = int.Parse(item.ThanhTien.ToString());
                db.ChiTietDonDatHangs.Add(cthd);
            
            }
            db.SaveChanges();
            Session["Cart"] = null;
            //////////////////////Gửi Email Đơn Hàng//////////////////////////////////////
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Mail/Mailsend.html"));
            content = content.Replace("{{KHACHHANG}}", kh.TENKH);
            content = content.Replace("{{SDT}}", kh.SDT.ToString());
            content = content.Replace("{{TENSP}}", send.tensp);
            content = content.Replace("{{SIZE}}", send.size);
            content = content.Replace("{{SOLUONG}}", send.sl.ToString());
            content = content.Replace("{{ThanhTien}}", send.total.ToString("#,##"));


            send.to = kh.EMAIL;
            MailMessage mail = new MailMessage(System.Configuration.ConfigurationManager.AppSettings["Email"].ToString(), send.to);
            mail.Subject = "Mua hàng thành Công";
            mail.Body = content;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Timeout = 1000000;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential nc = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Email"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Password"].ToString());
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mail);
            return RedirectToAction("Success","Announce");
        }
    }
}
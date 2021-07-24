using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public class AddCustomerController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Admin/AddCustomer
        public ActionResult NewCus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCus(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(s => s.USERNAME == kh.USERNAME);
                if (check == null)
                {
                    kh.UPASSWORD = GetMD5(kh.UPASSWORD);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                    return RedirectToAction("Customer","Admin");
                }
                else
                {
                    ViewBag.error = "User already exists";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            KhachHang pr = db.KhachHangs.SingleOrDefault(n => n.KHACHHANG_ID == id);
            if (pr == null)
            {
                return HttpNotFound();
            }
            return View(pr);
        }
        [HttpPost]
        public ActionResult Edit(KhachHang model, int id)
        {
            KhachHang x = db.KhachHangs.First(m => m.KHACHHANG_ID.CompareTo(id) == 0);
            x.TENKH = model.TENKH;
            x.SDT = model.SDT;
            x.EMAIL = model.EMAIL;
            x.DIACHI = model.DIACHI;
            x.USERNAME = model.USERNAME;
            x.UPASSWORD = GetMD5(model.UPASSWORD.ToString());
            db.SaveChanges();

            return RedirectToAction("Customer", "Admin");
        }
        public ActionResult Delete(int id)
        {
            KhachHang pr = db.KhachHangs.SingleOrDefault(n => n.KHACHHANG_ID == id);
            if (pr == null)
            {
                return HttpNotFound();
            }
            return View(pr);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang model = db.KhachHangs.SingleOrDefault(n => n.KHACHHANG_ID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.KhachHangs.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Customer", "Admin");
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}
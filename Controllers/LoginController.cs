using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Login
        //[HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string USERNAME, string UPASSWORD,Login login)
        {
            if (ModelState.IsValid)
            {
                string f_password = GetMD5(UPASSWORD);
                var data = db.KhachHangs.Where(s => s.USERNAME.Equals(USERNAME) && s.UPASSWORD.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    Session["Email"] = data.FirstOrDefault().EMAIL;
                    Session["username"] = data.FirstOrDefault().USERNAME;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Resigter()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resigter(KhachHang _khachhang,Account acc)
        {
            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(s => s.USERNAME == _khachhang.USERNAME);
                if (check == null)
                {
                    _khachhang.UPASSWORD = GetMD5(_khachhang.UPASSWORD);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(_khachhang);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "User already exists";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
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
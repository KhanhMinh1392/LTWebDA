using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        public QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Admin/LoginAdmin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string ADUSERNAME, string ADPASSWORD)
        {
            if (ModelState.IsValid)
            {
                string f_password = GetMD5(ADPASSWORD);
                var data = db.NhanViens.Where(s => s.ADUSERNAME.Equals(ADUSERNAME) && s.ADPASSWORD.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    Session["Email"] = data.FirstOrDefault().EMAIL;
                    Session["username"] = data.FirstOrDefault().ADUSERNAME;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
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
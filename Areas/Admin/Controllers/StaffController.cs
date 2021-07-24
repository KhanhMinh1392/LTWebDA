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
    public class StaffController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Admin/Staff
        public ActionResult Createstaff()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Createstaff(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                var check = db.NhanViens.FirstOrDefault(s => s.ADUSERNAME == nv.ADUSERNAME);
                if (check == null)
                {
                    nv.ADPASSWORD = GetMD5(nv.ADPASSWORD);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.NhanViens.Add(nv);
                    db.SaveChanges();
                    return RedirectToAction("Staff", "Admin");
                }
                else
                {
                    ViewBag.error = "Nhan Vien da ton tai";
                    return View();
                }
            }
            return View();
        }
        //edit
        public ActionResult EditStaff(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            NhanVien pr = db.NhanViens.SingleOrDefault(n => n.NHANVIEN_ID == id);
            if (pr == null)
            {
                return HttpNotFound();
            }
            return View(pr);
        }
        [HttpPost]
        public ActionResult EditStaff(NhanVien model, int id)
        {
            NhanVien x = db.NhanViens.First(m => m.NHANVIEN_ID.CompareTo(id) == 0);
            x.TENNV = model.TENNV;
            x.SDT = model.SDT;
            x.EMAIL = model.EMAIL;
            x.DIACHI = model.DIACHI;
            x.NGAYSINH = model.NGAYSINH;
            x.ADUSERNAME = model.ADUSERNAME;
            x.ADPASSWORD = GetMD5(model.ADPASSWORD.ToString());
            db.SaveChanges();

            return RedirectToAction("Staff", "Admin",model);
        }
        //detele
        public ActionResult Delete(int id)
        {
            NhanVien pr = db.NhanViens.SingleOrDefault(n => n.NHANVIEN_ID == id);
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
            NhanVien model = db.NhanViens.SingleOrDefault(n => n.NHANVIEN_ID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.NhanViens.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Staff", "Admin");
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
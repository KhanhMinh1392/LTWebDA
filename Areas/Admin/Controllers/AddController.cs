using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public class AddController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Admin/Adđ
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product pr, HttpPostedFileBase hinhanh)
        {
            ViewBag.PRODUCT_ID = new SelectList(db.CategoryCons.OrderBy(n => n.CATEGORYCON_ID), "CATEGORYCON_ID", "TENLOAISP");
            if (hinhanh.ContentLength > 0)
            {
                var filename = Path.GetFileName(hinhanh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/img/it_service"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    hinhanh.SaveAs(path);
                    pr.HINHANH = filename;
                }
            }
            db.Products.Add(pr);
            db.SaveChanges();
            return RedirectToAction("Sanpham", "Admin");
        }
        public ActionResult CreateBill()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                Response.StatusCode = 404;
            }
            Product pr = db.Products.SingleOrDefault(n => n.PRODUCT_ID == id);
            if(pr==null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORY_ID = new SelectList(db.CategoryCons.OrderBy(n => n.CATEGORYCON_ID), "CATEGORYCON_ID", "TENLOAISP",pr.PRODUCT_ID);
            return View(pr);
        }
        [HttpPost]
        public ActionResult Edit(Product model, HttpPostedFileBase hinhanh)
        {
            if (hinhanh.ContentLength > 0)
            {
                var fileName = Path.GetFileName(hinhanh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/img/it_service"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    hinhanh.SaveAs(path);
                    model.HINHANH = fileName;
                }
                if (ModelState.IsValid)
                {
                    ViewBag.PRODUCT_ID = new SelectList(db.CategoryCons.OrderBy(n => n.CATEGORYCON_ID), "CATEGORYCON_ID", "TENLOAISP", model.PRODUCT_ID);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Sanpham", "Admin");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            Product pr = db.Products.SingleOrDefault(n => n.PRODUCT_ID == id);
            if(pr == null)
            {
                return HttpNotFound();
            }
            ViewBag.PRODUCT_ID = new SelectList(db.CategoryCons.OrderBy(n => n.CATEGORYCON_ID), "CATEGORYCON_ID", "TENLOAISP", pr.PRODUCT_ID);
            return View(pr);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product model = db.Products.SingleOrDefault(n => n.PRODUCT_ID == id);
            if(model == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Sanpham", "Admin");
        }
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
                    ViewBag.error = "Staff already exists";
                    return View();
                }
            }
            return View();
        }
        //detele
        
  
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
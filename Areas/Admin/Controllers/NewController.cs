using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public class NewController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        public double ThongkeNV()
        {
            double nv = db.NhanViens.Count();
            return nv;
        }
        public double ThongkeKH()
        {
            double tkkh = db.KhachHangs.Count();
            return tkkh;
        }
        // GET: Admin/New
        public ActionResult News(int? page)
        {
            var listnew = new List<News>();
            listnew = db.News.ToList();
            int pagesize = 4;
            int pagenumber = (page ?? 1);
            ViewBag.Tongnhanvien = ThongkeNV();
            ViewBag.Tongkhachhang = ThongkeKH();
            ViewBag.Songuoitruycap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.Songuoidangonl = HttpContext.Application["SoNguoiDangOnl"].ToString();
            return View(listnew.ToPagedList(pagenumber, pagesize));
        }
        //create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(News nw, HttpPostedFileBase hinhanh)
        {
            if (hinhanh.ContentLength > 0)
            {
                var filename = Path.GetFileName(hinhanh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/img/img_news"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    hinhanh.SaveAs(path);
                    nw.HINHANH = filename;
                }
            }
            db.News.Add(nw);
            db.SaveChanges();
            return RedirectToAction("News", "New");
        }
        //edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            News pr = db.News.SingleOrDefault(n => n.NEWS_ID == id);
            if (pr == null)
            {
                return HttpNotFound();
            }
            return View(pr);
        }
        [HttpPost]
        public ActionResult Edit(News model, HttpPostedFileBase hinhanh)
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
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("News", "New");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            News pr = db.News.SingleOrDefault(n => n.NEWS_ID == id);
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
            News model = db.News.SingleOrDefault(n => n.NEWS_ID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.News.Remove(model);
            db.SaveChanges();
            return RedirectToAction("News", "New");
        }
    }
}
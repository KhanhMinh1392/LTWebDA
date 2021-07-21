using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using PagedList;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        QLWebBanHangEntities1 db = new QLWebBanHangEntities1();
        // GET: Home
        public ActionResult Index()
        {
            Sanpham sp = new Sanpham();
            sp.Listsanpham = db.Products.ToList();
            sp.Listloaisanpham = db.CategoryChas.ToList();
            return View(sp);
        }
        public ActionResult IndexDark()
        {
            return View();
        }
        public ActionResult Shop(int? page)
        {
            Sanpham sp = new Sanpham();
            sp.Listsanpham = db.Products.ToList();

            //phân trang
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            return View(sp.Listsanpham.ToPagedList(pagenumber,pagesize));
        }
        public ActionResult Restock(int? page)
        {
            Sanpham sp = new Sanpham();
            sp.Listrestock = db.Restocks.ToList();
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            return View(sp.Listrestock.ToPagedList(pagenumber,pagesize));
        }
        public ActionResult News(int? page)
        {
            var listnew = new List<News>();
            listnew = db.News.ToList();
            int pagesize = 3;
            int pagenumber = (page ?? 1);
            return View(listnew.ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Contact1()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Detail(int Id)
        {
            var dbsanpham = db.Products.Where(n => n.PRODUCT_ID == Id).FirstOrDefault();
            var listsanpham = db.Products.ToList();

            Sanpham _sanpham = new Sanpham();
            _sanpham.dbsanpham = dbsanpham;
            _sanpham.Listsanpham = db.Products.ToList();
            return View(_sanpham);
        }
        public ActionResult DetailRS(int Id)
        {
            var dbsanphamrs = db.Restocks.Where(n => n.RESTOCKS_ID == Id).FirstOrDefault();
            var listsanpham = db.Restocks.ToList();

            Sanpham _restock = new Sanpham();
            _restock.dbrestock = dbsanphamrs;
            _restock.Listrestock = db.Restocks.ToList();
            return View(_restock);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Cart
    {
        public int PRODUCT_ID { get; set; }
        public string TENSP { get; set; }
        public string Size { get; set; }
        public int sl { get; set; }
        public decimal ThanhTien { get; set; }
        public decimal GIA { get; set; }
        public string HINHANH { get; set; }
        public Cart(int imasp)
        {
            using (QLWebBanHangEntities1 db = new QLWebBanHangEntities1())
            {
                this.PRODUCT_ID = imasp;
                Product pr = db.Products.Single(n => n.PRODUCT_ID == imasp);
                this.TENSP = pr.TENSP;
                this.Size = pr.Size.TENSIZE;
                this.HINHANH = pr.HINHANH;
                this.GIA = (decimal)pr.GIA;
                this.sl = 1;
                this.ThanhTien = GIA *sl;
            }
        }
        public Cart(int imasp, int sl)
        {
            using (QLWebBanHangEntities1 db = new QLWebBanHangEntities1())
            {
                this.PRODUCT_ID = imasp;
                Product pr = db.Products.Single(n => n.PRODUCT_ID == imasp);
                this.TENSP = pr.TENSP;
                this.Size = pr.Size.TENSIZE;
                this.HINHANH = pr.HINHANH;
                this.GIA = (decimal)pr.GIA.Value;
                this.sl = sl;
                this.ThanhTien = GIA * sl;
            }
        }
        public Cart()
        {

        }
    }
}
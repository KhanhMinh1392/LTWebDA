using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Sanpham
    {
        public List<Product> Listsanpham { get; set; }
        public List<CategoryCha> Listloaisanpham { get; set; }
        public Product dbsanpham { get; set; }




        //////////////////////////////
        public List<Restock> Listrestock { get; set; }
        public List<CategoryCha> Listloairestock { get; set; }
        public Restock dbrestock { get; set; }
    }
}
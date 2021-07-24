using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Web.Models
{
    public class SendMailDonHang
    {
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string tensp { get; set; }
        public int sl { get; set; }
        public string size { get; set; }
        public decimal total { get; set; }
    }
}
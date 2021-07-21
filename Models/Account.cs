using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Account
    {
        [Required(ErrorMessage ="* nhập username")]
        public string USERNAME { get; set; }

        [Required(ErrorMessage = "* nhập password")]
        public string UPASSWORD { get; set; }

        [Required(ErrorMessage = "* nhập Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "* nhập họ và tên")]
        public string TenKH { get; set; }

        [Required(ErrorMessage = "* nhập SĐT")]
        [Phone]
        [StringLength(10,ErrorMessage ="Số Điện Thoại chỉ gồm 10 số")]
        public string SDT { get; set; }
    }
    public class Login
    {
        [Required(ErrorMessage = "* nhập username")]
        public string USERNAME { get; set; }

        [Required(ErrorMessage = "* nhập password")]
        public string UPASSWORD { get; set; }
    }
}
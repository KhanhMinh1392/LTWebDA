//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Restock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restock()
        {
            this.GioHangs = new HashSet<GioHang>();
        }
    
        public int RESTOCKS_ID { get; set; }
        public Nullable<int> SIZE_ID { get; set; }
        public string TENSP { get; set; }
        public string HINHANH { get; set; }
        public string Mota { get; set; }
        public int SL { get; set; }
        public Nullable<double> GIA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual Size Size { get; set; }
    }
}

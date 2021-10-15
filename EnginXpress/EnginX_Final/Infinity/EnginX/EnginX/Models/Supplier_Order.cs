//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnginX.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplier_Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier_Order()
        {
            this.Supplier_Order_Line = new HashSet<Supplier_Order_Line>();
        }
    
        public int SupplierOrderID { get; set; }
        public int SupplierID { get; set; }
        public int AdminID { get; set; }
        public int SOStatusID { get; set; }
        public string SupplierOrderNumber { get; set; }
        public System.DateTime SupplierOrderDate { get; set; }
        public string SupplierOrderDescription { get; set; }
        public int SupplierOrderQuantity { get; set; }
        public string SupplierOrderNote { get; set; }
    
        public virtual Supplier Supplier { get; set; }
        public virtual Supplier_Order_Status Supplier_Order_Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier_Order_Line> Supplier_Order_Line { get; set; }
        public virtual Administrator Administrator { get; set; }
    }
}

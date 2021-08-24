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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Feedbacks = new HashSet<Feedback>();
            this.Order_Product = new HashSet<Order_Product>();
            this.Order_Status1 = new HashSet<Order_Status>();
        }
    
        public int OrderID { get; set; }
        public string Order_Number { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> OrderStatusID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> AddressID { get; set; }
        public Nullable<int> PaymentID { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Customer_Order Customer_Order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Product> Order_Product { get; set; }
        public virtual Order Order1 { get; set; }
        public virtual Order Order2 { get; set; }
        public virtual Order_Status Order_Status { get; set; }
        public virtual Payment Payment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Status> Order_Status1 { get; set; }
    }
}

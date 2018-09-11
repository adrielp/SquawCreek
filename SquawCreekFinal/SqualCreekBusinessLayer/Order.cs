//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqualCreekBusinessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.Guid> MemberID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<bool> Pickup { get; set; }
        public Nullable<bool> MailTo { get; set; }
        public System.Guid AddressId { get; set; }
        public string CustomersName { get; set; }
    
        public virtual UserAddress UserAddress { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class Cart
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid? MemberId { get; set; }
        public decimal TotalAmount { get; set; }
        public bool PickUp { get; set; }
        public bool MailTo { get; set; }

        public List<OrderLineItem> LineItems { get; set; }

        public string CustomersName { get; set; }

        public UserAddress Address { get; set; }

        public Guid OrderConfirmationId { get; set; }
       
    }
}

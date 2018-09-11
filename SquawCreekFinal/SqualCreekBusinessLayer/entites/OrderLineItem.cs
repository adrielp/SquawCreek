using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class OrderLineItem
    {
        public Guid Id { get; set; }
        public Guid OrderID {get;set;}
        public Guid ShopItemId { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }

        public string ItemTitle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqualCreekBusinessLayer.entites;
using SqualCreekBusinessLayer.infacing;

namespace SquawCreekFinal.Controllers
{
    public class ShopController : Controller
    {
        private ICommon m_commonBO = null;

        public ShopController(ICommon commonBO, IEvent eventBO)
        {
            m_commonBO = commonBO;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShoppingCart(PublicModel model)
        {
            Cart cart = new Cart();
            Guid OrderId = Guid.NewGuid();
            List<OrderLineItem> orderList = new List<OrderLineItem>();
            foreach (var item in model.ShopItems)
            {
                if (item.Qty > 0)
                {
                    OrderLineItem oli = new OrderLineItem();
                    oli.Id = Guid.NewGuid();
                    oli.Price = item.Price;
                    oli.Qty = item.Qty;
                    oli.ShopItemId = item.Id;
                    oli.OrderID = OrderId;
                    oli.ItemTitle = item.Title;
                    cart.TotalAmount += (decimal)item.Price;
                    orderList.Add(oli);
                }
            }
            cart.LineItems = orderList;
            return PartialView(cart);
        }

        public ActionResult DecidePayment(Cart cart)
        {
            if (cart.MailTo)
            {
                return View("CreditCardProcessing", cart);
            }
            else
            {
                CompleteOrder(cart);
                return View();
            }
        }

        public ActionResult CompleteOrder(Cart cart)
        {
            Cart final = new Cart();
            final = m_commonBO.SaveOrder(cart);
            return View("OrderComplete", final);
        }



    }
}
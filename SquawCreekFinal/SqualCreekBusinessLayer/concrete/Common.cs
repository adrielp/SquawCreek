using SqualCreekBusinessLayer.infacing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqualCreekBusinessLayer.entites;

namespace SqualCreekBusinessLayer.concrete
{
    public class Common : ICommon
    {
        public void AddRoleToMember(Guid userID)
        {
           
        }


        public void EmailAddition(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
                {
                    MailingList ml = new MailingList();
                    ml.Id = Guid.NewGuid();
                    ml.EmailAddress = email;
                    db.MailingLists.Add(ml);
                    db.SaveChanges();
                }
            }
        }


        public Cart SaveOrder(Cart cart)
        {

            Order order = new Order();
            UserAddress userAddress = new UserAddress();

            userAddress.Id = Guid.NewGuid();
            userAddress.Street = cart.Address.Street;
            userAddress.City = cart.Address.City;
            userAddress.State = cart.Address.State;
            userAddress.ZipCode = cart.Address.ZipCode;
            //there is no user id saved yet

            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                db.UserAddresses.Add(userAddress);
                db.SaveChanges();

                order.Id = Guid.NewGuid();
                cart.OrderConfirmationId = order.Id;
                order.Created = DateTime.Now;
                order.MemberID = null;
                order.Pickup = cart.PickUp;
                order.MailTo = cart.MailTo;
                order.AddressId = userAddress.Id;
                order.CustomersName = cart.CustomersName;

                db.Orders.Add(order);

                foreach (var li in cart.LineItems)
                {
                    OrderItem oi = new OrderItem();
                    oi.Id = Guid.NewGuid();
                    oi.OrderId = order.Id;
                    oi.ShopItemId = li.ShopItemId;
                    oi.Qty = li.Qty;
                    oi.Price = (decimal)li.Price;
                    db.OrderItems.Add(oi);
                    db.SaveChanges();
                }
            }

            return cart;

        }




    }
}

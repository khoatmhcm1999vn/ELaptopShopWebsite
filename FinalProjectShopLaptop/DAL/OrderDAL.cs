using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.DAL
{

    public class OrderDAL
    {

        private OrderDAL()
        {
            db = new ApplicationDbContext();
        }

        private static OrderDAL orderDAL;
        private ApplicationDbContext db;

        public static OrderDAL createOrderDAL()
        {
            if (orderDAL == null)
            {
                orderDAL = new OrderDAL();
            }
            return orderDAL;
        }

        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.Id;
        }

    }

}
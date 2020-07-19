using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.DAL
{
    public class OrderDetailDAL
    {

        private OrderDetailDAL()
        {
            db = new ApplicationDbContext();
        }

        private static OrderDetailDAL orderDetailDAL;
        private ApplicationDbContext db;

        public static OrderDetailDAL createOrderDetailDAL()
        {
            if (orderDetailDAL == null)
            {
                orderDetailDAL = new OrderDetailDAL();
            }
            return orderDetailDAL;
        }

        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

    }
}
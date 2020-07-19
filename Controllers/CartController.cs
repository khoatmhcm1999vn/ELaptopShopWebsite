using FinalProjectShopLaptop.DAL;
using FinalProjectShopLaptop.Helper;
using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FinalProjectShopLaptop.Controllers
{
    public class CartController : Controller
    {

        private const string CartSession = "CartSession";
        // GET: Cart

        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var sp = SanPhamDAL.createSanPhamDAL();
            ViewBag.SanPham = sp.getAllSanPham();
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }    
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.SanPham.Id == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(int? id, string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.SanPham.Id == item.SanPham.Id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var sp = SanPhamDAL.createSanPhamDAL();
            var product = sp.detailSanPham(productId);
            //var product = new SanPhamDAL().detailSanPham(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.SanPham.Id == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.SanPham.Id == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.SanPham = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.SanPham = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);

                //Gán vào session
                Session[CartSession] = list;

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipName = shipName;
            order.ShipEmail = email;

            try
            {
                var orderDAL = OrderDAL.createOrderDAL();
                var orderDetailDAL = OrderDetailDAL.createOrderDetailDAL();
                var id = orderDAL.Insert(order);
                //var id = new OrderDAL.Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                //var detailDao = new DAL.OrderDetailDAL();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductId = item.SanPham.Id;
                    orderDetail.OrderId = id;
                    orderDetail.Price = item.SanPham.Price;
                    orderDetail.Quantity = item.Quantity;
                    orderDetailDAL.Insert(orderDetail);

                    total += (item.SanPham.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = email;

                new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShopLaptop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShopLaptop", content);
            }
            catch (Exception ex)
            {
                //ghi log
                Console.WriteLine(ex.ToString());
                return RedirectToAction("Failed");
            }
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }

    }
}

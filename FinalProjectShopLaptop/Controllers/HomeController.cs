using FinalProjectShopLaptop.DAL;
using FinalProjectShopLaptop.Helper;
using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectShopLaptop.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
         {
            var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();
            var hang = HangDAL.createHangDAL();
            var sp = SanPhamDAL.createSanPhamDAL();
            ViewBag.NhomSP = nhomsp.getAllNhomSP();
            ViewBag.Hang = hang.getAllHang();
            ViewBag.SanPham = sp.getAllSanPham();
            return View(ViewBag);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

}

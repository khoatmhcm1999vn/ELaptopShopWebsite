using FinalProjectShopLaptop.DAL;
using FinalProjectShopLaptop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectShopLaptop.Controllers
{
    public class SanPhamController : Controller
    {

        ContextAddFavor Favor;

        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            var sp = SanPhamDAL.createSanPhamDAL();
            var model = sp.getAllSanPham();
            //return View(GetAllSanPham());
            return View(model);
        }

        //public List<SanPham> GetAllSanPham()
        //{
        //    //using (ApplicationDbContext db = new ApplicationDbContext())
        //    //{
        //    //    return db.SanPhams.ToList<SanPham>();
        //    //}
        //    //var sp = SanPhamDAL.createSanPhamDAL();
        //    //return sp.getAllSanPham();
        //    return db.SanPhams.ToList<SanPham>();
        //}

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();
            var cat = nhomsp.getAllNhomSP();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Ten });
            }
            return list;
        }

        public List<SelectListItem> GetHang()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var hang = HangDAL.createHangDAL();
            var han = hang.getAllHang();
            foreach (var item in han)
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Ten });
            }
            return list;
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            var sp = SanPhamDAL.createSanPhamDAL();
            SanPham sp1 = new SanPham();
            ViewBag.CategoryList = GetCategory();
            ViewBag.HangList = GetHang();
            if (id != 0)
            {
                //using (ApplicationDbContext db = new ApplicationDbContext())
                //{
                //    sp = db.SanPhams.Where(x => x.Id == id).FirstOrDefault<SanPham>();
                //}
                sp1 = sp.getSanPhamByProductId(id);
                //var a = spx.getSanPhamByProductId(id);

            }
            return View(sp1);
        }

        [HttpPost]
        public ActionResult AddOrEdit(SanPham sanPham)
        {
            var sp = SanPhamDAL.createSanPhamDAL();
            try
            {
                if (sanPham.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sanPham.ImageUpload.FileName);
                    string extension = Path.GetExtension(sanPham.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    sanPham.ImagePath = "~/ProductImg/" + fileName;
                    sanPham.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/ProductImg/"), fileName));
                }

                if (sanPham.Id == 0)
                {
                    //db.SanPhams.Add(sanPham);
                    //db.SaveChanges();
                    sp.insertSanPham(sanPham);
                }
                else
                {
                    //db.Entry(sanPham).State = EntityState.Modified;
                    //db.SaveChanges();
                    sp.updateSanPham(sanPham);
                }

                //using (ApplicationDbContext db = new ApplicationDbContext())
                //{
                //    if (sanPham.Id == 0)
                //    {
                //        db.SanPhams.Add(sanPham);
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        db.Entry(sanPham).State = EntityState.Modified;
                //        db.SaveChanges();

                //    }
                //}

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", sp.getAllSanPham()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            var sp = SanPhamDAL.createSanPhamDAL();
            try
            {

                //SanPham emp = db.SanPhams.Where(x => x.Id == id).FirstOrDefault<SanPham>();
                //db.SanPhams.Remove(emp);
                //db.SaveChanges();
                sp.deleteSanPhamx(id);

                //using (ApplicationDbContext db = new ApplicationDbContext())
                //{
                //    SanPham emp = db.SanPhams.Where(x => x.Id == id).FirstOrDefault<SanPham>();
                //    db.SanPhams.Remove(emp);
                //    db.SaveChanges();
                //}

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", sp.getAllSanPham()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //GET : Details MenuItem
        public ActionResult Detail(int id)
        {
            var sp = SanPhamDAL.createSanPhamDAL();
            SanPham sanPham = sp.detailSanPham(id);
            //SanPham sanPham = db.SanPhams.Where(p => p.Id == id).SingleOrDefault();
            ViewBag.sanPham = sanPham;

            ViewBag.CategoryList = GetCategory();
            ViewBag.HangList = GetHang();

            //if (id == null)
            //{
            //    return HttpNotFound();
            //}

            //if (model == null)
            //{
            //    return HttpNotFound();
            //}

            //var model = sp.getSanPhamById(id);

            bool exist;
            //var userId = User.Identity.GetUserId<string>();

            //exist = db.FavouriteSanPhams.Any(p => p.ProductId == id);
            exist = sp.checkSanPhamExistsInFavouriteById(id);

            ViewBag.inFavorite = exist;

            return View(sanPham);
        }

        [HttpPost]
        public ActionResult FavouriteSanPham()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return Json(new { result = -1, message = "Vui lòng đăng nhập để thực hiện chức năng này" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string userID = User.Identity.GetUserId<string>();
                int productID = Convert.ToInt32(Request.Form["sanPham"]);
                var sanphamfavDAL = SanPhamDAL.createSanPhamDAL();
                //kiểm tra sản phẩm tồn tại trong danh sách yêu thích ko
                Favor = new ContextAddFavor(null);
                if (sanphamfavDAL.checkSanPhamExistsInFavourite(productID, userID))
                {
                    //sanphamfavDAL.deleteFavouriteSanPham(productID,username);

                    Favor.TransitionTo(new RemoveFavor());
                    Favor.Favor(productID, userID);

                    return Json(new
                    {
                        result = 1,
                        imgsrc = "/Assets/image/icon/chuaThich.png",
                        message = "Xóa sản phẩm khỏi danh sách yêu thích thành công"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Favor.TransitionTo(new AddFavor());
                    Favor.Favor(productID, userID);
                    return Json(new
                    {
                        result = 1,
                        imgsrc = "/Assets/image/icon/daThich.png",
                        message = "Thêm bài hát vào danh sách yêu thích thành công"
                    }, JsonRequestBehavior.AllowGet);
                }  
            }
        }

    }
}

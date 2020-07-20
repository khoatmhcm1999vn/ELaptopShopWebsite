using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.DAL
{
    public class SanPhamDAL
    {

        private ApplicationDbContext db;

        private SanPhamDAL()
        {
            db = new ApplicationDbContext();
        }

        public static SanPhamDAL sanPhamDAL;

        public static SanPhamDAL createSanPhamDAL()
        {
            if (sanPhamDAL == null)
            {
                sanPhamDAL = new SanPhamDAL();
            }
            return sanPhamDAL;
        }

        public SanPham getSanPhamById(int id)
        {
            //return db.SanPhams.Find(id);
            return db.SanPhams.Where(s => s.Id == id).SingleOrDefault();
        }

        public SanPham getSanPhamByProductId(int id)
        {
            //return db.SanPhams.Find(id);
            return db.SanPhams.Where(x => x.Id == id).FirstOrDefault<SanPham>();
        }

        public List<SanPham> getAllSanPham()
        {
            return db.SanPhams.ToList();
        }

        public bool insertSanPham(SanPham sanPham)
        {
            //int i;
            db.SanPhams.Add(sanPham);
            db.SaveChanges();
            return true;
        }

        public bool updateSanPham(SanPham sanPham)
        {
            try
            {
                var local = db.Set<SanPham>()
                    .Local
                    .FirstOrDefault(f => f.Id == sanPham.Id);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(sanPham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        public bool deleteSanPhamx(int id)
        {
 
            SanPham sp = db.SanPhams.Where(x => x.Id == id).FirstOrDefault<SanPham>();
            db.SanPhams.Remove(sp);
            db.SaveChanges();

            return true;
        }

        public SanPham detailSanPham(int id)
        {
            var a = db.SanPhams.Find(id);
            return a;
        }

        //lấy tất cả các sản phẩm yêu thích của user
        public List<FavouriteSanPham> getFavouriteSanPhamByUser(int productId)
        {
            return db.FavouriteSanPhams.Where(p => p.ProductId == productId).ToList();
        }

        //kiểm tra sản phẩm có tồn tại trong danh sách yêu thích hay ko
        public bool checkSanPhamExistsInFavouriteById(int productID)
        {
            return db.FavouriteSanPhams.Any(p => p.ProductId == productID);
        }

        //kiểm tra sản phẩm có tồn tại trong danh sách yêu thích hay ko
        public bool checkSanPhamExistsInFavourite(int productID, string userID)
        {
            return db.FavouriteSanPhams.Any(p => p.ProductId == productID && p.UserId == userID);  
        }

        public bool addFavouriteSanPham(int productID, string userID)
        {
            try
            {
                FavouriteSanPham fal = new FavouriteSanPham();
               
                fal.UserId = userID;
                fal.ProductId = productID;

                //FavouriteSanPham emp = db.FavouriteSanPhams.Where(x => x.ProductId == productID && x.UserId == userID).FirstOrDefault<FavouriteSanPham>();

                db.FavouriteSanPhams.Add(fal);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteFavouriteSanPham(int productID, string userID)
        {
            //var entry = db.Entry(FavouriteSanPham);
            FavouriteSanPham fal = new FavouriteSanPham();

            fal.ProductId = productID;
            fal.UserId = userID;

            FavouriteSanPham sp = db.FavouriteSanPhams.Where(x => x.ProductId == productID && x.UserId == userID).FirstOrDefault<FavouriteSanPham>();
            db.FavouriteSanPhams.Remove(sp);
            db.SaveChanges();

            //db.FavouriteSanPhams.State = System.Data.Entity.EntityState.Deleted;
            //entry.State = EntityState.Modified;
            //db.FavouriteSanPhams.AsNoTracking();
            
            //db.FavouriteSanPhams.Attach(fal);
            //db.FavouriteSanPhams.Remove(fal);
            //db.SaveChanges();
            return true;
        }

    }
}

using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.DAL
{
    public class HangDAL
    {
        private HangDAL()
        {
            db = new ApplicationDbContext();
        }

        private static HangDAL hangDAL;
        private ApplicationDbContext db;

        public static HangDAL createHangDAL()
        {
            if (hangDAL == null)
            {
                hangDAL = new HangDAL();
            }
            return hangDAL;
        }

        public Hang getHangById(int? id)
        {
            return db.Hangs.Find(id);
        }

        public List<Hang> getAllHang()
        {
            return db.Hangs.ToList();
        }

        public bool insertHang(Hang hang)
        {
            try
            {
                db.Hangs.Add(hang);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool updateHang(Hang hang)
        {
            try
            {
                Hang hangx = db.Hangs.Find(hang.Id);
                db.Entry(hangx).CurrentValues.SetValues(hang);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteHang(int? id)
        {
            Hang hang = db.Hangs.Find(id);
            db.Hangs.Remove(hang);
            db.SaveChanges();
            return true;
        }

        public Hang detailHang(int? id)
        {
            var a = db.Hangs.Find(id);
            return a;
        }

    }
}
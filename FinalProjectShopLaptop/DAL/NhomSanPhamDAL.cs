using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.DAL
{
    public class NhomSanPhamDAL
    {
        private NhomSanPhamDAL()
        {
            db = new ApplicationDbContext();
        }

        private static NhomSanPhamDAL nhomSanPhamDAL;
        private ApplicationDbContext db;

        public static NhomSanPhamDAL createNhomSanPhamDAL()
        {
            if (nhomSanPhamDAL == null)
            {
                nhomSanPhamDAL = new NhomSanPhamDAL();
            }
            return nhomSanPhamDAL;
        }

        public NhomSanPham getNhomSPById(int? id)
        {
            return db.NhomSanPhams.Find(id);
        }

        public List<NhomSanPham> getAllNhomSP()
        {
            return db.NhomSanPhams.ToList();
        }

        public bool insertNhomSP(NhomSanPham nhomsp)
        {
            try
            {
                db.NhomSanPhams.Add(nhomsp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool updateNhomSP(NhomSanPham nhomsp)
        {
            try
            {
                NhomSanPham nsp = db.NhomSanPhams.Find(nhomsp.Id);
                db.Entry(nsp).CurrentValues.SetValues(nhomsp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteNhomSP(int? id)
        {
            NhomSanPham nsp = db.NhomSanPhams.Find(id);
            db.NhomSanPhams.Remove(nsp);
            db.SaveChanges();
            return true;
        }

        public NhomSanPham detailNhomSP(int? id)
        {
            var a = db.NhomSanPhams.Find(id);
            return a;
        }

    }
}
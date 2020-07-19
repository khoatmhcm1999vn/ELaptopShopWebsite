using FinalProjectShopLaptop.DAL;
using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectShopLaptop.Controllers
{
    public class NhomSanPhamController : Controller
    {

        // GET: NhomSanPham
        public ActionResult Index()
        {
            var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();
            var model = nhomsp.getAllNhomSP();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NhomSanPham model)
        {
            var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();

            if (ModelState.IsValid)
            {
                var id = nhomsp.insertNhomSP(model);
                if (id == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "abc");
                }
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();
            var category = nhomsp.getNhomSPById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(NhomSanPham model)
        {
            if (ModelState.IsValid)
            {
                var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();
                var id = nhomsp.updateNhomSP(model);
                if (id == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "abc");
                }
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();
            var category = nhomsp.getNhomSPById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();
            nhomsp.deleteNhomSP(id);
            return RedirectToAction("Index");
        }

        //GET - DETAILS
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var nhomsp = NhomSanPhamDAL.createNhomSanPhamDAL();
            var model = nhomsp.detailNhomSP(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

    }
}

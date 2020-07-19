using FinalProjectShopLaptop.DAL;
using FinalProjectShopLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectShopLaptop.Controllers
{
    public class HangController : Controller
    {

        // GET: Hang
        public ActionResult Index()
        {
            var hang = HangDAL.createHangDAL();
            var model = hang.getAllHang();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Hang model)
        {
            var hang = HangDAL.createHangDAL();

            if (ModelState.IsValid)
            {
                var id = hang.insertHang(model);
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
            var hang = HangDAL.createHangDAL();
            var hangx = hang.getHangById(id);
            if (hangx == null)
            {
                return HttpNotFound();
            }
            return View(hangx);
        }

        [HttpPost]
        public ActionResult Edit(Hang model)
        {
            if (ModelState.IsValid)
            {
                var hang = HangDAL.createHangDAL();
                var id = hang.updateHang(model);
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
            var hang = HangDAL.createHangDAL();
            var hangx = hang.getHangById(id);
            if (hangx == null)
            {
                return HttpNotFound();
            }
            return View(hangx);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            var hang = HangDAL.createHangDAL();
            hang.deleteHang(id);
            return RedirectToAction("Index");
        }

        //GET - DETAILS
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var hang = HangDAL.createHangDAL();
            var model = hang.detailHang(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

    }
}

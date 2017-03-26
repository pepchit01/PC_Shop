using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLPC.Models.BussinessModel;
using QLPC.Models.DataModel;
using PagedList;

namespace QLPC.Areas.Admin.Controllers
{
    public class HangsanxuatController : Controller
    {
        private QLPCDbContext db = new QLPCDbContext();

        // GET: Admin/Hangsanxuat
        public ActionResult Index(int? page)
        {
            var hangsx = db.hangsanxuat.ToList();
            var pageNumber = page ?? 1;
            var onePageOfHangsxs = hangsx.ToPagedList(pageNumber, 10);
            ViewBag.onePageOfHangsx = onePageOfHangsxs;
            return View();
        }

        // GET: Admin/Hangsanxuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hangsanxuat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAHSX,TENHSX,DCHIHSX,DTHOAIHSX")] HANGSX hANGSX)
        {
            if (ModelState.IsValid)
            {
                db.hangsanxuat.Add(hANGSX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hANGSX);
        }

        // GET: Admin/Hangsanxuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANGSX hANGSX = db.hangsanxuat.Find(id);
            if (hANGSX == null)
            {
                return HttpNotFound();
            }
            return View(hANGSX);
        }

        // POST: Admin/Hangsanxuat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAHSX,TENHSX,DCHIHSX,DTHOAIHSX")] HANGSX hANGSX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hANGSX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hANGSX);
        }
        

        // POST: Admin/Hangsanxuat/Delete/5
        public ActionResult Delete(int id)
        {
            HANGSX hANGSX = db.hangsanxuat.Find(id);
            db.hangsanxuat.Remove(hANGSX);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

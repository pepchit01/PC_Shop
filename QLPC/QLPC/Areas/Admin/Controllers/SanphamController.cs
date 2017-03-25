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
    public class SanphamController : Controller
    {
        private QLPCDbContext db = new QLPCDbContext();

        // GET: Admin/Sanpham
        public ActionResult Index(int? page)
        {
            var sanpham = db.sanpham.Include(s => s.HANGSX).Include(s => s.NHAPHANPHOI).ToList();
            var pageNumber = page ?? 1;
            var onePageOfSanpham = sanpham.ToPagedList(pageNumber, 20);
            ViewBag.sanpham = onePageOfSanpham;
            return View();
        }

        // GET: Admin/Sanpham/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.sanpham.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/Sanpham/Create
        public ActionResult Create()
        {
            ViewBag.MAHSX = new SelectList(db.hangsanxuat, "MAHSX", "TENHSX");
            ViewBag.MANPP = new SelectList(db.nhaphanphoi, "MANPP", "TENNPP");
            return View();
        }

        // POST: Admin/Sanpham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SERIAL,MAHSX,MANPP,TENMAY,MODEL,GIA,SOLUONG,THOIHANBAOHANH,IMAGE,MOREIMAGE,GIAKHUYENMAI,DETAIL")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.sanpham.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAHSX = new SelectList(db.hangsanxuat, "MAHSX", "TENHSX", sANPHAM.MAHSX);
            ViewBag.MANPP = new SelectList(db.nhaphanphoi, "MANPP", "TENNPP", sANPHAM.MANPP);
            return View(sANPHAM);
        }

        // GET: Admin/Sanpham/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.sanpham.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAHSX = new SelectList(db.hangsanxuat, "MAHSX", "TENHSX", sANPHAM.MAHSX);
            ViewBag.MANPP = new SelectList(db.nhaphanphoi, "MANPP", "TENNPP", sANPHAM.MANPP);
            return View(sANPHAM);
        }

        // POST: Admin/Sanpham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SERIAL,MAHSX,MANPP,TENMAY,MODEL,GIA,SOLUONG,THOIHANBAOHANH,IMAGE,MOREIMAGE,GIAKHUYENMAI,DETAIL")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAHSX = new SelectList(db.hangsanxuat, "MAHSX", "TENHSX", sANPHAM.MAHSX);
            ViewBag.MANPP = new SelectList(db.nhaphanphoi, "MANPP", "TENNPP", sANPHAM.MANPP);
            return View(sANPHAM);
        }

        // GET: Admin/Sanpham/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.sanpham.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/Sanpham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SANPHAM sANPHAM = db.sanpham.Find(id);
            db.sanpham.Remove(sANPHAM);
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

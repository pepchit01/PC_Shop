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
    public class MuabanController : Controller
    {
        private QLPCDbContext db = new QLPCDbContext();

        // GET: Admin/Muaban
        public ActionResult Index(int? page)
        {
            var muaban = db.muaban.Include(m => m.KHACHHANG).Include(m => m.SANPHAM).ToList();
            var pageNumber = page ?? 1;
            var onePageOfMuaBans = muaban.ToPagedList(pageNumber, 15);
            ViewBag.onePageOfMuaBan = onePageOfMuaBans;
            return View(onePageOfMuaBans);
        }

        // GET: Admin/Muaban/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUABAN mUABAN = db.muaban.Find(id);
            if (mUABAN == null)
            {
                return HttpNotFound();
            }
            return View(mUABAN);
        }

        // GET: Admin/Muaban/Create
        public ActionResult Create()
        {
            ViewBag.MAKH = new SelectList(db.khachhang, "MAKH", "TENKH");
            ViewBag.SERIAL = new SelectList(db.sanpham, "SERIAL", "TENMAY");
            return View();
        }

        // POST: Admin/Muaban/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAMB,SERIAL,MAKH,NGAYMUA,SOTIEN,SOLUONG,THANHTOAN")] MUABAN mUABAN)
        {
            if (ModelState.IsValid)
            {
                db.muaban.Add(mUABAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAKH = new SelectList(db.khachhang, "MAKH", "TENKH", mUABAN.MAKH);
            ViewBag.SERIAL = new SelectList(db.sanpham, "SERIAL", "TENMAY", mUABAN.SERIAL);
            return View(mUABAN);
        }

        // GET: Admin/Muaban/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUABAN mUABAN = db.muaban.Find(id);
            if (mUABAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAKH = new SelectList(db.khachhang, "MAKH", "TENKH", mUABAN.MAKH);
            ViewBag.SERIAL = new SelectList(db.sanpham, "SERIAL", "TENMAY", mUABAN.SERIAL);
            return View(mUABAN);
        }

        // POST: Admin/Muaban/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAMB,SERIAL,MAKH,NGAYMUA,SOTIEN,SOLUONG,THANHTOAN")] MUABAN mUABAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mUABAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAKH = new SelectList(db.khachhang, "MAKH", "TENKH", mUABAN.MAKH);
            ViewBag.SERIAL = new SelectList(db.sanpham, "SERIAL", "TENMAY", mUABAN.SERIAL);
            return View(mUABAN);
        }

        // GET: Admin/Muaban/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUABAN mUABAN = db.muaban.Find(id);
            if (mUABAN == null)
            {
                return HttpNotFound();
            }
            return View(mUABAN);
        }

        // POST: Admin/Muaban/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MUABAN mUABAN = db.muaban.Find(id);
            db.muaban.Remove(mUABAN);
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

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

namespace QLPC.Areas.Admin.Controllers
{
    public class NhaphanphoiController : Controller
    {
        private QLPCDbContext db = new QLPCDbContext();

        // GET: Admin/Nhaphanphoi
        public ActionResult Index()
        {
            return View(db.nhaphanphoi.ToList());
        }

        // GET: Admin/Nhaphanphoi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAPHANPHOI nHAPHANPHOI = db.nhaphanphoi.Find(id);
            if (nHAPHANPHOI == null)
            {
                return HttpNotFound();
            }
            return View(nHAPHANPHOI);
        }

        // GET: Admin/Nhaphanphoi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nhaphanphoi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANPP,TENNPP,DCHINPP,DTHOAINPP,FAXNPP")] NHAPHANPHOI nHAPHANPHOI)
        {
            if (ModelState.IsValid)
            {
                db.nhaphanphoi.Add(nHAPHANPHOI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHAPHANPHOI);
        }

        // GET: Admin/Nhaphanphoi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAPHANPHOI nHAPHANPHOI = db.nhaphanphoi.Find(id);
            if (nHAPHANPHOI == null)
            {
                return HttpNotFound();
            }
            return View(nHAPHANPHOI);
        }

        // POST: Admin/Nhaphanphoi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANPP,TENNPP,DCHINPP,DTHOAINPP,FAXNPP")] NHAPHANPHOI nHAPHANPHOI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHAPHANPHOI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHAPHANPHOI);
        }

        // GET: Admin/Nhaphanphoi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAPHANPHOI nHAPHANPHOI = db.nhaphanphoi.Find(id);
            if (nHAPHANPHOI == null)
            {
                return HttpNotFound();
            }
            return View(nHAPHANPHOI);
        }

        // POST: Admin/Nhaphanphoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHAPHANPHOI nHAPHANPHOI = db.nhaphanphoi.Find(id);
            db.nhaphanphoi.Remove(nHAPHANPHOI);
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

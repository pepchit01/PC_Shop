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
    public class KhachhangController : Controller
    {
        private QLPCDbContext db = new QLPCDbContext();

        // GET: Admin/Khachhang
        public ActionResult Index(int ? page)
        {
            var khachhangs = db.khachhang.ToList();
            var pageNumber = page ?? 1;
            var onePageOfKhachhang = khachhangs.ToPagedList(pageNumber, 20);
            ViewBag.khachhang = onePageOfKhachhang;
            return View(/*db.khachhang.ToList()*/);
        }

        // GET: Admin/Khachhang/Details/5
        public ActionResult Details(string id)
        {          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.khachhang.Find(id);
            ViewBag.luotmua = db.muaban.Where(x => x.MAKH.Equals(id)).Count();
            var sp = db.muaban.Include(s => s.SANPHAM).Where(x => x.MAKH.Equals(id)).ToList();
            //var sanphams = (from m in db.muaban //lấy danh sách sản phẩm mà người dùng mua
            //                                join s in db.sanpham
            //                                on m.SERIAL equals s.SERIAL
            //                                where m.MAKH.Equals(id)
            //                                select new 
            //                                {                   
            //                                    TENMAY = s.TENMAY,
            //                                    MODEL = s.MODEL,
            //                                    IMAGE = s.IMAGE,
            //                                    GIA = s.GIA
            //                                }).ToList();
            ViewBag.sanpham = sp;
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: Admin/Khachhang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Khachhang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAKH,TENKH,DCHIKH,DTHOAIKH,TINH,PASS,EMAIL")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.khachhang.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Admin/Khachhang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.khachhang.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/Khachhang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAKH,TENKH,DCHIKH,DTHOAIKH,TINH,PASS,EMAIL")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACHHANG);
        }


        // POST: Admin/Khachhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            KHACHHANG kHACHHANG = db.khachhang.Find(id);
            db.khachhang.Remove(kHACHHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

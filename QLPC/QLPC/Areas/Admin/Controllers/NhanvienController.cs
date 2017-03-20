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
    public class NhanvienController : Controller
    {
        private QLPCDbContext db = new QLPCDbContext();

        // GET: Admin/Nhanvien
        //Trang hiển thị danh sách nhân viên
        public ActionResult Index()
        {
            return View(db.nhanvien.ToList());
        }

        // GET: Admin/Nhanvien/Details/5
        //Chuyển đến trang xem chi tiết thông tin nhân viên
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.nhanvien.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // GET: Admin/Nhanvien/Create
        //Di chuyển đến trang tạo nhân viên
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nhanvien/Create
        //Tạo một nhân viên mới tuef những thông tin do người dùng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANV,TENNV,DTHOAINV,DCHINV,PASS,GRANT,EMAIL,AVATAR")] NHANVIEN nHANVIEN)
        { 
            if (ModelState.IsValid)
            {
                nHANVIEN.AVATAR = "/Areas/Admin/Content/dist/img/" + nHANVIEN.AVATAR;
                db.nhanvien.Add(nHANVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Admin/Nhanvien/Edit/5
        //Nhận vào một mã nhân viên sau đó chuyển đến trang xem chi tiết
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.nhanvien.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: Admin/Nhanvien/Edit/5
        // Cập nhật thông tin nhân viên sau khi chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANV,TENNV,DTHOAINV,DCHINV,PASS,GRANT,EMAIL,AVATAR")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHANVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHANVIEN);
        }

        // GET: Admin/Nhanvien/Delete/5
        //Xóa nhân viên từ danh sách
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.nhanvien.Find(id);
            if (nHANVIEN != null)
            {
                db.nhanvien.Remove(nHANVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}

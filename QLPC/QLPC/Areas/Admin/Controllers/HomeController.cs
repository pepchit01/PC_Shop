using QLPC.Models.BussinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QLPCDbContext db = new QLPCDbContext();

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string dthoainv, string pass)
        {
           var nv= db.nhanvien.SingleOrDefault(x => x.DTHOAINV == dthoainv && x.PASS == pass && x.GRANT==2);
            if (nv != null)
            {
                Session["MANV"] = nv.MANV;
                Session["TENNV"] = nv.TENNV;
                Session["AVATAR"] = nv.AVATAR;
                Session["DTHOAINV"] = nv.DTHOAINV;
                return RedirectToAction("Index");
            }
            ViewBag.error = "Tài khoản không đúng, vui lòng kiểm tra lại";
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["MANV"] = null;
            Session["TENNV"] = null;
            Session["AVATAR"] = null;
            Session["DTHOAINV"] = null;
            return RedirectToAction("Login");
        }
    }
}
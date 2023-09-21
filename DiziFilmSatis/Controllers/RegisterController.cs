using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Kayıtol(Tbl_Uye p)
        {
            p.Durum = false;
            db.Tbl_Uye.Add(p);
            db.SaveChanges();
            return  RedirectToAction("Index", "Vitrin");
        }
    }
}
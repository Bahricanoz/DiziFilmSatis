using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class HakkımdaController : Controller
    {

        // GET: Hakkımda
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        [HttpGet]
        public ActionResult Index()
        {
            int id = 1;
            var hakkımızda = db.Tbl_Hakkimizda.FirstOrDefault(x=>x.Id == 1);
            return View("Index", hakkımızda);
        }
        [HttpPost]
        public ActionResult Index(Tbl_Hakkimizda p)
        {
            var deger = db.Tbl_Hakkimizda.FirstOrDefault(x => x.Id == 1);
            deger.icerik1 = p.icerik1;
            deger.icerik2 = p.icerik2;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class MesajController : Controller
    {
        // GET: Mesaj
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var mesaj = db.Tbl_Mesaj.OrderByDescending(x=>x.Id).ToList();
            return View(mesaj);
        }
        public ActionResult Sil(int id)
        {
            var sil = db.Tbl_Mesaj.Find(id);
            db.Tbl_Mesaj.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Mesaj.FirstOrDefault(x => x.Id == id);
            return View("Detay", bul);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Mesaj p)
        {
            db.Tbl_Mesaj.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
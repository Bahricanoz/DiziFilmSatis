using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class TurController : Controller
    {
        // GET: Tur
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var tur = db.Tbl_Tur.ToList();
            return View(tur);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Tur.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Tur.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Tur p)
        {
            p.Durum = true;
            db.Tbl_Tur.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Tur.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Tur p)
        {
            var bul = db.Tbl_Tur.FirstOrDefault(x => x.Id == p.Id);
            bul.Durum = true;
            bul.Turad = p.Turad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
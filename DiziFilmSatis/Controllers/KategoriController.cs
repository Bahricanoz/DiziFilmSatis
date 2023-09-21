using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var kat = db.Tbl_Kategori.ToList();
            return View(kat);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Kategori.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Kategori.Find(id);
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
        public ActionResult Ekle(Tbl_Kategori p)
        {
            p.Durum = true;
            db.Tbl_Kategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Kategori.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Kategori p)
        {
            var bul = db.Tbl_Kategori.FirstOrDefault(x => x.Id == p.Id);
            bul.Durum = true;
            bul.Katad = p.Katad;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
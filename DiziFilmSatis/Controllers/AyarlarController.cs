using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var admin = db.Tbl_Admin.ToList();
            return View(admin);
        }
        public ActionResult Sil(int id)
        {
            var bul = db.Tbl_Admin.Find(id);
            db.Tbl_Admin.Remove(bul);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Admin.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Admin.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Admin p)
        {
            p.Durum = true;
            db.Tbl_Admin.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Admin.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Admin p)
        {
            var deger = db.Tbl_Admin.Find(p.Id);
            deger.sifre = p.sifre;
            deger.Name = p.Name;
            deger.kullaniciadi = p.kullaniciadi;
            deger.Yetki = p.Yetki;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
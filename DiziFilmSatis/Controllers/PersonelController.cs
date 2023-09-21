using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var per = db.Tbl_Personel.ToList();
            return View(per);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Personel.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Personel.Find(id);
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
        public ActionResult Ekle(Tbl_Personel p)
        {
            p.Durum = true;
            db.Tbl_Personel.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Personel.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Personel p)
        {
            var deger = db.Tbl_Personel.FirstOrDefault(x => x.Id == p.Id);
            deger.Durum = true;
            deger.Ad = p.Ad;
            deger.Soyad = p.Soyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
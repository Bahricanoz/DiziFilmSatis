using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class YonetmenController : Controller
    {
        // GET: Yonetmen
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var yonetmen = db.Tbl_Yonetmen.ToList();
            return View(yonetmen);
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Yonetmen.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Yonetmen.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Eser(int id)
        {
            var bul = db.Tbl_Filmdizi.Where(x => x.Yonetmenid == id).ToList();
            var ad = db.Tbl_Yonetmen.FirstOrDefault(x => x.Id == id);
            var isim = ad.Ad + " " + " " + ad.Soyad;
            ViewBag.isim = isim;
            return View(bul);
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Yonetmen.Find(id);
            return View("Detay", bul);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Yonetmen p)
        {
            p.Durum = true;
            db.Tbl_Yonetmen.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Yonetmen.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Yonetmen p)
        {
            var deger = db.Tbl_Yonetmen.Find(p.Id);
            deger.Durum = true;
            deger.Ad = p.Ad;
            deger.Detay = p.Detay;
            deger.Soyad = p.Soyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
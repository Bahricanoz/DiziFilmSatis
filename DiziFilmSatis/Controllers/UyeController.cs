using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index(string p)
        {
            var uye = from x in db.Tbl_Uye select x;
            if (!string.IsNullOrEmpty(p))
            {
                uye = uye.Where(x => x.Ad.Contains(p));
            }
            //var uye = db.Tbl_Uye.ToList();
            return View(uye.ToList());
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Uye.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Uye.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Uye.Find(id);
            return View("Detay",bul);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Uye p)
        {
            p.Durum = true;
            db.Tbl_Uye.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Uye.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Uye p)
        {
            var bul = db.Tbl_Uye.FirstOrDefault(x => x.Id == p.Id);
            bul.Durum = true;
            bul.Kullaniciadi = p.Kullaniciadi;
            bul.Soyad = p.Soyad;
            bul.Ad = p.Ad;
            bul.Sifre = p.Sifre;
            bul.Mail = p.Mail;
            bul.Telefon = p.Telefon;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
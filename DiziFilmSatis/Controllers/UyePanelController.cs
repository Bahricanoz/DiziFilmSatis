using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class UyePanelController : Controller
    {
        // GET: UyePanel
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var bilgi = Session["Kullaniciadi"].ToString();
            var bilgiler = db.Tbl_Uye.FirstOrDefault(x => x.Kullaniciadi == bilgi);
            ViewBag.ad = bilgiler.Ad.ToString();
            ViewBag.soyad = bilgiler.Soyad.ToString();
            ViewBag.egitim = bilgiler.Egitim.ToString();
            ViewBag.yas = bilgiler.Yas.ToString();
            ViewBag.detay = bilgiler.Detay.ToString();
            ViewBag.foto = bilgiler.Foto.ToString();
            ViewBag.telefon = bilgiler.Telefon.ToString();

            var duyuru = db.Tbl_Duyurular.Where(x => x.Durum == true).Count();
            ViewBag.duyuru = duyuru;

            var gelenmesaj = db.Tbl_Mesaj.Where(x => x.Alıcı == bilgiler.Mail).Count();
            ViewBag.mesaj = gelenmesaj;

            var ödüncalma = db.Tbl_Hareket.Where(x => x.Tbl_Uye.Mail == bilgiler.Mail).Count();
            ViewBag.odunc = ödüncalma;

            var fiyat1 = db.Tbl_Hareket.Where(x => x.Tbl_Uye.Mail == bilgiler.Mail).Sum(x => x.Para);
            var fiyat2 = db.Tbl_Ceza.Where(x => x.Tbl_Uye.Mail == bilgiler.Mail).Sum(x => x.Paracezası);
            var fiyat = fiyat1 + fiyat2;
            ViewBag.fiyat = fiyat;
            return View("Index",bilgiler);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Uye p)
        {
            var bilgi = Session["Kullaniciadi"].ToString();
            var deger = db.Tbl_Uye.FirstOrDefault(x => x.Kullaniciadi == bilgi);
            deger.Ad = p.Ad;
            deger.Soyad = p.Soyad;
            deger.Mail = p.Mail;
            deger.Kullaniciadi = p.Kullaniciadi;
            deger.Sifre = p.Sifre;
            deger.Foto = p.Foto;
            deger.Detay = p.Detay;
            deger.Egitim = p.Egitim;
            deger.Yas = p.Yas;
            deger.Telefon = p.Telefon;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult Duyurular()
        {
            var duyuru = db.Tbl_Duyurular.Where(x => x.Durum == true).ToList();
            return PartialView(duyuru);
        }
        
    }
}
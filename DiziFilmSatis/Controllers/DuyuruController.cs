using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var duyuru = db.Tbl_Duyurular.OrderByDescending(x=>x.Id).ToList();
            return View(duyuru);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Duyurular.FirstOrDefault(x => x.Id == id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            return View("Detay", bul);
        }
        public ActionResult Sil(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            db.Tbl_Duyurular.Remove(bul);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Duyurular p)
        {
            p.Durum = true;
            db.Tbl_Duyurular.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Duyurular p)
        {
            var deger = db.Tbl_Duyurular.Find(p.Id);
            deger.Durum = true;
            deger.Baslik = p.Baslik;
            deger.Detay = p.Detay;
            deger.Tarih = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
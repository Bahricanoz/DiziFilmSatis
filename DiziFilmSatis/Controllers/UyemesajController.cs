using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class UyemesajController : Controller
    {
        // GET: Uyemesaj
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var bilgi = Session["Kullaniciadi"].ToString();
            var bilgiler = db.Tbl_Uye.FirstOrDefault(x => x.Kullaniciadi == bilgi);
            var mesajlar = db.Tbl_Mesaj.Where(x => x.Alıcı == bilgiler.Mail).ToList();
            return View(mesajlar);
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Mesaj.FirstOrDefault(x => x.Id == id);
            ViewBag.name = bul.Name;
            ViewBag.gonderen = bul.Mail;
            ViewBag.tarih = Convert.ToDateTime(bul.Tarih).ToShortDateString();
            ViewBag.konu = bul.Konu;
            ViewBag.mesaj = bul.Mesaj;
            return View("Detay", bul);
        }
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        public ActionResult Gonderilen()
        {
            var bilgi = Session["Mail"].ToString();
            var mesaj = db.Tbl_Mesaj.Where(x => x.Mail == bilgi).ToList();
            return View(mesaj);
        }
        [HttpGet]
        public ActionResult Gonder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Gonder(Tbl_Mesaj p)
        {
            p.Name = Session["Ad"].ToString() + " " + Session["Soyad"].ToString(); 
            p.Tarih = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.Mail = Session["Mail"].ToString();
            db.Tbl_Mesaj.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
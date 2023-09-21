using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class UyeFilmController : Controller
    {
        // GET: UyeFilm
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var mail = Session["Mail"].ToString();
            var urun = db.Tbl_Hareket.Where(x => x.Tbl_Uye.Mail == mail && x.Durum == false).ToList();
            return View(urun);
        }
        public ActionResult Iade(int id)
        {
            var p = db.Tbl_Hareket.FirstOrDefault(x => x.Id == id);
            p.Durum = true;
            p.Tbl_Filmdizi.Durum = true;
            p.Teslimtarihi = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime d1 = Convert.ToDateTime(p.Iadetarihi);
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan fark = d2 - d1;
            if(fark.TotalDays> 0)
            {
                Tbl_Ceza t = new Tbl_Ceza();
                t.Uyeid = p.Uyeid;
                t.Hareketid = p.Id;
                t.Alistarihi = p.Alistarihi;
                t.Teslimtarihi = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                t.Paracezası = Convert.ToDecimal(15 * fark.TotalDays);
                db.Tbl_Ceza.Add(t);

            }
            db.SaveChanges();
            return RedirectToAction("islemlerim");

        }
        public ActionResult islemlerim()
        {
            var mail = Session["Mail"].ToString();
            var urun = db.Tbl_Hareket.Where(x => x.Tbl_Uye.Mail == mail && x.Durum == true).ToList();
            return View(urun);
        }
        public ActionResult Detay(Tbl_Filmdizi p)
        {
            var bul = db.Tbl_Filmdizi.FirstOrDefault(x => x.Id == p.Id);
            ViewBag.yonet = bul.Tbl_Yonetmen.Ad + bul.Tbl_Yonetmen.Soyad;
            ViewBag.foto= bul.Foto;
            return View("Detay", bul);
        }
        [HttpGet]
        public ActionResult OduncAl()
        {
            List<SelectListItem> eser = (from x in db.Tbl_Filmdizi.Where(x => x.Durum == true).ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.Ad,
                                             Value = x.Id.ToString()
                                         }).ToList();
            ViewBag.eser = eser;
            return View();
        }
        [HttpPost]
        public ActionResult OduncAl(Tbl_Hareket p)
        {
            var mail = Session["Mail"].ToString();
            var bul = db.Tbl_Uye.FirstOrDefault(x => x.Mail == mail);
            var eser = db.Tbl_Filmdizi.Where(x => x.Id == p.Tbl_Filmdizi.Id).FirstOrDefault();
            p.Tbl_Filmdizi = eser;
            p.Uyeid = bul.Id;
            p.Perid = 1;
            p.Durum = false;
            p.Tbl_Filmdizi.Durum = false;
            p.Para = 100;
            db.Tbl_Hareket.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
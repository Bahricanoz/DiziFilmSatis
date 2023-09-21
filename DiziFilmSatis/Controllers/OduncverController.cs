using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class OduncverController : Controller
    {
        // GET: Oduncver
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        [HttpGet]
        public ActionResult Index()
        {
            List<SelectListItem> uye = (from x in db.Tbl_Uye.Where(x => x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Ad + " " + x.Soyad
                                        }).ToList();
            ViewBag.uye = uye;
            List<SelectListItem> urun = (from x in db.Tbl_Filmdizi.Where(x => x.Durum == true).ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.Ad,
                                             Value = x.Id.ToString()
                                         }).ToList();
            ViewBag.urun = urun;
            List<SelectListItem> per = (from x in db.Tbl_Personel.Where(x => x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad + " " + x.Soyad,
                                            Value = x.Id.ToString()
                                        }).ToList();
            ViewBag.per = per;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Tbl_Hareket p)
        {
            var per = db.Tbl_Personel.Where(x => x.Id == p.Tbl_Personel.Id).FirstOrDefault();
            var uye = db.Tbl_Uye.Where(x => x.Id == p.Tbl_Uye.Id).FirstOrDefault();
            var urn = db.Tbl_Filmdizi.Where(x => x.Id == p.Tbl_Filmdizi.Id).FirstOrDefault();
            p.Tbl_Personel = per;
            p.Tbl_Uye = uye;
            p.Tbl_Filmdizi = urn;
            p.Durum = false;
            p.Para = 100;
            p.Tbl_Filmdizi.Durum = false;
            db.Tbl_Hareket.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "OduncAl");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;

namespace DiziFilmSatis.Controllers
{
    public class OduncAlController : Controller
    {
        // GET: OduncAl
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var bilgi = db.Tbl_Hareket.Where(x => x.Durum == false).OrderByDescending(x=>x.Id).ToList();
            return View(bilgi);
        }
        public ActionResult Teslimal(Tbl_Hareket p)
        {
            var deger = db.Tbl_Hareket.FirstOrDefault(x => x.Id == p.Id);
            deger.Teslimtarihi = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            deger.Durum = true;
            deger.Tbl_Filmdizi.Durum = true;
            DateTime d1 = Convert.ToDateTime(deger.Alistarihi);
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan fark = d2 - d1;
            if(fark.TotalDays > 0)
            {
                Tbl_Ceza ceza = new Tbl_Ceza();
                ceza.Uyeid = deger.Uyeid;
                ceza.Hareketid = deger.Id;
                ceza.Alistarihi = deger.Alistarihi;
                ceza.Teslimtarihi = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                ceza.Paracezası = Convert.ToDecimal(15 * fark.TotalDays);
                db.Tbl_Ceza.Add(ceza);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "islemler");
        }

    }
}
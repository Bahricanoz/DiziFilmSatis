using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class VitrinController : Controller
    {
        // GET: Vitrin
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var film = db.Tbl_Filmdizi.OrderByDescending(x => x.Id).Take(6).ToList();
            return View(film);
        }
        public PartialViewResult About()
        {
            var hakkkımzda = db.Tbl_Hakkimizda.ToList();
            return PartialView(hakkkımzda); 
        }
        [HttpGet]
        public PartialViewResult Mesaj()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Mesaj(Tbl_Mesaj p)
        {
            p.Alıcı = "Admin@gmail.com";
            p.Tarih = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            db.Tbl_Mesaj.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
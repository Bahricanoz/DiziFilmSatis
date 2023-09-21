using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;

namespace DiziFilmSatis.Controllers
{
    public class islemlerController : Controller
    {
        // GET: islemler
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index()
        {
            var ceza = db.Tbl_Hareket.Where(x => x.Durum == true).OrderByDescending(x=>x.Teslimtarihi).ToList();
            

            return View(ceza);
        }
    }
}
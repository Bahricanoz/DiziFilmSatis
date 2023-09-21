using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminGiris(Tbl_Admin p)
        {
            var bilgiler = db.Tbl_Admin.FirstOrDefault(x => x.kullaniciadi == p.kullaniciadi && x.sifre == p.sifre && x.Durum == true);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullaniciadi, false);
                return RedirectToAction("Index","Anasayfa");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Admincikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public ActionResult Uyegiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Uyegiris(Tbl_Uye p)
        {
            var bilgi = db.Tbl_Uye.FirstOrDefault(x => x.Kullaniciadi == p.Kullaniciadi && x.Sifre == p.Sifre && x.Durum == true);
            if(bilgi!= null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.Kullaniciadi, false);
                Session["Kullaniciadi"] = bilgi.Kullaniciadi.ToString();
                Session["Mail"] = bilgi.Mail.ToString();
                Session["Ad"] = bilgi.Ad.ToString();
                Session["Soyad"] = bilgi.Soyad.ToString();
                Session["Foto"] = bilgi.Foto.ToString();
                return RedirectToAction("Index", "UyePanel");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Uyecikis()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Vitrin");
        }
    }
}
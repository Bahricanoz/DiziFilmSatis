using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class AnasayfaController : Controller
    {
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        // GET: Anasayfa
        public ActionResult Index()
        {
            var dizifilm = db.Tbl_Filmdizi.Count();
            ViewBag.dizifilm = dizifilm;
            var uye = db.Tbl_Uye.Count();
            ViewBag.uye = uye;
            var emanetler = db.Tbl_Filmdizi.Where(x => x.Durum == false).Count();
            ViewBag.emanetler = emanetler;
            var kasa = db.Tbl_Hareket.Sum(x => x.Para);
            var ceza = db.Tbl_Ceza.Sum(x => x.Paracezası);
            ViewBag.para = kasa + ceza;
            return View();
        }
        public ActionResult Sorgular()
        {
            var dizifilm = db.Tbl_Filmdizi.Count();
            ViewBag.dizifilm = dizifilm;

            var uye = db.Tbl_Uye.Count();
            ViewBag.uye = uye;

            var aktifdizifilm = db.Tbl_Filmdizi.Where(x => x.Durum == true).Count();
            ViewBag.aktif = aktifdizifilm;

            var kasa = db.Tbl_Hareket.Sum(x => x.Para);
            var ceza = db.Tbl_Ceza.Sum(x => x.Paracezası);
            ViewBag.kasa = kasa + ceza;
            ViewBag.ceza = ceza;

            var tur = db.Tbl_Tur.Where(x => x.Durum == true).Count();
            ViewBag.tur = tur;

            var mesaj = db.Tbl_Mesaj.Count();
            ViewBag.mesaj = mesaj;

            var duyuru = db.Tbl_Duyurular.Where(x => x.Durum == true).Count();
            ViewBag.duyuru = duyuru;

            var yonetmen = db.Tbl_Yonetmen.Count();
            ViewBag.yonetmen = yonetmen;

            var encoktur = db.Tbl_Filmdizi
                           .GroupBy(x => x.Tbl_Tur.Turad)
                           .OrderByDescending(z => z.Count())
                           .Select(y => y.Key)
                           .FirstOrDefault();
            ViewBag.encoktur = encoktur;
            var kat = db.Tbl_Filmdizi
                       .GroupBy(x => x.Tbl_Kategori.Katad)
                       .OrderByDescending(z => z.Count())
                       .Select(y => y.Key)
                       .FirstOrDefault();
            ViewBag.kat = kat;
            // en fazla dizi-filmi olan yönetmen
            var yonet = db.Tbl_Filmdizi
                        .GroupBy(x => new { x.Tbl_Yonetmen.Ad, x.Tbl_Yonetmen.Soyad })
                        .OrderByDescending(z => z.Count())
                        .Select(y => y.Key)
                        .FirstOrDefault();
            ViewBag.yonet = $"{yonet.Ad} {yonet.Soyad}";

            // en fazla izlenen yönetmen
            var izle = db.Tbl_Hareket
                       .GroupBy(x => x.Tbl_Filmdizi.Ad)
                       .OrderByDescending(z => z.Count())
                       .Select(y => y.Key)
                       .FirstOrDefault();
            ViewBag.izle = izle;

            // en aktif personel
            var personel = db.Tbl_Hareket
                          .GroupBy(x => new { x.Tbl_Personel.Ad, x.Tbl_Personel.Soyad })
                          .OrderByDescending(z => z.Count())
                          .Select(y => y.Key)
                          .FirstOrDefault();
            ViewBag.personel = $"{personel.Ad} {personel.Soyad}";

            // en aktif üye
            var aktifuye = db.Tbl_Hareket
                          .GroupBy(x => new { x.Tbl_Uye.Ad, x.Tbl_Uye.Soyad })
                          .OrderByDescending(z => z.Count())
                          .Select(y =>y.Key)
                          .FirstOrDefault();
            ViewBag.aktifuye = $"{aktifuye.Ad} {aktifuye.Soyad}";

            // en çok ceza alan üye
            var uyeceza = db.Tbl_Ceza
                          .GroupBy(x => new { x.Tbl_Uye.Ad, x.Tbl_Uye.Soyad })
                          .OrderByDescending(z => z.Count())
                          .Select(y => y.Key)
                          .FirstOrDefault();
            ViewBag.uyeceza = $"{uyeceza.Ad} {uyeceza.Soyad}";
            return View();
        }
    }
}
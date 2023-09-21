using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using DiziFilmSatis.Models.Entity;
namespace DiziFilmSatis.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Db_DiziFilmSatisEntities db = new Db_DiziFilmSatisEntities();
        public ActionResult Index(int sayfa=1)
        {
            var satis = db.Tbl_Filmdizi.ToList().ToPagedList(sayfa,3);
            return View(satis);
        }
        public ActionResult Yonetmen(int id)
        {
            var bul = db.Tbl_Yonetmen.FirstOrDefault(x => x.Id == id);
            return RedirectToAction("Detay", "Yonetmen", bul);
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Filmdizi.Find(id);
            return View("Detay", bul);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> kat = (from x in db.Tbl_Kategori.Where(x => x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Katad
                                        }).ToList();
            ViewBag.kat = kat;

            List<SelectListItem> tur = (from x in db.Tbl_Tur.Where(x => x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Turad
                                        }).ToList();
            ViewBag.tur = tur;

            List<SelectListItem> yonetmen = (from x in db.Tbl_Yonetmen.Where(x => x.Durum == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Value = x.Id.ToString(),
                                                 Text = x.Ad + " " + x.Soyad
                                             }).ToList();
            ViewBag.yonet = yonetmen;
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Filmdizi p)
        {
            var kat = db.Tbl_Kategori.Where(x => x.Id == p.Tbl_Kategori.Id).FirstOrDefault();
            var tur = db.Tbl_Tur.Where(x => x.Id == p.Tbl_Tur.Id).FirstOrDefault();
            var yonet = db.Tbl_Yonetmen.Where(x => x.Id == p.Tbl_Yonetmen.Id).FirstOrDefault();
            p.Tbl_Kategori = kat;
            p.Tbl_Tur = tur;
            p.Tbl_Yonetmen = yonet;
            p.Durum = true;
            db.Tbl_Filmdizi.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            List<SelectListItem> kat = (from x in db.Tbl_Kategori.Where(x => x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Katad,
                                            Value = x.Id.ToString()
                                        }).ToList();
            ViewBag.kat = kat;

            List<SelectListItem> tur = (from x in db.Tbl_Tur.Where(x => x.Durum == true).ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.Turad,
                                                  Value = x.Id.ToString()
                                              }).ToList();
            ViewBag.tur = tur;

            List<SelectListItem> yonet = (from x in db.Tbl_Yonetmen.Where(x => x.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.Ad + " " + x.Soyad,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.yonet = yonet;

            var bul = db.Tbl_Filmdizi.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Filmdizi p)
        {
            var kat = db.Tbl_Kategori.Where(x => x.Id == p.Tbl_Kategori.Id).FirstOrDefault();
            var tur = db.Tbl_Tur.Where(x => x.Id == p.Tbl_Tur.Id).FirstOrDefault();
            var yonet = db.Tbl_Yonetmen.Where(x => x.Id == p.Tbl_Yonetmen.Id).FirstOrDefault();
            var deger = db.Tbl_Filmdizi.FirstOrDefault(x => x.Id == p.Id);
            deger.Detay = p.Detay;
            deger.Ad = p.Ad;
            deger.Foto = p.Foto;
            deger.Turid = tur.Id;
            deger.Katid = kat.Id;
            deger.Yonetmenid = yonet.Id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
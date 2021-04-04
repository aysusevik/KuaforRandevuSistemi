using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class GiderController : Controller
    {
        public ActionResult Giderler()
        {
            KuaforContext db = new KuaforContext();
            Kullanici k = (Kullanici)Session["Kullanici"]; //Sessiondan kullanıcı bilgisini aldık

            ViewBag.GiderListe = db.Gider.Where(x => x.firmaID == k.firmaID).ToList();

            ViewBag.GiderTurListe = new SelectList(db.GiderTur.Select(x => new
            {
                x.turID,
                x.ad
            }), "turID", "ad");

            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Gider gider)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            Kullanici k = (Kullanici)Session["Kullanici"]; //Sessiondan kullanıcı bilgisini aldık
            try
            {
                gider.kullaniciID = k.kullaniciID; // bu gideri ekleyen kullanıcı bilgisi
                gider.firmaID = k.firmaID; // kullanıcının firma bilgisi
                db.Gider.Add(gider); //Giderler tablosuna gider parametrelerini ekledik.
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Gider/Giderler");
        }
        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();

            ViewBag.GiderTurListe = new SelectList(db.GiderTur.Select(x => new
            {
                x.turID,
                x.ad
            }),"turID","ad");
            var gider = db.Gider.Find(id); //id'ye göre gider tablosundaki bilgileri bul.
            return View(gider);
        }
        [HttpPost]
        public ActionResult Duzenle(Gider g)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var gider = db.Gider.Find(g.giderID);
                gider.turID = g.turID;
                gider.tutar = g.tutar;
                gider.tarih = g.tarih;
                gider.aciklama = g.aciklama;
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Gider/Giderler");
        }
        public ActionResult Sil(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var gider = db.Gider.Where(x => x.giderID == id).SingleOrDefault();
                db.Gider.Remove(gider);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Gider/Giderler");
        }

    }
}
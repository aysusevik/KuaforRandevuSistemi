using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class HizmetController : Controller
    {
        // GET: Hizmet
        public ActionResult Hizmetler()
        {
            KuaforContext db = new KuaforContext();
            ViewBag.HizmetListe = db.Hizmet.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Hizmet hizmet)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;

            try
            {
                db.Hizmet.Add(hizmet); //Veritabanına hizmet bilgilerini ekle.
                db.SaveChanges(); //Değişiklikleri kaydet
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc; //işlemin gerçekleşip gerçekleşmediğini uyarı mesajı döndür.
            return Redirect("/Hizmet/Hizmetler");
        }

        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();
            var hizmet = db.Hizmet.Find(id); //id'ye göre hizmeti bul.
            return View(hizmet); //Geriye hizmeti döndür.
        }
        [HttpPost]
        public ActionResult Duzenle(Hizmet h)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var hizmet = db.Hizmet.Find(h.hizmetID); //mevcut hizmet
                hizmet.ad = h.ad;
                hizmet.sure = h.sure;
                hizmet.ucret = h.ucret;
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Hizmet/Hizmetler");

        }

        public ActionResult Sil(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var hizmet = db.Hizmet.Where(x => x.hizmetID == id).SingleOrDefault();
                db.Hizmet.Remove(hizmet);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Hizmet/Hizmetler");
        }
    }
}
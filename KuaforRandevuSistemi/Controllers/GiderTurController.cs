using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class GiderTurController : Controller
    {
        public ActionResult Turler()
        {
            KuaforContext db = new KuaforContext();
            ViewBag.TurListe = db.GiderTur.ToList(); //Veritabanından GiderTur tablosunu al TurListe'ya ata.
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(GiderTur tur)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;

            try
            {
                db.GiderTur.Add(tur);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/GiderTur/Turler");
        }

        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();
            var tur = db.GiderTur.Find(id);
            return View(tur);
        }
        [HttpPost]
        public ActionResult Duzenle(GiderTur t)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var tur = db.GiderTur.Find(t.turID); //mevcut hizmet
                tur.ad = t.ad;
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/GiderTur/Turler");
        }
        public ActionResult Sil(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var tur = db.GiderTur.Where(x => x.turID == id).SingleOrDefault();
                db.GiderTur.Remove(tur);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/GiderTur/Turler");
        }
    }
}
using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class IzinController : Controller
    {
        // GET: Izin
        public ActionResult Izinler()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            KuaforContext db = new KuaforContext();

            ViewBag.IzinListe = db.Izin.Include("Kullanici").Where(y=>y.onayliMi == true && y.Kullanici.yetkiID == 2 && y.Kullanici.firmaID == k.firmaID).ToList();
            ViewBag.PersonelListe = new SelectList(db.Kullanici.Where(y=>y.yetkiID == 2 && y.firmaID == k.firmaID).Select(x => new
            {
                x.kullaniciID,
                adSoyad = x.ad + " "+ x.soyad
            }),"kullaniciID", "adSoyad");
            
            ViewBag.PersonelIzinTalep = db.Izin.Where(x => x.onayliMi == false && x.Kullanici.yetkiID == 2 && x.Kullanici.firmaID == k.firmaID).ToList(); // eğer izin onaylı değilse personel talep etmiştir.

            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Izin izin)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                db.Izin.Add(izin);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Izin/Izinler");

        }
        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();
            var izin = db.Izin.Find(id);
            return View(izin);
        }
        [HttpPost]
        public ActionResult Duzenle(Izin i)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var izin = db.Izin.Find(i.izinID);
                izin.Kullanici.ad = i.Kullanici.ad;
                izin.baslamaTarihi = i.baslamaTarihi;
                izin.bitisTarihi = i.bitisTarihi;
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Izin/Izinler");
        }
        public ActionResult Sil(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var izin = db.Izin.Where(x => x.izinID == id).SingleOrDefault();
                db.Izin.Remove(izin);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Izin/Izinler");
            
        }

        public ActionResult Onayla(int id)
        {
            KuaforContext db = new KuaforContext();

            Izin i = db.Izin.Where(x => x.izinID == id).SingleOrDefault();

            i.onayliMi = true;

            db.SaveChanges();

            return RedirectToAction("Izinler");

            //return View("~/Views/Randevu/Randevular.cshtml");
        }
    }
}
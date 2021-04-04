using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class PersonelIzinController : Controller
    {
        // GET: PersonelIzin
        public ActionResult Izinler()
        {
            KuaforContext db = new KuaforContext();

            Kullanici k = (Kullanici)Session["Kullanici"];

            ViewBag.IzinListe = db.Izin.Where(x => x.onayliMi == false && x.kullaniciID == k.kullaniciID).ToList();

            ViewBag.IzinListeOnayli = db.Izin.Where(x => x.onayliMi == true && x.kullaniciID == k.kullaniciID).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Izin izin)
        {
            KuaforContext db = new KuaforContext();
            Kullanici k = (Kullanici)Session["Kullanici"];
            bool sonuc = false;
            try
            {
                izin.onayliMi = false;
                izin.kullaniciID = k.kullaniciID;
                db.Izin.Add(izin);
                db.SaveChanges();
                sonuc = true;
                int izinID = izin.izinID; // yeni eklenen randevunun id bilgisi
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/PersonelIzin/Izinler");
        }

        public ActionResult Sil(int id)
        {

            KuaforContext db = new KuaforContext();

            try
            {
                Izin i = db.Izin.Where(x => x.izinID == id).SingleOrDefault();

                db.Izin.Remove(i);
                db.SaveChanges();
                TempData["Sonuc"] = true;

            }
            catch (Exception)
            {
                TempData["Sonuc"] = false;
            }

            return RedirectToAction("Izinler");

        }
    }
}
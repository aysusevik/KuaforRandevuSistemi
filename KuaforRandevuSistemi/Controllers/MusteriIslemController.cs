using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class MusteriIslemController : Controller
    {
        // GET: MusteriIslem
        public ActionResult Anasayfa()
        {
            KuaforContext db = new KuaforContext();

            Musteri m = (Musteri)Session["Musteri"];

            List<Randevu> randevuListesi = db.Randevu.Include("Firma").Include("Kullanici").Include("RandevuHizmet").Where(x => x.musteriID == m.musteriID).ToList();



            return View(randevuListesi);
        }


        [HttpPost]
        public ActionResult Degerlendir(string degerlendirme, int id)
        {
            try
            {
                KuaforContext db = new KuaforContext();

                Randevu r = db.Randevu.Where(x => x.randevuID == id).SingleOrDefault();

                r.degerlendirme = degerlendirme;

                db.SaveChanges();

                TempData["Sonuc"] = true;
            }
            catch (Exception)
            {
                TempData["Sonuc"] = false;
            }

            return RedirectToAction("Anasayfa");
        }
    }
}
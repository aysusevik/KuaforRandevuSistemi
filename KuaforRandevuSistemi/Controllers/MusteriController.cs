using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        public ActionResult Musteriler()
        {
            KuaforContext db = new KuaforContext();

            ViewBag.MusteriListe = db.Musteri.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Musteri musteri)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;

            try
            {
                musteri.resim = "bos.png";
                db.Musteri.Add(musteri);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            TempData["Sonuc"] = sonuc;

            return Redirect("/Musteri/Musteriler");
        }

        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();
            var musteri = db.Musteri.Find(id);
            return View(musteri);
        }

        [HttpPost]
        public ActionResult Duzenle(Musteri m) //Formdan gelen bilgiler
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;

            try
            {
                var musteri = db.Musteri.Find(m.musteriID); //mevcut müsteri bilgisini bul.
                musteri.ad = m.ad; // mevcut bilgilerin yerine yeni gelen bilgileri aldık.
                musteri.soyad = m.soyad;
                musteri.telefonNo = m.telefonNo;
                musteri.email = m.email;
                db.SaveChanges(); // kaydettik.
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            TempData["Sonuc"] = sonuc;

            return Redirect("/Musteri/Musteriler");//müşteri listesine geri döndürdük.
        }

        public ActionResult Sil(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var musteri = db.Musteri.Where(x => x.musteriID == id).SingleOrDefault();

                if (musteri.Randevu.Count() > 0) // müşterinin randevu kaydı var
                {
                    TempData["MusteriSil"] = "randevuVar";
                    return Redirect("/Musteri/Musteriler");
                }

                else // randevu kaydı olmayan musteri silinebilir.
                {
                    db.Musteri.Remove(musteri);
                    db.SaveChanges();
                    sonuc = true;
                }

            }
            catch (Exception)
            {
                sonuc = false;
            }

            TempData["Sonuc"] = sonuc;


            return Redirect("/Musteri/Musteriler");
        }

        public ActionResult Durum(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            var musteri = db.Musteri.Find(id); // musteriyi bul.

            try
            {
                if (musteri.aktifMi == true) // durumu aktif ise
                {
                    musteri.aktifMi = false; // pasif yap
                }
                else // pasif ise 
                {
                    musteri.aktifMi = true; // aktif yap
                }

                db.SaveChanges(); // değişiklikleri kaydet

                sonuc = true;

            }
            catch (Exception)
            {
                sonuc = false;
            }

            TempData["Sonuc"] = sonuc;
            return RedirectToAction("Musteriler"); // listeye don
        }
    }
}
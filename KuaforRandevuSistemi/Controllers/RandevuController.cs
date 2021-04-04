using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class RandevuController : Controller
    {
        // GET: Randevu
        public ActionResult Randevular()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            KuaforContext db = new KuaforContext();
            DateTime gun = DateTime.Now.Date;


            ViewBag.RandevuListe = db.Randevu.Include("RandevuHizmet").Where(x => x.onayliMi == true).Where(y => y.tarih.Value >= gun && y.firmaID == k.firmaID).ToList();// Yöneticinin eklediği veya müşterinin talep ettiklerinden onaylanan randevular burada listelenecek.

            ViewBag.GecmisRandevuListe = db.Randevu.Include("Kullanici").Where(x => x.tarih.Value < gun && x.firmaID == k.firmaID).ToList();

            ViewBag.MusteriListe = new SelectList(db.Musteri.Where(y => y.aktifMi == true).Select(x => new
            {
                x.musteriID,
                adSoyad = x.ad + " " + x.soyad
            }), "musteriID", "adSoyad");

            ViewBag.PersonelListe = new SelectList(db.Kullanici.Select(x => new
            {
                x.Izin,
                x.firmaID,
                x.kullaniciID,
                adSoyad = x.ad + " " + x.soyad,

            }).Where(x => x.firmaID == k.firmaID && x.Izin.Where(y => y.kullaniciID == x.kullaniciID && y.baslamaTarihi <= DateTime.Now && y.bitisTarihi >= DateTime.Now).Count() == 0), "kullaniciID", "adSoyad");

            ViewBag.MusteriRandevuTalep = db.Randevu.Where(x => x.onayliMi == false && x.firmaID == k.firmaID).ToList(); // eğer randevu onaylı değilse müşteri talep etmiştir.

            ViewBag.Hizmet = new SelectList(db.Hizmet.Select(x => new
            {
                x.hizmetID,
                adUcret = x.ad + "-" + x.ucret + "₺"
            }), "hizmetID", "adUcret");

            return View();

        }

        [HttpPost]
        public ActionResult Ekle(Randevu randevu, List<int> hizmetID)
        {
            KuaforContext db = new KuaforContext();

            Kullanici k = (Kullanici)Session["Kullanici"];

            bool sonuc = false;

            try
            {
                randevu.onayliMi = true;
                randevu.geldiMi = false;
                randevu.firmaID = k.firmaID;
                db.Randevu.Add(randevu);
                db.SaveChanges();

                int randevuID = randevu.randevuID; // yeni eklenen randevunun id bilgisi

                foreach (int item in hizmetID)
                {
                    RandevuHizmet rh = new RandevuHizmet();
                    rh.randevuID = randevuID;
                    rh.hizmetID = item;
                    db.RandevuHizmet.Add(rh);
                    db.SaveChanges();
                }

                sonuc = true;
            }

            catch (Exception)
            {
                sonuc = false;
            }

            TempData["Sonuc"] = sonuc;

            return Redirect("/Randevu/Randevular");
        }
        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();

            var randevu = db.Randevu.Find(id); //id'ye ait randevu bilgilerini bul.

            ViewBag.MusteriListe = new SelectList(db.Musteri.Select(x => new
            {
                x.musteriID,
                adSoyad = x.ad + " " + x.soyad
            }), "musteriID", "adSoyad", randevu.musteriID);

            ViewBag.PersonelListe = new SelectList(db.Kullanici.Select(x => new
            {
                x.kullaniciID,
                adSoyad = x.ad + " " + x.soyad
            }), "kullaniciID", "adSoyad");

            ViewBag.Hizmet = new MultiSelectList(db.Hizmet.Select(x => new
            {
                x.hizmetID,
                adUcret = x.ad + "-" + x.ucret + "₺"
            }), "hizmetID", "adUcret", randevu.RandevuHizmet.Select(x => x.hizmetID).ToList());

            return View(randevu);
        }
        [HttpPost]
        public ActionResult Duzenle(Randevu r)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var randevu = db.Randevu.Find(r.randevuID);
                randevu.tarih = r.tarih;
                randevu.saat = r.saat;
                randevu.musteriID = r.musteriID;
                randevu.kullaniciID = r.kullaniciID;
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Randevu/Randevular");
        }
        public ActionResult Sil(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var randevu = db.Randevu.Where(x => x.randevuID == id).SingleOrDefault();

                if (randevu.geldiMi == false)
                {
                    var hizmet = db.RandevuHizmet.Where(x => x.randevuID == id).ToList();

                    if (hizmet != null)
                    {
                        foreach (var item in hizmet)
                        {
                            db.RandevuHizmet.Remove(item);
                            db.SaveChanges();
                        }
                    }
                    

                    db.Randevu.Remove(randevu);
                    db.SaveChanges();
                    sonuc = true;
                }
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Randevu/Randevular");

        }

        public ActionResult Onayla(int id)
        {
            KuaforContext db = new KuaforContext();

            Randevu r = db.Randevu.Where(x => x.randevuID == id).SingleOrDefault();

            r.onayliMi = true;

            db.SaveChanges();

            return RedirectToAction("Randevular");

            //return View("~/Views/Randevu/Randevular.cshtml");
        }

        public ActionResult DurumDegistir(int id)
        {
            KuaforContext db = new KuaforContext();

            Randevu r = db.Randevu.Where(x => x.randevuID == id).SingleOrDefault();

            if (r.geldiMi == true)
            {
                r.geldiMi = false;
            }
            else
            {
                r.geldiMi = true;
            }
            db.SaveChanges();
            return RedirectToAction("Randevular");
        }
    }
}
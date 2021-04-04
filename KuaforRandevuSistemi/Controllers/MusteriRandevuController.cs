using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class MusteriRandevuController : Controller
    {
        // GET: MusteriRandevu
        public ActionResult Randevular()
        {
            KuaforContext db = new KuaforContext();
            DateTime gun = DateTime.Now.Date;
            Kullanici k = new Kullanici();

            Musteri m = (Musteri)Session["Musteri"]; //Giriş yapan müşteri bilgileri            

            ViewBag.RandevuListe = db.Randevu.Where(x => x.onayliMi == false && x.musteriID == m.musteriID).ToList();

            ViewBag.OnayliRandevu = db.Randevu.Where(x => x.onayliMi == true && x.musteriID == m.musteriID).ToList();

            ViewBag.GecmisRandevuListe = db.Randevu.Where(x => x.tarih.Value < gun).ToList();

            ViewBag.Il = new SelectList(db.Il.OrderBy(x=>x.ilAdi).ToList(), "ilID", "ilAdi");

            ViewBag.Ilce = new SelectList(db.Ilce.Where(x => x.ilID == 1).ToList(), "ilceID", "ilceAdi");

            ViewBag.Firma = new SelectList(db.Firma.ToList(), "firmaID", "ad");

            ViewBag.PersonelListe = new SelectList(db.Kullanici.Select(x => new
            {
                x.Izin,
                x.firmaID,
                x.kullaniciID,
                adSoyad = x.ad + " " + x.soyad,

            }).Where(x=>x.firmaID == k.firmaID && x.Izin.Where(y=>y.kullaniciID == x.kullaniciID && y.baslamaTarihi<= DateTime.Now && y.bitisTarihi>=DateTime.Now).Count() == 0), "kullaniciID", "adSoyad");

            ViewBag.Hizmet = new MultiSelectList(db.Hizmet.Select(x => new
            {
                x.hizmetID,
                adUcret = x.ad + "-" + x.ucret + "₺"
            }), "hizmetID", "adUcret");

            return View();
        }

        public JsonResult ilceleriGetir(string id)
        {
            KuaforContext db = new KuaforContext();

            int ilID = Convert.ToInt32(id);

            return Json(db.Ilce.Where(x => x.ilID == ilID).Select(x => new
            {
                Text = x.ilceAdi,
                Value = x.ilceID
            }));
        }

        public JsonResult firmalariGetir(string id)
        {
            KuaforContext db = new KuaforContext();

            int ilceID = Convert.ToInt32(id);

            return Json(db.Firma.Where(x => x.ilceID == ilceID).Select(x => new
            {
                Text = x.ad,
                Value = x.firmaID
            }));
        }

        public JsonResult personelGetir(string id)
        {
            KuaforContext db = new KuaforContext();

            int firmaID = Convert.ToInt32(id);

            int yetkiID = db.Yetki.Where(x => x.yetkiAd == "Personel").SingleOrDefault().yetkiID;

            return Json(db.Kullanici.Where(x => x.firmaID == firmaID && x.yetkiID == yetkiID && x.Izin.Where(y=>y.kullaniciID == x.kullaniciID && y.baslamaTarihi<= DateTime.Now && y.bitisTarihi>= DateTime.Now).Count()==0).Select(x => new
            {
                Text = x.ad + " " + x.soyad,
                Value = x.kullaniciID
            }));
        }

        [HttpPost]
        public ActionResult Ekle(Randevu randevu, List<int> hizmetID)
        {
            KuaforContext db = new KuaforContext();
            Musteri m = (Musteri)Session["Musteri"];
            bool sonuc = false;

            try
            {
                randevu.onayliMi = false;
                randevu.geldiMi = false;
                randevu.musteriID = m.musteriID;
                //randevu.firmaID = m.Randevu.Where(x => x.firmaID);
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

            return Redirect("/MusteriRandevu/Randevular");
        }

        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();

            Randevu randevu = db.Randevu.Include("RandevuHizmet").Where(x => x.randevuID == id).SingleOrDefault();

            ViewBag.PersonelListe = new SelectList(db.Kullanici.Select(x => new
            {
                x.kullaniciID,
                adSoyad = x.ad + " " + x.soyad
            }), "kullaniciID", "adSoyad", randevu.kullaniciID);

            ViewBag.Hizmet = new MultiSelectList(db.Hizmet.Select(x => new
            {
                x.hizmetID,
                adUcret = x.ad + "-" + x.ucret + "₺"
            }), "hizmetID", "adUcret", randevu.RandevuHizmet.Select(x => x.hizmetID).ToList());

            return View(randevu);
        }
        [HttpPost]
        public ActionResult Duzenle(Randevu r, List<int> hizmetID)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                Randevu randevu = db.Randevu.Include("RandevuHizmet").Where(x => x.randevuID == r.randevuID).SingleOrDefault();
                randevu.tarih = r.tarih;
                randevu.saat = r.saat;
                randevu.kullaniciID = r.kullaniciID;
                db.SaveChanges();
                sonuc = true;

                List<int> mevcutHizmetler = randevu.RandevuHizmet.Select(x => x.hizmetID).ToList(); // 3 , 7

                foreach (int yeniHizmet in hizmetID) // 3
                {
                    if (randevu.RandevuHizmet.Where(x => x.hizmetID == yeniHizmet).Count() == 0)
                    {
                        RandevuHizmet rh = new RandevuHizmet();
                        rh.randevuID = randevu.randevuID;
                        rh.hizmetID = yeniHizmet;
                        db.RandevuHizmet.Add(rh);
                        db.SaveChanges();
                    }

                    foreach (var eskiHizmet in mevcutHizmetler)
                    {
                        if (eskiHizmet != yeniHizmet)
                        {
                            RandevuHizmet rg = db.RandevuHizmet.Where(x=>x.hizmetID == eskiHizmet && x.randevuID == r.randevuID).SingleOrDefault();
                            db.RandevuHizmet.Remove(rg);
                            db.SaveChanges();
                        }
                    }

                }
               
            }
            catch (Exception ex)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/MusteriRandevu/Randevular");
        }
        public ActionResult Sil(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var randevu = db.Randevu.Where(x => x.randevuID == id).SingleOrDefault();
                db.Randevu.Remove(randevu);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/MusteriRandevu/Randevular");
        }
    }
}
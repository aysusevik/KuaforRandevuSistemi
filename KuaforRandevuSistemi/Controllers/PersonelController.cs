using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class PersonelController : Controller
    {
        public ActionResult Personeller()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            KuaforContext db = new KuaforContext();
            ViewBag.PersonelListe = db.Kullanici.Where(x=>x.yetkiID == 2 && x.firmaID == k.firmaID).ToList(); //Yetkisi personel olan kullanıcı bilgilerini listele ve PersonelListe'ye at.
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Kullanici personel)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                personel.resim = "bos.png";
                db.Kullanici.Add(personel);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {

                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Personel/Personeller");
        }
        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();
            var personel = db.Kullanici.Find(id);
            return View(personel);

        }
        [HttpPost]
        public ActionResult Duzenle(Kullanici p)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var personel = db.Kullanici.Find(p.kullaniciID);
                personel.ad = p.ad;
                personel.soyad = p.soyad;
                personel.telefonNo = p.telefonNo;
                personel.eposta = p.eposta;
                personel.adres = p.adres;
                personel.iseBaslamaTarihi = p.iseBaslamaTarihi;
                personel.cikisTarihi = p.cikisTarihi;
                personel.maas = p.maas;
                //personel.egitimDurumu = p.egitimDurumu;
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Personel/Personeller");
        }
        public ActionResult Sil(int id)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var personel = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
                db.Kullanici.Remove(personel);
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Personel/Personeller");

        }

        public ActionResult Izin(int id)
        {
            KuaforContext db = new KuaforContext();

            var izin = db.Izin.Where(x => x.kullaniciID == id).ToList();

            ViewBag.Personel = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();

            return View(izin);
        }


    }
}
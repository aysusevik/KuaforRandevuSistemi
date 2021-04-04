using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Firma()
        {
            return View();
        }       
       

        [HttpPost]
        public ActionResult Firma(string eposta, string parola)
        {
            KuaforContext db = new KuaforContext();
            Kullanici k = db.Kullanici.Where(x => x.eposta == eposta && x.parola == parola).SingleOrDefault();
            if (k == null) //Kullanıcı bilgisi boş veya eşleşmiyorsa
            {
                ViewBag.Sonuc = "E-posta veya Şifre Yanlış";
                return View();
            }
            else
            {
                Session["Kullanici"] = k;
                return Redirect("Yonetici/Anasayfa");
            }
        }

        public ActionResult FirmaKayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FirmaKayit(string firmaAdi, string ad, string soyad, string eposta, string parola)
        {
            KuaforContext db = new KuaforContext();
            Firma f = new Firma();
            f.ad = firmaAdi;
            db.Firma.Add(f);
            Kullanici k = new Kullanici();
            k.firmaID = f.firmaID;
            k.yetkiID = 1;
            k.ad = ad;
            k.soyad = soyad;
            k.eposta = eposta;
            k.parola = parola;

            db.Kullanici.Add(k);
            db.SaveChanges();


            return Redirect("/Login/Firma");
        }

        public ActionResult Musteri()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Musteri(string email, string sifre)
        {
            KuaforContext db = new KuaforContext();
            Musteri m  = db.Musteri.Where(x => x.email == email && x.sifre == sifre).SingleOrDefault();
            if (m == null) //Kullanıcı bilgisi boş veya eşleşmiyorsa
            {
                ViewBag.Sonuc = "E-posta veya Şifre Yanlış";
                return View();
            }
            else
            {
                Session["Musteri"] = m;
                return Redirect("/MusteriIslem/Anasayfa");
            }
        }
        public ActionResult MusteriKayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MusteriKayit(string ad, string soyad, string eposta, string parola)
        {
            KuaforContext db = new KuaforContext();
            Musteri m = new Musteri();
            m.ad = ad;
            m.soyad = soyad;
            m.email = eposta;
            m.sifre = parola;
            db.Musteri.Add(m);
            db.SaveChanges();

            return Redirect("/Login/Musteri");
        }

        public ActionResult Cikis()
        {
            Session.Abandon();
            return Redirect("/Login/Firma");
        }

    }
}
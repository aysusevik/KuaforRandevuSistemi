using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class KullaniciController : Controller
    { 
        // GET: Kullanici
        public ActionResult Index()
        {
            KuaforContext db = new KuaforContext();
            Kullanici k = (Kullanici)Session["Kullanici"];
            Kullanici ku = db.Kullanici.Find(k.kullaniciID);
            return View(ku);
        }
        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();
            Kullanici kullanici = db.Kullanici.Find(id);
            return View(kullanici);

        }
        [HttpPost]
        public ActionResult Duzenle(Kullanici k)
        {
            KuaforContext db = new KuaforContext();
            bool sonuc = false;
            try
            {
                var kullanici = db.Kullanici.Find(k.kullaniciID);
                kullanici.ad = k.ad;
                kullanici.soyad = k.soyad;
                kullanici.telefonNo = k.telefonNo;
                kullanici.eposta = k.eposta;
                db.SaveChanges();
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            TempData["Sonuc"] = sonuc;
            return Redirect("/Kullanici/Index");
        }
    }
}
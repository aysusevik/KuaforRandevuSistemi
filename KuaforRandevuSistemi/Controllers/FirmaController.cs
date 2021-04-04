using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class FirmaController : Controller
    {
        // GET: Firma

        KuaforContext db = new KuaforContext();
        
        public ActionResult Index()
        {
            Kullanici k = (Kullanici)Session["Kullanici"]; //Giriş yapan kullanıcı bilgisini değişkene atadık.
            Firma f = db.Firma.Where(x => x.firmaID == k.firmaID).SingleOrDefault();   
            return View(f);
        }
        public ActionResult Duzenle(int id)
        {
            KuaforContext db = new KuaforContext();

            Firma firma = db.Firma.Find(id);

            return View(firma);

        }
        [HttpPost]
        public ActionResult Duzenle(Firma f) //Formdan gelen bilgiler
        {

            bool sonuc = false;

            try
            {
                var firma = db.Firma.Find(f.firmaID); 
                firma.ad = f.ad; 
                firma.adres = f.adres;
                firma.telefon = f.telefon;
                db.SaveChanges(); // kaydettik.
                sonuc = true;

            }
            catch (Exception)
            {
                sonuc = false;
            }

            TempData["Sonuc"] = sonuc;

            return Redirect("/Firma/Index");
        }
    }
}
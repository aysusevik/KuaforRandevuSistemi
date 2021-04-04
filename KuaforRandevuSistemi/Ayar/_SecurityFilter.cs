using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Ayar
{
    public class _SecurityFilter : ActionFilterAttribute
    {
     
        // Her action dan önce çalışacak fonksiyon
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Controller ve Action isimlerini aldık
            string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string ActionName = filterContext.ActionDescriptor.ActionName;

            
            if (ControllerName == "Login") // Login controller a girmek istiyorsa bir kontrola gerek yok
            {
                base.OnActionExecuting(filterContext);
            }
            else if (HttpContext.Current.Session["Kullanici"] == null && HttpContext.Current.Session["Musteri"] == null) // Diğer bütün sayfalarda mutlaka session olması gerekyor.
            {
                filterContext.Result = new RedirectResult("/Login/Firma?sonuc=yetkisiz");
                return;
            }
            else if (HttpContext.Current.Session["Kullanici"] != null) // Yönetici veya personel
            {
                // Giriş yapan çalışanın hangi yetkileri var
                Kullanici k = (Kullanici)HttpContext.Current.Session["Kullanici"];
                KuaforContext db = new KuaforContext();
                YetkiSayfa sayfa = db.YetkiSayfa.Where(x => x.controllerName == ControllerName && x.actionName == ActionName).SingleOrDefault();
                if(sayfa==null) // veritabanına eklenmeyen sayfalarda bu hata alınır
                {
                    filterContext.Result = new RedirectResult("/Yonetici/AnaSayfa?sonuc=sayfaTanimsiz");
                    return;
                }
                if ((k.Yetki.yetkiAd=="Yonetici" && (bool)sayfa.yonetici) || (k.Yetki.yetkiAd == "Personel" && (bool)sayfa.personel))
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Yonetici/AnaSayfa?sonuc=yetkisiz");
                    return;
                }
            }
            else if (HttpContext.Current.Session["Musteri"] != null) // Müşteri giriş yaptıysa
            {
                Musteri m = (Musteri)HttpContext.Current.Session["Musteri"];
                KuaforContext db = new KuaforContext();
                YetkiSayfa sayfa = db.YetkiSayfa.Where(x => x.controllerName == ControllerName && x.actionName == ActionName).SingleOrDefault();
                if (sayfa == null) // veritabanına eklenmeyen sayfalarda bu hata alınır
                {
                    filterContext.Result = new RedirectResult("/MusteriIslem/Anasayfa?sonuc=sayfaTanimsiz");
                    return;
                }
                if ((bool)sayfa.musteri)
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectResult("/MusteriIslem/Anasayfa?sonuc=yetkisiz");
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
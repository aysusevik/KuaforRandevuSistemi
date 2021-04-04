using KuaforRandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaforRandevuSistemi.Controllers
{
    public class YoneticiController : Controller
    {
        // GET: Yonetici
        public ActionResult AnaSayfa()
        {
            KuaforContext db = new KuaforContext();

            ViewBag.Randevular = db.Randevu.Where(x => x.onayliMi == true).ToList();

            var randevular = db.Randevu.Where(x => x.onayliMi == true).ToList();
            string veri = "";
            //"start:" + r.tarih.Value.Year + "-" + r.tarih.Value.Month + "-" + r.tarih.Value.Day + "," +
            foreach (Randevu r in randevular)
            {
                veri = veri + "{title:'" + r.Musteri.ad + " " + r.Musteri.soyad + "'," +
                "start:new Date(" + r.tarih.Value.Year + ", " + (r.tarih.Value.Month - 1) + ", " + r.tarih.Value.Day + ", " + r.saat.Value.Hours + ", " + r.saat.Value.Minutes + ")," +
                "backgroundColor:" + "'#f39c12'" + "," +
                "borderColor:" + "'#f39c12'" + "," +
               "allDay: false" +
                "},";

            }
            ViewBag.veri = veri;
            return View();
        }


    }
}
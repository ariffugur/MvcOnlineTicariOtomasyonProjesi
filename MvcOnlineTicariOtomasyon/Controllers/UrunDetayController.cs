using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c = new Context();
        public ActionResult Index()
        {
            var urunler = from x in c.Uruns select x;
            return View(urunler.ToList());
            //Class1 cs = new Class1();
            ////var degerler=c.Uruns.Where(x=>x.UrunId==1).ToList();
            //cs.Deger1 = c.Uruns.Where(x => x.UrunId == 1).ToList();
            //cs.Deger2 = c.Detays.Where(y => y.DetayId == 1).ToList();
            //return View(cs);
        }
        public ActionResult UrunuGoruntule(int id)
        {
            var urundeger = c.Uruns.Find(id);
            ViewBag.urunAd=urundeger.UrunAd;
            ViewBag.satisFiyat = urundeger.SatisFiyati.ToString();
            ViewBag.alisFiyat = urundeger.AlisFiyati.ToString();
            ViewBag.aciklama = urundeger.Aciklama;
            ViewBag.marka = urundeger.Marka;
            ViewBag.stok = urundeger.Stok;
            ViewBag.gorsel = urundeger.UrunGorsel;
            ViewBag.kategori = urundeger.Kategori.ToString();
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailId=c.Carilers.Where(x=>x.CariMail==mail).Select(y=>y.CariId).FirstOrDefault();
            ViewBag.mid = mailId;
            var toplamSatis=c.SatisHarekets.Where(x=>x.CariId==mailId).Count();
            ViewBag.toplamSatis=toplamSatis;
            var toplamTutar=c.SatisHarekets.Where(x=>x.CariId == mailId).Sum(y=>y.ToplamTutar);
            var toplamUrunSayisi=c.SatisHarekets.Where(x=>x.CariId == mailId).Sum(y=>y.Adet);
            ViewBag.toplamTutar=toplamTutar;
            ViewBag.toplamUrunSayisi = toplamUrunSayisi;
            var adSoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adSoyad=adSoyad;
            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.CariId == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidenSayisi;
            ViewBag.d1 = gelenSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(z => z.MesajID).ToList();
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidenSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();
            var mail = (string)Session["CariMail"];
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidenSayisi;
            ViewBag.d1 = gelenSayisi;
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidenSayisi;
            ViewBag.d1 = gelenSayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var mail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail;
            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(p));

            return View(k.ToList());
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
        public PartialViewResult Partial1()
        {

            var mail = (string)Session["CariMail"];
            var id=c.Carilers.Where(x=>x.CariMail==mail).Select(y=>y.CariId).FirstOrDefault();
            var cariBul = c.Carilers.Find(id);
            return PartialView("Partial1", cariBul);
        }
    }
}
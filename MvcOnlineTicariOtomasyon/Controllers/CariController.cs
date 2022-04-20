using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c=new Context();
        public ActionResult Index()
        { 
            var degerler=c.Carilers.Where(x=>x.Durum==true).ToList();  
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler p)
        {
            c.Carilers.Add(p);
            p.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var cr = c.Carilers.Find(id);
            cr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
using MVCYonetimPaneli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCYonetimPaneli.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        //Db ye Contex sınıfından bağlanılması
        KuzeyYeliContext ctx = new KuzeyYeliContext();
        // GET: Supplier

        public ActionResult Index()
        {
            List<Tedarikciler> suppliers = ctx.Tedarikcilers.ToList();
            return View(suppliers);
        }
        //Tedarikçi ekleme GET
        public ActionResult SupplierAdd()
        {
            return View();
        }

        //Tedarikçi ekleme POST
        [HttpPost]
        public ActionResult SupplierAdd(Tedarikciler spr)
        {
            ctx.Tedarikcilers.Add(spr);
            ctx.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public string SupplierRemove(int id)
        {
            Tedarikciler sp = ctx.Tedarikcilers.FirstOrDefault(x => x.TedarikciID == id);
            ctx.Tedarikcilers.Remove(sp);
            try
            {
                ctx.SaveChanges();
                return "başarılı";
            }
            catch (Exception)
            {
                return "hata";
            }
        }
    }
}
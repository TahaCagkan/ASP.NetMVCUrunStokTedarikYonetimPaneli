using MVCYonetimPaneli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCYonetimPaneli.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        //Db ye Contex sınıfından bağlanılması

        KuzeyYeliContext ctx = new KuzeyYeliContext();
        // GET: Category
        public ActionResult Index()
        {
            List<Kategoriler> ctg = ctx.Kategorilers.ToList();
            return View(ctg);
        }
        //Kategori Ekleme GET
        public ActionResult CategoryAdd()
        {
            return View();
        }
        //Kategori Ekleme POST

        [HttpPost]
        public ActionResult CategoryAdd(Kategoriler ctg)
        {
            ctx.Kategorilers.Add(ctg);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //Kategori Silme işlemi
        [HttpPost]
        public void CategoryRemove(int id)
        {
            Kategoriler ktg = ctx.Kategorilers.FirstOrDefault(x => x.KategoriID == id);
            ctx.Kategorilers.Remove(ktg);
            ctx.SaveChanges();
        }
    }
}
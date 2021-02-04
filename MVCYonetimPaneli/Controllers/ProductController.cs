using MVCYonetimPaneli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCYonetimPaneli.Controllers
{
    using App_Classes;

    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        //Db ye Contex sınıfından bağlanılması
        KuzeyYeliContext ctx = new KuzeyYeliContext();
        public ActionResult Index()
        {
            //Ürünlerin listelenmesi
            List<Urunler> products = ctx.Urunlers.ToList();
            return View(products);
        }
        //Ürün Ekleme GET sayfası
        public ActionResult ProductAdd()
        {
            ViewBag.Categories = ctx.Kategorilers.ToList();
            ViewBag.Supplies = ctx.Tedarikcilers.ToList();
            return View();
        }
        //Ürün Ekleme POST
        [HttpPost]
        public ActionResult ProductAdd(Urunler prd)
        {
            ctx.Urunlers.Add(prd);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //Ürünler Sil işlemi
        [HttpPost]
        public void ProductRemove(int id)
        {
            Urunler prd = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            ctx.Urunlers.Remove(prd);
            ctx.SaveChanges();

        }

        //QueryString e göre Ürün Detay bilgilerini getirme
        public ActionResult ProductDetail()
        {
            int id =Convert.ToInt32(Request.QueryString["id"].ToString());
            string adi = Request.QueryString["adi"].ToString();
            Urunler ur = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            return View(ur);
        }
        //Sepete At 
        [HttpPost]
        public void BoxSend(int id)
        {
            Box box;
            //Hiç sepet yoksa sepet oluştuldu.
            if (Session["ActiveBox"] == null)
            {
                 box = new Box();
            }
            else
            {
                //Akitf Sepet null değil ise Box cast edilebilir.
                 box = (Box)Session["ActiveBox"];
            }
            //Ürünlere ait ürün seçilmesi için Linq kullanıldı.
            Urunler prd = ctx.Urunlers.FirstOrDefault(p => p.UrunID == id);
            box.Product.Add(prd);
            //bu sepeti Aktif Sepete at
            Session["ActiveBox"] = box;
        }

          //Sepetteki Ürünler Sil işlemi
        [HttpPost]
        public void BoxProductRemove(int id)
        {
            /*
            Box prd = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            ctx.Urunlers.Remove(prd);
            ctx.SaveChanges();
            */
        }


    }
}
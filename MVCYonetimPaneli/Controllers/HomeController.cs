using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCYonetimPaneli.Controllers
{
    using Models;
    using App_Classes;

    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Aktif Kullanıcı ve Toplam Ziyaretçi,Global.asax da Session Start içerisinde Application durumun da tutulamktadır. 
            ViewBag.ActiveUser = HttpContext.Application["ActiveUser"];
            ViewBag.TotalVisiter = HttpContext.Application["TotalVisiter"];
            return View();
        }

        //Çerez Yöntemi ile bilgileri getirme işlemleri,GET
        public ActionResult CookieSend()
        {
            return View();
        }

        //Çerez Yönetimi Post
        [HttpPost]
        public ActionResult CookieSend(string CookieAdi, string CookieDegeri)
        {
            HttpCookie ck = new HttpCookie(CookieAdi);
            ck.Value = CookieDegeri;
            ck.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(ck);
            return View();
        }

        //Çerez Okuma 
        public ActionResult CookieRead()
        {
            string deger = Response.Cookies["Grup Kodu"].Value;
            ViewBag.Cerez = deger;
            return View(deger);
        }
        
        //Sepetimdeki Ürünler
        public ActionResult MyBox()
        {
            //Sepetimde ürün yok ise boş liste döndür,boş değil ise Mevcut ürünleri listele
            List<Urunler> product = new List<Urunler>();
            if(Session["ActiveBox"] != null)
            {
                Box bx = (Box)Session["ActiveBox"];
                product = bx.Product;
            }
                return View(product);
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCYonetimPaneli.Controllers
{
    using App_Classes;
    using System.Web.Security;

    [Authorize]
    public class MemberController : Controller
    {
        // GET: Member
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        //Login kısmı
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Users us,string Hatirla)
        {
            //MemberShip içerisinde Kullanıcı kontrolleri sağlanıcaktır,
            //Eğer kullanıcı Beni hatırla derse cookies kullanıcı adı on olucak değil ise false olucaktır
            bool sonuc = Membership.ValidateUser(us.KullaniciAdi, us.Parola);
            if (sonuc)
            {
                if (Hatirla == "on")
                    FormsAuthentication.RedirectFromLoginPage(us.KullaniciAdi, true);
                else
                    FormsAuthentication.RedirectFromLoginPage(us.KullaniciAdi, false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı adı veya Parola Hatalı!";
                return View();
            }
        }

        //Çıkış kısmı
        public ActionResult ExitNow()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(Users us)
        {
            //Üye olan kullanıcıya göre parolayı Gizli Soru ve Cevaba göre değişitme işlemi
            MembershipUser mu = Membership.GetUser(us.KullaniciAdi);

            if (mu.PasswordQuestion == us.GizliSoru)
            {
                string pwd = mu.GetPassword(us.GizliCevap);
                mu.ChangePassword(pwd, us.Parola);

                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Mesaj = "Girilen bilgiler yanlıştı!";
                return View();

            }
        }
    }
}
using MVCYonetimPaneli.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCYonetimPaneli.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        //Kullanıcı index sayfası
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();

            return View(users);
        }
        //Kullanıcı Ekleme GET
        [AllowAnonymous]
        public ActionResult UsersAdd()
        {
            return View();
        }
        //Kullanıcı Ekleme Post
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UsersAdd(Users users)
        {
            //MemberShip işlemleri User sınıfını kullanarak yapıldı
            MembershipCreateStatus statusd;
            string mesaj = "";
            Membership.CreateUser(users.KullaniciAdi, users.Parola, users.Email, users.GizliSoru, users.GizliCevap,true,out statusd);

            switch (statusd)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "Geçersiz Kullanıcı Adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "Geçersşz Parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "Geçersiz Gizli Soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "Geçersşz Gizli Cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "Geçersşz Mail Adresi";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Kullanılmış Kullanıcı Adı";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Kullanılmış Mail Adresi Girildi.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += "Kullanıcı Engel hatası";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += "Geçersiz Kullanıcı anahtar hatası";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Kullanılmış Kullanıcı anahtar hatası";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += "Üye Yönetimi sağlayıcı hatası";
                    break;
                default:
                    break;
            }
            //Aynı return View oldupu için,html kısmında sectiona göre mesaj dönücektir.
            ViewBag.Mesaj = mesaj;
            if (statusd == MembershipCreateStatus.Success)
                return RedirectToAction("Index");
            else
                return View();
        }

        //Role Atama,Sadece Admin kullanıcı açabilir
        [Authorize(Roles="Admin")]
        public ActionResult RoleSend()
        {
            //Rolleri listeleme ve tüm üyelere ait Kullanıcıları getir.
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            ViewBag.Kullnicilar = Membership.GetAllUsers();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RoleSend(string[] KullaniciAdi, string RoleAdi)
        {
            Roles.AddUsersToRole(KullaniciAdi, RoleAdi);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string UsersRoles(string userName)
        {
            //Kullanıcı adlarını Role içerisindeki al listele ve roles Liste tipinde olan string değişkeni içerisine at
            List<string> roles = Roles.GetRolesForUser(userName).ToList();
            string rol = "";
            foreach (string r in roles)
            {
                rol += r + "-";
            }
            rol = rol.Remove(rol.Length - 1, 1);
            return rol;
        }
    }
}
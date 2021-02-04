using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCYonetimPaneli.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            List<string> roles = Roles.GetAllRoles().ToList();
            return View(roles);
        }
        //Rol ekleme Get
        public ActionResult RoleAdd()
        {
            return View();
        }
        //Rol ekleme Post

        public ActionResult RoleAdd(string RolAdi)
        {
            Roles.CreateRole(RolAdi);
            return RedirectToAction("Index");
        }

    }
}
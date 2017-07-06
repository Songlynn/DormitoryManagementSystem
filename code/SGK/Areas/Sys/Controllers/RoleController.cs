using SGK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGK.Areas.Sys.Controllers
{
    public class RoleController : Controller
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        // GET: Sys/Role
        public ActionResult Index()
        {
            var role = from r in db.T_Role where 1 == 1 select r;
            ViewBag.gridRole = role.ToList();
            return View();
        }
    }
}
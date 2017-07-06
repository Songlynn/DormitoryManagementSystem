using SGK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGK.Areas.Sys.Controllers
{
    public class AccountController : Controller
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        // GET: Sys/Account
        public ActionResult Index()
        {
            var user = from a in db.vw_Account where 1 == 1 select a;
            ViewBag.gridAccount = user.ToList();
            return View();
        }
    }
}
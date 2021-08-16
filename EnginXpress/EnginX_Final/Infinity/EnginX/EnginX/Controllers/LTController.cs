using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnginX.Controllers
{
    public class LTController : Controller
    {
        // GET: LT
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

    }
}
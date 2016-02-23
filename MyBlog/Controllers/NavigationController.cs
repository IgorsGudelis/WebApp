using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class NavigationController : Controller
    {
        public PartialViewResult Navigation()
        {
            return PartialView();
        }

        public ActionResult Home()
        {
            return View();
        }
	}
}
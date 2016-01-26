using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class StartPageController : Controller
    {
        public ActionResult HomePage()
        {
            return View("HomePage");
        }
	}
}
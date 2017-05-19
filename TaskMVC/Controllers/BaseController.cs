using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskMVC.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Debug()
        {
            return View();
        }
    }
}
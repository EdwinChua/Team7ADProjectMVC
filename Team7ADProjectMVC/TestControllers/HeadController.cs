using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.TestControllers
{
    public class HeadController : Controller
    {
        // GET: Head
        public ActionResult Index()
        {
            return View("DelegateRole");
        }
    }
}
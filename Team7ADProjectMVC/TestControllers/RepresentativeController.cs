using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.TestControllers
{
    public class RepresentativeController : Controller
    {
        // GET: Representative
        public ActionResult Index()
        {
            return View("ViewRequisitionDetails");
        }

        public ActionResult Test()
        {
            return Index();
        }
    }
}
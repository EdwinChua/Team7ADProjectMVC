using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Team7ADProjectMVC.Models;

namespace Team7ADProjectMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            //Clears loaded view engines, loads customized one
            ViewEngines.Engines.Clear();

            ExtendedRazorViewEngine engine = new ExtendedRazorViewEngine();
            engine.AddViewLocationFormat("~/Views/Department/{0}.cshtml");

            engine.AddPartialViewLocationFormat("~/Views/Store/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Views/Store/{1}/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Views/Store/Adjustment/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Views/Store/Disbursement/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Views/Store/Inventory/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Views/Store/Maintain/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Views/Store/Resupply/{0}.cshtml");

            engine.AddPartialViewLocationFormat("~/Views/TestViews/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Views/TestViews/{1}/{0}.cshtml");

            ViewEngines.Engines.Add(engine);

            AreaRegistration.RegisterAllAreas();
            //RegisterRoutes(RouteTable.Routes);
            Application["RetrievalList"] = new RetrievalList();
        }
        protected void Session_Start()
        {
            Session["user"] = new Employee(); // To prevent layout page from throwing exception
            Session["adjustment"] = new Adjustment();
            Session["requisition"] = new Requisition();
        }
    }
}

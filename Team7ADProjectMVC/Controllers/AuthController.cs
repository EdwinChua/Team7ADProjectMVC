using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.Controllers
{
    public class AuthController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        // GET: Auth
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = Int32.Parse(User.Identity.Name);
                Session["user"] = db.Employees.Find(userId);
                return Content(((Employee)Session["User"]).EmployeeName);
            }

            else return Content("not authenticated");
            
        }
    }
}
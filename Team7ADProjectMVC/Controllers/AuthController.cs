using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        [HttpPost]
        public ActionResult AndroidAuth(FormCollection f)
        {
            if (Membership.ValidateUser(Request.Form["1"], Request.Form["2"]))
            {
                int userId = Int32.Parse(Request.Form["1"]);

                return Content(db.Employees.Find(userId).EmployeeName);
            }

            return Content("failed");
        }
    }
}
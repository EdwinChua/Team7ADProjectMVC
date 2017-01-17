using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC;

namespace Team7ADProjectMVC.TestControllers
{
  

    public class HeadController : Controller
    {
        private ProjectEntities db = new ProjectEntities();
        // GET: Head
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Approve()
        {
            return View();
        }
        public ActionResult DelegateRole()
        {
            return View();
        }
        public ActionResult ListAllEmployees()
        {
            var requisitions = db.Requisitions.ToList();
            ViewBag.Cat = requisitions;
            return View();

        }

        public ActionResult EmployeeRequisition(string id)
        {
            //var empReq = db.RequisitionDetails
            return View("Approve");
        }
    }
}
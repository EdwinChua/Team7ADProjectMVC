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
    public class DepartmentController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Department
        public ActionResult Index()
        {
            var requisitions = db.Requisitions.ToList();
            ViewBag.Cat = requisitions;
            return View();
        }

        public ActionResult MakeRequisition()
        {
            var requisitions = db.Requisitions.ToList();
            ViewBag.Cat = requisitions;
            return View("MakeRequisition");
        }

        // GET: TESTRequisitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = db.Requisitions.Find(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            return View(requisition);
        }


        public ActionResult ViewRequisitionDetails(int? id)
        {

            //db.Customers.Where(u => u.ContactName == "张学友").OrderBy(u => u.ContactName).Take(2)
            id = 2;

            Requisition requisition = db.Requisitions.Find(id);
            ViewBag.re = requisition;
          
            return View(requisition);

            List<RequisitionDetail> relis = db.RequisitionDetails.Where(u => u.RequisitionId == id).ToList();

            ViewBag.rel = relis;

            return View(requisition);
        }
    }
}



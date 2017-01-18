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

        // GET: TESTRequisitions/Create
        public ActionResult MakeRequisition()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");

            List<RequisitionDetail> relis = db.RequisitionDetails.Take(3).ToList();
            

            ViewBag.MembershipList = db.Inventories.ToList();

            //var query = from t in db.Inventories
            //            where t.Category.CategoryName.Equals("Clips")
            //            select t;

            ViewBag.clips = db.Inventories.Where(x => x.Category.CategoryName == "Clips").ToList();
            //ViewBag.clips = query.ToList();


            ViewBag.rel = relis;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeRequisition([Bind(Include = "RequisitionId,EmployeeId,DepartmentId,ApprovedBy,ApprovedDate,OrderedDate,RequisitionStatus")] Requisition requisition)
        {

            if (ModelState.IsValid)
            {
                db.Requisitions.Add(requisition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<RequisitionDetail> relis = db.RequisitionDetails.Take(3).ToList();

            ViewBag.rel = relis;

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", requisition.EmployeeId);
            return View(requisition);
            //var requisitions = db.Requisitions.ToList();
            //ViewBag.Cat = requisitions;



            ////List<RequisitionDetail> relis = db.RequisitionDetails.ToList();

            ////ViewBag.rel = relis;

           

            //return View(requisitions);
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
            

            Requisition requisition = db.Requisitions.Find(id);
            ViewBag.re = requisition;
          

        
            List<RequisitionDetail> relis = db.RequisitionDetails.Where(u => u.RequisitionId == id).ToList();

            ViewBag.rel = relis;

            return View(requisition);
        }
    }
}



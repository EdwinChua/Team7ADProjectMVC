using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.TestControllers
{
    public class RepresentativeController : Controller
    {
        private ProjectEntities db = new ProjectEntities();
        // GET: Representative
        public ActionResult Index()
        {
            return View("ViewRequisitionDetails");
        }
        public ActionResult MakeRequisition()
        {
            return View("MakeRequisition");
        }

        public ActionResult Confirm()
        {
            return View("ConfirmDisbursementList");
        }
        public ActionResult Change(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee e = db.Employees.Find(id);
            Department department = e.Department;
            
            if (department == null)
            {
                return HttpNotFound();
            }
            
            //ViewBag.CollectionPointId = new SelectList(db.CollectionPoints, "CollectionPointId", "PlaceName", department.CollectionPointId);
            //ViewBag.RepresentativeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", department.RepresentativeId);
            ViewBag.Message = db.CollectionPoints.ToList();
            return View("ChangeCollectionPoint", department);
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change([Bind(Include = "DepartmentId,CollectionPointId,DepartmentName,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewRequisitionDetails");
            }

            //ViewBag.CollectionPointId = new SelectList(db.CollectionPoints, "CollectionPointId", "PlaceName", department.CollectionPointId);
            ViewBag.Message = db.CollectionPoints.ToList();
            return View(department);

        }

    }
}
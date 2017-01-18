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
        private ProjectEntities da = new ProjectEntities();
        // GET: Representative
        public ActionResult ViewDisbursmentList()
        {
            var disbursementLists = da.DisbursementLists.Include(d => d.CollectionPoint).Include(d => d.Department).Include(d => d.Retrieval);
            return View(disbursementLists.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisbursementList disbursementList = da.DisbursementLists.Find(id);
            if (disbursementList == null)
            {
                return HttpNotFound();
            }
            return View(disbursementList);
        }
        public ActionResult MakeRequisition()
        {
            return View("MakeRequisition");
        }

        //public ActionResult Confirm(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DisbursementList dbl=db.DisbursementLists.Find()
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("Details", employee);
        //    return View("ConfirmDisbursementList");
        //}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee e = da.Employees.Find(id);
            Department department = e.Department;
            
            if (department == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.Message = da.CollectionPoints.ToList();
            return View("ChangeCollectionPoint", department);
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit( [Bind(Include = "DepartmentId,CollectionPointId")] Department department)
        {
            
            if (ModelState.IsValid)
            {

                var r = Request.Form["radio"];
                int id = department.DepartmentId;
                da.Departments.Single(model => model.DepartmentId== id).CollectionPointId = int.Parse(r);
                
                da.SaveChanges();

                return RedirectToAction("ViewRequisitionDetails");
            }
            ViewBag.Message = da.CollectionPoints.ToList();
            return View(department);

        }

    }
}
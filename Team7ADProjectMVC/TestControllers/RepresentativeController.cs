using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;

namespace Team7ADProjectMVC.TestControllers
{
    public class RepresentativeController : Controller
    {
        private IDisbursementService disbursementSvc;
        private ProjectEntities db = new ProjectEntities();

        public RepresentativeController()
        {
            disbursementSvc = new DisbursementService();
        }
        // GET: Representative
        public ActionResult Viewdisbursements()
        {

            return View(disbursementSvc.GetAllDisbursements());


        }
        public ActionResult Searchdisbursements(string date, String status)
        {

            return View("Viewdisbursements", disbursementSvc.FindDisbursementsBySearch(date, status));
        }
        public ActionResult ViewDisbursementDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisbursementList dl = disbursementSvc.GetDisbursementById(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            ViewBag.DisbursementList = disbursementSvc.GetDisbursementById(id);
            ViewBag.Cpname = disbursementSvc.findCpnameByDisburse(id);
            ViewBag.Cptime = disbursementSvc.findCptimeByDisburse(id);
            ViewBag.status = disbursementSvc.findDisbursenmentStatus(id);
            return View(disbursementSvc.GetdisbursementdetailById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewDisbursementDetail(int id)
        {



            int rid = db.DisbursementLists.Find(id).Retrieval.RetrievalId;
            List<RequisitionDetail> rdlist= db.Requisitions.Single(model => model.RetrievalId == rid).RequisitionDetails.ToList();
            foreach(var item in rdlist)
            {
                db.RequisitionDetails.Find(item.RequisitionDetailId).DeliveryStatus = "Completed";
            }
           


            db.DisbursementLists.Find(id).Status = "Completed";
            db.SaveChanges();
            return RedirectToAction("Viewdisbursements");



        }



        public ActionResult Edit(int? id)
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

            ViewBag.Message = db.CollectionPoints.ToList();
            return View("ChangeCollectionPoint", department);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "DepartmentId,CollectionPointId")] Department department)
        {

            if (ModelState.IsValid)
            {

                var r = Request.Form["radio"];
                int id = department.DepartmentId;
                db.Departments.Single(model => model.DepartmentId == id).CollectionPointId = int.Parse(r);

                db.SaveChanges();

                return RedirectToAction("Viewdisbursements");
            }
            ViewBag.Message = db.CollectionPoints.ToList();
            return View(department);

        }


    }
}
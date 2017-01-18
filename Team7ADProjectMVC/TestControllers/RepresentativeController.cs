﻿using System;
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
        public ActionResult Viewdisbursements(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            
            return View( disbursementSvc.GetdisbursementsByDept(id));


        }
        public ActionResult Searchdisbursements(String status)
        {
            
            return View("Viewdisbursements", disbursementSvc.GetdisbursementsByStatus(status));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisbursementList disbursementList = db.DisbursementLists.Find(id);
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

                return RedirectToAction("ViewRequisitionDetails");
            }
            ViewBag.Message = db.CollectionPoints.ToList();
            return View(department);

        }

    }
}
﻿using System;
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

    public class PersonModel
    {
        public List<RoleModel> Roles { get; set; }
        public List<ItemModel> Items { get; set; }

        public string Name { get; set; }
    }
    public class ItemModel
    {
        public string Item { get; set; }
        public string Quantity { get; set; }
    }

    public class RoleModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
    //public class QuantityModel
    //{
    //    public string Quantity { get; set; }
    //}

    public class DepartmentController : Controller
    {
        private ProjectEntities db = new ProjectEntities();
        List<String> Roles;
        // GET: Department
        public ActionResult Index()
        {
            var requisitions = db.Requisitions.ToList();

            ViewBag.Cat = requisitions;
            return View();
        }
        public ActionResult Search(int id)
        {
            //var inventories = inventorySvc.GetInventoryListByCategory(id);
            //var categories = inventorySvc.GetAllCategories();
            //ViewBag.Cat = categories.ToList();
            var requisitions = db.Requisitions.ToList();

            ViewBag.Cat = requisitions;
            return View("Index");
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

            Requisition req = new Requisition();
           
            


            List<RequisitionDetail> redlis = new List<RequisitionDetail>();
            



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

        [HttpPost]
        public ActionResult AddUser(PersonModel model)
        {
        //    model.Items[0];
            if (model != null)
            {
                return Json("Success");
            }
            else
            {
                return Json("An Error Has occoured");
            }

        }

        //[HttpPost]
        //public ActionResult AddUser(List<String> rs)
        //{
        //    this.Roles = rs;
        //    foreach (string i in Roles)
        //    {

        //        Console.WriteLine(i.ToString());

        //    }

        //    return null;

        //}

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



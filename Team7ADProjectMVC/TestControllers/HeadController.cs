﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;
using Team7ADProjectMVC.Models.ListAllRequisitionService;


namespace Team7ADProjectMVC.TestControllers
{
  
    
    public class HeadController : Controller
    {
        private IIListAllRequisition listsvc;
      

       private ProjectEntities db = new ProjectEntities();
        public HeadController()
        {
            listsvc =new IListAllRequisiton();
       
        }

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
            var requisitions = listsvc.GetAllRequisition();
            ViewBag.req = requisitions.ToList();
          
                return View("ListAllEmployees", requisitions);

        }

        public ActionResult EmployeeRequisition(int id)
        {
            Requisition r = listsvc.FindById(id);
            if (r == null)
            {
                return HttpNotFound();
            }

            
            return View("Approve",r);
        }
        public ActionResult ApproveReject(int id)
        {
            Requisition r = listsvc.FindById(id);
            if (r == null)
            {
                return HttpNotFound();
            }


            return View("Approve", r);
        }
        public ActionResult ApproveRequisition(int id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition r = listsvc.FindById(id);

            listsvc.UpdateApproveStatus(r.RequisitionId);
                return RedirectToAction("ViewInventory");

        }
        public ActionResult RejectRequisition(int id)
        {
            Requisition r = listsvc.FindById(id);
            if (r == null)
            {
                return HttpNotFound();
            }


            return View("Approve", r);
        }
    }
}
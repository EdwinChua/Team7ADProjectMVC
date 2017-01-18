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

        public ActionResult EmployeeRequisition(int? id)
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
        public ActionResult MarkAsCollected(int? rid, string textcomments,string status)
        {
            Requisition r = listsvc.FindById(rid);
            if (status.Equals("Approve")) {              
                listsvc.UpdateApproveStatus(r, textcomments);
                return RedirectToAction("ListAllEmployees");
            }
           
                listsvc.UpdateRejectStatus(r, textcomments);
                return RedirectToAction("ListAllEmployees");
           

        }


        //public ActionResult ApproveRequisition(int id)
        //{

        //    Requisition r = listsvc.FindById(id);

        //    listsvc.UpdateApproveStatus(r);
        //    return RedirectToAction("ListAllEmployees");

        //}
        //public ActionResult RejectRequisition(int id)
        //{
        //    Requisition r = listsvc.FindById(id);

        //    listsvc.UpdateRejectStatus(r);
        //    return RedirectToAction("ListAllEmployees");
        //}
    }
}
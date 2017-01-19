﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;
using Team7ADProjectMVC.Models.DepartmentService;
using Team7ADProjectMVC.Models.ListAllRequisitionService;


namespace Team7ADProjectMVC.TestControllers
{
  
    
    public class HeadController : Controller
    {
        private IRequisitionService listsvc;

        private IDepartmentService depsvc;
       private ProjectEntities db = new ProjectEntities();
        public HeadController()
        {
            listsvc =new RequisitionService();
            depsvc = new DepartmentService();
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
        //----------------------------Delegation Part----------------------------------start
        public ActionResult show()
        {
            //var Employeelist = depsvc.GetAllEmployee();
            ViewBag.empList = depsvc.GetAllEmployeebyDepId(4);
            //Find employee with delegated role
            //ViewBag.DelegatedEmployee
            return View("DelegateRole");

        }
        public ActionResult Manage(int? empId, string status,string startDate, string endDate, string approveReq, string changeCP, string viewReq, string makeReq, string delegateRol, string viewColDetl)

        {

            String []  s = startDate.Split('/');
            DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));
            String[] e = endDate.Split('/');
            DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));


            Employee emp = depsvc.FindById(empId);
            if (status.Equals("Delegate"))
            {
                bool approveReqint = true;
                bool changeCPint = true;
                bool viewReqint = true;
                bool makeReqint = true;
                bool delegateRolint = true;
                bool viewColDetlint = true;
               
                if (approveReq == null)
                {
                    approveReqint = false;
                }
                if (changeCP==null)
                {
                    changeCPint = false;
                }
                if (viewReq == null)
                {
                    viewReqint = false;
                }
                if (makeReq == null)
                {
                    makeReqint = false;
                }
                if (delegateRol == null)
                {
                    delegateRolint = false;
                }
                if (viewColDetl == null)
                {
                    viewColDetlint = false;
                }

                depsvc.manageDelegate(emp, sdate, edate, approveReqint,changeCPint,viewReqint,makeReqint,delegateRolint,viewColDetlint);
                return RedirectToAction("ListAllEmployees");
            }

            //listsvc.manageTerminate();
            return RedirectToAction("ListAllEmployees");


        }


    }
}
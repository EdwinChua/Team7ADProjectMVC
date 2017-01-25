using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;
using Team7ADProjectMVC.Models.DelegateRoleService;
using Team7ADProjectMVC.Models.ListAllRequisitionService;

using PagedList;
namespace Team7ADProjectMVC.TestControllers
{


    public class HeadController : Controller
    {
        
        private IRequisitionService reqsvc;
        public static int count = 0;
        private IDelegateRoleService depsvc;
        private ProjectEntities db = new ProjectEntities();
        private Employee u;
        //Employee user = (Employee)(Session["Employee"]); need real session of employee from login
        int depIdofLoginUser = 4;//int depId=user.Department.DepartmentId
        public HeadController()
        {
            reqsvc = new RequisitionService();
            depsvc = new DelegateRoleService();
            //u = (Employee)System.Web.HttpContext.Current.Session["user"];
            //u = depsvc.FindById(1);
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
        //----------------------------View/Approve/Reject Requisition Part--------------start here
        public ActionResult ListAllEmployees(string currentFilter, string searchString, int? page)
        {
            
            

            var requisitions = reqsvc.GetAllRequisition(depIdofLoginUser);
            

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 3;
            int pageNumber = (page ?? 1);           

            if (!String.IsNullOrEmpty(searchString))
            {
                var q = db.Requisitions.Where(s => s.Employee.EmployeeName.Contains(searchString)
                                       || s.OrderedDate.ToString().Contains(searchString));                                      
                requisitions = q.ToList();
            }
            

            ViewBag.req = requisitions.ToList();
            ViewBag.Employee = u;
            return View("ListAllEmployees", requisitions.ToPagedList(pageNumber, pageSize)); 


        }

        public ActionResult EmployeeRequisition(int? id)
        {
            Requisition r = reqsvc.FindById(id);
            if (r == null)
            {
                return HttpNotFound();
            }


            return View("Approve", r);
        }
        public ActionResult ApproveReject(int id)
        {
            Requisition r = reqsvc.FindById(id);
            if (r == null)
            {
                return HttpNotFound();
            }
            
            return View("Approve", r);
        }
        public ActionResult MarkAsCollected(int? rid, string textcomments, string status)
        {
            Requisition r = reqsvc.FindById(rid);
            if (status.Equals("Approve"))
            {
                if(textcomments.Equals("Enter comment here..."))
                {
                    textcomments = "No comment";
                    reqsvc.UpdateApproveStatus(r, textcomments);
                }
                reqsvc.UpdateApproveStatus(r, textcomments);

                return RedirectToAction("ListAllEmployees");
            }


            if (textcomments.Equals("Enter comment here..."))
            {
                textcomments = "No comment";
                reqsvc.UpdateRejectStatus(r, textcomments);
            }
            reqsvc.UpdateRejectStatus(r, textcomments);

            return RedirectToAction("ListAllEmployees");


        }

        //----------------------------View/Approve/Reject Requisition Part--------------end here
        //----------------------------Delegation Part----------------------------------start here
        public ActionResult show()
        {                   
            Delegate delegatedEmployee= depsvc.getDelegatedEmployee(depIdofLoginUser);

            if (delegatedEmployee == null)
            {
                ViewBag.empList = depsvc.GetAllEmployeebyDepId(depIdofLoginUser);
                return View("DelegateRole");

            }
            //else
            //{
            //    if(delegatedEmployee.ActualEndDate.Equals(DateTime.Today))
            //    {

            //    }
                return RedirectToAction("fill");
            //}


        }


        public ActionResult ManageDelegation(int? empId, string status, string startDate, string endDate, int? DelegateId /*, string approveReq, string changeCP, string viewReq, string makeReq, string delegateRol, string viewColDetl,*/ )

        {

            Employee emp = depsvc.FindById(empId);
            Delegate d = depsvc.FinddelegaterecordById(DelegateId);
          



            if (status.Equals("Delegate"))
            {
              
                String[] s = startDate.Split('/');
                DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));

                String[] e = endDate.Split('/');
                DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));

                //bool approveReqint = true;
                //bool changeCPint = true;
                //bool viewReqint = true;
                //bool makeReqint = true;
                //bool delegateRolint = true;
                //bool viewColDetlint = true;

                //if (approveReq == null)
                //{
                //    approveReqint = false;
                //}
                //if (changeCP == null)
                //{
                //    changeCPint = false;
                //}
                //if (viewReq == null)
                //{
                //    viewReqint = false;
                //}
                //if (makeReq == null)
                //{
                //    makeReqint = false;
                //}
                //if (delegateRol == null)
                //{
                //    delegateRolint = false;
                //}
                //if (viewColDetl == null)
                //{
                //    viewColDetlint = false;
                //}

                depsvc.manageDelegate(emp, sdate, edate/*, approveReqint, changeCPint, viewReqint, makeReqint, delegateRolint, viewColDetlint*/);
                Delegate delegatedEmployee = depsvc.getDelegatedEmployee(4);//need dynamic id
          
                return RedirectToAction("fill");
            }
            else if (status.Equals("Update"))
            {
               if(startDate.Equals("") && !(endDate.Equals("")))
                {

                    String[] e = endDate.Split('/');
                    DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));

                    ViewBag.s1 = d.StartDate;

                    depsvc.updateDelegate(emp, d, ViewBag.s1, edate /*approveReqint, changeCPint, viewReqint, makeReqint, delegateRolint, viewColDetlint*/);

                    return RedirectToAction("ListAllEmployees");
                }
               else if(endDate.Equals("") && !(startDate.Equals("")))
                {
                    String[] s = startDate.Split('/');
                    DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));

                    ViewBag.e1 = d.EndDate;

                    depsvc.updateDelegate(emp, d, sdate, ViewBag.e1 /*approveReqint, changeCPint, viewReqint, makeReqint, delegateRolint, viewColDetlint*/);

                    return RedirectToAction("ListAllEmployees");
                }
               else
                {
                    String[] s = startDate.Split('/');
                    DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));
                    String[] e = endDate.Split('/');
                    DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));
                    depsvc.updateDelegate(emp, d, sdate, edate /*approveReqint, changeCPint, viewReqint, makeReqint, delegateRolint, viewColDetlint*/);

                    return RedirectToAction("ListAllEmployees");
                }


                //bool approveReqint = true;
                //bool changeCPint = true;
                //bool viewReqint = true;
                //bool makeReqint = true;
                //bool delegateRolint = true;
                //bool viewColDetlint = true;

                //if (approveReq == null)
                //{
                //    approveReqint = false;
                //}
                //if (changeCP == null)
                //{
                //    changeCPint = false;
                //}
                //if (viewReq == null)
                //{
                //    viewReqint = false;
                //}
                //if (makeReq == null)
                //{
                //    makeReqint = false;
                //}
                //if (delegateRol == null)
                //{
                //    delegateRolint = false;
                //}
                //if (viewColDetl == null)
                //{
                //    viewColDetlint = false;
                //}


            }

            depsvc.TerminateDelegate(emp, d);
            return RedirectToAction("ListAllEmployees");


        }

        public ActionResult fill()

        {
            // DateTime today = DateTime.Today;

            //List<Delegate> a = depsvc.getDelegate(tda);

            //foreach (var i in a)
            //{
            //    count = count + 1;
            //}
            //Delegate b=a.ElementAt(count-1);
            //  Delegate d = depsvc.FinddelegaterecordById(b.DelegateId);



            Delegate d = depsvc.getDelegatedEmployee(4);
            Employee e = depsvc.FindById(d.EmployeeId);
            ViewBag.emp = e.EmployeeName;
            ViewBag.empid = e.EmployeeId;
            ViewBag.s1 = d.StartDate;
            ViewBag.e1 = d.EndDate;
            //ViewBag.approveReq = e.Permission.ApproveRequisition;
            //ViewBag.changeCP = e.Permission.ChangeCollectionPoint;
            //ViewBag.viewReq = e.Permission.ViewRequisition;
            //ViewBag.makeReq = e.Permission.MakeRequisition;
            //ViewBag.delegateRol = e.Permission.DelegateRole;
            //ViewBag.viewColDetl = e.Permission.ViewCollectionDetails;
            ViewBag.delegateId = d.DelegateId;
            return View("Terminate");
        }
        public ActionResult ManageTerminate(int? empId, string status, string startDate, string endDate/* string approveReq, string changeCP, string viewReq, string makeReq, string delegateRol, string viewColDetl*/)
        {
            return View("DelegateRole");
        }
        //----------------------------Delegation Part----------------------------------end here

    }
}
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


namespace Team7ADProjectMVC.TestControllers
{


    public class HeadController : Controller
    {
        private IRequisitionService listsvc;
        public static int count = 0;
        private IDelegateRoleService depsvc;
        private ProjectEntities db = new ProjectEntities();
        public HeadController()
        {
            listsvc = new RequisitionService();
            depsvc = new DelegateRoleService();
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


            return View("Approve", r);
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
        public ActionResult MarkAsCollected(int? rid, string textcomments, string status)
        {
            Requisition r = listsvc.FindById(rid);
            if (status.Equals("Approve"))
            {
                listsvc.UpdateApproveStatus(r, textcomments);
                return RedirectToAction("ListAllEmployees");
            }

            listsvc.UpdateRejectStatus(r, textcomments);
            return RedirectToAction("ListAllEmployees");


        }


        //----------------------------Delegation Part----------------------------------start
        public ActionResult show()
        {
            //var Employeelist = depsvc.GetAllEmployee();
            ViewBag.empList = depsvc.GetAllEmployeebyDepId(4);
            //Find employee with delegated role
            //ViewBag.DelegatedEmployee
            return View("DelegateRole");

        }
        public ActionResult ManageDelegate(int? empId, string status, string startDate, string endDate, string approveReq, string changeCP, string viewReq, string makeReq, string delegateRol, string viewColDetl,int? DelegateId)

        {

            Employee emp = depsvc.FindById(empId);
            Delegate d = depsvc.FinddelegaterecordById(DelegateId);
            Permission p = depsvc.FindPermissionRecordById(emp);

            if (status.Equals("Delegate"))
            {
                String[] s = startDate.Split('/');
                DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));

                String[] e = endDate.Split('/');
                DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));
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
                if (changeCP == null)
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

                depsvc.manageDelegate(emp, sdate, edate, approveReqint, changeCPint, viewReqint, makeReqint, delegateRolint, viewColDetlint);            
                return RedirectToAction("fill");
            }
            else if (status.Equals("Update"))
            {

                String[] s = startDate.Split('/');
                DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));

                String[] e = endDate.Split('/');
                DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));
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
                if (changeCP == null)
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
           
                depsvc.updateDelegate(emp,d,p, sdate, edate, approveReqint, changeCPint, viewReqint, makeReqint, delegateRolint, viewColDetlint);


                return RedirectToAction("ListAllEmployees");
            }

            depsvc.TerminateDelegate(emp, d, p);
            return RedirectToAction("ListAllEmployees");


        }

        public ActionResult fill()

        {
            List<Delegate> a = depsvc.getDelegate();

            foreach (var i in a)
            {
                count = count + 1;
            }
            Delegate b=a.ElementAt(count-1);
            Delegate d = depsvc.FinddelegaterecordById(b.DelegateId);
            Employee e = depsvc.FindById(d.EmployeeId);
            ViewBag.emp = e.EmployeeName;
            ViewBag.empid = e.EmployeeId;
            ViewBag.s1 = d.StartDate;
            ViewBag.e1 = d.EndDate;
            ViewBag.approveReq = e.Permission.ApproveRequisition;
            ViewBag.changeCP = e.Permission.ChangeCollectionPoint;
            ViewBag.viewReq = e.Permission.ViewRequisition;
            ViewBag.makeReq = e.Permission.MakeRequisition;
            ViewBag.delegateRol = e.Permission.DelegateRole;
            ViewBag.viewColDetl = e.Permission.ViewCollectionDetails;
            ViewBag.delegateId = d.DelegateId;
            return View("Terminate");
        }
        public ActionResult ManageTerminate(int? empId, string status, string startDate, string endDate, string approveReq, string changeCP, string viewReq, string makeReq, string delegateRol, string viewColDetl)
        {
            return View("DelegateRole");
        }

    }
}
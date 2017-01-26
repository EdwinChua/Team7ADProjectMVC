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



        Employee user ;
        int? depIdofLoginUser;
        int? depHeadId;


        public HeadController()
        {
            reqsvc = new RequisitionService();
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
//----------------------------View/Approve/Reject Requisition Part--------------start here
        public ActionResult ListAllEmployees(string currentFilter, string searchString, int? page)
        {

            //user = (Employee)Session["user"];
            depIdofLoginUser = 4; //user.DepartmentId;
            depHeadId = 8; //user.EmployeeId;


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
          
            return View("ListAllEmployees", requisitions.ToPagedList(pageNumber, pageSize)); 


        }

        public ActionResult EmployeeRequisition(int? id)
        {
            //user = (Employee)Session["user"];
            depIdofLoginUser = 4; //user.DepartmentId;
            depHeadId = 8; //user.EmployeeId;
            Requisition r = reqsvc.FindById(id);
            if (r == null)
            {
                return HttpNotFound();
            }


            return View("Approve", r);
        }
       
        public ActionResult MarkAsCollected(int? rid, string textcomments, string status)
        {
            //user = (Employee)Session["user"];
            depIdofLoginUser = 4; //user.DepartmentId;
            depHeadId = 8; //user.EmployeeId;
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

        //----------------------------View/Approve/Reject Requisition Part-------------------------end here


        //----------------------------Delegation Part----------------------------------------------start here
        public ActionResult show()
        {
           // user = (Employee)Session["user"];
             depIdofLoginUser = 4; //user.DepartmentId;
             depHeadId =8; //user.EmployeeId;
             Delegate delegatedEmployee= depsvc.getDelegatedEmployee(depIdofLoginUser);

            if (delegatedEmployee == null)
            {
                string[] startdate =DateTime.Today.ToString().Split(' ');
                string[] enddate =DateTime.Today.ToString().Split(' ');
                string[] sd = startdate[0].Split('/');
                string[] ed = enddate[0].Split('/');

                ViewBag.autoStartdate = sd[1] + "/" + sd[0] + "/" + sd[2];            
                ViewBag.autoEnddate = ed[1] + "/" + ed[0] + "/" + ed[2];
                ViewBag.empList = depsvc.GetAllEmployeebyDepId(depIdofLoginUser);

                return View("DelegateRole");

            }    
                return RedirectToAction("fill");          

        }


        public ActionResult ManageDelegation(int? empId, string status, string startDate, string endDate, int? DelegateId )

        { //user = (Employee)Session["user"];
            depIdofLoginUser = 4; //user.DepartmentId;
            depHeadId = 8; //user.EmployeeId;
            Employee emp = depsvc.FindById(empId);
            Delegate d = depsvc.FinddelegaterecordById(DelegateId);

        
            if (status.Equals("Delegate"))
            {

              
                if (startDate.Equals("") && !(endDate.Equals("")))
                {
                    String[] e = endDate.Split('/');
                    DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));
                    DateTime sdate = DateTime.Today;

                    depsvc.manageDelegate(emp, sdate, edate, depHeadId);
                    return RedirectToAction("fill");
                }
                else if (startDate.Equals("") && (endDate.Equals("")))
                {
                    DateTime edate = DateTime.Today;
                    DateTime sdate = DateTime.Today;

                    depsvc.manageDelegate(emp, sdate, edate, depHeadId);
                    return RedirectToAction("fill");
                }
                else
                {
                    String[] s = startDate.Split('/');
                    DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));

                    String[] e = endDate.Split('/');
                    DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));

                    depsvc.manageDelegate(emp, sdate, edate, depHeadId);

                    return RedirectToAction("fill");
                }
            
            }
 //update-----------------------------------------------------------------------------------------------------------------------  
            else if (status.Equals("Update"))
            {
               if(startDate.Equals("") && !(endDate.Equals("")))
                {
                    String[] e = endDate.Split('/');
                    DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));
                    ViewBag.s1 = d.StartDate;

                    depsvc.updateDelegate(emp, d, ViewBag.s1, edate, depHeadId);

                    return RedirectToAction("ListAllEmployees");
                }
               else if(endDate.Equals("") && !(startDate.Equals("")))
                {
                    String[] s = startDate.Split('/');
                    DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));                    
                    ViewBag.e1 = d.EndDate;

                    depsvc.updateDelegate(emp, d, sdate, ViewBag.e1, depHeadId);

                    return RedirectToAction("ListAllEmployees");
                }
                else if (endDate.Equals("") && (startDate.Equals("")))
                {
                    ViewBag.s1 = d.StartDate;
                    ViewBag.e1 = d.EndDate;

                    depsvc.updateDelegate(emp, d, ViewBag.s1, ViewBag.e1, depHeadId);

                    return RedirectToAction("ListAllEmployees");
                }
                else
                {
                    String[] s = startDate.Split('/');
                    DateTime sdate = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));
                    String[] e = endDate.Split('/');
                    DateTime edate = new DateTime(Int32.Parse(e[2]), Int32.Parse(e[1]), Int32.Parse(e[0]));
                    depsvc.updateDelegate(emp, d, sdate, edate, depHeadId);

                    return RedirectToAction("ListAllEmployees");
                }
            }
 //terminate-----------------------------------------------------------------------------------------------------------
                   depsvc.TerminateDelegate(emp, d);
                  return RedirectToAction("ListAllEmployees");

        }

        public ActionResult fill()
        { //user = (Employee)Session["user"];

            

            depIdofLoginUser = 4; //user.DepartmentId;
            depHeadId = 8; //user.EmployeeId;
            Delegate d = depsvc.getDelegatedEmployee(4);
            Employee e = depsvc.FindById(d.EmployeeId);

            string[] startdate = d.StartDate.ToString().Split(' ');
            string[] enddate = d.EndDate.ToString().Split(' ');
            string[] sd = startdate[0].Split('/');
            string[] ed = enddate[0].Split('/');         
            
            ViewBag.s1 = sd[1] + "/" + sd[0] + "/" + sd[2];
            ViewBag.s2= d.StartDate;
            ViewBag.e1 = ed[1] + "/" + ed[0] + "/" + ed[2];
            ViewBag.e2 = d.EndDate;

            ViewBag.delegateId = d.DelegateId;
            ViewBag.emp = e.EmployeeName;
            ViewBag.empid = e.EmployeeId;
            return View("Terminate");
        }


        public ActionResult ManageTerminate(int? empId, string status, string startDate, string endDate)
        {  //user = (Employee)Session["user"];
            depIdofLoginUser = 4; //user.DepartmentId;
            depHeadId = 8; //user.EmployeeId;
            return View("DelegateRole");
        }
        //----------------------------Delegation Part----------------------------------end here

    }
}
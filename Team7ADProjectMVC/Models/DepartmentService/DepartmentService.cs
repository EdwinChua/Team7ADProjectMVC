using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Models.DepartmentService
{
    class DepartmentService : IDepartmentService
    {
        ProjectEntities db = new ProjectEntities();
        public List<Department> ListAllDepartments()
        {
            return (db.Departments.ToList());
        }
        public List<Employee> GetAllEmployeebyDepId(int depId)
        {
            var queryBydepId= from t in db.Employees
                                where t.DepartmentId == depId
                                orderby t.EmployeeId ascending
                                select t;
            return (queryBydepId.ToList());
        }
        public Employee FindById(int? empid)
        {
            return db.Employees.Find(empid);
        }
        public  void manageDelegate(Employee e, DateTime startDate, DateTime endDate, bool approveReq, bool changeCP, bool viewReq, bool makeReq, bool delegateRol, bool viewColDetl)
        {
              
            Permission p = new Permission();
            Delegate d = new Delegate();

            d.EmployeeId = e.EmployeeId;            
            d.StartDate = startDate.Date;
            d.EndDate = endDate.Date;
            d.ActualEndDate = endDate.Date;
            d.ApprovedBy = 8;//default dep head id
            d.ApprovedDate = DateTime.Today;
            db.Delegates.Add(d);
            db.SaveChanges();

           
            p.ApproveRequisition = approveReq;
            p.ChangeCollectionPoint = changeCP;
            p.ViewRequisition = viewReq;
            p.MakeRequisition = makeReq;
            p.DelegateRole = delegateRol;
            p.ViewCollectionDetails = viewColDetl;

            db.Permissions.Add(p);
            db.SaveChanges();

            e.RoleId = 3;
            e.PermissionId = p.PermissionId;
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();


        }
       
    }
}

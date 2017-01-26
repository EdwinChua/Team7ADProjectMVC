using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7ADProjectMVC.Models;

namespace Team7ADProjectMVC.Services.DepartmentService
{
    
    class DepartmentService : IDepartmentService
    {
        ProjectEntities db = new ProjectEntities();
        PushNotification notify = new PushNotification();

        public Requisition FindById(string id)
        {
            throw new NotImplementedException();
        }
        public Department FinddeById(string id)
        {
            throw new NotImplementedException();
        }
        public Employee FindEmployeeById(int id)
        {
            return (db.Employees.Find(id));
        }
        

        public Requisition FindRequisitionById(string id)
        {
            throw new NotImplementedException();
        }

        public List<Requisition> GetRequisitionByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public List<Requisition> ListAllRequisition()
        {
           
             return (db.Requisitions.ToList());
        }

        public List<Department> ListAllDepartments()
        {
            return (db.Departments.ToList());
        }

        public List<Requisition> ListAllDepartment()
        {
            throw new NotImplementedException();
        }
        public Department findDeptByID(int ? id)
        {
            return (db.Departments.Find(id));
        }
        public void changeDeptCp(Department department,int cpId)
        {
            
            int id = department.DepartmentId;
            db.Departments.Single(model => model.DepartmentId == id).CollectionPointId = cpId;
            db.SaveChanges();
            
            notify.CollectionPointChanged(id);
        }
    }
}

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

        public Requisition FindById(string id)
        {
            throw new NotImplementedException();
        }
        public Department FinddeById(string id)
        {
            throw new NotImplementedException();
        }
        public Employee FindemplById(string id)
        {
            throw new NotImplementedException();
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
    }
}

using System;
using System.Collections.Generic;
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

    }
}

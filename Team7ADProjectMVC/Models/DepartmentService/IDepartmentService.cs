using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Models.DepartmentService
{
    interface IDepartmentService
    {
        List<Department> ListAllDepartments();

        List<Requisition> ListAllDepartment();
        Requisition FindRequisitionById(String id);
        List<Requisition> GetRequisitionByStatus(string status);
    }
}

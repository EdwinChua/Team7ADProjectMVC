using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Services.DepartmentService
{
    interface IDepartmentService
    {
        List<Department> ListAllDepartments();
        Requisition FindById(string id);
        Requisition FindRequisitionById(string id);
        List<Requisition> GetRequisitionByStatus(string status);
        List<Requisition> ListAllDepartment();
        List<Requisition> ListAllRequisition();
        Department FinddeById(string id);
        Employee FindEmployeeById(int id);
        Department findDeptByID(int? id);
        void changeDeptCp(Department department, int cpId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Services.DepartmentService
{
    interface IDepartmentService
    {
        List<Employee> GetAllEmployees();
        List<Department> ListAllDepartments();
        Department FindDeptById(string id);
        Department FindDeptById(int id);
        Employee FindEmployeeById(int id);
        void changeDeptCp(Department department, int cpId);
        List<RequisitionDetail> GetRequisitionDetailByDept(int dId, int rId);
    }
}

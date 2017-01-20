using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Models.DelegateRoleService
{
    interface IDelegateRoleService
    {
        List<Delegate> getDelegate(); 
        List<Employee> GetAllEmployeebyDepId(int depId);
        Employee FindById(int? empid);
        void manageDelegate(Employee e, DateTime startDate, DateTime endDate, bool approveReq, bool changeCP, bool viewReq, bool makeReq, bool delegateRol, bool viewColDetl);
    }
}

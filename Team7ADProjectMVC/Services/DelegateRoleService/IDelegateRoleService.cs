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
        List<Employee> GetAllEmployeebyDepId(int? depId);
        Employee FindById(int? empid);
        Permission FindPermissionRecordById(Employee e);
        void TerminateDelegate(Employee emp, Delegate d);
        Delegate FinddelegaterecordById(int? count);
        Delegate  getDelegatedEmployee(int? depId);
    



        void manageDelegate(Employee e, DateTime startDate, DateTime endDate,int? depHeadId);
        void updateDelegate(Employee e, Delegate d,/*Permission p, */ DateTime startDate, DateTime endDate, int? depHeadId);
    }
}

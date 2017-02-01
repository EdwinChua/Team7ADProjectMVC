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
        void TerminateDelegate(Delegate del);
        Delegate FinddelegaterecordById(int? delegateId);
        Delegate  getDelegatedEmployee(int? depId);
    



        void manageDelegate(Employee e, DateTime startDate, DateTime endDate,int? depHeadId);
        void updateDelegate( Delegate d, DateTime startDate, DateTime endDate, int? depHeadId);
    }
}

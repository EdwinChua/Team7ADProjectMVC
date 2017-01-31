using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Models.ChangeRepresentativeService
{
    interface IChangeRepresentativeService
    {
       Employee GetCurrentRep( int? depIdofLoginUser);
        List<Employee> GetAllEmployee(int? depIdofLoginUser, int currentRepId);
        Employee GetEmpbyId(int? empIdforRep);
        void ChangeRep(Employee currentRep, Employee newRep);
    }
}

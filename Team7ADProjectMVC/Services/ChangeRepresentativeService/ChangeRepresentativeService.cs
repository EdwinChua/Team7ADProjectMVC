using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Team7ADProjectMVC.Models.ChangeRepresentativeService
{
    class ChangeRepresentativeService : IChangeRepresentativeService
    {
        ProjectEntities db = new ProjectEntities();

        public Employee GetCurrentRep(int? depId)
        {
            var queryBydepId = from t in db.Employees
                               where t.DepartmentId == depId
                               select t;
            var q2 = queryBydepId.ToList();
            foreach (var emp in q2)
            {
                if ((emp.RoleId == 4) || (emp.RoleId == 7 ))
                {
                    return emp;
                }
            }
            return null;

        }
        public List<Employee> GetAllEmployee(int? depId, int currentRepId)
        {
            var queryBydepId = from t in db.Employees
                               where t.DepartmentId == depId && t.EmployeeId != currentRepId && (t.RoleId != 2 && (t.RoleId != 6 && t.RoleId != 5))
                               orderby t.EmployeeId ascending
                               select t;
            return (queryBydepId.ToList());
        }
        public Employee GetEmpbyId(int? empIdforRep)
        {
            return db.Employees.Find(empIdforRep);
        }
        public void ChangeRep(Employee currentRep, Employee newRep)
        {

            if (currentRep.RoleId == 7)
            {
                currentRep.RoleId = 1;
                newRep.RoleId = 7;
                newRep.Department.RepresentativeId = newRep.EmployeeId;
                db.Entry(currentRep).State = EntityState.Modified;
                db.Entry(newRep).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                currentRep.RoleId = 3;
                newRep.RoleId = 4;
                newRep.Department.RepresentativeId = newRep.EmployeeId;
                db.Entry(currentRep).State = EntityState.Modified;
                db.Entry(newRep).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}


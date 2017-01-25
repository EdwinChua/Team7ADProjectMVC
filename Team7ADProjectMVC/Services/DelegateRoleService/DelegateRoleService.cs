﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Team7ADProjectMVC.Models.DelegateRoleService
{
    class DelegateRoleService : IDelegateRoleService
    {
        ProjectEntities db = new ProjectEntities();

         public  Delegate getDelegatedEmployee(int depId)
        {
            var queryBydepId = from t in db.Delegates
                               where t.Employee.DepartmentId == depId 
                               select t;
            var q2 = queryBydepId.ToList();
            foreach (var xyz in q2)
            {
                if (xyz.EndDate.Equals(xyz.ActualEndDate) &&(xyz.ActualEndDate > DateTime.Today || xyz.ActualEndDate.Equals(DateTime.Today)))
                {
                    return xyz;
                }
            }
            return null;
        }
       
        public List<Employee> GetAllEmployeebyDepId(int depId)
        {
            var queryBydepId= from t in db.Employees
                                where t.DepartmentId == depId
                                orderby t.EmployeeId ascending
                                select t;
            return (queryBydepId.ToList());
        }
        
        public Permission FindPermissionRecordById(Employee e)
        {
            return db.Permissions.Find(e.PermissionId);
        }
        public Employee FindById(int? empid)
        {
            return db.Employees.Find(empid);
        }
        public  void manageDelegate(Employee e, DateTime startDate, DateTime endDate,int depHeadId)
        {
              
            Permission p = new Permission();
            Delegate d = new Delegate();

            d.EmployeeId = e.EmployeeId;            
            d.StartDate = startDate.Date;
            d.EndDate = endDate.Date;
            d.ActualEndDate = endDate.Date;
            d.ApprovedBy = depHeadId;//default dep head id
            d.ApprovedDate = DateTime.Today;
            db.Delegates.Add(d);
            db.SaveChanges();
            
            e.RoleId = 3;

            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();


        }
        public void updateDelegate(Employee e, Delegate d,DateTime startDate, DateTime endDate,int depHeadId)
        {
            d.StartDate = startDate.Date;
            d.EndDate = endDate.Date;
            d.ActualEndDate = endDate.Date;
            d.ApprovedBy = depHeadId;//default dep head id
            d.ApprovedDate = DateTime.Today.Date;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void TerminateDelegate(Employee emp, Delegate d)
        {
            d.ActualEndDate = DateTime.Today.AddDays(-1);
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<Delegate> getDelegate()
        {

            var query= from t in db.Delegates
                       orderby t.DelegateId ascending
                       select t ;
         

            return (query.ToList());
        }
        public Delegate FinddelegaterecordById(int? count)
        {

            return db.Delegates.Find(count);
        }
        public List<Department> ListAllDepartments()
        {
            throw new NotImplementedException();
        }

    }
}

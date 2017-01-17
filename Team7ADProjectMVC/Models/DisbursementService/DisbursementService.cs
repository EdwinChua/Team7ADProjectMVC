using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    public class DisbursementService : IDisbursementService
    {
        ProjectEntities db = new ProjectEntities();
        
        public List<DisbursementList> GetAllDisbursements()
        {
            var disbursementList = from d in db.DisbursementLists
                                   orderby d.Status
                                   select d;
            
            return (disbursementList.ToList());
        }

        public DisbursementList GetDisbursementById(string id)
        {
            return db.DisbursementLists.Find(id);
        }

        public List<DisbursementList> GetDisbursementsBySearchCriteria(int? departmentId, string status)
        {
            if (status == null && departmentId == null)
            {
                return (db.DisbursementLists.ToList());
            }
            else if (departmentId == null)
            {
                var queryResults = from d in db.DisbursementLists
                               where d.DepartmentId == departmentId
                               orderby d.OrderedDate
                               select d;
                return (queryResults.ToList());
            }
            else if (status == null)
            {
                var queryResults = from d in db.DisbursementLists
                                   where d.Status == status
                                   orderby d.OrderedDate
                                   select d;
                return (queryResults.ToList());
            }
            else
            {
                var queryResults = from d in db.DisbursementLists
                                   where d.DepartmentId == departmentId
                                   && d.Status == status
                                   orderby d.OrderedDate
                                   select d;
                return (queryResults.ToList());
            }

        }

        public void UpdateDisbursementList(DisbursementList disbursementList)
        {
            db.Entry(disbursementList).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
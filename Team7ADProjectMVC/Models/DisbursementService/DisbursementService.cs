using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    public class DisbursementService : IDisbursementService
    {
        ProjectEntities db = new ProjectEntities();
        public List<DisbursementList> GetAllDisbursements()
        {
            //TODO: Linda 
            var disbursementList = db.DisbursementLists;
            return (disbursementList.ToList());
            throw new NotImplementedException();
        }

        public List<DisbursementList> GetDisbursementsBySearchCriteria(Department department, string status)
        {
            //TODO: Linda 
            
            throw new NotImplementedException();
        }
    }
}
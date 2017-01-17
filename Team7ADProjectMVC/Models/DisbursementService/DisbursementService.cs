﻿using System;
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
            var disbursementList = from d in db.DisbursementLists
                                   orderby d.Status
                                   select d;
            return (disbursementList.ToList());
        }

        public List<DisbursementList> GetDisbursementsBySearchCriteria(Department department, string status)
        {
            var queryResults = from d in db.DisbursementLists
                                where d.Department == department
                                && d.Status == status
                                orderby d.OrderedDate
                                select d;
            return (queryResults.ToList());
        }
    }
}
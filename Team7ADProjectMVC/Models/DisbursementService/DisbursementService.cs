﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
        

        public DisbursementList GetDisbursementById(int? id)
        {

            return db.DisbursementLists.Find(id);
        }


        public List<DisbursementList> GetDisbursementsBySearchCriteria(int? departmentId, string status)
        {
            if ((status == null || status == "") && departmentId == null)
            {
                return (db.DisbursementLists.ToList());
            }
            else if (status == null || status == "")
            {
                var queryResults = from d in db.DisbursementLists
                               where d.DepartmentId == departmentId
                               orderby d.OrderedDate
                               select d;
                return (queryResults.ToList());
            }
            else if (departmentId == null) 
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
        public List<DisbursementList> GetdisbursementsByStatus(string status)
        {
            var queryResults = from d in db.DisbursementLists
                               where d.Status.Equals(status)
                               orderby d.DeliveryDate
                               select d;
            return (queryResults.ToList());
        }
        public List<DisbursementList> GetdisbursementsByDept(int? id)
        {

            var disbursementLists = from d in db.Employees.Find(id).Department.DisbursementLists
                                   orderby d.Status
                                   select d;
            return (disbursementLists.ToList());
        }
        public List<DisbursementDetail> GetdisbursementdetailById(int? id)
        {
            var disbursementDetails = db.DisbursementDetails.Where(model=>model.DisbursementListId==id).Include(d => d.DisbursementList).Include(d => d.RequisitionDetail);
            return (disbursementDetails.ToList());

        }
        public string findCpnameByDisburse(int? id)
        {
            return (db.DisbursementLists.Find(id).CollectionPoint.PlaceName);
        }
        public string findCptimeByDisburse(int? id)
        {
            return (db.DisbursementLists.Find(id).CollectionPoint.CollectTime.ToString());
        }
    }
}
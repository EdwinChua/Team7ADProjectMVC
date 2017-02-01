﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ListAllRequisitionService
{
    public class RequisitionService : IRequisitionService
    {
        ProjectEntities db = new ProjectEntities();
        PushNotification notify = new PushNotification(); 

        public List<Requisition> GetAllRequisition(int? depId)
        {
            var queryByStatus = from t in db.Requisitions 
                                  where t.RequisitionStatus == "Pending Approval" && t.DepartmentId == depId
                                 orderby t.RequisitionId ascending
                                  select t;
            return (queryByStatus.ToList());
 
        }
      
        public Requisition FindById(int? requisitionId)
        {
            return db.Requisitions.Find(requisitionId);
        }
        
        public void  UpdateApproveStatus(Requisition requisition, string comments)
        {

            requisition.RequisitionStatus = "Approved";
            requisition.Comment = comments;
            requisition.ApprovedDate = DateTime.Today.Date;
            
            db.Entry(requisition).State = EntityState.Modified;
            db.SaveChanges();

            string reqListId = requisition.RequisitionId.ToString();
            notify.NewRequisitonMade(reqListId);
        }
        public void UpdateRejectStatus(Requisition requisition, string comments)
        {

            requisition.Comment = comments;
            requisition.ApprovedDate = DateTime.Today.Date;
            requisition.RequisitionStatus = "Rejected";
            db.Entry(requisition).State = EntityState.Modified;
            db.SaveChanges();
        }
         public List<Requisition> getDataForPagination(string searchString)
        {
            var queryByStatus= db.Requisitions.Where(s => (s.Employee.EmployeeName.Contains(searchString)
                                       || s.OrderedDate.ToString().Contains(searchString)));
            return (queryByStatus.ToList());
        }

    }
}
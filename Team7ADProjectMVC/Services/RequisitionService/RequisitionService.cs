using System;
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
      
        public Requisition FindById(int? id)
        {
            return db.Requisitions.Find(id);
        }
        
        public void  UpdateApproveStatus(Requisition r,String c)
        {
            
            r.RequisitionStatus = "Approved";
            r.Comment = c;
            r.ApprovedDate = DateTime.Today.Date;
            
            db.Entry(r).State = EntityState.Modified;
            db.SaveChanges();

            string reqListId = r.RequisitionId.ToString();
            notify.NewRequisitonMade(reqListId);
        }
        public void UpdateRejectStatus(Requisition r,String c)
        {
           
            r.Comment = c;
            r.ApprovedDate = DateTime.Today.Date;
            r.RequisitionStatus = "Rejected";
            db.Entry(r).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
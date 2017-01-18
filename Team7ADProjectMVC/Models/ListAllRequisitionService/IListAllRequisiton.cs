using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ListAllRequisitionService
{
    public class IListAllRequisiton:IIListAllRequisition
    {
        ProjectEntities db = new ProjectEntities();
      

        public List<Requisition> GetAllRequisition()
        {
            var queryByStatus = from t in db.Requisitions 
                                  where t.RequisitionStatus == "Pending"
                                  orderby t.RequisitionId ascending
                                  select t;
            return (queryByStatus.ToList());
        }
        public Requisition FindById(int id)
        {
            return db.Requisitions.Find(id);
        }
        public void  UpdateApproveStatus(int id)
        {
            var r = from t in db.Requisitions
                                where t.RequisitionId == id
                                orderby t.RequisitionId ascending
                                select t;
           
            db.SaveChanges();
        }

    }
}
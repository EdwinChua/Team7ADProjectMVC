using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ListAllRequisitionService
{
    public interface IRequisitionService
    {
        List<Requisition> GetAllRequisition(int? depId);       
        Requisition FindById(int? id);
      
        void UpdateApproveStatus(Requisition r,String c);
        void UpdateRejectStatus(Requisition r,String c);
    }
}
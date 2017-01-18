using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ListAllRequisitionService
{
    public interface IIListAllRequisition
    {
        List<Requisition> GetAllRequisition();
       
        Requisition FindById(int? id);
        void UpdateApproveStatus(Requisition r,String c);
        void UpdateRejectStatus(Requisition r,String c);
    }
}
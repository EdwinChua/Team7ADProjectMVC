using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ListAllRequisitionService
{
    public interface IIListAllRequisition
    {
        List<Requisition> GetAllRequisition();
       
        Requisition FindById(int id);
        void UpdateApproveStatus(Requisition r);
        void UpdateRejectStatus(Requisition r);
    }
}
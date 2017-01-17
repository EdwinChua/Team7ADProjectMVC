using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ListAllRequisitionService
{
    public interface IIRequisitionDetails
    {
        List<RequisitionDetail> GetAllRequisition();
       List<RequisitionDetail> FindById(int id);
    }
}
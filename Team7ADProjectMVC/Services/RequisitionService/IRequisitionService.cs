﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ListAllRequisitionService
{
    public interface IRequisitionService
    {
        List<Requisition> GetAllRequisition(int? depId);       
        Requisition FindById(int? requisitionId);
      
        void UpdateApproveStatus(Requisition requisition,string comments);
        void UpdateRejectStatus(Requisition requisition, string comments);
        List<Requisition> getDataForPagination(string searchString);
    }
}
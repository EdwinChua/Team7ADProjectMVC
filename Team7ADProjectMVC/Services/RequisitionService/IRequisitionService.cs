﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ListAllRequisitionService
{
    public interface IRequisitionService
    {
        List<Requisition> ListAllRequisition();
        List<Requisition> GetAllRequisition(int? depId);       
        Requisition FindById(int? requisitionId);
      
        void UpdateApproveStatus(Requisition requisition,string comments);
        void UpdateRejectStatus(Requisition requisition, string comments);
        List<Requisition> getDataForPagination(string searchString);
        List<RequisitionDetail> GetAllRequisitionDetails(int dId, int rId);
        List<RequisitionDetail> GetAllRequisitionDetails();
        void CreateRequisition(Requisition r);
        void UpdateRequisition(Requisition requisition, Requisition req, int idd, int eid, int? deid);
    }
}
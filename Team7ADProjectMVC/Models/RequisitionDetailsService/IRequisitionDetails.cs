using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.RequisitionDetailsService
{
    public class IRequisitionDetails
    {
        ProjectEntities db = new ProjectEntities();


        public List<RequisitionDetail> GetAllRequisition()
        {
            //var queryByStatus = from t in db.Requisitions
            //                    where t.RequisitionStatus == "Pending"
            //                    orderby t.RequisitionId ascending
            //                    select t;
            //return (queryByStatus.ToList());
        }
        public List<RequisitionDetail> FindById(int id)
        {
            var queryByid = from t in db.RequisitionDetails
                                where t.RequisitionId == id
                               
                                select t;
            return (queryByid.ToList());
        }
    }
}
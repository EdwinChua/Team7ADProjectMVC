using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.InventoryAdjustmentService
{
    public class InventoryAdjustmentService : IInventoryAdjustmentService
    {
        ProjectEntities db = new ProjectEntities();
        public string findRolebyUserID(int userid)
        {
            string role = db.Employees.Find(userid).Role.Name;
            return (role);
        }
        public List<Adjustment> findSupervisorAdjustmentList()
        {

            var adjustmentlist = (from x in db.Adjustments
                                  where x.Status == "Pending Approval"
                                  || x.Status == "Approved"
                                  || x.Status == "Rejected"
                                  orderby x.AdjustmentDate
                                  select x
                                   ).ToList();


            return (adjustmentlist);
        }
        public List<Adjustment> findManagerAdjustmentList()
        {
            var adjustmentlist = (from x in db.Adjustments
                                  where x.Status == "Pending final Approval"
                                  || x.Status == "Approved"
                                  || x.Status == "Rejected"
                                  orderby x.AdjustmentDate
                                  select x
                      ).ToList();

            return (adjustmentlist);
        }
        //public List<Adjustment > FindAdjustmentBySearch(int employee ,string date,string status)
        //{

        //}

    }
}
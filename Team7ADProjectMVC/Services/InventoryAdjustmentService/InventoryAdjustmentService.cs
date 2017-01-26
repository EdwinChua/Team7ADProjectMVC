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

        //    public List<Adjustment> FindAdjustmentBySearch(string employee, string date, string status)
        //    {

        //        int epyid = Int32.Parse(employee);
        //        List<String> datesplit = date.Split('/').ToList<String>();
        //        DateTime selectedate = new DateTime(Int32.Parse((datesplit[2])), Int32.Parse((datesplit[1])), Int32.Parse((datesplit[0])));
        //        if ((status == null || status == "") && (date == null || date == "")&& (employee == null || date == ""))
        //        {

        //        }
        //        else if ( (date == null || date == "") && (employee == null || date == ""))//only select status
        //        {

        //        }
        //        else if((status == null || status == "") && (date == null || date == ""))//only select employee
        //        {

        //        }
        //        else if ( (status == null || status == "") && (employee == null || date == ""))//only select date
        //        {

        //        }
        //        else if ((date == null || date == ""))
        //        {

        //        }
        //        else if ((status == null || status == ""))
        //        {

        //        }
        //        else if ((employee == null || date == ""))
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }

        //}
    }
}
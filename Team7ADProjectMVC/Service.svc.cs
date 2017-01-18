using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Team7ADProjectMVC
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
	public class Service : IService
	{
        ProjectEntities db = new ProjectEntities();
        public List<WCFMsg> DoWork()
        {
            List<WCFMsg> l = new List<WCFMsg>();
            l.Add(new WCFMsg("ok"));
            l.Add(new WCFMsg("ok2"));
            l.Add(new WCFMsg("ok3"));
            Console.Write(l.ToString());
            return l;

        }

        public List<wcfRequisitionList> RequisitionList()
        {
            List<wcfRequisitionList> making = new List<wcfRequisitionList>();
           
         List<Requisition> r = db.Requisitions.ToList();

         foreach(Requisition rr in r)
         {
             wcfRequisitionList rl = new wcfRequisitionList();
             rl.Employeename = rr.Employee.EmployeeName;
             rl.Status = rr.RequisitionStatus;
             rl.Id = rr.RequisitionId.ToString();
             making.Add(rl);
         }
         return making;
        }


        public List<wcfRequisitionItem> getrequisitionitem(String id)
        {
            List<wcfRequisitionItem> making = new List<wcfRequisitionItem>();
            int newid = Convert.ToInt32(id);
            List<RequisitionDetail> r = db.RequisitionDetails.Where(x => x.RequisitionId == newid).ToList();

            foreach (RequisitionDetail rr in r)
            {
                wcfRequisitionItem rl = new wcfRequisitionItem();
                rl.Itemname = rr.Inventory.Description;
                rl.Quanity= rr.Quantity.ToString();
                rl.Uom = rr.OutstandingQuantity.ToString();
                making.Add(rl);
            }
            return making;
        }

        public List<wcfTodayCollectionlist> getTodayCollection(String deptid)
        {
            List<wcfTodayCollectionlist> making = new List<wcfTodayCollectionlist>();
            int newid = Convert.ToInt32(deptid);
            var r = from x in db.DisbursementLists
                                       where x.DepartmentId == newid
                                       && x.Status != "Completed"
                                       select x;
            
            foreach (DisbursementList rr in r)
            {
                wcfTodayCollectionlist rl = new wcfTodayCollectionlist();
                rl.Collectionpt = rr.CollectionPoint.PlaceName;
                rl.Time = rr.CollectionPoint.CollectTime.ToString();
                making.Add(rl);
            }
            return making.ToList();
        }

        public List<wcfTodayCollectionDetail> getTodayCollectionDetail(String deptid, String reqDetailID)
        {
            List<wcfTodayCollectionDetail> collectionDetail = new List<wcfTodayCollectionDetail>();
            int did = Convert.ToInt32(deptid);
            int reqID = Convert.ToInt32(reqDetailID);
        

             var dDetail = from r in db.DisbursementDetails
                          where r.DisbursementList.DepartmentId == did
                          && r.RequisitionDetailId == reqID
                          select r;

            foreach (DisbursementDetail dd in dDetail)
            {
                wcfTodayCollectionDetail cd = new wcfTodayCollectionDetail();
                cd.RequestedQty =dd.RequisitionDetail.Quantity.ToString();
                cd.DisbursedQty = dd.Quantity.ToString();
                cd.ItemDescription = dd.RequisitionDetail.Inventory.Description;
                collectionDetail.Add(cd);
            }
            return collectionDetail.ToList();
        }
    }
}

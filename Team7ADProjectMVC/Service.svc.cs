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

        public List<wcfRequisitionList> RequisitionList(string deptid)
        {
            List<wcfRequisitionList> making = new List<wcfRequisitionList>();
            int departmentId = Convert.ToInt32(deptid);
            var reqList = from req in db.Requisitions
                          where req.DepartmentId == departmentId
                          select req;

         foreach(Requisition rr in reqList)
         {
             wcfRequisitionList rl = new wcfRequisitionList();
             rl.Employeename = rr.Employee.EmployeeName;
             rl.Status = rr.RequisitionStatus;
             rl.Id = rr.RequisitionId.ToString();
             making.Add(rl);
         }
         return making.ToList();
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
            var r = from x in db.DisbursementDetails
                    where x.DisbursementList.DepartmentId == newid
                    && x.DisbursementList.Status != "Completed"
                    && x.DisbursementList.DeliveryDate.Equals(DateTime.Today)
                    orderby x.DisbursementList.Status
                    select x;
            var list = r.ToList();
            List<DisbursementDetail> tempList = new List<DisbursementDetail>();
            foreach (var item in list)
            {
                if(item.DisbursementList.DeliveryDate.Equals(DateTime.Today))
                {
                    wcfTodayCollectionlist itemTemp = new wcfTodayCollectionlist();
                    itemTemp.Collectionpt = item.DisbursementList.CollectionPoint.PlaceName;
                    string time = item.DisbursementList.CollectionPoint.CollectTime.ToString();
                    string reqDetailID = item.RequisitionDetailId.ToString();

                    making.Add(itemTemp);
                }
            }
            return making;
            //foreach (DisbursementDetail rr in r)
            //{
            //    wcfTodayCollectionlist rl = new wcfTodayCollectionlist();
            //    rl.Collectionpt = rr.DisbursementList.CollectionPoint.PlaceName.ToString();
            //    rl.Time = rr.DisbursementList.CollectionPoint.CollectTime.ToString();
            //    rl.RequisitionDetailID = rr.RequisitionDetailId.ToString();
            //    making.Add(rl);
            //}
            //return making.ToList();
        }

        public List<wcfTodayCollectionDetail> getTodayCollectionDetail(String deptid, String requisitionID)
        {
            List<wcfTodayCollectionDetail> collectionDetail = new List<wcfTodayCollectionDetail>();
            int did = Convert.ToInt32(deptid);
            int reqID = Convert.ToInt32(requisitionID);
        
            var dDetail = from r in db.DisbursementDetails
                          where r.DisbursementList.DepartmentId == did
                          && r.RequisitionDetail.RequisitionId == reqID
                          select r;

            foreach (DisbursementDetail dd in dDetail)
            {
                wcfTodayCollectionDetail cd = new wcfTodayCollectionDetail();
                cd.RequestedQty =dd.RequisitionDetail.Quantity.ToString();
                cd.DisbursedQty = dd.DeliveredQuantity.ToString();
                cd.ItemDescription = dd.RequisitionDetail.Inventory.Description;
                collectionDetail.Add(cd);
            }
            return collectionDetail.ToList();
        }

        public List<wcfApproveRequisitions> getApproveReqList(String deptid)
        {
            List<wcfApproveRequisitions> approvalList = new List<wcfApproveRequisitions>();
            int did = Convert.ToInt32(deptid);

            var aList = from a in db.Requisitions
                          where a.DepartmentId == did
                          && a.RequisitionStatus != "Approved"
                          orderby a.OrderedDate
                          select a;

            foreach (Requisition req in aList)
            {
                wcfApproveRequisitions cd = new wcfApproveRequisitions();
                cd.EmployeeName = req.Employee.EmployeeName.ToString();
                cd.RequestedDate = req.OrderedDate.ToString();
                cd.RequisitionID = req.RequisitionId.ToString();
                approvalList.Add(cd);
            }
            return approvalList.ToList();
        }
     
    }
}

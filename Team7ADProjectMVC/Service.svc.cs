using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Team7ADProjectMVC.Models;

namespace Team7ADProjectMVC
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
	public class Service : IService
	{
        ProjectEntities db = new ProjectEntities();

        InventoryService invService = new InventoryService();
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
                          orderby req.RequisitionStatus descending
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

        public List<wcfRequisitionItem> getrequisitionitem(String deptId, String reqID)
        {
            List<wcfRequisitionItem> making = new List<wcfRequisitionItem>();
            int dId = Convert.ToInt32(deptId);
            int rId = Convert.ToInt32(reqID);
            var reqItem= from req in db.RequisitionDetails
                         where req.Requisition.DepartmentId == dId
                        && req.RequisitionId==rId
                        orderby req.DeliveryStatus ascending
                        select req;
            
            foreach (RequisitionDetail rr in reqItem)
            {
                wcfRequisitionItem rl = new wcfRequisitionItem();
                rl.Itemname = rr.Inventory.Description;
                rl.Quantity = rr.Quantity.ToString();
                rl.Uom = rr.OutstandingQuantity.ToString();
                making.Add(rl);
            }
            return making;
        }

        public List<wcfTodayCollectionlist> getTodayCollection(String deptid)
        {
            List<wcfTodayCollectionlist> making = new List<wcfTodayCollectionlist>();
            int dId = Convert.ToInt32(deptid);
            var r = from x in db.DisbursementLists
                    where x.DepartmentId == dId
                    && x.Status != "Completed"
                    orderby x.Status
                    select x;
            var list = r.ToList();
            List<DisbursementDetail> tempList = new List<DisbursementDetail>();
            foreach (var item in list)
            {
                if(item.DeliveryDate.Equals(DateTime.Today))
                {
                    wcfTodayCollectionlist itemTemp = new wcfTodayCollectionlist();
                    itemTemp.Collectionpt = item.CollectionPoint.PlaceName;
                    itemTemp.Time = item.CollectionPoint.CollectTime.ToString();
                    itemTemp.DisbursementListID = item.DisbursementListId.ToString();
                    making.Add(itemTemp);
                }
            }
            return making;
           
        }

        public List<wcfTodayCollectionDetail> getTodayCollectionDetail(String deptid, String disListID)
        {
            List<wcfTodayCollectionDetail> collectionDetail = new List<wcfTodayCollectionDetail>();
            int did = Convert.ToInt32(deptid);
            int disbursementListID = Convert.ToInt32(disListID);
        
            var dDetail = from r in db.DisbursementDetails
                          where r.DisbursementList.DepartmentId == did
                          && r.DisbursementListId== disbursementListID
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
                cd.EmpName = req.Employee.EmployeeName.ToString();
                cd.ReqDate = req.OrderedDate.ToString();
                cd.ReqID = req.RequisitionId.ToString();
                approvalList.Add(cd);
            }
            return approvalList.ToList();
        }

        public List<wcfApproveReqDetails> getApproveReqDetails(String deptId, String reqId)
        {
            List<wcfApproveReqDetails> approvalList = new List<wcfApproveReqDetails>();
            int dId = Convert.ToInt32(deptId);
            int rId = Convert.ToInt32(reqId);
            var aList = from a in db.RequisitionDetails
                        where a.RequisitionId == rId
                        && a.Requisition.DepartmentId == dId
                        && a.Requisition.RequisitionStatus != "Approved"
                        && a.DeliveryStatus != "Delivered" 
                        orderby a.Inventory.Description ascending
                        select a;

            foreach (RequisitionDetail req in aList)
            {
                wcfApproveReqDetails rd = new wcfApproveReqDetails();
                rd.Item = req.Inventory.Description;
                rd.Quantity = req.Quantity.ToString();
                rd.UOM = req.Inventory.Measurement.UnitOfMeasurement;
                approvalList.Add(rd);
            }
            return approvalList.ToList();
        }


        public List<String> getCollectionPoint(String deptid)
        {
            List<String> sl = new List<string>();
            int dId = Convert.ToInt32(deptid);
             var collectionLocation = from c in db.DisbursementLists
                                    where c.DepartmentId == dId
                                   select c;
            String s;
             foreach (DisbursementList d in collectionLocation)
            {
                s= d.CollectionPoint.PlaceName +" "+ d.CollectionPoint.CollectTime;
               sl.Add(s);
            }
         
            return sl;
        }

        public List<wcfDisbursementList> getDisbursementList()
        {
            List<wcfDisbursementList> dList = new List<wcfDisbursementList>();
            var disburse = from d in db.DisbursementLists
                           where d.Status != "Completed"
                           select d;

            foreach (DisbursementList d in disburse)
            {
                wcfDisbursementList dl = new wcfDisbursementList();
                dl.DeptName = d.Department.DepartmentName;
                dl.CollectionPoint = d.CollectionPoint.PlaceName;
                dl.DeliveryDate = d.DeliveryDate.ToString();
                dl.DeliveryTime = d.CollectionPoint.CollectTime.ToString();
                dl.RepName = d.Department.Employee.EmployeeName.ToString();
                dl.RepPhone = d.Department.Employee.PhNo.ToString();
                dl.DisListID = d.DisbursementListId.ToString();
                dList.Add(dl);
            }
            return dList;
        }

        public List<wcfDisbursementListDetail> getDisbursementListDetails(String disListID)
        {
            List<wcfDisbursementListDetail> dDetail = new List<wcfDisbursementListDetail>();
            int dId = Convert.ToInt32(disListID);
            var disDetail = from dd in db.DisbursementDetails
                            where dd.DisbursementListId == dId
                            select dd;

            foreach (DisbursementDetail d in disDetail)
            {
                wcfDisbursementListDetail dd = new wcfDisbursementListDetail();
                dd.ItemName = d.RequisitionDetail.Inventory.Description;
                dd.ReqQty = d.RequisitionDetail.Quantity.ToString();
                dd.DisbQty = d.DeliveredQuantity.ToString();
                dd.Remarks = d.Remark;
                dDetail.Add(dd);
            }
            return dDetail;
        }

        public List<wcfStockReorder> getStockReorder()
        {
            List<wcfStockReorder> soList = new List<wcfStockReorder>();
            var reOrders = from so in db.Inventories
                           where so.Quantity <= so.ReorderLevel
                           select so;

            foreach (Inventory i in reOrders)
            {
                wcfStockReorder inv = new wcfStockReorder();
                inv.ItemName = i.Description;
                inv.ActualQty = i.Quantity.ToString();
                inv.ReorderLevel = i.ReorderLevel.ToString();
                inv.ReorderQty = i.ReorderQuantity.ToString();
                inv.Supplier1 = i.Supplier.SupplierName;
                inv.S1Phone = i.Supplier.PhNo.ToString();
                inv.Supplier2 = i.Supplier1.SupplierName;
                inv.S2Phone = i.Supplier1.PhNo.ToString();
                inv.Supplier3 = i.Supplier2.SupplierName;
                inv.S3Phone = i.Supplier2.PhNo.ToString();

                soList.Add(inv);
            }
            return soList;
        }


        public List<wcfRetrivalList> getRetrivalList()
        {
            List<wcfRetrivalList> retrialList = new List<wcfRetrivalList>();
            
            RetrievalList reList = new RetrievalList();
            invService.PopulateRetrievalList();
            invService.PopulateRetrievalListItems();
            reList = invService.GetRetrievalList();
            int? rid =reList.retrievalId;
            List<RetrievalListItems> itemsToR = reList.itemsToRetrieve;

            foreach (RetrievalListItems r in itemsToR)
            {
                wcfRetrivalList rl = new wcfRetrivalList();
                rl.ItemNo = r.itemNo;
                rl.ItemName = r.description;
                rl.RequestedQty = r.requiredQuantity.ToString();
                rl.RetrievedQty = r.collectedQuantity.ToString();
                String st = "";
                if(r.collectionStatus.ToString().Equals("False"))
                {
                    st = "Not Collected";
                }
                else
                {
                    st = "Collected";
                }
                rl.Status = st;
                retrialList.Add(rl);
            }
            //List<Requisition> reqList = retrivallist.requisitionList;
            return retrialList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Team7ADProjectMVC.Models
{
    public class InventoryService : IInventoryService
    {
        ProjectEntities db = new ProjectEntities();

        public Inventory FindById(string id)
        {
            return db.Inventories.Find(id);
        }
        public void AddItem(Inventory inventory)
        {
            db.Inventories.Add(inventory);
            db.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            var categories = db.Categories;
            return (categories.ToList());
        }

        public List<Inventory> GetAllInventory()
        {
            var inventories = db.Inventories;
            return (inventories.ToList());
        }

        public List<Measurement> GetAllMeasurements()
        {
            var measurements = db.Measurements;
            return (measurements.ToList());
        }

        public List<Supplier> GetAllSuppliers()
        {
            var suppliers = db.Suppliers;
            return (suppliers.ToList());
        }

        public void UpdateInventory(Inventory inventory)
        {
            db.Entry(inventory).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<Inventory> GetInventoryListByCategory(int id)
        {
            var queryByCategory = from t in db.Inventories
                                  where t.Category.CategoryId == id
                                  orderby t.Description ascending
                                  select t;
            return (queryByCategory.ToList());
        }
        public List<StockCard> GetStockCardFor(String id)
        {
            var query = from stockCard in db.StockCards
                        where stockCard.ItemNo == id
                        orderby stockCard.Date
                        select stockCard;
            return (query.ToList());
        }

        public List<Requisition> GetOutStandingRequisitions()
        {
            var query = from rq in db.Requisitions
                        where rq.RequisitionStatus != "Complete"
                        && rq.RequisitionStatus != "Pending"
                        && rq.RequisitionStatus != "Rejected"
                        orderby rq.ApprovedDate
                        select rq;

            List<Requisition> temp = query.ToList();
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList rList = (RetrievalList)System.Web.HttpContext.Current.Application["RetrievalList"];
            System.Web.HttpContext.Current.Application.UnLock();

            try
            {
                if (temp.Count != 0 && rList.requisitionList.Count != 0)
                {
                    try
                    {
                        temp = temp.Intersect(rList.requisitionList).ToList();
                    }
                    catch
                    {

                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Expected" + e.ToString());
            }
            return (temp);

        }

        public RetrievalList GetRetrievalList()
        {
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList rList = (RetrievalList)System.Web.HttpContext.Current.Application["RetrievalList"];


            if (rList.retrievalId == null)
            {
                var query = from rt in db.Retrievals
                            orderby rt.RetrievalId
                            select rt;
                rList.retrievalId = (query.ToList()).Last().RetrievalId + 1;
                Retrieval tempRetrieval = new Retrieval();
                tempRetrieval.RetrievalId = (int)rList.retrievalId;
                tempRetrieval.RetrievalDate = DateTime.Today;
                db.Retrievals.Add(tempRetrieval);
                db.SaveChanges();
                System.Web.HttpContext.Current.Application["RetrievalList"] = rList;
            }

            System.Web.HttpContext.Current.Application.UnLock();
            return rList;
        }

        public void PopulateRetrievalList()
        {
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList rList = (RetrievalList)System.Web.HttpContext.Current.Application["RetrievalList"];

            rList.requisitionList = GetOutStandingRequisitions();

            System.Web.HttpContext.Current.Application["RetrievalList"] = rList;
            System.Web.HttpContext.Current.Application.UnLock();
        }
        public void PopulateRetrievalListItems()
        {
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList rList = (RetrievalList)System.Web.HttpContext.Current.Application["RetrievalList"];
            if (rList.itemsToRetrieve == null)
            {
                rList.itemsToRetrieve = new List<RetrievalListItems>();

                List<RetrievalListItems> unconsolidatedList = new List<RetrievalListItems>();

                foreach (Requisition requisition in rList.requisitionList)
                {
                    foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                    {
                        RetrievalListItems newItem = new RetrievalListItems();
                        newItem.itemNo = reqDetails.ItemNo;
                        newItem.requiredQuantity = (int)reqDetails.OutstandingQuantity;
                        newItem.binNo = reqDetails.Inventory.BinNo;
                        newItem.description = reqDetails.Inventory.Description;
                        newItem.collectionStatus = false;
                        unconsolidatedList.Add(newItem);
                    }
                }
                RetrievalListItemsComparer comparer = new RetrievalListItemsComparer();
                unconsolidatedList.Sort(comparer);

                int i = 0;
                foreach (var item in unconsolidatedList)
                {
                    if (i == 0)
                    {
                        rList.itemsToRetrieve.Add(item);
                        i++;
                    }
                    else if (item.itemNo.Equals(rList.itemsToRetrieve[i - 1].itemNo))
                    {
                        rList.itemsToRetrieve[i - 1].requiredQuantity += item.requiredQuantity;
                    }
                    else
                    {
                        rList.itemsToRetrieve.Add(item);
                        i++;
                    }
                }
            }

            HttpContext.Current.Application["RetrievalList"] = rList;
            HttpContext.Current.Application.UnLock();
        }

        public void ClearRetrievalList()
        {
            System.Web.HttpContext.Current.Application["RetrievalList"] = new RetrievalList();
        }

        public void AutoAllocateDisbursements()
        {
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList retrievalList = (RetrievalList)System.Web.HttpContext.Current.Application["RetrievalList"];


            List<Requisition> requisitionListFromRList = retrievalList.requisitionList;
            RequisitionComparer comparer = new RequisitionComparer();
            requisitionListFromRList.Sort(comparer); //Sorts by dept

            //List<DisbursementList> newDisbursementList = new List<DisbursementList>();
            DisbursementList dList = new DisbursementList();
            List<DisbursementDetail> tempDisbursementDetailList = new List<DisbursementDetail>();
            HashSet<DisbursementDetail> DisbursementDetailHashSet;
            List<DisbursementDetail> DisbursementDetailListNoDuplicates;
            int? currentDisbursementListId = null;

            int i = 0;
            foreach (Requisition requisition in requisitionListFromRList)
            {
                if (i == 0) // if its first time entering loop, create new disbursementlist for dept
                {
                    Department d = db.Departments.Find(requisition.DepartmentId);
                    dList.DepartmentId = d.DepartmentId;
                    dList.CollectionPointId = d.CollectionPointId;
                    dList.OrderedDate = requisition.OrderedDate;
                    dList.RetrievalId = retrievalList.retrievalId;
                    dList.Status = "Pending Delivery";
                    dList.DeliveryDate = DateTime.Today.AddDays(2); //TODO: Place logic for date later

                    db.Set(typeof(DisbursementList)).Attach(dList);
                    db.DisbursementLists.Add(dList);
                    db.SaveChanges(); // creates new disbursementlist

                    currentDisbursementListId = dList.DepartmentId; //returns created disbursementlist Id


                    foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                    {
                        DisbursementDetail newDisbursementDetail = new DisbursementDetail();
                        //newDisbursementDetail.RequisitionDetailId = reqDetails.RequisitionDetailId;
                        newDisbursementDetail.DisbursementListId = currentDisbursementListId;
                        newDisbursementDetail.ItemNo = reqDetails.ItemNo;
                        tempDisbursementDetailList.Add(newDisbursementDetail);
                        //db.Set(typeof(DisbursementDetail)).Attach(newDisbursementDetail);
                        //db.DisbursementDetails.Add(newDisbursementDetail);
                        //db.SaveChanges();
                    }

                    i++;
                }
                else if (requisition.DepartmentId.Equals(dList.DepartmentId))
                {
                    DisbursementList currentDisbursementList = db.DisbursementLists.Find(currentDisbursementListId);
                    foreach (DisbursementDetail dDetail in currentDisbursementList.DisbursementDetails) //check current disbursement detail for existing item
                    {
                        foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                        {
                            DisbursementDetail newDisbursementDetail = new DisbursementDetail();
                            //newDisbursementDetail.RequisitionDetailId = reqDetails.RequisitionDetailId;
                            newDisbursementDetail.DisbursementListId = currentDisbursementListId;
                            newDisbursementDetail.ItemNo = reqDetails.ItemNo;
                            tempDisbursementDetailList.Add(newDisbursementDetail);
                            //db.Set(typeof(DisbursementDetail)).Attach(newDisbursementDetail);
                            //db.DisbursementDetails.Add(newDisbursementDetail);
                            //db.SaveChanges();
                        }
                    }
                }
                else // different dept, create new disbursementlist
                {
                    DisbursementDetailHashSet = new HashSet<DisbursementDetail>(tempDisbursementDetailList);
                    // remove duplicates
                    DisbursementDetailListNoDuplicates = DisbursementDetailHashSet.ToList();

                    foreach (DisbursementDetail newDisbursementDetail in DisbursementDetailListNoDuplicates)
                    {
                        db.Set(typeof(DisbursementDetail)).Attach(newDisbursementDetail);
                        db.DisbursementDetails.Add(newDisbursementDetail);
                        db.SaveChanges();
                    }
                    tempDisbursementDetailList.Clear();
                    DisbursementDetailHashSet.Clear();
                    DisbursementDetailListNoDuplicates.Clear();

                    Department d = db.Departments.Find(requisition.DepartmentId);
                    dList.DepartmentId = d.DepartmentId;
                    dList.CollectionPointId = d.CollectionPointId;
                    dList.OrderedDate = requisition.OrderedDate;
                    dList.RetrievalId = retrievalList.retrievalId;
                    dList.Status = "Pending Delivery";
                    dList.DeliveryDate = DateTime.Today.AddDays(2); //TODO: Place logic for date later

                    db.Set(typeof(DisbursementList)).Attach(dList);
                    db.DisbursementLists.Add(dList);
                    db.SaveChanges(); // creates new disbursementlist

                    currentDisbursementListId = dList.DepartmentId; //returns created disbursementlist Id

                    foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                    {
                        DisbursementDetail newDisbursementDetail = new DisbursementDetail();
                        newDisbursementDetail.ItemNo = reqDetails.ItemNo;
                        //newDisbursementDetail.RequisitionDetailId = reqDetails.RequisitionDetailId;
                        newDisbursementDetail.DisbursementListId = currentDisbursementListId;
                        tempDisbursementDetailList.Add(newDisbursementDetail);
                        //db.Set(typeof(DisbursementDetail)).Attach(newDisbursementDetail);
                        //db.DisbursementDetails.Add(newDisbursementDetail);
                        //db.SaveChanges();
                    }
                }
            }
            DisbursementDetailHashSet = new HashSet<DisbursementDetail>(tempDisbursementDetailList);// remove duplicates
            DisbursementDetailListNoDuplicates = DisbursementDetailHashSet.ToList();

            foreach (DisbursementDetail newDisbursementDetail in DisbursementDetailListNoDuplicates)
            {
                db.Set(typeof(DisbursementDetail)).Attach(newDisbursementDetail);
                db.DisbursementDetails.Add(newDisbursementDetail);
                db.SaveChanges();
            }




            foreach (RetrievalListItems retrievalListItem in retrievalList.itemsToRetrieve)
            {
                foreach (Requisition requisition in retrievalList.requisitionList)
                {
                    foreach (RequisitionDetail requisitionDetail in requisition.RequisitionDetails)
                    {
                        if (retrievalListItem.itemNo.Equals(requisitionDetail.ItemNo))
                        {
                            int requestedQty = (int)requisitionDetail.Quantity;
                            if (retrievalListItem.collectedQuantity >= requestedQty)
                            {

                                //requisitionDetail.DisbursementDetails
                            }
                        }
                    }
                }
            }

            HttpContext.Current.Application["RetrievalList"] = retrievalList;
            HttpContext.Current.Application.UnLock();
        }

    }
}
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
                        orderby rq.ApprovedDate
                        select rq;

            List<Requisition> temp = query.ToList();
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList rList = (RetrievalList)System.Web.HttpContext.Current.Application["RetrievalList"];
            System.Web.HttpContext.Current.Application.UnLock();
            if (temp != null && rList != null)
            {
                try
                {
                    temp = temp.Intersect(rList.requisitionList).ToList();
                }
                catch
                {
                    
                }
            }
            return (temp);
        }

        public RetrievalList GetRetrievalList()
        {
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList rList = (RetrievalList) System.Web.HttpContext.Current.Application["RetrievalList"];
            

            if (rList.retrievalId == null)
            {
                var query = from rt in db.Retrievals
                            orderby rt.RetrievalId
                            select rt;
                rList.retrievalId = (query.ToList()).Last().RetrievalId + 1;
                Retrieval tempRetrieval = new Retrieval();
                tempRetrieval.RetrievalId = (int) rList.retrievalId;
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

            rList.itemsToRetrieve = new List<RetrievalListItems>();

            List<RetrievalListItems>  unconsolidatedList = new List<RetrievalListItems>();

            foreach (Requisition requisition in rList.requisitionList)
            {
                foreach(RequisitionDetail reqDetails in requisition.RequisitionDetails)
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
                if (i==0)
                {
                    rList.itemsToRetrieve.Add(item);
                    i++;
                }
                else if (item.itemNo.Equals(rList.itemsToRetrieve[i-1].itemNo))
                {
                    rList.itemsToRetrieve[i-1].requiredQuantity += item.requiredQuantity;
                }
                else
                {
                    rList.itemsToRetrieve.Add(item);
                    i++;
                }
            }

            System.Web.HttpContext.Current.Application["RetrievalList"] = rList;
            System.Web.HttpContext.Current.Application.UnLock();
        }

        public void ClearRetrievalList()
        {
            System.Web.HttpContext.Current.Application["RetrievalList"] = new RetrievalList();
        }
    }
}
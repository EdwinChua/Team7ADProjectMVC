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

        public String GetItemCode(String itemDesc)
        {
            String startingLetter = itemDesc[0].ToString();
            ItemCodeGenerator result= db.ItemCodeGenerators.Find(startingLetter);
            result.itemcount++;
            db.SaveChanges();
            string fmt = "000";
            return startingLetter + ((int)result.itemcount).ToString(fmt);
        }

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
            if (rList.requisitionList == null)
            {
                rList.requisitionList = GetOutStandingRequisitions();
            }

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


        public void AutoAllocateDisbursementsByOrderOfRequisition()
        {
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList retrievalList = (RetrievalList)System.Web.HttpContext.Current.Application["RetrievalList"];

            List<Requisition> requisitionListFromRList = retrievalList.requisitionList;


            DisbursementList dList = new DisbursementList();
            List<DisbursementDetail> tempDisbursementDetailList = new List<DisbursementDetail>();

            int? currentDisbursementListId = null;

            foreach (Requisition requisition in requisitionListFromRList)
            {
                var q = (from x in db.DisbursementLists
                         where x.RetrievalId == retrievalList.retrievalId
                         && x.DepartmentId == requisition.DepartmentId
                         select x).FirstOrDefault();
                if (q == null) // if its first time entering loop, create new disbursementlist for dept
                {

                    currentDisbursementListId = CreateNewDisbursementListForDepartment(dList, requisition, retrievalList, currentDisbursementListId);

                    foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                    {
                        AddDisbursementDetailToTempList(currentDisbursementListId, reqDetails, retrievalList, tempDisbursementDetailList);
                    }
                }
                else if (q.DepartmentId == requisition.DepartmentId)
                {
                    foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                    {
                        currentDisbursementListId = q.DisbursementListId;
                        AddDisbursementDetailToTempList(currentDisbursementListId, reqDetails, retrievalList, tempDisbursementDetailList);
                    }
                }
            }
            SaveDisbursementDetailsIntoDB(tempDisbursementDetailList);

            foreach (Requisition r in requisitionListFromRList)
            {
                Requisition temp = db.Requisitions.Find(r.RequisitionId);
                temp.RetrievalId = retrievalList.retrievalId;
                temp.RequisitionStatus = "Pending Collection";
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }

            ClearRetrievalList();
            HttpContext.Current.Application.UnLock();
        }

        public void SaveDisbursementDetailsIntoDB(List<DisbursementDetail> tempDisbursementDetailList)
        {
            var q = tempDisbursementDetailList
                    .GroupBy(ac => new
                    {
                        ac.DisbursementListId,
                        ac.ItemNo,
                    })
                    .Select(ac => new DisbursementDetail
                    {
                        DisbursementListId = (int)ac.Key.DisbursementListId,
                        ItemNo = ac.Key.ItemNo,
                        PreparedQuantity = ac.Sum(acs => acs.PreparedQuantity),
                        DeliveredQuantity = ac.Sum(acs => acs.DeliveredQuantity)
                    });


            foreach (DisbursementDetail newDisbursementDetail in q.ToList())
            {
                db.Set(typeof(DisbursementDetail)).Attach(newDisbursementDetail);
                db.DisbursementDetails.Add(newDisbursementDetail);
                db.SaveChanges();
            }
        }
        public void AddDisbursementDetailToTempList(int? currentDisbursementListId, RequisitionDetail reqDetails, RetrievalList retrievalList, List<DisbursementDetail> tempDisbursementDetailList)
        {
            DisbursementDetail newDisbursementDetail = new DisbursementDetail();
            newDisbursementDetail.DisbursementListId = currentDisbursementListId;
            newDisbursementDetail.ItemNo = reqDetails.ItemNo;

            var x = (from y in retrievalList.itemsToRetrieve
                     where y.itemNo == newDisbursementDetail.ItemNo
                     select y).SingleOrDefault();
            if (x.collectedQuantity >= reqDetails.OutstandingQuantity && x.collectedQuantity != 0)
            {
                newDisbursementDetail.PreparedQuantity = reqDetails.OutstandingQuantity;
                newDisbursementDetail.DeliveredQuantity = newDisbursementDetail.PreparedQuantity;
                x.collectedQuantity = x.collectedQuantity - (int)reqDetails.OutstandingQuantity;
            }
            else
            {
                newDisbursementDetail.PreparedQuantity = x.collectedQuantity;
                newDisbursementDetail.DeliveredQuantity = newDisbursementDetail.PreparedQuantity;
                x.collectedQuantity = x.collectedQuantity - (int)newDisbursementDetail.PreparedQuantity;
            }

            tempDisbursementDetailList.Add(newDisbursementDetail);
        }
        public int? CreateNewDisbursementListForDepartment(DisbursementList dList, Requisition requisition, RetrievalList retrievalList, int? currentDisbursementListId)
        {
            Department d = db.Departments.Find(requisition.DepartmentId);
            dList.DepartmentId = d.DepartmentId;
            dList.OrderedDate = requisition.OrderedDate;
            dList.RetrievalId = retrievalList.retrievalId;
            dList.Status = "Processing";
            dList.DeliveryDate = DateTime.Today.AddDays(2); //TODO: Place logic for date later

            db.Set(typeof(DisbursementList)).Attach(dList);
            db.DisbursementLists.Add(dList);
            db.SaveChanges(); // creates new disbursementlist

            currentDisbursementListId = db.DisbursementLists
                                        .OrderByDescending(x => x.DisbursementListId)
                                        .FirstOrDefault().DisbursementListId; //returns created disbursementlist Id
            return currentDisbursementListId;
        }

        //TODO: Historical code. To remove if nothing breaks
        public void AutoAllocateDisbursements() //Unused
        {
            System.Web.HttpContext.Current.Application.Lock();
            RetrievalList retrievalList = (RetrievalList)System.Web.HttpContext.Current.Application["RetrievalList"];

            List<Requisition> requisitionListFromRList = retrievalList.requisitionList;

            CustomizedComparers comparer = new CustomizedComparers();
            requisitionListFromRList.Sort(comparer); //Sorts by dept

            DisbursementList dList = new DisbursementList();
            List<DisbursementDetail> tempDisbursementDetailList = new List<DisbursementDetail>();

            int? currentDisbursementListId = null;

            int i = 0;
            foreach (Requisition requisition in requisitionListFromRList)
            {
                var q = (from x in db.DisbursementLists
                         where x.RetrievalId == requisition.RetrievalId
                         && x.DepartmentId == requisition.DepartmentId
                         select x).First();
                if (i == 0) // if its first time entering loop, create new disbursementlist for dept
                {

                    CreateNewDisbursementListForDepartment(dList, requisition, retrievalList, currentDisbursementListId);

                    currentDisbursementListId = db.DisbursementLists
                                                .OrderByDescending(x => x.DisbursementListId)
                                                .FirstOrDefault().DisbursementListId; //returns created disbursementlist Id

                    foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                    {
                        AddDisbursementDetailToTempList(currentDisbursementListId, reqDetails, retrievalList, tempDisbursementDetailList);
                        i++;
                    }
                }
                else if (requisition.DepartmentId.Equals(dList.DepartmentId))
                {
                    foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                    {
                        AddDisbursementDetailToTempList(currentDisbursementListId, reqDetails, retrievalList, tempDisbursementDetailList);
                    }
                }
                else if (!requisition.DepartmentId.Equals(dList.DepartmentId))// different dept, create new disbursementlist
                {
                    dList = new DisbursementList();
                    CreateNewDisbursementListForDepartment(dList, requisition, retrievalList, currentDisbursementListId);

                    currentDisbursementListId = db.DisbursementLists
                                                .OrderByDescending(x => x.DisbursementListId)
                                                .FirstOrDefault().DisbursementListId; //returns created disbursementlist Id

                    foreach (RequisitionDetail reqDetails in requisition.RequisitionDetails)
                    {
                        AddDisbursementDetailToTempList(currentDisbursementListId, reqDetails, retrievalList, tempDisbursementDetailList);
                    }
                }
            }
            SaveDisbursementDetailsIntoDB(tempDisbursementDetailList);

            ClearRetrievalList();
            HttpContext.Current.Application.UnLock();
        }

        public List<DisbursementDetail> GenerateListForManualAllocation()
        {
            int lastRetrievalListId = db.Retrievals
                                        .OrderByDescending(x => x.RetrievalId)
                                        .FirstOrDefault().RetrievalId;

            var currentDisbursement = (from x in db.DisbursementLists
                                       where x.RetrievalId == lastRetrievalListId
                                       select x).ToList();

            List<DisbursementDetail> tempDisbursementDetailList = new List<DisbursementDetail>();
            List<DisbursementDetail> returnDisbursementDetailList = new List<DisbursementDetail>();
            foreach (var x in currentDisbursement)
            {
                foreach (var y in x.DisbursementDetails)
                {
                    tempDisbursementDetailList.Add(y);
                }
            }

            var consolidatedDisbursementList = tempDisbursementDetailList
                                                .GroupBy(ac => new
                                                {
                                                    ac.ItemNo
                                                })
                                                .Select(ac => new DisbursementDetail
                                                {
                                                    ItemNo = ac.Key.ItemNo,
                                                    PreparedQuantity = ac.Sum(acs => acs.PreparedQuantity)
                                                });

            List<RequisitionDetail> tempRequisitionDetailList = new List<RequisitionDetail>();

            var test = (from x in db.Requisitions
                        where x.RetrievalId == lastRetrievalListId
                        select x).ToList();

            foreach (var item in test)
            {
                foreach (var item2 in item.RequisitionDetails)
                {
                    tempRequisitionDetailList.Add(item2);
                }
            }

            var consolidatedRequisitionList = tempRequisitionDetailList
                                                .GroupBy(ac => new
                                                {
                                                    ac.ItemNo
                                                })
                                                .Select(ac => new RequisitionDetail
                                                {
                                                    ItemNo = ac.Key.ItemNo,
                                                    OutstandingQuantity = ac.Sum(acs => acs.OutstandingQuantity),
                                                });


            foreach (var item in consolidatedDisbursementList)
            {
                var t = (from x in consolidatedRequisitionList
                         where item.ItemNo == x.ItemNo
                         select x).FirstOrDefault();
                if (t.OutstandingQuantity == item.PreparedQuantity)
                {
                    //ignore item
                    Console.WriteLine("Equal");
                }
                else if (t.OutstandingQuantity != item.PreparedQuantity)
                {
                    //do something
                    Console.WriteLine("Not Equal");
                    var x = (from y in tempDisbursementDetailList
                             where y.ItemNo == item.ItemNo
                             select y).ToList();
                    foreach (var i2 in x)
                    {
                        returnDisbursementDetailList.Add(i2);
                    }
                }
            }
            return returnDisbursementDetailList;
        }


        public int GetLastRetrievalListId()
        {
            int currentRetrievalListId = db.Retrievals
                                        .OrderByDescending(x => x.RetrievalId)
                                        .FirstOrDefault().RetrievalId;
            return currentRetrievalListId;
        }

        public List<Requisition> GetRequisitionsSummedByDept(int currentRetrievalListId)
        {
            List<Requisition> returnList = new List<Requisition>();
            //get current retrieval list
            var q = (from x in db.Requisitions
                    where x.RetrievalId == currentRetrievalListId
                    select x).ToList();
            HashSet<Department> test = new HashSet<Department>();

            foreach (var x in q.ToList())
            {
                Department d = db.Departments.Find(x.DepartmentId);
                test.Add(d);
            }

            foreach (Department d in test)
            {
                var q2 = from x in db.RequisitionDetails
                         where x.Requisition.RetrievalId == currentRetrievalListId
                         && x.Requisition.DepartmentId == d.DepartmentId
                         select x;

                var pp = q2.ToList();

                var q3 = pp
                        .GroupBy(ac => new
                        {
                            ac.ItemNo,
                        })
                        .Select(ac => new RequisitionDetail
                        {
                            ItemNo = ac.Key.ItemNo,
                            OutstandingQuantity = ac.Sum(acs => acs.OutstandingQuantity),
                        });
                Requisition req = new Requisition();
                req.RequisitionDetails = q3.ToList();
                req.DepartmentId = d.DepartmentId;
                returnList.Add(req);
            }
            return returnList;
        }

        public void ManuallyAllocateDisbursements(int[] departmentId, int[] preparedQuantity, int[] disbursementListId, int[] disbursementDetailId, string[] itemNo)
        {

            int deptId;
            int prepQty;
            int disburseListId;
            int disburseDetailId;
            string itemNumber;
            
            for (int i = 0; i < departmentId.Length; i++)
            {
                deptId = departmentId[i];
                prepQty = preparedQuantity[i];
                disburseListId = disbursementListId[i];
                disburseDetailId = disbursementDetailId[i];
                itemNumber = itemNo[i];

                //var q = (from x in db.DisbursementLists
                //        where x.DepartmentId == deptId
                //        && x.DisbursementListId == disburseListId
                //        select x).ToList();
                var q = (from x in db.DisbursementDetails
                         where x.DisbursementList.DepartmentId == deptId
                         && x.DisbursementListId == disburseListId
                         && x.DisbursementDetailId == disburseDetailId
                         && x.ItemNo == itemNumber
                         select x).SingleOrDefault();
                q.PreparedQuantity = prepQty;
                db.Entry(q).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
    }
}
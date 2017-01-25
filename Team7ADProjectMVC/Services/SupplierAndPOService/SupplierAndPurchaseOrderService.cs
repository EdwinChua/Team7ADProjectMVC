using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Services.SupplierService
{
    public class SupplierAndPurchaseOrderService : ISupplierAndPurchaseOrderService
    {
        ProjectEntities db = new ProjectEntities();
        public List<Supplier> GetAllSuppliers()
        {
            return (db.Suppliers.ToList());
        }

        public Supplier FindSupplierById(int? id)
        {
            return db.Suppliers.Find(id);
        }

        public List<Inventory> FindInventoryItemsBySupplier(int? id)
        {
            var q = from x in db.Inventories
                    where x.SupplierId1 == id
                    || x.SupplierId2 == id
                    || x.SupplierId3 == id
                    select x;
            return q.ToList();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            db.Entry(supplier).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddNewSupplier(Supplier supplier)
        {
            db.Suppliers.Add(supplier);
            db.SaveChanges();
        }

        public List<Inventory> GetAllItemsToResupply()
        {
            var q = (from x in db.Inventories
                     where (x.Quantity + x.HoldQuantity) < x.ReorderQuantity
                     select x).ToList();
            return q;
        }

        public void GeneratePurchaseOrders(string[] itemNo, int[] supplier, int?[] orderQuantity)
        {
            List<PurchaseOrder> listOfPurchaseOrdersToUpdate = new List<PurchaseOrder>();
            List<PurchaseDetail> listOfPurchaseDetails = new List<PurchaseDetail>();
            for (int i = 0; i < itemNo.Count(); i++)
            {
                if (orderQuantity[i] != null && orderQuantity[i] > 0)
                {
                    PurchaseDetail tempPurchaseDetail = new PurchaseDetail();
                    tempPurchaseDetail.ItemNo = itemNo[i];
                    tempPurchaseDetail.SupplierId = supplier[i];
                    tempPurchaseDetail.Quantity = orderQuantity[i];
                    listOfPurchaseDetails.Add(tempPurchaseDetail);
                }
            }
            int[] distinctSupplierArray = supplier.Distinct().ToArray();
            List<int> purgedSupplierList = new List<int>();
            for (int i = 0; i < distinctSupplierArray.Count(); i++)
            {
                var q = (from x in listOfPurchaseDetails
                         where x.SupplierId == distinctSupplierArray[i]
                         select x).ToList();
                if(q.Count() >0)
                {
                    purgedSupplierList.Add(distinctSupplierArray[i]);
                }
            }
            

            for (int i = 0; i < purgedSupplierList.Count(); i++)
            {
                int localSupplierId = purgedSupplierList[i];
                PurchaseOrder tempPurchaseOrder = new PurchaseOrder();
                tempPurchaseOrder.OrderDate = DateTime.Today;
                tempPurchaseOrder.SupplierId = localSupplierId;
                //TODO - Employee ID
                db.PurchaseOrders.Add(tempPurchaseOrder);
                db.SaveChanges();

                var lastCreatedPOId = db.PurchaseOrders
                                    .OrderByDescending(x => x.PurchaseOrderId)
                                    .FirstOrDefault().PurchaseOrderId;

                var q = (from x in listOfPurchaseDetails
                         where x.SupplierId == localSupplierId
                         select x).ToList();
                foreach (var item in q)
                {
                    item.PurchaseOrderId = lastCreatedPOId;
                    db.PurchaseDetails.Add(item);
                    db.SaveChanges();

                    Inventory tempInv = db.Inventories.Find(item.ItemNo);
                    tempInv.HoldQuantity += item.Quantity;
                    db.Entry(tempInv).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public List<PurchaseOrder> GetAllPOOrderByApproval()
        {
            var q = (from x in db.PurchaseOrders
                     orderby x.AuthorizedBy ascending
                     select x).ToList(); // Unapproved ones will be at the top
            
            return q;
        }
    }
}
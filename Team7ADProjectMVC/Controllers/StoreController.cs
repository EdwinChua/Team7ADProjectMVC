using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Exceptions;
using Team7ADProjectMVC.Models;
using Team7ADProjectMVC.Models.DelegateRoleService;
using Team7ADProjectMVC.Services;
using Team7ADProjectMVC.Services.DepartmentService;
using Team7ADProjectMVC.Services.SupplierService;

namespace Team7ADProjectMVC.TestControllers
{
    public class StoreController : Controller
    {
        private IInventoryService inventorySvc;
        private IDisbursementService disbursementSvc;
        private IDepartmentService deptSvc;
        private IDelegateRoleService delegateSvc;
        private ISupplierAndPurchaseOrderService supplierAndPOSvc;

        public StoreController()
        {
            inventorySvc = new InventoryService();
            disbursementSvc = new DisbursementService();
            deptSvc = new DepartmentService();
            delegateSvc = new DelegateRoleService();
            supplierAndPOSvc = new SupplierAndPurchaseOrderService();
        }

        //**************** INVENTORY ********************

        // GET: Store
        public ActionResult Index()
        {
            return View("Dashboard");
            //TODO: EDWIN - Create a nice dashboard or delete this
        }

        public ActionResult Inventory()
        {
            var inventories = inventorySvc.GetAllInventory();
            var categories = inventorySvc.GetAllCategories();
            ViewBag.Cat = categories.ToList();
            return View("ViewInventory",inventories);
        }

        public ActionResult InventoryItem(String id)
        {
            Inventory inventory = inventorySvc.FindIventoryItemById(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.inv = inventory;
            ViewBag.sCard = inventorySvc.GetStockCardFor(id);
            return View("ViewStockCard",inventory);
        }
        public ActionResult RetrievalList()
        {
            RetrievalList rList = inventorySvc.GetRetrievalList();
            ViewBag.RList = rList;
            return View("ViewRetrievalList");
        }

        public ActionResult MarkAsCollected(int collectedQuantity, string itemNo)
        {
            RetrievalList rList = inventorySvc.GetRetrievalList();
            foreach (var item in rList.itemsToRetrieve)
            {
                if (item.itemNo.Equals(itemNo))
                {
                    item.collectedQuantity = collectedQuantity;
                    item.collectionStatus = true;
                }
            }
            return RedirectToAction("RetrievalList");
        }

        public ActionResult New()
        {
            ViewBag.CategoryId = new SelectList(inventorySvc.GetAllCategories(), "CategoryId", "CategoryName");
            ViewBag.MeasurementId = new SelectList(inventorySvc.GetAllMeasurements(), "MeasurementId", "UnitOfMeasurement");
            ViewBag.SupplierId1 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode");
            ViewBag.SupplierId2 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode");
            ViewBag.SupplierId3 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode");
            return View("NewStockCard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "ItemNo,CategoryId,Description,ReorderLevel,ReorderQuantity,MeasurementId,Quantity,HoldQuantity,SupplierId1,Price1,SupplierId2,Price2,SupplierId3,Price3,BinNo")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                if ((inventory.SupplierId1 != inventory.SupplierId2) && (inventory.SupplierId1 != inventory.SupplierId3) && (inventory.SupplierId2 != inventory.SupplierId3))
                {
                    inventory.ItemNo = inventorySvc.GetItemCode(inventory.Description);
                    inventorySvc.AddItem(inventory);
                    return RedirectToAction("Inventory");
                }
                else
                {
                    ViewBag.Error = "Please ensure that all three suppliers are different.";
                }
            }
            else
            {
                ViewBag.Error = "Please ensure that all three suppliers are different.";
            }

            ViewBag.CategoryId = new SelectList(inventorySvc.GetAllCategories(), "CategoryId", "CategoryName", inventory.CategoryId);
            ViewBag.MeasurementId = new SelectList(inventorySvc.GetAllMeasurements(), "MeasurementId", "UnitOfMeasurement", inventory.MeasurementId);
            ViewBag.SupplierId1 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId1);
            ViewBag.SupplierId2 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId2);
            ViewBag.SupplierId3 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId3);
            return View("NewStockCard");
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = inventorySvc.FindIventoryItemById(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(inventorySvc.GetAllCategories(), "CategoryId", "CategoryName", inventory.CategoryId);
            ViewBag.MeasurementId = new SelectList(inventorySvc.GetAllMeasurements(), "MeasurementId", "UnitOfMeasurement", inventory.MeasurementId);
            ViewBag.SupplierId1 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId1);
            ViewBag.SupplierId2 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId2);
            ViewBag.SupplierId3 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId3);
            ViewBag.inv = inventory;
            return View("UpdateStockCard",inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemNo,CategoryId,Description,ReorderLevel,ReorderQuantity,MeasurementId,Quantity,HoldQuantity,SupplierId1,Price1,SupplierId2,Price2,SupplierId3,Price3,BinNo")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                inventorySvc.UpdateInventory(inventory);
                return RedirectToAction("Inventory");
            }
            ViewBag.CategoryId = new SelectList(inventorySvc.GetAllCategories(), "CategoryId", "CategoryName", inventory.CategoryId);
            ViewBag.MeasurementId = new SelectList(inventorySvc.GetAllMeasurements(), "MeasurementId", "UnitOfMeasurement", inventory.MeasurementId);
            ViewBag.SupplierId1 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId1);
            ViewBag.SupplierId2 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId2);
            ViewBag.SupplierId3 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId3);
            ViewBag.inv = inventory;
            return View("UpdateStockCard",inventory);
        }

        public ActionResult Search(int id)
        {
            var inventories = inventorySvc.GetInventoryListByCategory(id);
            var categories = inventorySvc.GetAllCategories();
            ViewBag.Cat = categories.ToList();
            return View("ViewInventory", inventories);
        }

        //************** DISBURSEMENTS **************

        public ActionResult ViewDisbursements()
        {
            ViewBag.Departments = deptSvc.ListAllDepartments();
            return View(disbursementSvc.GetAllDisbursements());
        }

        public ActionResult ViewDisbursement(int id)
        {
            DisbursementList dl = disbursementSvc.GetDisbursementById(id);
            ViewBag.disbursementListInfo = dl;
            ViewBag.Representative = deptSvc.FindEmployeeById((int)dl.Department.RepresentativeId);
            return View(dl);
        }

        public ActionResult SearchDisbursements(int? id, String status)
        { 
            ViewBag.Departments = deptSvc.ListAllDepartments();

            return View("ViewDisbursements", disbursementSvc.GetDisbursementsBySearchCriteria(id, status));
        }

        public ActionResult UpdateDisbursement(int disbursementListId, string[] itemNo, int[] originalPreparedQty, int[] adjustedQuantity, string[] remarks)
        {
            inventorySvc.UpdateDisbursementListDetails(disbursementListId, itemNo, originalPreparedQty, adjustedQuantity, remarks);
            return RedirectToAction("ViewDisbursements");
        }

        // ********************* ADJUSTMENTS *******************

        public ActionResult InventoryAdjustment()
        {
            //TODO: EDWIN - CX and Seng doing?

            return View();
        }

        public ActionResult CreateNewAdjustment()
        {
            //TODO: EDWIN - CX and Seng doing?
            return View();
        }

        // ********************* MAINTAIN *******************
        public ActionResult SupplierList()
        {
            return View(supplierAndPOSvc.GetAllSuppliers());
        }

        public ActionResult Supplier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierAndPOSvc.FindSupplierById(id);
            List<Inventory> listOfItemsFromSupplier = supplierAndPOSvc.FindInventoryItemsBySupplier(id);
            ViewBag.SupplierItems = listOfItemsFromSupplier;
            ViewBag.SupplierId = supplier.SupplierId;
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Supplier([Bind(Include = "SupplierId,SupplierCode,SupplierName,ContactName,PhNo,FaxNo,Address,GstRegistrationNo")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {

                supplierAndPOSvc.UpdateSupplier(supplier);
                return RedirectToAction("SupplierList");
            }
            return View("Supplier",supplier);
        }

        public ActionResult AddSupplier()
        {
            return View("Supplier");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSupplier([Bind(Include = "SupplierId,SupplierCode,SupplierName,ContactName,PhNo,FaxNo,Address,GstRegistrationNo")] Supplier supplier)
        {
                supplierAndPOSvc.AddNewSupplier(supplier);
                return RedirectToAction("SupplierList");
        }


        //****************** Outstanding Requisitions ***************

        public ActionResult ViewRequisitions()
        {
            RetrievalList rList = inventorySvc.GetRetrievalList();
            ViewBag.rList = rList;
            return View(inventorySvc.GetOutStandingRequisitions());
        }

        public ActionResult GenerateRetrievalList()
        {   
            inventorySvc.PopulateRetrievalList();
            inventorySvc.PopulateRetrievalListItems();
            return RedirectToAction("ViewRequisitions");
        }

        public ActionResult ClearRetrievalList()
        {
            inventorySvc.ClearRetrievalList();
            return RedirectToAction("ViewRequisitions");
        }

        public ActionResult DisburseItems()
        {
            inventorySvc.AutoAllocateDisbursementsByOrderOfRequisition();
            return RedirectToAction("ReallocateDisbursements");
        }
        public ActionResult ReallocateDisbursements()
        {
            List<DisbursementDetail> reallocationList = inventorySvc.GenerateListForManualAllocation();
            DisbursementListComparer comparer = new DisbursementListComparer(); //sort by item no
            reallocationList.Sort(comparer);
            int currentRetrievalListId = inventorySvc.GetLastRetrievalListId();
            List<Requisition> summedListByDepartment = inventorySvc.GetRequisitionsSummedByDept(currentRetrievalListId);
            ViewBag.MaxQuantityOfEachItem = summedListByDepartment;
            if (TempData["PrepQtyException"] != null)
            {
                ViewBag.PrepQtyException = TempData["PrepQtyException"].ToString();
            }  

            return View(reallocationList);
        }

        // ********************* Other *******************

        public ActionResult GenerateReports()
        {
            //Seng has done. To wire up with view
            return View();
        }


        public ActionResult Test(int[] departmentId, int[] preparedQuantity,int [] disbursementListId, int[] disbursementDetailId, string[] itemNo, int[] adjustedQuantity)
        {
            try
            {
                inventorySvc.ManuallyAllocateDisbursements(departmentId, preparedQuantity, adjustedQuantity, disbursementListId, disbursementDetailId, itemNo);
            }
            catch (InventoryAndDisbursementUpdateException e)
            {
                TempData["PrepQtyException"] = e;
            }

            return RedirectToAction("ReallocateDisbursements");
        }
    }
}
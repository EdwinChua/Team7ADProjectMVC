using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;
using Team7ADProjectMVC.Models.DepartmentService;

namespace Team7ADProjectMVC.TestControllers
{
    public class StoreController : Controller
    {
        private IInventoryService inventorySvc;
        private IDisbursementService disbursementSvc;
        private IDepartmentService deptSvc;

        public StoreController()
        {
            inventorySvc = new InventoryService();
            disbursementSvc = new DisbursementService();
            deptSvc = new DepartmentService();
        }

        //**************** INVENTORY ********************

        // GET: Store
        public ActionResult Index()
        {
            return View("Dashboard");
            //TODO: EDWIN - Create a nice dashboard
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
            Inventory inventory = inventorySvc.FindById(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.inv = inventory;
            //TODO: EDWIN - Require a view in DB to populate data to table
            return View("ViewStockCard",inventory);
        }
        public ActionResult RetrievalList()
        {
            //TODO: EDWIN - Implementation code here
            return View("ViewRetrievalList");
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
                inventorySvc.AddItem(inventory);
                return RedirectToAction("Inventory");
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
            Inventory inventory = inventorySvc.FindById(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(inventorySvc.GetAllCategories(), "CategoryId", "CategoryName", inventory.CategoryId);
            ViewBag.MeasurementId = new SelectList(inventorySvc.GetAllMeasurements(), "MeasurementId", "UnitOfMeasurement", inventory.MeasurementId);
            ViewBag.SupplierId1 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId1);
            ViewBag.SupplierId2 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId2);
            ViewBag.SupplierId3 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId3);
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
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(inventorySvc.GetAllCategories(), "CategoryId", "CategoryName", inventory.CategoryId);
            ViewBag.MeasurementId = new SelectList(inventorySvc.GetAllMeasurements(), "MeasurementId", "UnitOfMeasurement", inventory.MeasurementId);
            ViewBag.SupplierId1 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId1);
            ViewBag.SupplierId2 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId2);
            ViewBag.SupplierId3 = new SelectList(inventorySvc.GetAllSuppliers(), "SupplierId", "SupplierCode", inventory.SupplierId3);
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

        public ActionResult ViewDisbursement(String id)
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }

        public ActionResult SearchDisbursements(int? id, String status)
        {
            //disbursementSvc.GetDisbursementsBySearchCriteria(id, status);
            ViewBag.Departments = deptSvc.ListAllDepartments();

            return View("ViewDisbursements", disbursementSvc.GetDisbursementsBySearchCriteria(id, status));
        }

        // ********************* ADJUSTMENTS *******************

        public ActionResult InventoryAdjustment()
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }

        public ActionResult CreateNewAdjustment()
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }

        // ********************* MAINTAIN *******************
        public ActionResult SupplierList()
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }

        public ActionResult Supplier(String id)
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }


        // ********************* RESUPPLY *******************

        public ActionResult GeneratePO()
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }

        public ActionResult PurchaseOrderSummary(String id)
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }

        public ActionResult ViewReceiveOrder(String id)
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }

        // ********************* Other *******************

        public ActionResult GenerateReports()
        {
            //TODO: EDWIN - Implementation code here
            return View();
        }

    }
}
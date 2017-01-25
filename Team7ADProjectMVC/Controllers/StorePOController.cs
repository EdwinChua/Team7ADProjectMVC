using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;
using Team7ADProjectMVC.Models.DelegateRoleService;
using Team7ADProjectMVC.Services;
using Team7ADProjectMVC.Services.DepartmentService;
using Team7ADProjectMVC.Services.SupplierService;

namespace Team7ADProjectMVC.Controllers
{
    public class StorePOController : Controller
    {
        private IInventoryService inventorySvc;
        private IDisbursementService disbursementSvc;
        private IDepartmentService deptSvc;
        private IDelegateRoleService delegateSvc;
        private ISupplierAndPurchaseOrderService supplierAndPOSvc;

        public StorePOController()
        {
            inventorySvc = new InventoryService();
            disbursementSvc = new DisbursementService();
            deptSvc = new DepartmentService();
            delegateSvc = new DelegateRoleService();
            supplierAndPOSvc = new SupplierAndPurchaseOrderService();
        }

        public ActionResult GeneratePO()
        {
            //TODO: EDWIN
            List<Inventory> itemsToResupply = supplierAndPOSvc.GetAllItemsToResupply();

            return View(itemsToResupply);
        }

        public ActionResult GeneratePurchaseOrders(string[] itemNo, int[] supplier, int?[] orderQuantity)
        {
            supplierAndPOSvc.GeneratePurchaseOrders(itemNo, supplier, orderQuantity);

            List<Inventory> itemsToResupply = supplierAndPOSvc.GetAllItemsToResupply();
            return RedirectToAction("GeneratePO");
        }

        public ActionResult PurchaseOrderSummary()
        {
            List<PurchaseOrder> poList = supplierAndPOSvc.GetAllPOOrderByApproval();
            
            return View(poList);
        }
        public ActionResult SearchPurchaseOrderSummary(string orderStatus, DateTime? dateOrdered, DateTime? dateApproved)
        {
            List<PurchaseOrder> poList = supplierAndPOSvc.GetAllPOOrderByApproval();

            return View(poList);
        }

        public ActionResult ViewReceiveOrder(String id)
        {
            //TODO: EDWIN
            return View();
        }
    }
}
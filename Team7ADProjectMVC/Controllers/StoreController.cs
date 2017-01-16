using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;

namespace Team7ADProjectMVC.TestControllers
{
    public class StoreController : Controller
    {
        private IInventoryService inventorySvc;

        public StoreController()
        {
            inventorySvc = new InventoryService();
        }

        // GET: Store
        public ActionResult Index()
        {
            return View("Dashboard");
        }

        public ActionResult Inventory()
        {
            var inventories = inventorySvc.ReturnAllInventory();
            var categories = inventorySvc.ReturnAllCategories();
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
            return View("ViewStockCard",inventory);
        }
    }
}
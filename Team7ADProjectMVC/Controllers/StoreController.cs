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
        ProjectEntities db = new ProjectEntities();
        private IStoreService storeSvc;

        public StoreController()
        {
            storeSvc = new StoreService();
        }

        // GET: Store
        public ActionResult Index()
        {
            return View("Dashboard");
        }

        public ActionResult Inventory()
        {
            //var inventories = db.Inventories;
            //var categories = db.Categories;
            
            var inventories = storeSvc.ReturnAllInventory();
            var categories = storeSvc.ReturnAllCategories();
            ViewBag.Cat = categories.ToList();
            return View("ViewInventory",inventories);
        }

        public ActionResult InventoryItem(String id)
        {
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.inv = inventory;
            return View("ViewStockCard",inventory);
        }
    }
}
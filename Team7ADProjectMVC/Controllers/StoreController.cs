using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.TestControllers
{
    public class StoreController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        // GET: Store
        public ActionResult Index()
        {
            return View("Dashboard");
        }

        public ActionResult Inventory()
        {
            var inventories = db.Inventories;
            var categories = db.Categories;
            ViewBag.Cat = categories.ToList();
            return View("ViewInventory",inventories.ToList());
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
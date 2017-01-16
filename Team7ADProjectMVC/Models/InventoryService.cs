using System;
using System.Collections.Generic;
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

        public List<Category> ReturnAllCategories()
        {
            var categories = db.Categories;
            return (categories.ToList());
        }

        public List<Inventory> ReturnAllInventory()
        {
            var inventories = db.Inventories;
            return (inventories.ToList());
        }
        
    }
}
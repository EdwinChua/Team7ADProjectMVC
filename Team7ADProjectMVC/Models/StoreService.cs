using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    public class StoreService : IStoreService
    {
        ProjectEntities db = new ProjectEntities();

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
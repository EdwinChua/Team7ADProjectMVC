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
                        orderby rq.ApprovedDate
                        select rq;
            return (query.ToList());

        }
    }
}
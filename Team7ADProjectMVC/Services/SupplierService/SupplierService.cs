using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Services.SupplierService
{
    public class SupplierService : ISupplierService
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
    }
}
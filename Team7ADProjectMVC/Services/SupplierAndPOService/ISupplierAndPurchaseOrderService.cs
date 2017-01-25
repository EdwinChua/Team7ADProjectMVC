using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Services.SupplierService
{
    interface ISupplierAndPurchaseOrderService
    {
        List<Supplier> GetAllSuppliers();
        Supplier FindSupplierById(int? id);
        List<Inventory> FindInventoryItemsBySupplier(int? id);
        void UpdateSupplier(Supplier supplier);
        void AddNewSupplier(Supplier supplier);
        List<Inventory> GetAllItemsToResupply();
        void GeneratePurchaseOrders(string[] itemNo, int[] supplier, int?[] orderQuantity);
        List<PurchaseOrder> GetAllPOOrderByApproval();
    }
}

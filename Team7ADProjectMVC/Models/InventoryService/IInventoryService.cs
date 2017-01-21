using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Models
{
    interface IInventoryService
    {
        List<Inventory> GetAllInventory();

        List<Category> GetAllCategories();

        Inventory FindById(String id);

        List<Measurement> GetAllMeasurements();

        List<Supplier> GetAllSuppliers();

        void AddItem(Inventory inventory);

        void UpdateInventory(Inventory inventory);

        List<Inventory> GetInventoryListByCategory(int id);
        //Takes category ID, returns list of inventory

        List<StockCard> GetStockCardFor(String id);

        List<Requisition> GetOutStandingRequisitions();

        RetrievalList GetRetrievalList();

        void PopulateRetrievalList();

        void PopulateRetrievalListItems();

        void ClearRetrievalList();

        void AutoAllocateDisbursements();

        void AutoAllocateDisbursementsByOrderOfRequisition();

        List<DisbursementDetail> GenerateListForManualAllocation();

        int GetLastRetrievalListId();

        List<RequisitionDetail> GetRequisitionsSummedByDept(int currentRetrievalListId);
    }
}

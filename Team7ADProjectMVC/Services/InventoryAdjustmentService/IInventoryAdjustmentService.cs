using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Models.InventoryAdjustmentService
{
    interface IInventoryAdjustmentService
    {
        string findRolebyUserID(int userid);
        List<Adjustment> findSupervisorAdjustmentList();
        List<Adjustment> findManagerAdjustmentList();
        List<Adjustment> FindAdjustmentBySearch(List<Adjustment> searchlist, string employee, string date, string status);
    }
}

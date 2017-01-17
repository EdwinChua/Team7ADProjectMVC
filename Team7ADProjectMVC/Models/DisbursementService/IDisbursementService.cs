using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    interface IDisbursementService
    {
        List<DisbursementList> GetAllDisbursements();

        List<DisbursementList> GetDisbursementsBySearchCriteria(int? departmentId, String status);
        //Search for disbursements by department and status

        DisbursementList GetDisbursementById(String id);

        void UpdateDisbursementList(DisbursementList disbursementList);
    }
}
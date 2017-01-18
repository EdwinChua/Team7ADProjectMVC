using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    public class RequisitionComparer : IComparer<Requisition>
    {
        public int Compare(Requisition x, Requisition y)
        {
            if (x.DepartmentId.Equals(y.DepartmentId))
            {
                return 0;
            }
            else if (x.DepartmentId < y.DepartmentId)
            {
                return -1;
            }
            else
            {
                return +1;
            }
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Team7ADProjectMVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class DisbursementDetail
    {
        public int DisbursementDetailId { get; set; }
        public Nullable<int> DisbursementListId { get; set; }
        public Nullable<int> RequisitionDetailId { get; set; }
        public string Remark { get; set; }
        public Nullable<int> PreparedQuantity { get; set; }
        public Nullable<int> DeliveredQuantity { get; set; }
    
        public virtual DisbursementList DisbursementList { get; set; }
        public virtual RequisitionDetail RequisitionDetail { get; set; }
    }
}

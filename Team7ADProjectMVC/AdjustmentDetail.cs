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
    
    public partial class AdjustmentDetail
    {
        public int AdjustmentDetailId { get; set; }
        public Nullable<int> AdjustmentId { get; set; }
        public string ItemNo { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Reason { get; set; }
    
        public virtual Adjustment Adjustment { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}

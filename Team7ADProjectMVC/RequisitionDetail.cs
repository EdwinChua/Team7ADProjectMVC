//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Team7ADProjectMVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequisitionDetail
    {
        public int RequisitionDetailId { get; set; }
        public Nullable<int> RequisitionId { get; set; }
        public string ItemNo { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> OutstandingQuantity { get; set; }
        public string DeliveryStatus { get; set; }
    
        public virtual Inventory Inventory { get; set; }
        public virtual Requisition Requisition { get; set; }
    }
}

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
    
    public partial class PurchaseDetail
    {
        public int PurchaseDetailId { get; set; }
        public Nullable<int> PurchaseOrderId { get; set; }
        public string ItemNo { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> SupplierId { get; set; }
    
        public virtual Inventory Inventory { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}

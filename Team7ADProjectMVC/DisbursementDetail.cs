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

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
    
    public partial class DisbursementList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DisbursementList()
        {
            this.DisbursementDetails = new HashSet<DisbursementDetail>();
        }
    
        public int DisbursementListId { get; set; }
        public Nullable<int> RetrievalId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string Status { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DisbursementDetail> DisbursementDetails { get; set; }
        public virtual Retrieval Retrieval { get; set; }
    }
}

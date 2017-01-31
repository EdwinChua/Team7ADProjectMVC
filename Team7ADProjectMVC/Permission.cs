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
    
    public partial class Permission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permission()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int PermissionId { get; set; }
        public Nullable<bool> ApproveRequisition { get; set; }
        public Nullable<bool> ChangeCollectionPoint { get; set; }
        public Nullable<bool> ViewRequisition { get; set; }
        public Nullable<bool> MakeRequisition { get; set; }
        public Nullable<bool> DelegateRole { get; set; }
        public Nullable<bool> ViewCollectionDetails { get; set; }
        public Nullable<bool> ConfirmDisbursement { get; set; }
        public Nullable<bool> Disbursement { get; set; }
        public Nullable<bool> MakeAdjustment { get; set; }
        public Nullable<bool> ApproveAdjustment { get; set; }
        public Nullable<bool> InventoryManagement { get; set; }
        public Nullable<bool> ViewReports { get; set; }
        public Nullable<bool> MakePurchaseOrder { get; set; }
        public Nullable<bool> ApprovePurchaseOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}

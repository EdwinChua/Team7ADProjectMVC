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
    
    public partial class Inventory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inventory()
        {
            this.AdjustmentDetails = new HashSet<AdjustmentDetail>();
            this.DeliveryDetails = new HashSet<DeliveryDetail>();
            this.DisbursementDetails = new HashSet<DisbursementDetail>();
            this.PurchaseDetails = new HashSet<PurchaseDetail>();
            this.RequisitionDetails = new HashSet<RequisitionDetail>();
        }
    
        public string ItemNo { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Description { get; set; }
        public string BinNo { get; set; }
        public Nullable<int> ReorderLevel { get; set; }
        public Nullable<int> ReorderQuantity { get; set; }
        public Nullable<int> MeasurementId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> HoldQuantity { get; set; }
        public Nullable<int> SupplierId1 { get; set; }
        public Nullable<decimal> Price1 { get; set; }
        public Nullable<int> SupplierId2 { get; set; }
        public Nullable<decimal> Price2 { get; set; }
        public Nullable<int> SupplierId3 { get; set; }
        public Nullable<decimal> Price3 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdjustmentDetail> AdjustmentDetails { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DisbursementDetail> DisbursementDetails { get; set; }
        public virtual Measurement Measurement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequisitionDetail> RequisitionDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Supplier Supplier1 { get; set; }
        public virtual Supplier Supplier2 { get; set; }
    }
}

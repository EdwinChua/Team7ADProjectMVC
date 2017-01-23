using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.Models
{
    //public class AdjustmentDetail
    //{
    //    [Required]
    //    public string ItemNo { get; set; }
    //    [Required]
    //    public int Quantity { get; set; }
    //    [Required]
    //    public string Reason { get; set; }
    //    public string Index { get; set; }

    //}
    public partial class AdjustmentDetail
    {
        [Key]
        public int AdjustmentDetailId { get; set; }
        public Nullable<int> AdjustmentId { get; set; }
        [Required]
        public string ItemNo { get; set; }
        [Required]
        public Nullable<int> Quantity { get; set; }
        [Required]
        public string Reason { get; set; }
        public string Index { get; set; }
        public virtual Adjustment Adjustment { get; set; }
        public virtual Inventory Inventory { get; set; }

    }
}
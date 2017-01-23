using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    //public class Adjustment
    //{
    //    [Required]
    //    public DateTime AdjustmentDate { get; set; }
    //    [Required]
    //    public string Status { get; set;}
    //    [Required]
    //    public int EmployeeId { get; set; }
    //    public List<AdjustmentDetail> Details { get; set; }
    //    public Adjustment()
    //    {
    //        Status = "Pending";
    //        AdjustmentDate = DateTime.Now;
    //        //EmployeeId=
    //        Details = new List<AdjustmentDetail>();
    //    }
    //}
    public partial class Adjustment
    {
        
        public Adjustment()
        {
            Status = "Pending";
            AdjustmentDate = DateTime.Now;
            this.AdjustmentDetails = new HashSet<AdjustmentDetail>();

        }
        [Key]
        public int AdjustmentId { get; set; }
        [Required]
        public Nullable<System.DateTime> AdjustmentDate { get; set; }
        [Required]
        public Nullable<int> EmployeeId { get; set; }
        
        public Nullable<int> SupervisorId { get; set; }
        
        public Nullable<System.DateTime> SupervisorAuthorizedDate { get; set; }
        public Nullable<int> HeadId { get; set; }
        public Nullable<System.DateTime> HeadAuthorizedDate { get; set; }
        [Required]
        public string Status { get; set; }

        
        public virtual ICollection<AdjustmentDetail> AdjustmentDetails { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

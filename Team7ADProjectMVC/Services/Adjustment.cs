using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    public class Adjustment
    {
        [Required]
        public DateTime AdjustmentDate { get; set; }
        [Required]
        public string Status { get; set;}
        [Required]
        public int EmployeeId { get; set; }
        public List<AdjustmentDetail> Details { get; set; }
        public Adjustment()
        {
            Status = "Pending";
            AdjustmentDate = DateTime.Now;
            //EmployeeId=
            Details = new List<AdjustmentDetail>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    public class AdjustmentDetail
    {
        [Required]
        public string ItemNo { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public string Index { get; set; }

    }
}
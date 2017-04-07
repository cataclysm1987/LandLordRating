using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }
        public string ComplaintName { get; set; }
        public string ComplaintDescription { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class LandLord
    {
        [Key]
        public int LandLordId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "City")]
        public string State { get; set; }

        [Display(Name ="Overall Rating")]
        public double OverallRating { get; set; }


        public LandLord()
        {
            var Ratings = new List<Rating>();
            var Properties = new List<Property>();
            var Complaints = new List<Complaint>();
        }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
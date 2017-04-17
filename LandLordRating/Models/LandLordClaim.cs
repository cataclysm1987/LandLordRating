using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class LandLordClaim
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "LandLordClaim Name - Please Provide a Subject Name For Your Message")]
        public string ClaimName { get; set; }
        [Display(Name = "Please Explain Your Association With This LandLord Page")]
        public string ClaimDescription { get; set; }

        public string DocumentFilePath { get; set; }

        public LandLordClaim()
        {
            IsPending = true;
            IsApproved = false;
        }


        public bool IsPending { get; set; }
        public bool IsApproved { get; set; }


        //One to many with users, landlords (LL can have many claims, User can have many claims)

        public virtual LandLord LandLord { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
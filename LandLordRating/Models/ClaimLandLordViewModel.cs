using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class ClaimLandLordViewModel
    {
        public string LandLordName { get; set; }
        public LandLord LandLord { get; set; }
        [Required]
        [Display(Name = "LandLordClaim Name - Please Provide a Subject Name For Your Message")]
        public string ClaimName { get; set; }
        [Required]
        [Display(Name = "Please Explain Your Association With This LandLord Page")]
        public string ClaimDescription { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload a document with supporting information on your identity such as a driver's license or passport.")]
        public HttpPostedFileBase Document { get; set; }
    }
}
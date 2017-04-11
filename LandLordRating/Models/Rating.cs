using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public enum UnspecifiedYesNo { Unspecified, Yes, No }
    public enum YesNo { Yes, No }
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        [Required]
        [Display(Name = "Name Your Rating")]
        public string RatingName { get; set; }
        [Required]
        [Display(Name ="Describe Your LandLord's Overall Service")]
        public string RatingDescription { get; set; }
        [Display(Name = "Do you pay more than $25 or 5% of the monthly lease for late rent payment?")]
        public UnspecifiedYesNo LateFees { get; set; }
        [Display(Name = "Does your landlord give you 24-hours notice before before entering your property?")]
        public UnspecifiedYesNo LandLordNotice { get; set; }
        [Display(Name = "Does your landlord respond to your requests within 24-48 hours?")]
        public UnspecifiedYesNo LandLordResponse { get; set; }
        [Display(Name = "Do you have a 24-hour contact number for your landlord?")]
        public UnspecifiedYesNo ContactPhoneNumer { get; set; }
        [Display(Name = "Would you recommend your landlord's rental property to friends and family?")]
        public UnspecifiedYesNo RecommendLandLord { get; set; }
        [Display(Name = "Has your rent increased in the last 30 days without notice?")]
        public UnspecifiedYesNo RentIncrease { get; set; }
        [Required]
        [Display(Name = "On a scale from 1 to 10 how would you rate your landlord?")]
        public int LandLordRating { get; set; }
        [Display(Name = "Rate Anonymously?")]
        public YesNo RateAnonymously { get; set; }
        
        public int LandLordId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual LandLord LandLord { get; set; }
    }
}
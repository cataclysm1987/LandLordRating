using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public enum YesNo { Yes, No }
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        [Display(Name = "Name You Rating")]
        public string RatingName { get; set; }
        [Display(Name = "Are you using a Housing Choice Voucher?")]
        public YesNo VoucherUser { get; set; }
        [Display(Name = "Do you feel safe in your rental unit and on the property grounds in general?")]
        public YesNo Safty { get; set; }
        [Display(Name = "Is there a working fire extinguisher in your kitchen?")]
        public YesNo FireExtinguisher { get; set; }
        [Display(Name = "Are there properly working smoke detectors in the rental property?")]
         public YesNo SmokeDetectors { get; set; }
        [Display(Name = "Are there properly working carbon monoxide detectors in the rental property?")]
        public YesNo CarbonMonoxcide { get; set; }
        [Display(Name = "Do you pay more than $25 or 5% of the monthly lease for late rent payment?")]
        public YesNo LateFees { get; set; }
        [Display(Name = "Does your landlord give you 24-hours notice before before entering your property?")]
        public YesNo LandLordNotice { get; set; }
        [Display(Name = "Does your landlord respond to your requests within 24-48 hours?")]
        public YesNo LandLordResponse { get; set; }
        [Display(Name = "Do you have a 24-hour contact number for your landlord?")]
        public YesNo ContactPhoneNumer { get; set; }
        [Display(Name = "Would you recommend your landlord's rental property to friends and family?")]
        public YesNo RecommendLandLord { get; set; }
        [Display(Name = "Has your rent increased in the last 30 days without notice?")]
        public YesNo RentIncrease { get; set; }
        [Display(Name = "Do you have a written lease?")]
        public YesNo WrittenLease { get; set; }
        [Display(Name = "Were you evicted?")]
        public YesNo Eviction { get; set; }
        [Display(Name = "Did you end your lease agreement?")]
        public YesNo EndLease { get; set; }
        [Display(Name = "Have many times have you moved in the last year?")]
        public int TimesHaveMoved { get; set; }
        [Display(Name = "On a scale from 1 to 10 how would you rate your landlord?")]
        public int LandLordRating { get; set; }
        [Display(Name = "Additional Comments")]
        public string AdditionalComments { get; set; }
        public YesNo RateAnonymously { get; set; }
        
        public int LandLordId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual LandLord LandLord { get; set; }
    }
}
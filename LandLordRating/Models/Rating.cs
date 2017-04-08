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
        public string RatingName { get; set; }
        public YesNo VoucherUser { get; set; }
        public YesNo Safty { get; set; }
        public YesNo FireExtinguisher { get; set; }
        public YesNo SmokeDetectors { get; set; }
        public YesNo CarbonMonoxcide { get; set; }
        public YesNo LateFees { get; set; }
        public YesNo LandLordNotice { get; set; }
        public YesNo LandLordResponse { get; set; }
        public YesNo ContactPhoneNumer { get; set; }
        public YesNo RecommendLandLord { get; set; }
        public YesNo RentIncrease { get; set; }
        public YesNo WrittenLease { get; set; }
        public YesNo Eviction { get; set; }
        public YesNo EndLease { get; set; }
        public int TimesHaveMoved { get; set; }
        public string AdditionalComments { get; set; }
        public int LandLordRating { get; set; }
        public YesNo RateAnonymously { get; set; }
        
        public int LandLordId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual LandLord LandLord { get; set; }
    }
}
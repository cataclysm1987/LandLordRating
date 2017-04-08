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
        public string RatingDescription { get; set; }
        public int PropertyRating { get; set; }
        public int LandLordRating { get; set; }
        public int SafetyRating { get; set; }
        public int CommunicationRating { get; set; }
        public YesNo RateAnonymously { get; set; }
        
        public int LandLordId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual LandLord LandLord { get; set; }
    }
}
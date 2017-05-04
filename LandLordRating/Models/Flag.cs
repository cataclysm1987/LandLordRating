using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public enum FlaggedObject { LandLord, Rating, RatingReply }
    public enum FlaggedReason { Spam, Harassment, Guidelines, NSFW }
    public class Flag
    {
        public int FlagId { get; set; }
        public string Description { get; set; }
        public FlaggedObject FlaggedObject { get; set; }
        public FlaggedReason FlaggedReason { get; set; }
        public int FlaggedObjectId { get; set; }
        public bool IsReviewed { get; set; }

        public Flag()
        {
            IsReviewed = false;
        }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class RatingViewModel
    {
        public Rating Rating { get; set; }
        public bool IsClaimingUser { get; set; }
        public RatingReply RatingReply { get; set; }
    }
}
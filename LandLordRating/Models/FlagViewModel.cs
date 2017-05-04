using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class FlagViewModel
    {
        public Flag Flag { get; set; }
        public LandLord LandLord { get; set; }
        public Rating Rating { get; set; }
        public RatingReply RatingReply { get; set; }
    }
}
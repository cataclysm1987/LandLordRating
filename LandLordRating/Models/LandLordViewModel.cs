using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class LandLordViewModel
    {
        public LandLord LandLord { get; set; }
        public IPagedList<Rating> Ratings { get; set; }
        public IPagedList<PublicRecord> PublicRecords { get; set; }
        public bool IsClaimingUser { get; set; }
        public bool AreThereMoreThan5Ratings { get; set; }
        public bool AreThereMoreThan5PublicRecords { get; set; }
    }
}
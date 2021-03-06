﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace LandLordRating.Models
{
    public class AdminPanelViewModel
    {
        public int Users { get; set; }
        public int LandLords { get; set; }
        public int Ratings { get; set; }
        public int LandLordsAwaitingApproval { get; set; }
        public int LandLordsDeclined { get; set; }
        public int PendingLandLordClaims { get; set; }
        public int PendingRatingReplies { get; set; }
        public int PendingRatings { get; set; }
        public int PendingFlags { get; set; }
        public int PendingPublicRecords { get; set; }
        public int DeclinedPublicRecords { get; set; }
    }
}
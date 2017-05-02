using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class RatingReply
    {
        [Key]
        public int RatingReplyId { get; set; }
        [Required]
        [Display(Name = "Provide a Reply to This Rating")]
        public string ReplyDescription { get; set; }

        public bool IsApproved { get; set; }
        public bool IsDeclined { get; set; }

        public RatingReply()
        {
            IsApproved = false;
            IsDeclined = false;
        }

       

        [Required]
        public virtual Rating Rating { get; set; }
    }
}
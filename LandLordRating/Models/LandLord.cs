using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using GoogleMaps.LocationServices;

namespace LandLordRating.Models
{
    public class LandLord
    {
        [Key]
        public int LandLordId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        [StringLength(40, ErrorMessage = "Full Name cannot be longer than 40 characters.")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Zip Code must be numeric")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Required]
        [Display(Name = "State")]
        public States State { get; set; }
        [Display(Name = "LandLord Description")]
        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public LandLordOrTenant LandLordOrTenant { get; set; }
        [Required]
        [Display(Name = "Individual Or Company?")]
        public IndividualOrCompany IndividualOrCompany { get; set; }

        [Display(Name ="Overall Rating")]
        public double OverallRating { get; set; }

        public bool IsApproved { get; set; }
        public bool IsClaimed { get; set; }
        public bool IsDeclined { get; set; }
        [Display(Name = "Declined Reason")]
        public string DeclinedReason { get; set; }
        public bool IsClaimedDuringCreation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public LandLord()
        {
            var Ratings = new List<Rating>();
            var Properties = new List<Property>();
            //Move to respective landlord creation methods
            //DeclinedReason = "None";
            //var gls = new GoogleLocationService();
            //var latlong = gls.GetLatLongFromAddress(City + " " + ZipCode);
            //Latitude = latlong.Latitude;
            //Longitude = latlong.Longitude;
        }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<PublicRecord> PublicRecords { get; set; }
    }

    public enum LandLordOrTenant { LandLord, Tenant }
    public enum IndividualOrCompany { Unspecified, Individual, Company }

    public enum States
    {
        [Description("Alabama")]
        AL,

        [Description("Alaska")]
        AK,

        [Description("Arkansas")]
        AR,

        [Description("Arizona")]
        AZ,

        [Description("California")]
        CA,

        [Description("Colorado")]
        CO,

        [Description("Connecticut")]
        CT,

        [Description("D.C.")]
        DC,

        [Description("Delaware")]
        DE,

        [Description("Florida")]
        FL,

        [Description("Georgia")]
        GA,

        [Description("Hawaii")]
        HI,

        [Description("Iowa")]
        IA,

        [Description("Idaho")]
        ID,

        [Description("Illinois")]
        IL,

        [Description("Indiana")]
        IN,

        [Description("Kansas")]
        KS,

        [Description("Kentucky")]
        KY,

        [Description("Louisiana")]
        LA,

        [Description("Massachusetts")]
        MA,

        [Description("Maryland")]
        MD,

        [Description("Maine")]
        ME,

        [Description("Michigan")]
        MI,

        [Description("Minnesota")]
        MN,

        [Description("Missouri")]
        MO,

        [Description("Mississippi")]
        MS,

        [Description("Montana")]
        MT,

        [Description("North Carolina")]
        NC,

        [Description("North Dakota")]
        ND,

        [Description("Nebraska")]
        NE,

        [Description("New Hampshire")]
        NH,

        [Description("New Jersey")]
        NJ,

        [Description("New Mexico")]
        NM,

        [Description("Nevada")]
        NV,

        [Description("New York")]
        NY,

        [Description("Oklahoma")]
        OK,

        [Description("Ohio")]
        OH,

        [Description("Oregon")]
        OR,

        [Description("Pennsylvania")]
        PA,

        [Description("Rhode Island")]
        RI,

        [Description("South Carolina")]
        SC,

        [Description("South Dakota")]
        SD,

        [Description("Tennessee")]
        TN,

        [Description("Texas")]
        TX,

        [Description("Utah")]
        UT,

        [Description("Virginia")]
        VA,

        [Description("Vermont")]
        VT,

        [Description("Washington")]
        WA,

        [Description("Wisconsin")]
        WI,

        [Description("West Virginia")]
        WV,

        [Description("Wyoming")]
        WY

    }
}
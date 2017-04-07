using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string PropertyDescription { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
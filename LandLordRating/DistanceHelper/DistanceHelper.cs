using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandLordRating.DistanceHelper
{
    public class DistanceHelper
    {
        public double GetDistance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
                          Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
                dist = dist * 1.609344;
            else if (unit == 'N')
                dist = dist * 0.8684;
            return (dist);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

    }
}
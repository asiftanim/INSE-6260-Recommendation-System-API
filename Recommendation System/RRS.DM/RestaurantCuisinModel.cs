using RRS.DM.Common;
using System;
using System.Security.Cryptography.X509Certificates;

namespace RRS.DM
{
    public class RestaurantCuisinModel : BaseEntityModel
    {
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public int Rating { get; set; }
        public int FoodRating { get; set; }
        public int ServiceRating { get; set; }
    }
}


using RRS.Core.Shared;
using System;
using System.Security.Cryptography.X509Certificates;

namespace RRS.Core.Entities
{
    public class RestaurantCuisin : BaseEntity
    {
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public int Rating { get; set; }
        public int FoodRating { get; set; }
        public int ServiceRating { get; set; }
    }
}


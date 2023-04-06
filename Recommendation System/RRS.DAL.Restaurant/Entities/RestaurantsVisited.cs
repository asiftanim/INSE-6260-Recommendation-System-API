using RRS.Data.Entities.Shared;
using System;

namespace RRS.Data.Entities
{
    public class RestaurantsVisited : BaseEntity
    {
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public double Rating { get; set; }
        public int FoodRating { get; set; }
        public int ServiceRating { get; set; }
    }
}


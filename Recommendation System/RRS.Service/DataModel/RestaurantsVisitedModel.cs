using System;

namespace RRS.Service
{
    public class RestaurantsVisitedModel : BaseModel
    {
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public int Rating { get; set; }
        public int FoodRating { get; set; }
        public int ServiceRating { get; set; }
    }
}


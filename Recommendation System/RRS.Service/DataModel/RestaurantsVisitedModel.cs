using System;

namespace RRS.Service
{
    public class RestaurantsVisitedModel : BaseModel
    {
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public double Rating { get; set; }
        public int Rank { get; set; }
    }
}


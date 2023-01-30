using RRS.DM.Common;
using System;

namespace RRS.DM
{
    public class RestaurantRatingModel : BaseEntityModel
    {
        public int PlaceId { get; set; }
        public string Cuisine { get; set; }
    }
}

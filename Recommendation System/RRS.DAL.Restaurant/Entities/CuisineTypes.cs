using RRS.Data.Entities.Shared;
using System;

namespace RRS.Data.Entities
{
    public class CuisineTypes : BaseEntity
    {
        public int PlaceId { get; set; }
        public string Cuisine { get; set; }
    }
}

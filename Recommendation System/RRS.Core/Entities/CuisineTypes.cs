using RRS.Core.Shared;
using System;

namespace RRS.Core.Entities
{
    public class CuisineTypes : BaseEntity
    {
        public int PlaceId { get; set; }
        public string Cuisine { get; set; }
    }
}

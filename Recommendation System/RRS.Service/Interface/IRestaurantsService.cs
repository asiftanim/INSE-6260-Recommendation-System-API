using RRS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Service
{
    public interface IRestaurantsService
    {
        public List<RestaurantsVisitedModel> GetRestaurantByUserId(string Id);
        public RestaurantsVisitedModel RateRestaurant(RestaurantsVisitedModel restaurantsVisitedModel);
        public List<CuisineTypes> GetRandomRestaurants();
    }
}

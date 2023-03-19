using RRS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Data
{
    public interface IRestaurantRepository
    {
        public List<RestaurantsVisited> GetRestaurantByUserId(string Id);
        public RestaurantsVisited RateRestaurant(RestaurantsVisited restaurantsVisited);
        public List<CuisineTypes> GetRandomRestaurants();
    }
}

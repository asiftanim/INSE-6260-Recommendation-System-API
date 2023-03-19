using RRS.Data;
using RRS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Service
{
    public class RestaurantService : IRestaurantsService
    {
        private readonly IRestaurantRepository _restaurantsRespository;
        public RestaurantService(IRestaurantRepository restaurantsRespository)
        {
            _restaurantsRespository = restaurantsRespository;
        }
        public List<RestaurantsVisitedModel> GetRestaurantByUserId(string Id)
        {
            var restaurants = _restaurantsRespository.GetRestaurantByUserId(Id);
            List<RestaurantsVisitedModel> restaurantList = new List<RestaurantsVisitedModel>();

            foreach(var restaurant in restaurants)
            {
                restaurantList.Add(new RestaurantsVisitedModel() 
                { 
                    Id = restaurant.Id,
                    UserId = restaurant.UserId,
                    PlaceId = restaurant.PlaceId,
                    Rating = restaurant.Rating
                });
            }
            return restaurantList;
        }

        public RestaurantsVisitedModel RateRestaurant(RestaurantsVisitedModel restaurantsVisitedModel)
        {
            var restaurant = _restaurantsRespository.RateRestaurant(new RestaurantsVisited()
            {
                UserId = restaurantsVisitedModel.UserId,
                PlaceId = restaurantsVisitedModel.PlaceId,
                Rating = restaurantsVisitedModel.Rating
            });

            return new RestaurantsVisitedModel() { Id = restaurant.Id, UserId = restaurant.UserId, PlaceId = restaurant.PlaceId, Rating = restaurant.Rating};
        }

        public List<CuisineTypes> GetRandomRestaurants()
        {
            return _restaurantsRespository.GetRandomRestaurants();
        }
    }
}

using RRS.Core;
using RRS.Data;
using RRS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Data
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RRSDBContext _dbContext;
        public RestaurantRepository(RRSDBContext context)
        {
            _dbContext = context;
        }

        public List<RestaurantsVisited> GetRestaurantByUserId(string Id)
        {
            return _dbContext.RestaurantsVisited.Where(x => x.UserId == Id).ToList();
        }

        public RestaurantsVisited RateRestaurant(RestaurantsVisited restaurantsVisited)
        {
            _dbContext.RestaurantsVisited.Add(restaurantsVisited);
            _dbContext.SaveChanges();
            return restaurantsVisited;
        }

        public List<CuisineTypes> GetRandomRestaurants()
        {
            return _dbContext.CuisineTypes.OrderBy(x => x.PlaceId).OrderBy(x => Guid.NewGuid()).Take(10).ToList();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRS.Data.Entities;
using RRS.Service;

namespace Recommendation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantController(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        [HttpGet("GetRestaurantByUserId/{id}")]
        public List<RestaurantsVisitedModel> GetRestaurantByUserId(string Id)
        {
            return _restaurantsService.GetRestaurantByUserId(Id);
        }

        [HttpPost("RateRestaurant")]
        public RestaurantsVisitedModel RateRestaurant(RestaurantsVisitedModel restaurantsVisitedModel)
        {
            return _restaurantsService.RateRestaurant(restaurantsVisitedModel);
        }

        [HttpGet("GetRandomRestaurants")]
        public List<CuisineTypes> GetRandomRestaurants()
        {
            return _restaurantsService.GetRandomRestaurants();
        }
    }
}

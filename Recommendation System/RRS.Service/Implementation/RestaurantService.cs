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

        private double CalculatePearsonCorrelationCoefficient(double[] CurrentUser, double[] User)
        {
            double CurrentUserAverageRating = CurrentUser.Sum() / CurrentUser.Length;
            double UserAverageRating = User.Sum() / User.Length;
            //Console.WriteLine($"{CurrentUserAverageRating} - {UserAverageRating}");

            double DividendSum = 0;
            double Divisor = 0;
            double DivisorLeft = 0;
            double DivisorRight = 0;

            for (int i = 0; i < CurrentUser.Length; i++)
            {
                DividendSum += (CurrentUser[i] - CurrentUserAverageRating) * (User[i] - UserAverageRating); ;

            }
            //Console.WriteLine($"DividendSum = {DividendSum}");  //2


            for (int i = 0; i < CurrentUser.Length; i++)
            {
                DivisorLeft += Math.Pow((CurrentUser[i] - CurrentUserAverageRating), 2);
            }
            //Console.WriteLine($"DivisorLeft = {DivisorLeft}"); //2


            for (int i = 0; i < CurrentUser.Length; i++)
            {
                DivisorRight += Math.Pow((User[i] - UserAverageRating), 2);
            }
            //Console.WriteLine($"DivisorRight = {DivisorRight}");//3.2


            Divisor = Math.Sqrt(DivisorLeft) * Math.Sqrt(DivisorRight); // 1.42 * 1.79
            //Console.WriteLine($"Divisor = {Divisor}");
            double PCCResult = Math.Round(DividendSum / Divisor, 2);

            return PCCResult;
        }

        private double CalculatePrediction(double CUAR, double User1PCC, double User2PCC, double U1AR, double U2AR, double[] CompareValues)
        {
            Console.WriteLine($"CUAR : {CUAR}");
            Console.WriteLine($"User1PCC : {User1PCC}");
            Console.WriteLine($"User2PCC : {User2PCC}");
            Console.WriteLine($"U1AR : {U1AR}");
            Console.WriteLine($"U2AR : {U2AR}");
            Console.WriteLine($"CompareValues : {CompareValues[0]} - {CompareValues[1]}");
            return Math.Round(CUAR + (1 / (User1PCC + User2PCC)) * (User1PCC * (CompareValues[0] - U1AR) + User2PCC * (CompareValues[1] - U2AR)), 2);
        }
    }
}

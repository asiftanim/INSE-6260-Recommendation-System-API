using Microsoft.AspNetCore.Mvc;
using RRS.Service.DataModel;
using RRS.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recommendation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public List<UserModel> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpGet("GetUserByUserId/{id}")]
        public UserModel GetUserByUserId(string id)
        {
            return _userService.GetUserByUserId(id);
        }

        [HttpPost("CreateUser")]
        public UserCreateModel CreateUser(UserCreateModel user)
        {
            return _userService.CreateUser(user);
        }

        [HttpPost("Login")]
        public UserModel Login(UserCreateModel user)
        {
            return _userService.Login(user);
        }
    }
}

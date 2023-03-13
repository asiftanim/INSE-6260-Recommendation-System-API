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

        // GET api/<UserController>/5
        [HttpGet("GetUserByUserId/{id}")]
        public UserModel GetUserByUserId(string id)
        {
            return _userService.GetUserByUserId(id);
        }

        // POST api/<UserController>
        [HttpPost("CreateUser")]
        public UserCreateModel CreateUser(UserCreateModel user)
        {
            return _userService.CreateUser(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

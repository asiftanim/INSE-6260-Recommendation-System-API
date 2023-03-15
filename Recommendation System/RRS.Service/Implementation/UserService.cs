using RRS.Core;
using RRS.Data.Entities;
using RRS.Data.Interface;
using RRS.Service.DataModel;
using RRS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Service.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public UserModel GetUserByUserId(string id)
        {
            var user = _userRepository.GetUserByUserId(id);
            
            return user != null ? new UserModel() { Id = user.Id, UserId = user.UserId, IsNew = user.IsNew } : null;
        }

        public List<UserModel> GetAllUsers()
        {
            var users = _userRepository.GetUsers();
            List<UserModel> userModels = new List<UserModel>();

            foreach (var user in users)
            {
                userModels.Add(new UserModel() { Id = user.Id, UserId = user.UserId, IsNew = user.IsNew});
            }
            return userModels;
        }

        public UserCreateModel CreateUser(UserCreateModel userCreateModel)
        {
            var user = _userRepository.CreateUser(userCreateModel.UserId, userCreateModel.Password);

            return new UserCreateModel() { Id = user.Id, UserId = user.UserId, IsNew = user.IsNew};
        }
    }
}

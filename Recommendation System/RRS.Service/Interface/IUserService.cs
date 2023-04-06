using RRS.Data.Entities;
using RRS.Service.DataModel;

namespace RRS.Service.Interface
{
    public interface IUserService
    {
        public UserModel GetUserByUserId(string id);
        public List<UserModel> GetAllUsers();
        public UserCreateModel CreateUser(UserCreateModel user);
        public void ImportUsers();
        public UserModel Login(UserCreateModel user);
        public UserModel UpdateUserByUserId(string id);
    }
}

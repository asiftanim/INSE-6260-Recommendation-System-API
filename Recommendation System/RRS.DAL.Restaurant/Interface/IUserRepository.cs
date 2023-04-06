using RRS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Data.Interface
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public User GetUserByUserId(string id);
        public User UpdateUserByUserId(string id);
        public User CreateUser(string email, string password);
        public List<String> GetDistinctUsers();

    }
}

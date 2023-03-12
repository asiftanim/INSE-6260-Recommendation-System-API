using RRS.Service.DataModel;
using RRS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Service.Implementation
{
    internal class User: IUser
    {
        public User() { }

        public UserModel GetUserByUserId(string id)
        {
            return new UserModel();
        }
    }
}

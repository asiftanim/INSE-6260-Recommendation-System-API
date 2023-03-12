using RRS.Service.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Service.Interface
{
    internal interface IUser
    {
        public UserModel GetUserByUserId(string id);
    }
}

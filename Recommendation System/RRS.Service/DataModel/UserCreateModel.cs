using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Service.DataModel
{
    public class UserCreateModel : BaseModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool IsNew { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Service.DataModel
{
    internal class UserModel : BaseModel
    {
        public string UserId { get; set; }
        public int IsNew { get; set; }
    }
}

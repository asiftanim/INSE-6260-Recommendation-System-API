using RRS.Data.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Data.Entities
{
    public class User : BaseEntity
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool IsNew { get; set; }
    }
}

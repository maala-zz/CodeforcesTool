using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Handle { get; set; }
        public string Password { get; set; }
    }
}

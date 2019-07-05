using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeforcesTool.Entity
{
    public class UseTagRating
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TagId { get; set; }
        public int Rating { get; set; }
    }
}

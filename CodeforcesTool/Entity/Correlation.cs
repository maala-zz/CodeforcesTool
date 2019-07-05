using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeforcesTool.Entity
{
    public class Correlation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid User2Id { get; set; }
        public string FirstUserHandle { get; set; }
        public string SecondUserHandle { get; set; }
        public string FirstUserAvatar { get; set; }
        public string SecondUserAvatar { get; set; }
        public string FirstUserRating { get; set; }
        public string SecondUserRating { get; set; }
        public double Value { get; set; }
    }
}

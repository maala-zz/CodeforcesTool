using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeforcesTool.Entity
{
    public class UserProblem
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProblemId { get;set; }
        public Problem Problem { get; set; }
    //    public int Rating { get; set; }
    }
}

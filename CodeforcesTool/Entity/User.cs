using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeforcesTool.Entity
{
    public class User
    {
        public Guid Id { set; get; }
        public string Name { get; set; }
        public string Avatar { set; get; }
        public string Handle { get; set; }
        public int Contribution { get; set; }
        public int Rating { get; set; }
        public string Rank { get; set; }
        public string MaxRank { get; set; }
        public int MaxRating { get; set; }
        public ICollection<UserProblem> UserProblems { get; set; }
    }
}

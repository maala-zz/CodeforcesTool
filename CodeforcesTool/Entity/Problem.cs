using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeforcesTool.Entity
{
    public class Problem
    {
        public Guid Id { get; set; }
        public string Index { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int Rating { get; set; }
        public ICollection<ProblemTag> ProblemTags { get; set; }
        public ICollection<UserProblem> UserProblems { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeforcesTool.Entity
{
    public class ProblemTag
    {
        public Guid ProblemId { get; set; }
        public Problem Problem { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}

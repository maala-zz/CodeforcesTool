using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeforcesTool.Entity
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<ProblemTag> ProblemTags { get; set; }
    }
}

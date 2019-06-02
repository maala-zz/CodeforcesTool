using System;
using System.Collections.Generic;
using System.Text;

namespace SeedingData.EntityDto.Problem
{
    class ProblemDto
    {
        public int contestId { get; set; }
        public string index { get; set; }
        public string name { get; set; }
        public int rating { get; set; }
        public IList<String> tags { get; set; }
    }
}

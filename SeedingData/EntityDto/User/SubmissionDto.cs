using SeedingData.EntityDto.Problem;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeedingData.EntityDto.User
{
    class SubmissionDto
    {
        public int Id { get; set; }
        public string verdict { get; set; }
        public ProblemDto Problem { get; set; }
    }
}

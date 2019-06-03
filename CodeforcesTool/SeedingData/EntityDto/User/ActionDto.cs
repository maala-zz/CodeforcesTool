using SeedingData.EntityDto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeedingData.EntityDto
{
    class ActionDto
    {
        public string status { get; set; }
        public ICollection<SubmissionDto> result { get; set; }
    }
}

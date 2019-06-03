using System;
using System.Collections.Generic;
using System.Text;

namespace SeedingData.EntityDto
{
    class UserJson
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Handle { get; set; }
        public string avatar { get; set; }
        public int contribution { get; set; }
        public int Rating { get; set; }
        public string Rank { get; set; }
        public string maxRank { get; set; }
        public int maxRating { get; set; }
    }
}

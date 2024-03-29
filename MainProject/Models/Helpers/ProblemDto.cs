﻿using CodeforcesTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models.Helpers
{
    public class ProblemDto
    {
        public Guid Id { get; set; }
        public string Index { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Link { get; set; }
        public List<string> Tags { get; set; }
        public Boolean Solved { get; set; } = false;
    }
}

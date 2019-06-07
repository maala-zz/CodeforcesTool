using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeforcesTool.Entity
{
    public class CodeforcesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProblemTag> ProblemTags { get; set; }
        public DbSet<UserProblem> UserProblems { get; set; }
        public CodeforcesContext(DbContextOptions<CodeforcesContext> dbContextOptions)
            : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configure ProblemTag Table
            modelBuilder.Entity<ProblemTag>().HasKey(pt => new { pt.ProblemId, pt.TagId });
            // Set table relationship up
            modelBuilder.Entity<ProblemTag>().HasOne(pt => pt.Problem)
                        .WithMany(p => p.ProblemTags)
                        .HasForeignKey(pt => pt.ProblemId);
            modelBuilder.Entity<ProblemTag>().HasOne(pt => pt.Tag)
                        .WithMany(t => t.ProblemTags)
                        .HasForeignKey(pt => pt.TagId);
            #endregion

            #region  Configure UserProblem Table
            modelBuilder.Entity<UserProblem>().HasKey(up => new { up.UserId, up.ProblemId });
            modelBuilder.Entity<UserProblem>().HasOne(up => up.Problem)
                        .WithMany(p => p.UserProblems)
                        .HasForeignKey(up => up.ProblemId);
            modelBuilder.Entity<UserProblem>().HasOne(up => up.User)
                        .WithMany(u => u.UserProblems)
                        .HasForeignKey(up => up.UserId);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CodeforcesDB;Trusted_Connection=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}

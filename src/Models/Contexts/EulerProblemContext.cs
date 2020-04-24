using Microsoft.EntityFrameworkCore;
using ProjectEulerWebApp.Models.Entities.EulerProblem;

namespace ProjectEulerWebApp.Models.Contexts
{
    public class ProjectEulerWebAppContext : DbContext
    {
        public ProjectEulerWebAppContext(DbContextOptions<ProjectEulerWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<EulerProblem> EulerProblems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EulerProblemConfiguration());
        }
    }
}
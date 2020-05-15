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

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<EulerProblem> EulerProblem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EulerProblemConfiguration());
        }
    }
}
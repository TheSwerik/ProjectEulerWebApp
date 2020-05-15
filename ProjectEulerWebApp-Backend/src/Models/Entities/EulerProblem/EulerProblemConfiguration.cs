using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectEulerWebApp.Models.Entities.EulerProblem
{
    public class EulerProblemConfiguration : IEntityTypeConfiguration<EulerProblem>
    {
        public void Configure(EntityTypeBuilder<EulerProblem> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Title)
                   .IsRequired();

            builder.Property(prop => prop.Description)
                   .IsRequired();

            builder.Property(prop => prop.IsSolved)
                   .IsRequired();

            builder.Property(prop => prop.SolveDate)
                   .HasColumnType("TIMESTAMP(0)");

            builder.Property(prop => prop.Solution);

            builder.Property(prop => prop.PublishDate)
                   .HasColumnType("TIMESTAMP(0)");

            builder.Property(prop => prop.Difficulty);
        }
    }
}
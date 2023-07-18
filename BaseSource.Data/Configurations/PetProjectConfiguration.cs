using BaseSource.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Data.Configurations
{
    public class PetProjectConfiguration : IEntityTypeConfiguration<PetProject>
    {
        public void Configure(EntityTypeBuilder<PetProject> builder)
        {
            builder.ToTable("PetProjects");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.LinkDemo).HasMaxLength(500);
            builder.Property(x => x.LinkSourceCode).HasMaxLength(500);

            builder.HasOne(x => x.CategoryProject).WithMany(x => x.PetProjects).HasForeignKey(x => x.CategoryProjectId);
        }
    }
}

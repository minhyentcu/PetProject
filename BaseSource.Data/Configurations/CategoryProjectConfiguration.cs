using BaseSource.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Data.Configurations
{
    public class CategoryProjectConfiguration : IEntityTypeConfiguration<CategoryProject>
    {
        public void Configure(EntityTypeBuilder<CategoryProject> builder)
        {
            builder.ToTable("CategoryProjects");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(500);
        }
    }
}

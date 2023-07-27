using BaseSource.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Data.Configurations
{
    public class VoteProjectConfiguration : IEntityTypeConfiguration<VoteProject>
    {
        public void Configure(EntityTypeBuilder<VoteProject> builder)
        {
            builder.ToTable("VoteProjects");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.IP).HasMaxLength(50);
           

            builder.HasOne(x => x.PetProject).WithMany(x => x.VoteProjects).HasForeignKey(x => x.PetProjectId);
        }
    }
}

using BaseSource.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Data.Configurations
{
    public class ConfigSystemConfiguration : IEntityTypeConfiguration<ConfigSystem>
    {
        public void Configure(EntityTypeBuilder<ConfigSystem> builder)
        {
            builder.ToTable("ConfigSystems");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.WebsiteName).HasMaxLength(500);
            builder.Property(x => x.PhoneNumber).HasMaxLength(500);
            builder.Property(x => x.LinkFB).HasMaxLength(500);
            builder.Property(x => x.LinkYoutube).HasMaxLength(500);
        }
    }
}

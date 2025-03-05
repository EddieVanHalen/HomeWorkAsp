using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCAPP.DataAccess.Entities.Configurations;

public class BandEntityConfiguration : IEntityTypeConfiguration<BandEntity>
{
    public void Configure(EntityTypeBuilder<BandEntity> builder)
    {
        builder.ToTable("Bands");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Title).IsUnique(true);
        builder.Property(x => x.Title).IsRequired(true);
    }
}
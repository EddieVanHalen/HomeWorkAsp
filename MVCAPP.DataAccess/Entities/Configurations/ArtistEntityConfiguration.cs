using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCAPP.DataAccess.Entities.Configurations;

public class ArtistEntityConfiguration : IEntityTypeConfiguration<ArtistEntity>
{
    public void Configure(EntityTypeBuilder<ArtistEntity> builder)
    {
        builder.ToTable("Artists");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique(true);
        builder.Property(x => x.Name).IsRequired(true);

        builder.HasOne<BandEntity>(x => x.Band).WithMany(x => x.Artists).HasForeignKey(x => x.BandId);
    }
}
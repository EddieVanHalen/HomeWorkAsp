using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCAPP.DataAccess.Entities.Configurations;

public class AlbumEntityConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.ToTable("Albums");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Title).IsUnique(true);
        builder.Property(x => x.Title).IsRequired(true);

        builder.HasOne<ArtistEntity>(x => x.Artist).WithMany(x => x.Albums).HasForeignKey(x => x.ArtistId);
    }
}
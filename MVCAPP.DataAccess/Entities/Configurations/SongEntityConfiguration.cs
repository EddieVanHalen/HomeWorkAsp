using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCAPP.DataAccess.Entities.Configurations;

public class SongEntityConfiguration : IEntityTypeConfiguration<SongEntity>
{
    public void Configure(EntityTypeBuilder<SongEntity> builder)
    {
        builder.ToTable("Songs");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Title).IsUnique(true);
        builder.Property(x => x.Title).IsRequired(true);

        builder.HasOne<AlbumEntity>(x => x.Album).WithMany(x => x.Songs).HasForeignKey(x => x.AlbumId);
    }
}
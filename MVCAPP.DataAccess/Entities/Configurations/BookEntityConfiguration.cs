using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCAPP.DataAccess.Entities.Configurations;

public class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired();

        builder.Property(x => x.Genre).IsRequired().IsRequired(true);

        builder.Property(x => x.CoverImageUrl).IsRequired(false);
    }
}

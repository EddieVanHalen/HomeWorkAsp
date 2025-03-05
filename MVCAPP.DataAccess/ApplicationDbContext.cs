using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCAPP.DataAccess.Entities;
using MVCAPP.DataAccess.Entities.Configurations;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;

namespace MVCAPP.DataAccess;

public sealed class ApplicationDbContext : IdentityDbContext<ApiUser>
{
    public DbSet<SongEntity> Songs { get; set; } = null!;
    public DbSet<AlbumEntity> Albums { get; set; } = null!;
    public DbSet<ArtistEntity> Artists { get; set; } = null!;
    public DbSet<BandEntity> Bands { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new SongEntityConfiguration().Configure(modelBuilder.Entity<SongEntity>());
        new AlbumEntityConfiguration().Configure(modelBuilder.Entity<AlbumEntity>());
        new ArtistEntityConfiguration().Configure(modelBuilder.Entity<ArtistEntity>());
        new BandEntityConfiguration().Configure(modelBuilder.Entity<BandEntity>());
		
        base.OnModelCreating(modelBuilder);
    }
}
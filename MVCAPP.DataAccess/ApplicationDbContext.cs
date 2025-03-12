using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCAPP.DataAccess.Entities;
using MVCAPP.DataAccess.Entities.Configurations;
using MVCAPP.Domain.Models.Entities;

namespace MVCAPP.DataAccess;

public sealed class ApplicationDbContext : IdentityDbContext<ApiUser>
{
    public DbSet<BookEntity> Books { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new BookEntityConfiguration().Configure(modelBuilder.Entity<BookEntity>());

        base.OnModelCreating(modelBuilder);
    }
}

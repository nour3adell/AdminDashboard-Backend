using Microsoft.EntityFrameworkCore;
using Nour.Domain.Entities;

namespace Nour.Infrastructure.Persistence;

public class NourDbContext : DbContext
{
    public NourDbContext(DbContextOptions<NourDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().Ignore(p => p.Images);
        modelBuilder.Entity<Product>().Ignore(p => p.Attributes);
        modelBuilder.Entity<Category>().HasMany(c => c.Children)
            .WithOne()
            .HasForeignKey(c => c.ParentCategoryId);
    }
}

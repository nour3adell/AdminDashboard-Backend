using Microsoft.EntityFrameworkCore;
using Nour.Application.Interfaces;
using Nour.Domain.Entities;
using Nour.Infrastructure.Persistence;
using Nour.Infrastructure.Services;

namespace Nour.Tests;

public class CatalogServiceTests
{
    [Fact]
    public async Task GetCategoriesReturnsSeededCategory()
    {
        var options = new DbContextOptionsBuilder<NourDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        using var db = new NourDbContext(options);
        db.Categories.Add(new Category { Id = Guid.NewGuid(), Name = "Test", Slug = "test" });
        db.SaveChanges();
        ICatalogService service = new CatalogService(db);

        var categories = await service.GetCategoriesAsync();

        Assert.Single(categories);
    }
}

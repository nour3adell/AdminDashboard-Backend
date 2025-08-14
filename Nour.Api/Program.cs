using Microsoft.EntityFrameworkCore;
using Nour.Application.Interfaces;
using Nour.Infrastructure.Persistence;
using Nour.Infrastructure.Services;
using Nour.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NourDbContext>(opt =>
    opt.UseInMemoryDatabase("NourDb"));

builder.Services.AddScoped<ICatalogService, CatalogService>();

var app = builder.Build();

// seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NourDbContext>();
    if (!db.Categories.Any())
    {
        var cat = new Category { Id = Guid.NewGuid(), Name = "Default", Slug = "default" };
        db.Categories.Add(cat);
        db.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            SKU = "SKU001",
            Name = "Sample Product",
            Slug = "sample-product",
            Description = "Sample",
            Price = 9.99m,
            Currency = "USD",
            StockQty = 100,
            CategoryId = cat.Id,
            Images = new List<string>(),
            Attributes = new Dictionary<string, string>()
        });
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/catalog/categories", async (ICatalogService service) =>
{
    var cats = await service.GetCategoriesAsync();
    return Results.Ok(cats);
});

app.MapGet("/api/catalog/products", async (ICatalogService service, string? search) =>
{
    var products = await service.GetProductsAsync(search);
    return Results.Ok(products);
});

app.MapGet("/api/catalog/products/{id:guid}", async (ICatalogService service, Guid id) =>
{
    var product = await service.GetProductByIdAsync(id);
    return product is null ? Results.NotFound() : Results.Ok(product);
});

app.Run();

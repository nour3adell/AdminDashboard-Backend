using Microsoft.EntityFrameworkCore;
using Nour.Application.DTOs;
using Nour.Application.Interfaces;
using Nour.Domain.Entities;
using Nour.Infrastructure.Persistence;

namespace Nour.Infrastructure.Services;

public class CatalogService : ICatalogService
{
    private readonly NourDbContext _db;

    public CatalogService(NourDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        return await _db.Categories
            .AsNoTracking()
            .Select(c => new CategoryDto(c.Id, c.Name, c.Slug, c.ParentCategoryId, c.IsActive))
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(string? search = null)
    {
        var query = _db.Products.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }

        return await query
            .Select(p => new ProductDto(
                p.Id,
                p.SKU,
                p.Name,
                p.Slug,
                p.Description,
                p.Price,
                p.Currency,
                p.StockQty,
                p.CategoryId,
                p.Images,
                p.Attributes,
                p.IsActive,
                p.CreatedAt,
                p.UpdatedAt))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var p = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (p == null) return null;
        return new ProductDto(p.Id, p.SKU, p.Name, p.Slug, p.Description, p.Price, p.Currency, p.StockQty,
            p.CategoryId, p.Images, p.Attributes, p.IsActive, p.CreatedAt, p.UpdatedAt);
    }
}

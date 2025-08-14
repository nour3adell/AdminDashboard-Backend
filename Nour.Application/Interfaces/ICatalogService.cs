using System;
using Nour.Application.DTOs;

namespace Nour.Application.Interfaces;

public interface ICatalogService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<IEnumerable<ProductDto>> GetProductsAsync(string? search = null);
    Task<ProductDto?> GetProductByIdAsync(Guid id);
}

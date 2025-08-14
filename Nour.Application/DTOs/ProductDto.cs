using System;
using System.Collections.Generic;

namespace Nour.Application.DTOs;

public record ProductDto(
    Guid Id,
    string SKU,
    string Name,
    string Slug,
    string Description,
    decimal Price,
    string Currency,
    int StockQty,
    Guid CategoryId,
    IReadOnlyList<string> Images,
    IReadOnlyDictionary<string, string> Attributes,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

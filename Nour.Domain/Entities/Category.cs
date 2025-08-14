using System;
using System.Collections.Generic;

namespace Nour.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public Guid? ParentCategoryId { get; set; }
    public bool IsActive { get; set; } = true;

    public List<Category> Children { get; set; } = new();
}

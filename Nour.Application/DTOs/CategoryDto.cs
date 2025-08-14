using System;

namespace Nour.Application.DTOs;

public record CategoryDto(Guid Id, string Name, string Slug, Guid? ParentCategoryId, bool IsActive);

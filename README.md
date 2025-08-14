# Nour E-commerce API

This repository contains a minimal clean-architecture scaffold for the **Nour** e-commerce platform. It targets **.NET 8** and uses minimal APIs for lightweight routing and rapid development.

## Projects
- `Nour.Domain` – entity definitions.
- `Nour.Application` – DTOs and service abstractions.
- `Nour.Infrastructure` – EF Core persistence and service implementations.
- `Nour.Api` – minimal API host with seeded in-memory database.
- `Nour.Tests` – xUnit tests.

## Build & Test
```bash
dotnet build Nour.sln
dotnet test Nour.sln
```

## Sample Endpoints
- `GET /api/catalog/categories`
- `GET /api/catalog/products?search=term`
- `GET /api/catalog/products/{id}`

The API seeds a single category and product on startup when using the in-memory database.

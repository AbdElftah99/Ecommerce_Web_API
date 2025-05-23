# E-Commerce Web API (ASP.NET 8)

This project is a modular **E-Commerce Web API** built using **ASP.NET Core 8** with simple admin dashboard, following **Onion Architecture**. The solution is structured for scalability, maintainability, and separation of concerns.

---

## 📂 Project Structure

```
/Core
├── Domain
│   ├── Contracts (interfaces)
│   └── Entities (Domain models)
    └── Exceptions (Custom errors)
├── Services
└── Services.Abstraction

Infrastructure
├── Presentation (Controllers)
└── Persistence
    ├── Data (DbContext, Seeding, Configurations)
    ├── Repositories (EFCore implementations)
    └── Migrations (EF Core migrations)

Shared
├── Dto (Data Transfer Objects)
├── ErrorModels (Standard error responses)
├── JwtOptions.cs (JWT Config)
└── PaginatedResult.cs (Pagination logic)

Web_Api_E-commerc
├── Extensions (Service registration)
├── Middleware (Global error handler)
└── Program.cs (Main entry point)
```
---

## ✅ Features

- 🧱 **Onion Architecture**
- 🔐 **JWT Authentication** & Authorization
- 🛍️ **Products & Orders** domain modeling
- 🧾 **DTO Mapping** for secure data handling
- 📦 **Repository + Unit of Work** patterns
- 💾 **Entity Framework Core** with migrations
- 🚀 **Redis caching** support
- ⚠️ **Custom global error handling middleware**
- 📤 **Seeding support** from JSON files
- 🔍 **Specification Pattern** for queries
- 📑 **Swagger UI** integration (optional)
- 📮 **Postman Collection** for API testing

---

## 🔧 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/)
- Entity Framework Core
- SQL Server
- Redis
- AutoMapper
- JWT Authentication
- Swagger (optional)
- Visual Studio 2022 / VS Code

---

## 📁 Sample Data

Sample seed data can be found in:
```
Infrastructure/Persistence/Data/Seeding/
├── brands.json
├── delivery.json
├── products.json
└── types.json
```
---

## 🧪 Example Endpoints
```
GET /api/products
GET /api/products/{id}
POST /api/account/login
POST /api/account/register
GET /api/orders
POST /api/orders
```
---

## 📜 Entity Overview

### Products:
```
- `Product`
- `ProductBrand`
- `ProductType`
```
### Orders:
```
- `Order`
- `OrderItem`
- `DeliveryMethod`
```
### Identity:
```
- `User`
- `Address`
```
---

## 🛡️ Caching

You should run the Redis-service and Redis-CLI to test the project

## 🛡️ Authentication

Uses **JWT Bearer Tokens**:

- Token is issued upon login.
- Use `[Authorize]` attribute to protect endpoints.
- Add the token to headers as shown below:

```http
Authorization: Bearer {your_token_here}




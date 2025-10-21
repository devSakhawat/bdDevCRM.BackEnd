# bdDevCRM Backend

A comprehensive Customer Relationship Management (CRM) system backend built with ASP.NET Core, designed specifically for educational institutions.

## 📚 Documentation

- **[Architecture Documentation](ARCHITECTURE.md)** - Complete architecture overview, design patterns, and technical details
- **[File Structure Guide](FILE_STRUCTURE.md)** - Detailed file and folder organization
- **[Quick Reference](QUICK_REFERENCE.md)** - Fast lookup guide for common tasks

## 🚀 Quick Start

```bash
# Clone the repository
git clone https://github.com/devSakhawat/bdDevCRM.BackEnd.git

# Navigate to the API project
cd bdDevCRM.BackEnd/bdDevCRM.Api

# Run the application
dotnet run
```

Access Swagger UI at: `https://localhost:{port}/swagger`

## 🏗️ Technology Stack

- **Framework**: ASP.NET Core 8.0+ Web API
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer Token
- **Logging**: NLog
- **API Documentation**: Swagger/OpenAPI

## 📦 Projects Structure

The solution contains 13 projects organized into logical layers:

- **bdDevCRM.Api** - Web API entry point
- **bdDevCRM.Presentation** - Controllers layer
- **bdDevCRM.Service** - Business logic layer
- **bdDevCRM.ServiceContract** - Service interfaces
- **bdDevCRM.Repositories** - Data access layer
- **bdDevCRM.RepositoriesContracts** - Repository interfaces
- **bdDevCRM.Entities** - Domain entities
- **bdDevCRM.Sql** - Database context
- **bdDevCRM.Shared** - Shared DTOs
- **bdDevCRM.Utilities** - Helper functions
- **bdDevCRM.RepositoryDtos** - Repository DTOs
- **bdDevCRM.LoggerService** - Logging service

## 🎯 Key Features

- **Clean Architecture** with clear separation of concerns
- **Repository Pattern** for data access
- **Service Layer** for business logic
- **JWT Authentication** with token blacklisting
- **Comprehensive Exception Handling**
- **Grid Data Support** (pagination, sorting, filtering)
- **File Upload Management**
- **Audit Logging**
- **CORS Support**
- **Response Compression**

## 🔧 Configuration

Update `appsettings.json` with your settings:

```json
{
  "ConnectionStrings": {
    "DbLocation": "Server=YOUR_SERVER;Database=dbDevCRM;..."
  },
  "Jwt": {
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "ExpiryInMinutes": 540,
    "SecretKey": "your-secret-key"
  }
}
```

## 📖 Modules

### Core Module
- System administration
- User management
- Role-based access control
- Menu and module management

### HR Module
- Employee management
- Branch management
- Department management

### CRM Module
- Educational institute management
- Course catalog
- Student applications

### DMS Module
- Document management
- Version control
- Access logging

## 🛠️ Development

### Build
```bash
dotnet build bdDevCRM.BackEnd.sln
```

### Run Tests
```bash
dotnet test
```

### Database Migrations
```bash
# Add migration
dotnet ef migrations add YourMigrationName --project bdDevCRM.Sql

# Update database
dotnet ef database update --project bdDevCRM.Sql
```

## 📝 License

[Your License Here]

## 👥 Contributors

- Development Team

## 📞 Support

For detailed information, please refer to the documentation files listed above.

---

**Note**: Based on Educational agent requirements

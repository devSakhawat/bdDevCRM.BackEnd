# bdDevCRM Backend - Quick Reference Guide

## 🚀 Quick Start

### Run the Application
```bash
cd bdDevCRM.Api
dotnet run
```

### Access Swagger
```
https://localhost:{port}/swagger
```

---

## 📁 Project Quick Map

### Want to add a new feature? Here's where to start:

#### 1. **Add a new API endpoint?**
👉 Go to: `bdDevCRM.Presentation/Controllers/`
- Create controller inheriting from `BaseApiController`
- Add action methods with HTTP verbs

#### 2. **Add business logic?**
👉 Go to: `bdDevCRM.Service/`
- Create service class
- Define interface in `bdDevCRM.ServiceContract/`
- Register in `ServiceManager.cs`

#### 3. **Add database table?**
👉 Go to: `bdDevCRM.Entities/Entities/`
- Create entity class
- Add DbSet in `CRMContext.cs`
- Run migration: `dotnet ef migrations add YourMigrationName`

#### 4. **Add repository?**
👉 Go to: `bdDevCRM.Repositories/`
- Create repository class inheriting from `RepositoryBase<T>`
- Define interface in `bdDevCRM.RepositoriesContracts/`
- Register in `RepositoryManager.cs`

#### 5. **Add DTOs?**
👉 Go to: `bdDevCRM.Shared/DataTransferObjects/`
- Create DTO classes for data transfer

---

## 🏗️ Architecture Layers (Bottom to Top)

```
┌─────────────────────────────────────┐
│  📱 CLIENT (Browser, Mobile, etc.)  │
└─────────────┬───────────────────────┘
              │ HTTP Request
┌─────────────▼───────────────────────┐
│  🌐 API LAYER (bdDevCRM.Api)        │
│  • Program.cs                       │
│  • Middleware                       │
│  • Extensions                       │
└─────────────┬───────────────────────┘
              │
┌─────────────▼───────────────────────┐
│  🎮 CONTROLLERS                     │
│  (bdDevCRM.Presentation)            │
│  • CRM Controllers                  │
│  • HR Controllers                   │
│  • Admin Controllers                │
└─────────────┬───────────────────────┘
              │
┌─────────────▼───────────────────────┐
│  💼 SERVICES                        │
│  (bdDevCRM.Service)                 │
│  • Business Logic                   │
│  • Validation                       │
│  • Entity ↔ DTO Mapping            │
└─────────────┬───────────────────────┘
              │
┌─────────────▼───────────────────────┐
│  🗄️ REPOSITORIES                    │
│  (bdDevCRM.Repositories)            │
│  • Data Access                      │
│  • Queries                          │
│  • Transactions                     │
└─────────────┬───────────────────────┘
              │
┌─────────────▼───────────────────────┐
│  🗃️ DATABASE (SQL Server)           │
│  • Entity Framework Core            │
│  • CRMContext                       │
└─────────────────────────────────────┘
```

---

## 🔍 Finding Things Fast

### Configuration
- **Database Connection**: `bdDevCRM.Api/appsettings.json` → `ConnectionStrings:DbLocation`
- **JWT Settings**: `bdDevCRM.Api/appsettings.json` → `Jwt`
- **Logging**: `bdDevCRM.Api/nlog.config`

### Key Base Classes
- **Generic Repository**: `bdDevCRM.Repositories/RepositoryBase.cs`
- **Repository Manager**: `bdDevCRM.Repositories/RepositoryManager.cs`
- **Service Manager**: `bdDevCRM.Service/ServiceManager.cs`
- **Base Controller**: `bdDevCRM.Presentation/Controllers/BaseApiController.cs`

### Middleware
- **Exception Handling**: `bdDevCRM.Api/Middleware/ExceptionMiddleware.cs`
- **Audit**: `bdDevCRM.Api/Middleware/AuditMiddleware.cs`
- **Token Blacklist**: `bdDevCRM.Api/Extensions/TokenBlacklistMiddleware.cs`

---

## 📊 Module Breakdown

### Core Module (System Management)
```
Controllers: Presentation/Controllers/Core/SystemAdmin/
Services: Service/Core/SystemAdmin/
Repos: Repositories/Core/SystemAdmin/
```
**Features**: Users, Groups, Permissions, Menus, Modules, Settings

### HR Module (Human Resources)
```
Controllers: Presentation/Controllers/Core/HR/
Services: Service/Core/HR/
Repos: Repositories/Core/HR/
```
**Features**: Employees, Branches, Departments

### CRM Module (Customer Relations)
```
Controllers: Presentation/Controllers/CRM/
Services: Service/CRM/
Repos: Repositories/CRM/
```
**Features**: Institutes, Courses, Applications

### DMS Module (Document Management)
```
Controllers: Presentation/Controllers/DMS/
Services: Service/DMS/
Repos: Repositories/DMS/
```
**Features**: Documents, Folders, Tags, Versions

---

## 🛠️ Common Tasks

### Add New Entity
1. Create entity in `bdDevCRM.Entities/Entities/{Module}/`
2. Add DbSet in `CRMContext.cs`
3. Create migration: `dotnet ef migrations add AddYourEntity --project bdDevCRM.Sql`
4. Update database: `dotnet ef database update --project bdDevCRM.Sql`

### Add New API Endpoint
1. Create DTO in `bdDevCRM.Shared/DataTransferObjects/{Module}/`
2. Create repository interface in `bdDevCRM.RepositoriesContracts/{Module}/`
3. Create repository in `bdDevCRM.Repositories/{Module}/`
4. Register in `RepositoryManager.cs`
5. Create service interface in `bdDevCRM.ServiceContract/{Module}/`
6. Create service in `bdDevCRM.Service/{Module}/`
7. Register in `ServiceManager.cs`
8. Create controller in `bdDevCRM.Presentation/Controllers/{Module}/`

### Add Custom Exception
1. Create exception class in `bdDevCRM.Entities/Exceptions/`
2. Inherit from appropriate base class
3. Exception will be caught by `ExceptionMiddleware`

### Add Action Filter
1. Create filter in `bdDevCRM.Presentation/ActionFilters/`
2. Register in `Program.cs`
3. Apply to controllers with `[YourFilter]` attribute

---

## 🔐 Authentication Flow

```
1. Login Request
   POST /api/auth/login
   Body: { username, password }
   
2. AuthenticationController
   ↓
3. AuthenticationService
   • Validate credentials
   • Generate JWT token
   ↓
4. Return TokenResponse
   { token, expiration, refreshToken }
   
5. Client stores token
   
6. Subsequent Requests
   Header: Authorization: Bearer {token}
   
7. JWT Middleware validates token
   
8. Controller action executes
```

---

## 📦 Repository Pattern Usage

### Generic CRUD Operations
```csharp
// Get by ID
var entity = await _repository.GetByIdAsync(x => x.Id == id);

// List all
var entities = await _repository.ListAsync();

// List with condition
var filtered = await _repository.ListByConditionAsync(x => x.IsActive);

// Create
await _repository.CreateAsync(entity);
await _repositoryManager.SaveAsync();

// Update
_repository.Update(entity);
await _repositoryManager.SaveAsync();

// Delete
_repository.Delete(entity);
await _repositoryManager.SaveAsync();
```

### Grid Data with Pagination
```csharp
var gridData = await _repository.GridData<YourDto>(
    query: "SELECT * FROM YourTable",
    options: gridOptions,
    orderBy: "Id",
    condition: ""
);
```

---

## 🎯 Design Patterns Used

| Pattern | Location | Purpose |
|---------|----------|---------|
| **Repository** | bdDevCRM.Repositories | Data access abstraction |
| **Unit of Work** | RepositoryManager | Transaction management |
| **Service Layer** | bdDevCRM.Service | Business logic encapsulation |
| **DTO** | bdDevCRM.Shared | Data transfer objects |
| **Dependency Injection** | Program.cs | Loose coupling |
| **Lazy Loading** | Managers | Performance optimization |
| **Middleware Pipeline** | bdDevCRM.Api | Request/response processing |
| **Action Filters** | bdDevCRM.Presentation | Cross-cutting concerns |

---

## 📝 Code Conventions

### Naming
- **Controllers**: `{Entity}Controller` (e.g., `EmployeeController`)
- **Services**: `{Entity}Service` (e.g., `EmployeeService`)
- **Interfaces**: `I{Entity}Service` (e.g., `IEmployeeService`)
- **Repositories**: `{Entity}Repository` (e.g., `EmployeeRepository`)
- **DTOs**: `{Entity}Dto` (e.g., `EmployeeDto`)

### Async Methods
Always suffix async methods with `Async`:
```csharp
public async Task<Employee> GetEmployeeAsync(int id)
```

### Dependency Injection
Always inject interfaces, not implementations:
```csharp
public class EmployeeController : BaseApiController
{
    private readonly IServiceManager _service;
    
    public EmployeeController(IServiceManager service)
    {
        _service = service;
    }
}
```

---

## 🐛 Debugging Tips

### Enable Detailed Errors
In `appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

### View Logs
- **Application Logs**: Check NLog output
- **Internal Logs**: `bdDevCRM.Api/internal_logs/internallog.txt`

### Database Queries
Enable EF Core logging in `Program.cs`:
```csharp
builder.Services.AddDbContext<CRMContext>(options =>
    options.UseSqlServer(connectionString)
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors()
);
```

---

## 📚 Key Dependencies

| Package | Purpose |
|---------|---------|
| Microsoft.EntityFrameworkCore | ORM for database access |
| Microsoft.AspNetCore.Authentication.JwtBearer | JWT authentication |
| NLog.Web.AspNetCore | Logging framework |
| Newtonsoft.Json | JSON serialization |
| Swashbuckle.AspNetCore | Swagger/OpenAPI |

---

## 🔗 Useful Links

- **Solution File**: `bdDevCRM.BackEnd.sln`
- **Entry Point**: `bdDevCRM.Api/Program.cs`
- **Database Context**: `bdDevCRM.Sql/Context/CRMContext.cs`
- **API Docs**: `/swagger` (when running)

---

## 📞 Getting Help

1. **Architecture Details**: See `ARCHITECTURE.md`
2. **File Structure**: See `FILE_STRUCTURE.md`
3. **This Guide**: `QUICK_REFERENCE.md`

---

## 🎓 Learning Path

### New to the project?
1. Start with `ARCHITECTURE.md` - Understand the overall design
2. Review `FILE_STRUCTURE.md` - Know where everything lives
3. Look at `Program.cs` - See how it all starts
4. Explore a simple controller (e.g., `CountryController`)
5. Follow the flow: Controller → Service → Repository → Database
6. Try modifying an existing feature
7. Add a new simple feature following patterns

### Want to add features?
1. Identify which module (Core/CRM/HR/DMS)
2. Create entity (if needed)
3. Create repository + interface
4. Create service + interface
5. Create controller
6. Register in managers
7. Test via Swagger

---

**Last Updated**: 2025-10-21  
**Version**: 1.0

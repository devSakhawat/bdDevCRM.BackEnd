# bdDevCRM Backend - Architecture Documentation

## Table of Contents
1. [Overview](#overview)
2. [Technology Stack](#technology-stack)
3. [Project Structure](#project-structure)
4. [Software Architecture](#software-architecture)
5. [Layer Descriptions](#layer-descriptions)
6. [Design Patterns](#design-patterns)
7. [Module Organization](#module-organization)
8. [Data Flow](#data-flow)
9. [Key Components](#key-components)

---

## Overview

**bdDevCRM.BackEnd** is an ASP.NET Core Web API application implementing a Customer Relationship Management (CRM) system designed for educational institutions. The project follows **Clean Architecture** principles with clear separation of concerns across multiple layers.

### Project Statistics
- **Total Projects**: 13
- **Total C# Files**: ~426
- **Total Controllers**: 23
- **Solution Type**: .NET/C# Multi-Project Solution
- **Architecture Pattern**: Clean Architecture / Onion Architecture

---

## Technology Stack

### Core Framework
- **Framework**: ASP.NET Core 8.0+ (Web API)
- **Language**: C# (.NET)
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core

### Key Libraries & Technologies
- **NLog**: Logging framework
- **JWT Authentication**: Token-based authentication
- **Newtonsoft.Json**: JSON serialization
- **AutoMapper**: Object-to-object mapping (implied by usage patterns)
- **Entity Framework Core**: Database access and migrations
- **Memory Cache**: In-memory caching
- **Swagger/OpenAPI**: API documentation

### Additional Features
- Response Compression (Gzip)
- CORS Support
- File Upload Management
- CSV Export Functionality
- Custom Exception Handling Middleware
- Token Blacklisting

---

## Project Structure

```
bdDevCRM.BackEnd/
├── bdDevCRM.Api/                    # 🌐 Web API Entry Point
├── bdDevCRM.Presentation/           # 🎮 Controllers Layer
├── bdDevCRM.Service/                # 💼 Business Logic Layer
├── bdDevCRM.ServiceContract/        # 📜 Service Interfaces
├── bdDevCRM.Repositories/           # 🗄️ Data Access Layer
├── bdDevCRM.RepositoriesContracts/  # 📜 Repository Interfaces
├── bdDevCRM.RepositoryDtos/         # 📦 Repository DTOs
├── bdDevCRM.Entities/               # 📊 Domain Entities & Models
├── bdDevCRM.Sql/                    # 🗃️ Database Context
├── bdDevCRM.Shared/                 # 🔄 Shared DTOs & Response Models
├── bdDevCRM.Utilities/              # 🛠️ Helper Functions & Constants
└── bdDevCRM.LoggerSevice/          # 📝 Logging Service
```

### Detailed Folder Structure

#### 1. **bdDevCRM.Api** (Presentation/Entry Point)
```
bdDevCRM.Api/
├── Controllers/           # Sample weather controller
├── Extensions/           # Service configuration extensions
├── Middleware/           # Custom middleware (Exception, Audit, Token)
├── ApiResponseError/     # API response helpers
├── ContentFormatter/     # Custom formatters (CSV)
├── ContextFactory/       # DbContext factory for migrations
├── wwwroot/              # Static files (file uploads)
├── Program.cs           # Application entry point
└── appsettings.json     # Configuration
```

#### 2. **bdDevCRM.Presentation** (Controllers Layer)
```
bdDevCRM.Presentation/
├── Controllers/
│   ├── Authentication/   # Auth controllers
│   ├── CRM/             # CRM module controllers
│   ├── Core/            # Core system controllers
│   │   ├── HR/          # HR controllers
│   │   └── SystemAdmin/ # Admin controllers
│   ├── DMS/             # Document management
│   └── BaseApiController.cs
├── ActionFilters/       # Custom action filters
├── AuthorizeAttributes/ # Custom authorization
├── ModelBinders/        # Custom model binding
└── Extentions/          # Helper extensions
```

#### 3. **bdDevCRM.Service** (Business Logic)
```
bdDevCRM.Service/
├── Authentication/      # Auth services
├── CRM/                # CRM business logic
├── Core/
│   ├── HR/            # HR business logic
│   └── SystemAdmin/   # Admin business logic
├── DMS/               # Document management logic
└── ServiceManager.cs  # Service aggregator
```

#### 4. **bdDevCRM.ServiceContract** (Service Interfaces)
```
bdDevCRM.ServiceContract/
├── Authentication/     # Auth service interfaces
├── CRM/               # CRM service interfaces
├── Core/
│   ├── HR/           # HR service interfaces
│   └── SystemAdmin/  # Admin service interfaces
├── DMS/              # DMS service interfaces
└── IServiceManager.cs # Service manager interface
```

#### 5. **bdDevCRM.Repositories** (Data Access)
```
bdDevCRM.Repositories/
├── Core/
│   ├── Authentication/   # Auth repositories
│   ├── HR/              # HR repositories
│   └── SystemAdmin/     # Admin repositories
├── CRM/                 # CRM repositories
├── DMS/                 # DMS repositories
├── RepositoryBase.cs    # Generic repository base
└── RepositoryManager.cs # Repository aggregator
```

#### 6. **bdDevCRM.RepositoriesContracts** (Repository Interfaces)
```
bdDevCRM.RepositoriesContracts/
├── Core/
│   ├── Authentication/
│   ├── HR/
│   └── SystemAdmin/
├── CRM/
├── DMS/
├── IRepositoryBase.cs
└── IRepositoryManager.cs
```

#### 7. **bdDevCRM.Entities** (Domain Models)
```
bdDevCRM.Entities/
├── Entities/
│   ├── Core/           # Core domain entities
│   ├── System/         # System entities
│   ├── CRM/           # CRM entities
│   └── DMS/           # DMS entities
├── Exceptions/         # Custom exception classes
├── ExceptionEntities/  # Exception models
├── Token/             # Token response models
└── CRMGrid/           # Grid functionality utilities
```

#### 8. **bdDevCRM.Sql** (Database Context)
```
bdDevCRM.Sql/
└── Context/
    └── CRMContext.cs   # EF Core DbContext
```

#### 9. **bdDevCRM.Shared** (Shared DTOs)
```
bdDevCRM.Shared/
├── DataTransferObjects/
│   ├── Authentication/  # Auth DTOs
│   ├── CRM/            # CRM DTOs
│   ├── Core/           # Core DTOs
│   │   ├── HR/
│   │   └── SystemAdmin/
│   ├── DMS/            # DMS DTOs
│   └── Common/         # Common DTOs
└── ApiResponse/        # API response models
```

#### 10. **bdDevCRM.Utilities** (Helpers)
```
bdDevCRM.Utilities/
├── Common/            # Common helpers
├── Constants/         # Application constants
├── KendoGrid/        # Grid utilities
└── OthersLibrary/    # Additional utilities
```

#### 11. **bdDevCRM.RepositoryDtos**
```
bdDevCRM.RepositoryDtos/
├── Core/
│   ├── HR/
│   └── SystemAdmin/
├── CRM/
└── DMS/
```

#### 12. **bdDevCRM.LoggerSevice**
```
bdDevCRM.LoggerSevice/
└── LoggerManager.cs   # NLog implementation
```

---

## Software Architecture

### Architecture Pattern: **Clean Architecture / Onion Architecture**

The application follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────┐
│                 Presentation Layer                  │
│         (API Controllers, Middleware)               │
│                bdDevCRM.Api                         │
│              bdDevCRM.Presentation                  │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│              Application Layer                      │
│          (Business Logic / Services)                │
│               bdDevCRM.Service                      │
│           bdDevCRM.ServiceContract                  │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│            Infrastructure Layer                     │
│          (Data Access / Repositories)               │
│            bdDevCRM.Repositories                    │
│        bdDevCRM.RepositoriesContracts               │
│                 bdDevCRM.Sql                        │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│               Domain Layer                          │
│          (Entities, Domain Models)                  │
│              bdDevCRM.Entities                      │
└─────────────────────────────────────────────────────┘

         Cross-Cutting Concerns:
┌─────────────────────────────────────────────────────┐
│    bdDevCRM.Shared | bdDevCRM.Utilities             │
│        bdDevCRM.LoggerService                       │
└─────────────────────────────────────────────────────┘
```

---

## Layer Descriptions

### 1. **Presentation Layer** 
**Projects**: `bdDevCRM.Api`, `bdDevCRM.Presentation`

**Responsibilities**:
- HTTP request/response handling
- API endpoint definitions (Controllers)
- Authentication/Authorization filters
- Input validation
- Exception handling middleware
- Response formatting (JSON, XML, CSV)
- CORS configuration
- Swagger documentation

**Key Components**:
- Controllers (Authentication, CRM, HR, SystemAdmin, DMS)
- Action Filters (Logging, Validation, Media Type)
- Custom Authorization Attributes
- Exception Middleware
- API Response Helpers

---

### 2. **Application Layer** 
**Projects**: `bdDevCRM.Service`, `bdDevCRM.ServiceContract`

**Responsibilities**:
- Business logic implementation
- Data transformation (Entity ↔ DTO)
- Business rule validation
- Transaction coordination
- Service orchestration

**Key Features**:
- Service Manager pattern (aggregates all services)
- Lazy initialization of services
- Memory caching support
- Configuration injection

**Service Categories**:
- **Authentication Services**: Login, JWT token management, token blacklisting
- **CRM Services**: Institutes, Courses, Applications
- **HR Services**: Employees, Branches, Departments
- **System Admin Services**: Users, Groups, Permissions, Menus, Modules
- **DMS Services**: Document management, versioning, access logs

---

### 3. **Infrastructure Layer** 
**Projects**: `bdDevCRM.Repositories`, `bdDevCRM.RepositoriesContracts`, `bdDevCRM.Sql`

**Responsibilities**:
- Database access via Entity Framework Core
- Query execution (LINQ, SQL)
- Transaction management
- Data persistence

**Key Features**:
- **Generic Repository Pattern**: `RepositoryBase<T>`
- **Unit of Work Pattern**: `RepositoryManager`
- **Lazy Repository Loading**: Repositories created on-demand
- **Advanced Querying**: LINQ, Raw SQL, Stored Procedures
- **Grid Data Support**: Pagination, sorting, filtering (Kendo Grid)
- **Transaction Management**: Begin, Commit, Rollback

**Repository Base Capabilities**:
- CRUD operations (sync & async)
- Bulk operations (insert, delete)
- Filtering with LINQ expressions
- Pagination and sorting
- Custom SQL execution
- Transaction support
- Grid data retrieval with dynamic mapping

---

### 4. **Domain Layer** 
**Projects**: `bdDevCRM.Entities`

**Responsibilities**:
- Domain models (Entities)
- Business entities definition
- Custom exceptions
- Domain-specific logic

**Entity Categories**:
- **Core Entities**: Countries, Companies, Users, Groups, Permissions
- **CRM Entities**: Institutes, Courses, Applications, Months, Years
- **HR Entities**: Employees, Branches, Departments
- **System Entities**: Menus, Modules, Settings, Workflows
- **DMS Entities**: Documents, Folders, Tags, Versions, Access Logs

**Exception Types**:
- `AccessDeniedException`
- `DuplicateRecordException`
- `GenericNotFoundException`
- `UnauthorizedAccessException`
- `JWTSecurityException`
- Custom validation exceptions

---

### 5. **Cross-Cutting Concerns**
**Projects**: `bdDevCRM.Shared`, `bdDevCRM.Utilities`, `bdDevCRM.LoggerService`

**Shared** (DTOs & Response Models):
- Data Transfer Objects for all modules
- API Response wrappers
- Validation response models

**Utilities** (Helpers & Constants):
- Common helper functions
- Encryption/Decryption helpers
- File upload helpers
- Validation helpers
- Constants (Messages, Routes, Validation rules)
- Kendo Grid utilities

**Logger Service**:
- NLog-based logging
- Centralized logging interface

---

## Design Patterns

### 1. **Repository Pattern**
- Abstracts data access logic
- `RepositoryBase<T>` provides generic CRUD operations
- Specific repositories extend base for custom queries

### 2. **Unit of Work Pattern**
- `RepositoryManager` aggregates all repositories
- Provides centralized `SaveAsync()` method
- Manages database context lifecycle

### 3. **Service Layer Pattern**
- `ServiceManager` aggregates all services
- Decouples business logic from controllers
- Services coordinate between repositories and DTOs

### 4. **Lazy Initialization Pattern**
- Services and repositories created on-demand
- Improves startup performance
- Reduces memory footprint

### 5. **Dependency Injection**
- All dependencies injected via constructor
- Configured in `Program.cs` and `ServiceExtensions.cs`
- Interface-based design for testability

### 6. **Manager Pattern**
- `RepositoryManager` for data access coordination
- `ServiceManager` for business logic coordination
- Simplifies dependency injection

### 7. **Middleware Pipeline Pattern**
- Custom exception middleware
- Audit middleware
- Token blacklist middleware
- Response compression middleware

### 8. **Action Filter Pattern**
- `LogActionAttribute`: Action logging
- `EmptyObjectFilterAttribute`: Null validation
- `ValidateMediaTypeAttribute`: Media type validation

### 9. **DTO Pattern**
- Separate DTOs for data transfer
- Entity-to-DTO mapping in services
- Prevents exposing domain models directly

---

## Module Organization

The application is organized into three main functional modules:

### 1. **Core Module**
**Purpose**: System administration and core functionality

**Sub-modules**:
- **SystemAdmin**: Users, Groups, Permissions, Menus, Modules, Settings, Workflows
- **HR (Human Resources)**: Employees, Branches, Departments
- **Authentication**: Login, JWT tokens, token blacklisting

**Key Features**:
- User management
- Role-based access control (RBAC)
- Menu and module management
- Workflow engine
- Employee management

---

### 2. **CRM Module**
**Purpose**: Customer Relationship Management for educational institutions

**Features**:
- Institute management
- Course catalog
- Student applications
- Institute types
- Time periods (months, years)

**Domain Entities**:
- `CRMInstitute`: Educational institution details
- `CRMCourse`: Course offerings
- `CRMApplication`: Student applications
- `CRMInstituteType`: Classification of institutes
- `CRMMonth`, `CRMYear`: Time period entities

---

### 3. **DMS Module**
**Purpose**: Document Management System

**Features**:
- Document storage and retrieval
- Document versioning
- Folder organization
- Tag-based categorization
- Access logging
- Document type classification

**Domain Entities**:
- `DMSDocument`: Main document entity
- `DMSDocumentType`: Document classification
- `DMSDocumentFolder`: Folder structure
- `DMSDocumentTag`: Tags for categorization
- `DMSDocumentVersion`: Version history
- `DMSDocumentAccessLog`: Audit trail

---

## Data Flow

### Typical Request Flow

```
1. HTTP Request
   ↓
2. Middleware Pipeline
   ↓
3. Controller (Presentation Layer)
   ↓
4. Action Filters (Validation, Logging)
   ↓
5. Service Layer (Business Logic)
   ↓
6. Repository Layer (Data Access)
   ↓
7. Database (SQL Server via EF Core)
   ↓
8. Response (JSON/XML/CSV)
```

### Detailed Flow Example (Get Institute)

```
Client Request: GET /api/institutes/123
   ↓
ExceptionMiddleware (Error Handling)
   ↓
CRMApplicationController.GetInstitute(123)
   ↓
[LogAction] Filter (Logs action)
   ↓
ICRMInstituteService.GetByIdAsync(123)
   ↓
RepositoryManager.CRMInstitutes.GetByIdAsync()
   ↓
RepositoryBase<T>.GetByIdAsync()
   ↓
EF Core Query → SQL Server
   ↓
Map Entity → DTO
   ↓
ResponseHelper.CreateResponse(data)
   ↓
JSON Response → Client
```

---

## Key Components

### 1. **Program.cs** (Entry Point)
```csharp
- Configures services (CORS, Authentication, Database, etc.)
- Sets up middleware pipeline
- Configures dependency injection
- Enables Swagger
- Configures response compression
```

### 2. **ServiceExtensions.cs**
```csharp
- Extension methods for service configuration
- Database context configuration
- JWT authentication setup
- CORS policy configuration
- Repository/Service manager registration
```

### 3. **CRMContext.cs** (Database Context)
```csharp
- EF Core DbContext
- Entity configurations
- Database connection management
```

### 4. **RepositoryBase<T>**
```csharp
Key Methods:
- CreateAsync / Create
- Update / Delete
- GetByIdAsync / GetById
- ListAsync / List
- ListByConditionAsync
- BulkInsertAsync / BulkDelete
- TransactionBeginAsync / CommitAsync / RollbackAsync
- GridData (Pagination, Sorting, Filtering)
- ExecuteListQuery / ExecuteSingleData (Raw SQL)
```

### 5. **RepositoryManager**
```csharp
Aggregates all repositories:
- Countries, Companies, Users
- Employees, Branches
- CRMInstitutes, CRMCourses
- DMSDocuments, DMSFolders
- Provides SaveAsync() method
```

### 6. **ServiceManager**
```csharp
Aggregates all services:
- Authentication, Users, Groups
- Employees, Branches
- CRMInstitutes, CRMCourses
- DMSDocuments, DMSFolders
- Provides caching support
```

### 7. **ExceptionMiddleware**
```csharp
- Global exception handling
- Returns structured error responses
- Logs exceptions
```

### 8. **Authentication System**
```csharp
- JWT token-based authentication
- Token blacklisting support
- Custom authorization attributes
- Token expiration management
```

---

## API Response Structure

### Success Response
```json
{
  "isSuccess": true,
  "statusCode": 200,
  "message": "Success",
  "data": { ... }
}
```

### Error Response
```json
{
  "isSuccess": false,
  "statusCode": 400,
  "message": "Error message",
  "errors": [...]
}
```

---

## Configuration

### Database Connection
Located in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DbLocation": "Server=...;Database=dbDevCRM;..."
  }
}
```

### JWT Configuration
```json
{
  "Jwt": {
    "Issuer": "...",
    "Audience": "...",
    "ExpiryInMinutes": 540,
    "SecretKey": "..."
  }
}
```

---

## Security Features

1. **JWT Authentication**: Token-based authentication with configurable expiry
2. **Token Blacklisting**: Revoked tokens tracked in database
3. **Authorization Attributes**: Custom attributes for permission checking
4. **CORS Policy**: Configured for cross-origin requests
5. **Input Validation**: Model validation with action filters
6. **Exception Handling**: Sanitized error messages for production
7. **Audit Middleware**: Request/response logging

---

## File Upload Management

- **Upload Directory**: `wwwroot/Uploads/`
- **Module-specific Folders**: `CRMInstitute/`, etc.
- **Static File Serving**: Configured with CORS headers
- **File Size Limits**: Configurable via `ConfigureFileLimit()`

---

## Grid Functionality

The application includes sophisticated grid/table functionality:
- **Pagination**: Server-side pagination support
- **Sorting**: Dynamic column sorting
- **Filtering**: Advanced filtering with multiple criteria
- **Kendo Grid Integration**: Custom grid data source
- **CSV Export**: Export grid data to CSV
- **Dynamic Column Mapping**: Case-insensitive property mapping

---

## Testing & Development

### Build Command
```bash
dotnet build bdDevCRM.BackEnd.sln
```

### Run Application
```bash
dotnet run --project bdDevCRM.Api
```

### Migration Commands
```bash
dotnet ef migrations add InitialCreate --project bdDevCRM.Sql
dotnet ef database update --project bdDevCRM.Sql
```

---

## Best Practices Implemented

1. ✅ **Separation of Concerns**: Clear layer boundaries
2. ✅ **Dependency Inversion**: Depends on interfaces, not implementations
3. ✅ **Single Responsibility**: Each class has one clear purpose
4. ✅ **DRY Principle**: Generic base classes for common operations
5. ✅ **Async/Await**: Asynchronous operations throughout
6. ✅ **Exception Handling**: Structured exception hierarchy
7. ✅ **Logging**: Centralized logging with NLog
8. ✅ **Configuration Management**: Environment-specific settings
9. ✅ **API Versioning Ready**: Structure supports versioning
10. ✅ **Documentation**: Swagger/OpenAPI support

---

## Future Considerations

- Unit Testing project (currently not present)
- Integration Testing project
- API versioning implementation
- Docker containerization
- CI/CD pipeline configuration
- Rate limiting middleware
- Caching strategy enhancement
- Message queue integration
- Event sourcing for audit trail

---

## Solution Organization (Visual Studio)

The solution is organized into logical folders:

```
bdDevCRM.BackEnd.sln
├── 📁 Core
│   ├── bdDevCRM.Entities
│   ├── bdDevCRM.LoggerService
│   ├── bdDevCRM.Repositories
│   ├── bdDevCRM.RepositoriesContracts
│   ├── bdDevCRM.RepositoryDtos
│   └── bdDevCRM.Sql
├── 📁 Infrastructure
│   ├── bdDevCRM.Service
│   └── bdDevCRM.ServiceContract
├── 📁 Presentation
│   ├── bdDevCRM.Api
│   └── bdDevCRM.Presentation
└── 📁 Utilities
    ├── bdDevCRM.Shared
    └── bdDevCRM.Utilities
```

---

## Conclusion

The **bdDevCRM.BackEnd** project demonstrates a well-structured, enterprise-level ASP.NET Core Web API application following modern software architecture principles. The clear separation of concerns, extensive use of design patterns, and modular organization make it maintainable, testable, and scalable for future enhancements.

---

**Document Version**: 1.0  
**Last Updated**: 2025-10-21  
**Maintained By**: Development Team

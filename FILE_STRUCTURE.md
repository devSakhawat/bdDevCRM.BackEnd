# bdDevCRM.BackEnd - Complete File and Folder Structure

## Root Directory Overview

```
bdDevCRM.BackEnd/
├── .git/                          # Git version control
├── .gitattributes                 # Git attributes configuration
├── .gitignore                     # Git ignore patterns
├── README.md                      # Project readme
├── ARCHITECTURE.md                # Architecture documentation (this document)
├── FILE_STRUCTURE.md              # File structure documentation
├── bdDevCRM.BackEnd.sln          # Visual Studio solution file
│
├── bdDevCRM.Api/                 # 🌐 Web API Entry Point
├── bdDevCRM.Presentation/        # 🎮 Controllers Layer
├── bdDevCRM.Service/             # 💼 Business Logic Layer
├── bdDevCRM.ServiceContract/     # 📜 Service Interfaces
├── bdDevCRM.Repositories/        # 🗄️ Data Access Layer
├── bdDevCRM.RepositoriesContracts/ # 📜 Repository Interfaces
├── bdDevCRM.RepositoryDtos/      # 📦 Repository DTOs
├── bdDevCRM.Entities/            # 📊 Domain Entities
├── bdDevCRM.Sql/                 # 🗃️ Database Context
├── bdDevCRM.Shared/              # 🔄 Shared Resources
├── bdDevCRM.Utilities/           # 🛠️ Utilities
└── bdDevCRM.LoggerSevice/        # 📝 Logging Service
```

---

## 1. bdDevCRM.Api (Web API Entry Point)

```
bdDevCRM.Api/
├── Properties/
│   └── launchSettings.json                  # Launch configuration
├── Controllers/
│   └── WeatherForecastController.cs         # Sample controller
├── Extensions/
│   ├── ExceptionMiddlewareExtensions.cs     # Exception middleware setup
│   ├── HttpContextExtensions.cs             # HttpContext helpers
│   ├── ServiceExtensions.cs                 # Service configuration
│   └── TokenBlacklistMiddleware.cs          # Token blacklist middleware
├── Middleware/
│   ├── AuditMiddleware.cs                   # Request/response auditing
│   └── ExceptionMiddleware.cs               # Global exception handling
├── ApiResponseError/
│   ├── ApiException.cs                      # API exception class
│   ├── ApiResponse.cs                       # Standard API response
│   ├── ApiValidationErrorResponse.cs        # Validation error response
│   └── ResponseHelper.cs                    # Response helper methods
├── ContentFormatter/
│   └── CsvOutputFormatter.cs                # CSV output formatter
├── ContextFactory/
│   └── RepositoryContextFactory.cs          # DbContext factory for migrations
├── wwwroot/
│   └── Uploads/                             # File upload directory
│       └── CRMInstitute/                    # CRM institute files
│           └── 1/
│               ├── Logo/                     # Institute logos
│               └── Prospectus/              # Institute prospectus files
├── internal_logs/
│   └── internallog.txt                      # NLog internal logs
├── GlobalExceptionHandler.cs                # Global exception handler
├── Program.cs                               # Application entry point
├── WeatherForecast.cs                       # Sample model
├── appsettings.json                         # Configuration settings
├── appsettings.Development.json             # Development settings
├── bdDevCRM.Api.csproj                      # Project file
├── bdDevCRM.Api.http                        # HTTP test file
└── nlog.config                              # NLog configuration
```

**Purpose**: Main entry point for the Web API. Handles HTTP requests, middleware configuration, and application startup.

**Key Files**:
- `Program.cs`: Application bootstrap and configuration
- `ServiceExtensions.cs`: Dependency injection configuration
- `ExceptionMiddleware.cs`: Global error handling
- `appsettings.json`: Database connection, JWT settings, etc.

---

## 2. bdDevCRM.Presentation (Controllers Layer)

```
bdDevCRM.Presentation/
├── Controllers/
│   ├── Authentication/
│   │   ├── AuthenticationController.cs      # Login, logout, token refresh
│   │   └── ...
│   ├── Core/
│   │   ├── HR/
│   │   │   ├── EmployeeController.cs        # Employee management
│   │   │   ├── BranchController.cs          # Branch management
│   │   │   └── ...
│   │   └── SystemAdmin/
│   │       ├── CountryController.cs         # Country management
│   │       ├── CompanyController.cs         # Company management
│   │       ├── UsersController.cs           # User management
│   │       ├── MenuController.cs            # Menu management
│   │       ├── ModuleController.cs          # Module management
│   │       ├── GroupController.cs           # Group management
│   │       ├── StatusController.cs          # Status/workflow management
│   │       └── ...
│   ├── CRM/
│   │   ├── CRMApplicationController.cs      # CRM applications
│   │   ├── CRMInstituteController.cs        # Institute management (implied)
│   │   ├── CRMCourseController.cs           # Course management (implied)
│   │   └── ...
│   ├── DMS/
│   │   ├── DMSDocumentController.cs         # Document management
│   │   ├── DMSFolderController.cs           # Folder management
│   │   └── ...
│   ├── BaseApiController.cs                 # Base controller for common functionality
│   ├── BuggyController.cs                   # Error testing controller
│   ├── HomeController.cs                    # Home/health check
│   └── TestController.cs                    # Test endpoints
├── ActionFilters/
│   ├── EmptyObjectFilterAttribute.cs        # Validates non-null objects
│   ├── LogActionAttribute.cs                # Logs controller actions
│   └── ValidateMediaTypeAttribute.cs        # Validates media types
├── AuthorizeAttributes/
│   └── AuthorizeUserAttribute.cs            # Custom authorization
├── ModelBinders/
│   └── ArrayModelBinder.cs                  # Custom array model binding
├── Extentions/
│   └── HttpContextExtensions.cs             # HttpContext helpers
├── PresentationReference.cs                 # Assembly reference marker
└── bdDevCRM.Presentation.csproj             # Project file
```

**Purpose**: Contains all API controllers that handle HTTP requests and return responses.

**Controller Categories**:
- **Authentication**: User login, logout, token management
- **SystemAdmin**: Users, groups, permissions, menus, modules
- **HR**: Employees, branches, departments
- **CRM**: Institutes, courses, applications
- **DMS**: Document management

---

## 3. bdDevCRM.Service (Business Logic Layer)

```
bdDevCRM.Service/
├── Authentication/
│   ├── AuthenticationService.cs             # Authentication logic
│   └── TokenBlacklistService.cs             # Token blacklist management
├── Core/
│   ├── HR/
│   │   ├── EmployeeService.cs               # Employee business logic
│   │   ├── BranchService.cs                 # Branch business logic
│   │   └── DepartmentService.cs             # Department business logic
│   └── SystemAdmin/
│       ├── CountryService.cs                # Country business logic
│       ├── CompanyService.cs                # Company business logic
│       ├── CurrencyService.cs               # Currency business logic
│       ├── UsersService.cs                  # User business logic
│       ├── MenuService.cs                   # Menu business logic
│       ├── ModuleService.cs                 # Module business logic
│       ├── GroupService.cs                  # Group business logic
│       ├── SystemSettingsService.cs         # System settings logic
│       ├── QueryAnalyzerService.cs          # Query analyzer logic
│       ├── StatusService.cs                 # Status/workflow logic
│       ├── AccessControlService.cs          # Access control logic
│       └── ...
├── CRM/
│   ├── CRMInstituteService.cs               # Institute business logic
│   ├── CRMCourseService.cs                  # Course business logic
│   ├── CRMMonthService.cs                   # Month business logic
│   └── CRMYearService.cs                    # Year business logic
├── DMS/
│   ├── DmsDocumentService.cs                # Document business logic
│   ├── DmsDocumentService2.cs               # Alternative document service
│   ├── DmsdocumentAccessLogService.cs       # Access log business logic
│   ├── DmsdocumentFolderService.cs          # Folder business logic
│   ├── DmsdocumentTagService.cs             # Tag business logic
│   ├── DmsdocumentTagMapService.cs          # Tag mapping business logic
│   ├── DmsdocumentTypeService.cs            # Document type business logic
│   └── DmsdocumentVersionService.cs         # Version business logic
├── ServiceManager.cs                         # Service aggregator
└── bdDevCRM.Service.csproj                  # Project file
```

**Purpose**: Implements business logic, coordinates between repositories, performs data transformation.

**Responsibilities**:
- Business rule validation
- Entity to DTO mapping
- Transaction coordination
- Complex business operations

---

## 4. bdDevCRM.ServiceContract (Service Interfaces)

```
bdDevCRM.ServiceContract/
├── Authentication/
│   ├── IAuthenticationService.cs            # Auth service interface
│   └── ITokenBlacklistService.cs            # Token blacklist interface
├── Core/
│   ├── HR/
│   │   ├── IEmployeeService.cs              # Employee service interface
│   │   ├── IBranchService.cs                # Branch service interface
│   │   └── IDepartmentService.cs            # Department service interface
│   └── SystemAdmin/
│       ├── ICountryService.cs               # Country service interface
│       ├── ICompanyService.cs               # Company service interface
│       ├── ICurrencyService.cs              # Currency service interface
│       ├── IUsersService.cs                 # User service interface
│       ├── IMenuService.cs                  # Menu service interface
│       ├── IModuleService.cs                # Module service interface
│       ├── IGroupService.cs                 # Group service interface
│       ├── ISystemSettingsService.cs        # Settings service interface
│       ├── IQueryAnalyzerService.cs         # Query analyzer interface
│       ├── IStatusService.cs                # Status service interface
│       ├── IAccessControlService.cs         # Access control interface
│       └── ...
├── CRM/
│   ├── ICRMInstituteService.cs              # Institute service interface
│   ├── ICRMCourseService.cs                 # Course service interface
│   ├── ICRMMonthService.cs                  # Month service interface
│   └── ICRMYearService.cs                   # Year service interface
├── DMS/
│   ├── IDmsDocumentService.cs               # Document service interface
│   ├── IDmsDocumentAccessLogService.cs      # Access log service interface
│   ├── IDmsdocumentFolderService.cs         # Folder service interface
│   ├── IDmsdocumentTagService.cs            # Tag service interface
│   ├── IDmsdocumentTagMapService.cs         # Tag map service interface
│   ├── IDmsdocumentTypeService.cs           # Document type interface
│   └── IDmsdocumentVersionService.cs        # Version service interface
├── IServiceManager.cs                        # Service manager interface
└── bdDevCRM.ServiceContract.csproj          # Project file
```

**Purpose**: Defines contracts for all services, enabling dependency injection and loose coupling.

---

## 5. bdDevCRM.Repositories (Data Access Layer)

```
bdDevCRM.Repositories/
├── Core/
│   ├── Authentication/
│   │   ├── AuthenticationRepository.cs      # Authentication data access
│   │   └── TokenBlacklistRepository.cs      # Token blacklist data access
│   ├── HR/
│   │   ├── EmployeeRepository.cs            # Employee data access
│   │   ├── EmployeeTypeRepository.cs        # Employee type data access
│   │   ├── BranchRepository.cs              # Branch data access
│   │   └── ...
│   └── SystemAdmin/
│       ├── CountryRepository.cs             # Country data access
│       ├── CompanyRepository.cs             # Company data access
│       ├── CurrencyRepository.cs            # Currency data access
│       ├── UsersRepository.cs               # User data access
│       ├── MenuRepository.cs                # Menu data access
│       ├── ModuleRepository.cs              # Module data access
│       ├── GroupRepository.cs               # Group data access
│       ├── GroupMemberRepository.cs         # Group member data access
│       ├── GroupPermissionRepository.cs     # Group permission data access
│       ├── SystemSettingsRepository.cs      # System settings data access
│       ├── QueryAnalyzerRepository.cs       # Query analyzer data access
│       ├── StatusRepository.cs              # Status data access
│       ├── WFActionRepository.cs            # Workflow action data access
│       ├── WorkFlowSettingsRepository.cs    # Workflow settings data access
│       ├── AccessControlRepository.cs       # Access control data access
│       ├── AccessRestrictionRepository.cs   # Access restriction data access
│       └── ...
├── CRM/
│   ├── CRMApplicationRepository.cs          # CRM application data access
│   ├── CRMCourseRepository.cs               # Course data access
│   ├── CRMMonthRepository.cs                # Month data access
│   └── CRMYearRepository.cs                 # Year data access
├── DMS/
│   ├── DmsDocumentRepository.cs             # Document data access
│   ├── DmsdocumentAccessLogRepository.cs    # Access log data access
│   ├── DmsdocumentFolderRepository.cs       # Folder data access
│   ├── DmsdocumentTagRepository.cs          # Tag data access
│   ├── DmsdocumentTagMapRepository.cs       # Tag map data access
│   ├── DmsdocumentTypeRepository.cs         # Document type data access
│   └── DmsdocumentVersionRepository.cs      # Version data access
├── RepositoryBase.cs                         # Generic repository base class
├── RepositoryManager.cs                      # Repository aggregator
└── bdDevCRM.Repositories.csproj             # Project file
```

**Purpose**: Implements data access logic, executes database queries, manages transactions.

**Key Features**:
- Generic CRUD operations
- Custom queries with LINQ
- Raw SQL execution
- Transaction management
- Grid data retrieval

---

## 6. bdDevCRM.RepositoriesContracts (Repository Interfaces)

```
bdDevCRM.RepositoriesContracts/
├── Core/
│   ├── Authentication/
│   │   ├── IAuthenticationRepository.cs     # Auth repository interface
│   │   └── ITokenBlacklistRepository.cs     # Token blacklist interface
│   ├── HR/
│   │   ├── IEmployeeRepository.cs           # Employee repository interface
│   │   ├── IEmployeeTypeRepository.cs       # Employee type interface
│   │   ├── IBranchRepository.cs             # Branch repository interface
│   │   └── ...
│   └── SystemAdmin/
│       ├── ICountryRepository.cs            # Country repository interface
│       ├── ICompanyRepository.cs            # Company repository interface
│       ├── ICurrencyRepository.cs           # Currency repository interface
│       ├── IUsersRepository.cs              # User repository interface
│       ├── IMenuRepository.cs               # Menu repository interface
│       ├── IModuleRepository.cs             # Module repository interface
│       ├── IGroupRepository.cs              # Group repository interface
│       ├── IGroupMemberRepository.cs        # Group member interface
│       ├── IGroupPermissionRepository.cs    # Group permission interface
│       ├── ISystemSettingsRepository.cs     # Settings repository interface
│       ├── IQueryAnalyzerRepository.cs      # Query analyzer interface
│       ├── IStatusRepository.cs             # Status repository interface
│       ├── IWFActionRepository.cs           # Workflow action interface
│       ├── IWorkFlowSettingsRepository.cs   # Workflow settings interface
│       ├── IAccessControlRepository.cs      # Access control interface
│       ├── IAccessRestrictionRepository.cs  # Access restriction interface
│       └── ...
├── CRM/
│   ├── ICRMCourseRepository.cs              # Course repository interface
│   ├── ICRMMonthRepository.cs               # Month repository interface
│   └── ICRMYearRepository.cs                # Year repository interface
├── DMS/
│   ├── IDmsdocumentRepository.cs            # Document repository interface
│   ├── IDmsdocumentAccessLogRepository.cs   # Access log repository interface
│   ├── IDmsdocumentFolderRepository.cs      # Folder repository interface
│   ├── IDmsdocumentTagRepository.cs         # Tag repository interface
│   ├── IDmsdocumentTagMapRepository.cs      # Tag map repository interface
│   ├── IDmsdocumentTypeRepository.cs        # Document type interface
│   └── IDmsdocumentVersionRepository.cs     # Version repository interface
├── ILoggerManager.cs                         # Logger interface
├── IRepositoryBase.cs                        # Generic repository interface
├── IRepositoryManager.cs                     # Repository manager interface
└── bdDevCRM.RepositoriesContracts.csproj    # Project file
```

**Purpose**: Defines contracts for all repositories.

---

## 7. bdDevCRM.RepositoryDtos (Repository DTOs)

```
bdDevCRM.RepositoryDtos/
├── Core/
│   ├── HR/
│   │   └── (HR-specific DTOs)
│   └── SystemAdmin/
│       └── (Admin-specific DTOs)
├── CRM/
│   ├── CountryRepositoryDto.cs              # Country DTO
│   └── InstituteTypeRepositoryDto.cs        # Institute type DTO
├── DMS/
│   └── DMSRepositoryDto.cs                  # DMS DTO
└── bdDevCRM.RepositoryDtos.csproj           # Project file
```

**Purpose**: Contains DTOs specifically used for repository operations.

---

## 8. bdDevCRM.Entities (Domain Layer)

```
bdDevCRM.Entities/
├── Entities/
│   ├── Core/
│   │   ├── Country.cs                       # Country entity
│   │   ├── Company.cs                       # Company entity
│   │   ├── Users.cs                         # User entity
│   │   ├── Employee.cs                      # Employee entity
│   │   ├── Branch.cs                        # Branch entity
│   │   ├── Menu.cs                          # Menu entity
│   │   ├── Module.cs                        # Module entity
│   │   ├── Group.cs                         # Group entity
│   │   ├── Status.cs                        # Status entity
│   │   ├── Currency.cs                      # Currency entity
│   │   └── ...
│   ├── System/
│   │   ├── SystemSettings.cs                # System settings entity
│   │   ├── TokenBlacklist.cs                # Token blacklist entity
│   │   └── ...
│   ├── CRM/
│   │   ├── CRMInstituteType.cs              # Institute type entity
│   │   ├── Crminstitute.cs                  # Institute entity
│   │   ├── Crmcourse.cs                     # Course entity
│   │   ├── CrmcourseIntake.cs               # Course intake entity
│   │   ├── CrmapplicantCourseDetials.cs     # Applicant course details
│   │   ├── Crmmonth.cs                      # Month entity
│   │   └── Crmyear.cs                       # Year entity
│   └── DMS/
│       ├── DMSDocument.cs                   # Document entity
│       ├── DMSDocumentType.cs               # Document type entity
│       ├── DMSDocumentFolder.cs             # Folder entity
│       ├── DMSDocumentTag.cs                # Tag entity
│       ├── DMSDocumentVersion.cs            # Version entity
│       ├── DMSDocumentAccessLog.cs          # Access log entity
│       └── ...
├── Exceptions/
│   ├── BaseException/
│   │   └── BaseException.cs                 # Base exception class
│   ├── Company/
│   │   └── (Company-specific exceptions)
│   ├── AccessDeniedException.cs             # Access denied exception
│   ├── CollectionByIdsBadRequestException.cs # Collection exception
│   ├── CommonBadReuqestException.cs         # Bad request exception
│   ├── DuplicateRecordException.cs          # Duplicate record exception
│   ├── FileSizeExceededException.cs         # File size exception
│   ├── GenericListNotFoundException.cs      # List not found exception
│   ├── GenericNotFoundException.cs          # Not found exception
│   ├── IdMismatchBadRequestException.cs     # ID mismatch exception
│   ├── IdParametersBadRequestException.cs   # ID parameter exception
│   ├── InvalidCreateOperationException.cs   # Invalid create exception
│   ├── JWTSecurityException.cs              # JWT exception
│   ├── NullModelBadRequestException.cs      # Null model exception
│   ├── UnauthorizedAccessException.cs       # Unauthorized exception
│   └── UsernamePasswordMismatchException.cs # Login exception
├── ExceptionEntities/
│   └── ErrorDetails.cs                      # Error details model
├── Token/
│   └── TokenResponse.cs                     # Token response model
├── CRMGrid/
│   ├── AuthProviders/
│   │   └── (Authentication providers)
│   ├── Common/
│   │   ├── Helper/                          # Helper classes
│   │   ├── Json/                            # JSON utilities
│   │   └── Message/                         # Message utilities
│   ├── CustomData/
│   │   └── (Custom data handlers)
│   ├── FileConverter/
│   │   └── (File conversion utilities)
│   ├── GRID/
│   │   ├── GridEntity.cs                    # Grid entity model
│   │   ├── GridColumns.cs                   # Grid column definition
│   │   ├── CRMGridDataSource.cs             # Grid data source
│   │   └── ...
│   ├── Linq/
│   │   └── (LINQ utilities)
│   ├── Model/
│   │   └── (Grid models)
│   ├── Properties/
│   │   └── AssemblyInfo.cs
│   ├── Upload/
│   │   └── (Upload utilities)
│   ├── AzExportExcelToPdf.cs                # Excel to PDF export
│   ├── AzExportRptToPdf.cs                  # Report to PDF export
│   ├── AzExportToExcel.cs                   # Excel export
│   ├── AzFilter.cs                          # Filter utilities
│   ├── BulkInsert.cs                        # Bulk insert utility
│   ├── CommonConnection.cs                  # Connection utilities
│   ├── CommonReportParam.cs                 # Report parameters
│   ├── DatabaseType.cs                      # Database type enum
│   ├── DateDifference.cs                    # Date difference utility
│   ├── DateFormatter.cs                     # Date formatter
│   ├── DateTimeFormatter.cs                 # DateTime formatter
│   ├── DirectoryBrowser.cs                  # Directory browser
│   ├── Export.cs                            # Export utilities
│   ├── Folder.cs                            # Folder utilities
│   ├── GridDataBuilder.cs                   # Grid data builder
│   ├── GridOptions.cs                       # Grid options
│   ├── JsonLayer.cs                         # JSON layer
│   ├── KendoDataSource.cs                   # Kendo data source
│   ├── MyClone.cs                           # Clone utility
│   ├── Operation.cs                         # Operation utilities
│   ├── QueryBuilder.cs                      # Query builder
│   ├── ThumbnailCreator.cs                  # Thumbnail creator
│   ├── UniqCodeGeneratorFromIDPolicy.cs     # Unique code generator
│   └── AzUtilities.csproj                   # Utilities project
└── bdDevCRM.Entities.csproj                 # Project file
```

**Purpose**: Contains domain entities, exceptions, and grid utilities.

---

## 9. bdDevCRM.Sql (Database Context)

```
bdDevCRM.Sql/
├── Context/
│   └── CRMContext.cs                        # EF Core DbContext
├── Migrations/                              # EF Core migrations (generated)
└── bdDevCRM.Sql.csproj                      # Project file
```

**Purpose**: Database context configuration and migrations.

---

## 10. bdDevCRM.Shared (Shared Resources)

```
bdDevCRM.Shared/
├── DataTransferObjects/
│   ├── Authentication/
│   │   ├── LoginDto.cs                      # Login DTO
│   │   ├── TokenDto.cs                      # Token DTO
│   │   └── ...
│   ├── Core/
│   │   ├── HR/
│   │   │   ├── EmployeeDto.cs               # Employee DTO
│   │   │   ├── BranchDto.cs                 # Branch DTO
│   │   │   └── ...
│   │   └── SystemAdmin/
│   │       ├── CountryDto.cs                # Country DTO
│   │       ├── CompanyDto.cs                # Company DTO
│   │       ├── UserDto.cs                   # User DTO
│   │       ├── MenuDto.cs                   # Menu DTO
│   │       ├── ModuleDto.cs                 # Module DTO
│   │       ├── GroupDto.cs                  # Group DTO
│   │       └── ...
│   ├── CRM/
│   │   ├── InstituteDto.cs                  # Institute DTO
│   │   ├── CourseDto.cs                     # Course DTO
│   │   ├── ApplicationDto.cs                # Application DTO
│   │   └── ...
│   ├── DMS/
│   │   ├── DocumentDto.cs                   # Document DTO
│   │   ├── FolderDto.cs                     # Folder DTO
│   │   ├── TagDto.cs                        # Tag DTO
│   │   └── ...
│   ├── Common/
│   │   ├── PaginationDto.cs                 # Pagination DTO
│   │   └── ...
│   ├── CompanyDto.cs                        # Company DTO
│   └── CountryDto.cs                        # Country DTO
├── ApiResponse/
│   ├── ApiException.cs                      # API exception
│   ├── ApiResponse.cs                       # API response model
│   ├── ApiValidationErrorResponse.cs        # Validation error response
│   └── ResponseHelper.cs                    # Response helper
└── bdDevCRM.Shared.csproj                   # Project file
```

**Purpose**: Contains DTOs and API response models shared across layers.

---

## 11. bdDevCRM.Utilities (Helper Functions)

```
bdDevCRM.Utilities/
├── Common/
│   ├── CommonHelper.cs                      # Common helper functions
│   ├── EncryptDecryptHelper.cs              # Encryption utilities
│   └── ValidationHelper.cs                  # Validation utilities
├── Constants/
│   ├── MessageConstants.cs                  # Message constants
│   ├── ModelValidationConstant.cs           # Validation constants
│   └── RouteConstants.cs                    # Route constants
├── KendoGrid/
│   ├── CRMFilter.cs                         # Kendo grid filter
│   ├── GridOptions.cs                       # Grid options
│   └── GridResult.cs                        # Grid result
├── OthersLibrary/
│   ├── FileUploadHelper.cs                  # File upload utility
│   └── MyMapper.cs                          # Object mapper
└── bdDevCRM.Utilities.csproj                # Project file
```

**Purpose**: Utility functions, constants, and helper classes.

---

## 12. bdDevCRM.LoggerSevice (Logging)

```
bdDevCRM.LoggerSevice/
├── LoggerManager.cs                         # NLog implementation
└── bdDevCRM.LoggerSevice.csproj             # Project file
```

**Purpose**: Centralized logging service using NLog.

---

## File Statistics

### Overall Statistics
- **Total Projects**: 13
- **Total C# Files**: ~426
- **Total Controllers**: 23
- **Total Entities**: 40+
- **Total Services**: 30+
- **Total Repositories**: 30+

### File Count by Project
| Project | Approx. Files |
|---------|--------------|
| bdDevCRM.Api | 20+ |
| bdDevCRM.Presentation | 30+ |
| bdDevCRM.Service | 40+ |
| bdDevCRM.ServiceContract | 40+ |
| bdDevCRM.Repositories | 40+ |
| bdDevCRM.RepositoriesContracts | 40+ |
| bdDevCRM.Entities | 100+ |
| bdDevCRM.Shared | 50+ |
| bdDevCRM.Utilities | 15+ |
| bdDevCRM.RepositoryDtos | 10+ |
| bdDevCRM.Sql | 5+ |
| bdDevCRM.LoggerService | 2 |

### Lines of Code Estimate
- **Total LOC**: ~50,000+ lines (estimated)
- **Largest Project**: bdDevCRM.Entities (~15,000+ LOC)
- **Smallest Project**: bdDevCRM.LoggerService (~200 LOC)

---

## Technology-Specific Files

### Configuration Files
- `appsettings.json` - Application configuration
- `appsettings.Development.json` - Development environment config
- `nlog.config` - NLog configuration
- `launchSettings.json` - Launch profiles

### Project Files
- `*.csproj` - C# project files (13 total)
- `bdDevCRM.BackEnd.sln` - Visual Studio solution file

### Git Files
- `.gitattributes` - Git attributes
- `.gitignore` - Git ignore patterns

### Documentation Files
- `README.md` - Project readme
- `ARCHITECTURE.md` - Architecture documentation
- `FILE_STRUCTURE.md` - This file

---

## Module-Specific File Organization

### Authentication Module Files
```
Controllers/Authentication/
Services/Authentication/
Repositories/Core/Authentication/
Entities/Token/
DTOs/Authentication/
```

### CRM Module Files
```
Controllers/CRM/
Services/CRM/
Repositories/CRM/
Entities/Entities/CRM/
DTOs/CRM/
```

### HR Module Files
```
Controllers/Core/HR/
Services/Core/HR/
Repositories/Core/HR/
Entities/Entities/Core/ (Employee, Branch, etc.)
DTOs/Core/HR/
```

### DMS Module Files
```
Controllers/DMS/
Services/DMS/
Repositories/DMS/
Entities/Entities/DMS/
DTOs/DMS/
```

### System Admin Module Files
```
Controllers/Core/SystemAdmin/
Services/Core/SystemAdmin/
Repositories/Core/SystemAdmin/
Entities/Entities/Core/ (Users, Groups, Menus, etc.)
DTOs/Core/SystemAdmin/
```

---

## Naming Conventions

### File Naming Patterns
- **Controllers**: `{Entity}Controller.cs` (e.g., `EmployeeController.cs`)
- **Services**: `{Entity}Service.cs` (e.g., `EmployeeService.cs`)
- **Service Interfaces**: `I{Entity}Service.cs` (e.g., `IEmployeeService.cs`)
- **Repositories**: `{Entity}Repository.cs` (e.g., `EmployeeRepository.cs`)
- **Repository Interfaces**: `I{Entity}Repository.cs` (e.g., `IEmployeeRepository.cs`)
- **Entities**: `{Entity}.cs` (e.g., `Employee.cs`)
- **DTOs**: `{Entity}Dto.cs` (e.g., `EmployeeDto.cs`)
- **Exceptions**: `{Purpose}Exception.cs` (e.g., `NotFoundException.cs`)

### Namespace Conventions
- Controllers: `bdDevCRM.Presentation.Controllers.{Module}`
- Services: `bdDevCRM.Service.{Module}`
- Repositories: `bdDevCRM.Repositories.{Module}`
- Entities: `bdDevCRM.Entities.Entities.{Module}`
- DTOs: `bdDevCRM.Shared.DataTransferObjects.{Module}`

---

## Important File Locations

### Entry Point
- **Main Entry**: `/bdDevCRM.Api/Program.cs`

### Configuration
- **App Config**: `/bdDevCRM.Api/appsettings.json`
- **Log Config**: `/bdDevCRM.Api/nlog.config`

### Database
- **DbContext**: `/bdDevCRM.Sql/Context/CRMContext.cs`

### Core Infrastructure
- **Repository Base**: `/bdDevCRM.Repositories/RepositoryBase.cs`
- **Repository Manager**: `/bdDevCRM.Repositories/RepositoryManager.cs`
- **Service Manager**: `/bdDevCRM.Service/ServiceManager.cs`

### Middleware
- **Exception Handling**: `/bdDevCRM.Api/Middleware/ExceptionMiddleware.cs`
- **Audit**: `/bdDevCRM.Api/Middleware/AuditMiddleware.cs`

### Extensions
- **Service Configuration**: `/bdDevCRM.Api/Extensions/ServiceExtensions.cs`

---

## File Upload Structure

```
wwwroot/
└── Uploads/
    ├── CRMInstitute/
    │   └── {InstituteId}/
    │       ├── Logo/
    │       │   └── {logo-files}
    │       └── Prospectus/
    │           └── {prospectus-files}
    ├── Documents/
    │   └── {document-files}
    └── {other-modules}/
```

---

## Build Artifacts (Excluded from Git)

```
{Project}/
├── bin/              # Build output
├── obj/              # Intermediate files
└── .vs/              # Visual Studio cache
```

These directories are excluded via `.gitignore`.

---

**Document Version**: 1.0  
**Last Updated**: 2025-10-21  
**Total Files Documented**: 400+

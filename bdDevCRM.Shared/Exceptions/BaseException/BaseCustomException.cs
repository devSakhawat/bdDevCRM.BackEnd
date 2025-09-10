//using System.Net;

//namespace bdDevCRM.Shared.Exceptions.BaseException;

//// ================ BASE CUSTOM EXCEPTION ================
//public abstract class BaseCustomException : Exception
//{
//    public abstract int StatusCode { get; }
//    public abstract string ErrorCode { get; }
//    public virtual string UserFriendlyMessage => Message;
//    public Dictionary<string, object> AdditionalData { get; } = new();

//    protected BaseCustomException(string message) : base(message) { }
//    protected BaseCustomException(string message, Exception innerException) : base(message, innerException) { }
//}

//// ================ CATEGORY BASE EXCEPTIONS ================

//// Business Logic Exceptions (400-499)
//public abstract class BusinessException : BaseCustomException
//{
//    public override int StatusCode => 400;
//    protected BusinessException(string message) : base(message) { }
//    protected BusinessException(string message, Exception innerException) : base(message, innerException) { }
//}

//// Domain Exceptions (422)
//public abstract class DomainException : BaseCustomException
//{
//    public override int StatusCode => 422;
//    protected DomainException(string message) : base(message) { }
//    protected DomainException(string message, Exception innerException) : base(message, innerException) { }
//}

//// Infrastructure Exceptions (500+)
//public abstract class InfrastructureException : BaseCustomException
//{
//    public override int StatusCode => 500;
//    protected InfrastructureException(string message) : base(message) { }
//    protected InfrastructureException(string message, Exception innerException) : base(message, innerException) { }
//}

//// ================ HTTP STATUS BASE EXCEPTIONS ================

//// 400 Bad Request
//public abstract class BadRequestException : BusinessException
//{
//    public override int StatusCode => (int)HttpStatusCode.BadRequest; // 400
//    public override string ErrorCode => "BAD_REQUEST";
//    protected BadRequestException(string message) : base(message) { }
//    protected BadRequestException(string message, Exception innerException) : base(message, innerException) { }
//}

//// 401 Unauthorized
//public abstract class UnauthorizedException : BaseCustomException
//{
//    public override int StatusCode => (int)HttpStatusCode.Unauthorized; // 401
//    public override string ErrorCode => "UNAUTHORIZED";
//    protected UnauthorizedException(string message) : base(message) { }
//    protected UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
//}

//// 403 Forbidden
//public abstract class ForbiddenAccessException : BaseCustomException
//{
//    public override int StatusCode => (int)HttpStatusCode.Forbidden; // 403
//    public override string ErrorCode => "FORBIDDEN";
//    protected ForbiddenAccessException(string message) : base(message) { }
//    protected ForbiddenAccessException(string message, Exception innerException) : base(message, innerException) { }
//}

//// 404 Not Found
//public abstract class NotFoundException : BaseCustomException
//{
//    public override int StatusCode => (int)HttpStatusCode.NotFound; // 404
//    public override string ErrorCode => "NOT_FOUND";
//    protected NotFoundException(string message) : base(message) { }
//    protected NotFoundException(string message, Exception innerException) : base(message, innerException) { }
//}

//// 409 Conflict
//public abstract class ConflictException : BaseCustomException
//{
//    public override int StatusCode => (int)HttpStatusCode.Conflict; // 409
//    public override string ErrorCode => "CONFLICT";
//    protected ConflictException(string message) : base(message) { }
//    protected ConflictException(string message, Exception innerException) : base(message, innerException) { }
//}

//// 503 Service Unavailable
//public abstract class ServiceUnavailableException : InfrastructureException
//{
//    public override int StatusCode => (int)HttpStatusCode.ServiceUnavailable; // 503
//    public override string ErrorCode => "SERVICE_UNAVAILABLE";
//    protected ServiceUnavailableException(string message) : base(message) { }
//    protected ServiceUnavailableException(string message, Exception innerException) : base(message, innerException) { }
//}

//// ================ SPECIFIC BUSINESS EXCEPTIONS ================

//// Authentication & Authorization
//public sealed class UsernamePasswordMismatchException : UnauthorizedException
//{
//    public override string ErrorCode => "USERNAME_PASSWORD_MISMATCH";
//    public UsernamePasswordMismatchException() : base("The username or password is incorrect. Please try again.") { }
//    public UsernamePasswordMismatchException(string message) : base(message) { }
//}

//public sealed class GenericUnauthorizedException : UnauthorizedException
//{
//    public override string ErrorCode => "GENERIC_UNAUTHORIZED";
//    public GenericUnauthorizedException(string message) : base(message) { }
//}

//public sealed class UnauthorizedAccessCRMException : UnauthorizedException
//{
//    public override string ErrorCode => "UNAUTHORIZED_ACCESS_CRM";
//    public UnauthorizedAccessCRMException(string message) : base(message) { }
//}

//// Bad Request Exceptions
//public sealed class GenericBadRequestException : BadRequestException
//{
//    public override string ErrorCode => "GENERIC_BAD_REQUEST";
//    public GenericBadRequestException(string message) : base(message) { }
//}

//public sealed class CommonBadRequestException : BadRequestException
//{
//    public override string ErrorCode => "COMMON_BAD_REQUEST";
//    public CommonBadRequestException(string message) : base(message) { }
//}

//public sealed class IdParametersBadRequestException : BadRequestException
//{
//    public override string ErrorCode => "INVALID_ID_PARAMETERS";
//    public IdParametersBadRequestException() : base("Invalid ID parameter provided.") { }
//    public IdParametersBadRequestException(string message) : base(message) { }
//}

//public sealed class NullModelBadRequestException : BadRequestException
//{
//    public override string ErrorCode => "NULL_MODEL";
//    public NullModelBadRequestException(string modelName) : base($"The {modelName} model cannot be null.") { }
//}

//public sealed class IdMismatchBadRequestException : BadRequestException
//{
//    public override string ErrorCode => "ID_MISMATCH";
//    public IdMismatchBadRequestException(string providedId, string modelName) 
//        : base($"The provided ID {providedId} does not match the {modelName} ID.") { }
//}

//// Not Found Exceptions
//public sealed class GenericNotFoundException : NotFoundException
//{
//    public override string ErrorCode => "GENERIC_NOT_FOUND";
//    public GenericNotFoundException(string entityName, string propertyName, string value) 
//        : base($"The {entityName} with {propertyName}: {value} doesn't exist in the database.") { }
//}

//public sealed class CompanyNotFoundException : NotFoundException
//{
//    public override string ErrorCode => "COMPANY_NOT_FOUND";
//    public CompanyNotFoundException(int companyId) 
//        : base($"The company with id: {companyId} doesn't exist in the database.") { }
//}

//// Conflict Exceptions
//public sealed class GenericConflictException : ConflictException
//{
//    public override string ErrorCode => "GENERIC_CONFLICT";
//    public GenericConflictException(string message) : base(message) { }
//}

//public sealed class DuplicateRecordException : ConflictException
//{
//    public override string ErrorCode => "DUPLICATE_RECORD";
//    public DuplicateRecordException(string entityName, string propertyName) 
//        : base($"A {entityName} with the same {propertyName} already exists.") { }
//    public DuplicateRecordException(string message) : base(message) { }
//}

//// Domain/Business Logic Exceptions
//public sealed class InvalidCreateOperationException : DomainException
//{
//    public override string ErrorCode => "INVALID_CREATE_OPERATION";
//    public InvalidCreateOperationException(string message) : base(message) { }
//}

//public sealed class InvalidUpdateOperationException : DomainException
//{
//    public override string ErrorCode => "INVALID_UPDATE_OPERATION";
//    public InvalidUpdateOperationException(string message) : base(message) { }
//}

//// ================ SPECIFIC DOMAIN EXCEPTIONS ================

//// CRM Specific Exceptions
//public sealed class CrmApplicationNotFoundException : NotFoundException
//{
//    public override string ErrorCode => "CRM_APPLICATION_NOT_FOUND";
//    public CrmApplicationNotFoundException(int applicationId) 
//        : base($"CRM Application with ID {applicationId} not found.") { }
//}

//public sealed class CrmCourseNotFoundException : NotFoundException
//{
//    public override string ErrorCode => "CRM_COURSE_NOT_FOUND";
//    public CrmCourseNotFoundException(int courseId) 
//        : base($"CRM Course with ID {courseId} not found.") { }
//}

//public sealed class CrmInstituteNotFoundException : NotFoundException
//{
//    public override string ErrorCode => "CRM_INSTITUTE_NOT_FOUND";
//    public CrmInstituteNotFoundException(int instituteId) 
//        : base($"CRM Institute with ID {instituteId} not found.") { }
//}

//// User Management Exceptions
//public sealed class UserNotFoundException : NotFoundException
//{
//    public override string ErrorCode => "USER_NOT_FOUND";
//    public UserNotFoundException(int userId) 
//        : base($"User with ID {userId} not found.") { }
//}

//public sealed class UserSessionExpiredException : UnauthorizedException
//{
//    public override string ErrorCode => "USER_SESSION_EXPIRED";
//    public UserSessionExpiredException() : base("User session has expired. Please login again.") { }
//}

//// Workflow Exceptions
//public sealed class WorkflowStateNotFoundException : NotFoundException
//{
//    public override string ErrorCode => "WORKFLOW_STATE_NOT_FOUND";
//    public WorkflowStateNotFoundException(int stateId) 
//        : base($"Workflow state with ID {stateId} not found.") { }
//}

//public sealed class WorkflowActionNotFoundException : NotFoundException
//{
//    public override string ErrorCode => "WORKFLOW_ACTION_NOT_FOUND";
//    public WorkflowActionNotFoundException(int actionId) 
//        : base($"Workflow action with ID {actionId} not found.") { }
//}

//// Validation Exceptions
//public sealed class ValidationFailedException : BadRequestException
//{
//    public override string ErrorCode => "VALIDATION_FAILED";
//    public List<ValidationError> ValidationErrors { get; }
    
//    public ValidationFailedException(string message) : base(message) 
//    {
//        ValidationErrors = new List<ValidationError>();
//    }
    
//    public ValidationFailedException(string message, List<ValidationError> validationErrors) : base(message) 
//    {
//        ValidationErrors = validationErrors;
//    }
//}

//// Supporting Classes
//public class ValidationError
//{
//    public string Field { get; set; }
//    public string Message { get; set; }
//    public string ErrorCode { get; set; }
    
//    public ValidationError(string field, string message, string errorCode = null)
//    {
//        Field = field;
//        Message = message;
//        ErrorCode = errorCode ?? "VALIDATION_ERROR";
//    }
//}

//// Simple usage
//// throw new GenericBadRequestException("Invalid course ID provided"));

//// With additional data
//// var ex = new UserNotFoundException(userId);
//// ex.AdditionalData["attemptedAction"] = "update_profile";
//// ex.AdditionalData["timestamp"] = DateTime.UtcNow;
//// throw ex;

//// Validation with multiple errors
//// var validationErrors = new List<ValidationError>
//// {
////     new("Email", "Email is required"),
////     new("Password", "Password must be at least 8 characters")
//// };
//// throw new ValidationFailedException("Validation failed", validationErrors);
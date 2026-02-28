# Exception Handling উন্নতি - ব্যবহার নির্দেশিকা

## সমস্যা সমাধান সারসংক্ষেপ

### ✅ সমস্যা ১: Duplicate Middleware Files - সমাধান
Duplicate files ইতিমধ্যে delete করা হয়েছে। শুধুমাত্র `ExceptionMiddleware.cs` file ব্যবহার করা হচ্ছে।

### ✅ সমস্যা ২: BaseCustomException.cs Uncommented এবং বাস্তবায়ন
`BaseCustomException` class এখন active এবং নিম্নলিখিত features যুক্ত করা হয়েছে:
- `ErrorCode` property - Unique error tracking এর জন্য
- `AdditionalData` Dictionary - Context information store করার জন্য
- `WithData()` fluent method - সহজে data যোগ করার জন্য
- `UserFriendlyMessage` property - User-facing messages এর জন্য

### ✅ সমস্যা ৩: Enhanced Error Context - সমাধান
`GenericNotFoundException` এবং অন্যান্য exceptions এখন `AdditionalData` ব্যবহার করে rich context প্রদান করে।

## ব্যবহার উদাহরণ

### ১. BaseCustomException ব্যবহার করে নতুন Exception তৈরি

```csharp
using bdDevCRM.Shared.Exceptions.BaseException;

// Custom exception তৈরি করুন
public sealed class UserNotFoundException : NotFoundException
{
    public override string ErrorCode => "USER_NOT_FOUND";

    public UserNotFoundException(int userId)
        : base($"User with ID {userId} not found.")
    {
        // Additional context যোগ করুন
        this.WithData("UserId", userId)
            .WithData("Timestamp", DateTime.UtcNow);
    }
}
```

### ২. Exception throw করার সময় Additional Data যোগ করা

```csharp
// Service layer এ
public async Task<UsersDTO> GetUserByIdAsync(int userId)
{
    var user = await _repository.Users.GetByIdAsync(userId);

    if (user == null)
    {
        // Rich context সহ exception throw করুন
        throw new GenericNotFoundException("User", "UserId", userId.ToString())
            .WithData("RequestedBy", _currentUser.Id)
            .WithData("Module", "UserManagement")
            .WithData("Action", "GetUserById")
            .WithData("IpAddress", _httpContext.Connection.RemoteIpAddress?.ToString());
    }

    return _mapper.Map<UsersDTO>(user);
}
```

### ৩. Multiple Context Data যোগ করা

```csharp
public async Task UpdateUserAsync(int userId, UpdateUserDTO dto)
{
    var user = await _repository.Users.GetByIdAsync(userId);

    if (user == null)
    {
        var exception = new GenericNotFoundException("User", "UserId", userId.ToString());

        // Multiple context data
        exception.WithData("AttemptedUpdate", JsonSerializer.Serialize(dto))
                 .WithData("RequestedBy", _currentUser.Id)
                 .WithData("RequestTime", DateTime.UtcNow)
                 .WithData("ModuleName", "UserManagement");

        throw exception;
    }

    // Update logic...
}
```

### ৪. Custom Exception with Business Logic

```csharp
public sealed class InsufficientBalanceException : BadRequestException
{
    public override string ErrorCode => "INSUFFICIENT_BALANCE";

    public InsufficientBalanceException(decimal requiredAmount, decimal availableBalance)
        : base($"Insufficient balance. Required: {requiredAmount}, Available: {availableBalance}")
    {
        this.WithData("RequiredAmount", requiredAmount)
            .WithData("AvailableBalance", availableBalance)
            .WithData("Shortfall", requiredAmount - availableBalance);
    }
}

// Usage
if (account.Balance < paymentAmount)
{
    throw new InsufficientBalanceException(paymentAmount, account.Balance)
        .WithData("AccountId", account.Id)
        .WithData("TransactionId", transactionId);
}
```

## ExceptionMiddleware কিভাবে Handle করে

### Response Format (Development Mode)

যখন `BaseCustomException` throw হয়, ExceptionMiddleware automatically নিম্নলিখিত response তৈরি করে:

```json
{
  "statusCode": 404,
  "message": "User with UserId 123 was not found.",
  "isSuccess": false,
  "timestamp": "2024-02-28T10:30:00Z",
  "errorType": "RESOURCE_NOT_FOUND",
  "correlationId": "guid-123",
  "details": "Additional Context:\n{\n  \"entityName\": \"User\",\n  \"propertyName\": \"UserId\",\n  \"searchValue\": \"123\",\n  \"requestedBy\": 456,\n  \"module\": \"UserManagement\"\n}\n\nStack Trace:\n[stack trace here]"
}
```

### Response Format (Production Mode)

Production এ `AdditionalData` এবং `Stack Trace` hide থাকে:

```json
{
  "statusCode": 404,
  "message": "User with UserId 123 was not found.",
  "isSuccess": false,
  "timestamp": "2024-02-28T10:30:00Z",
  "errorType": "RESOURCE_NOT_FOUND",
  "correlationId": "guid-123"
}
```

## সব Base Exception Classes

এখন সব base exception classes `BaseCustomException` থেকে inherit করে:

### ১. BadRequestException (400)
```csharp
public class BadRequestException : BaseCustomException
{
    public override int StatusCode => 400;
    public override string ErrorCode => "BAD_REQUEST";
}
```

### ২. UnauthorizedException (401)
```csharp
public class UnauthorizedException : BaseCustomException
{
    public override int StatusCode => 401;
    public override string ErrorCode => "UNAUTHORIZED";
}
```

### ৩. ForbiddenAccessException (403)
```csharp
public abstract class ForbiddenAccessException : BaseCustomException
{
    public override int StatusCode => 403;
    public override string ErrorCode => "FORBIDDEN";
}
```

### ৪. NotFoundException (404)
```csharp
public abstract class NotFoundException : BaseCustomException
{
    public override int StatusCode => 404;
    public override string ErrorCode => "NOT_FOUND";
}
```

### ৫. ConflictException (409)
```csharp
public abstract class ConflictException : BaseCustomException
{
    public override int StatusCode => 409;
    public override string ErrorCode => "CONFLICT";
}
```

### ৬. ServiceUnavailableException (503)
```csharp
public abstract class ServiceUnavailableException : BaseCustomException
{
    public override int StatusCode => 503;
    public override string ErrorCode => "SERVICE_UNAVAILABLE";
}
```

## Migration Guide - পুরাতন Code থেকে নতুন Code

### আগে (Without AdditionalData):
```csharp
throw new GenericNotFoundException("User", "UserId", userId.ToString());
```

### এখন (With AdditionalData):
```csharp
throw new GenericNotFoundException("User", "UserId", userId.ToString())
    .WithData("RequestedBy", currentUser.Id)
    .WithData("Action", "GetUserProfile");
```

### পুরাতন code এখনো কাজ করবে!
Backward compatibility রাখা হয়েছে। পুরাতন code change না করলেও কাজ করবে।

## Logging এ কিভাবে দেখা যায়

ExceptionMiddleware automatically log করে:

```
[Error] [guid-123] GenericNotFoundException: User with UserId 123 was not found.
```

## বিশেষ সুবিধা

### ১. Error Tracking
`ErrorCode` ব্যবহার করে specific error track করা যায়:
- RESOURCE_NOT_FOUND
- USER_NOT_FOUND
- INSUFFICIENT_BALANCE
- DUPLICATE_RECORD

### ২. Context Information
`AdditionalData` থেকে debug করা সহজ:
- কে request করেছে
- কখন request হয়েছে
- কোন module থেকে
- কোন action attempt করা হয়েছে

### ৩. Security
Production mode এ sensitive data hide থাকে কিন্তু development এ সব দেখা যায়।

### ৪. Fluent API
`.WithData()` method chain করা যায়:
```csharp
exception.WithData("Key1", value1)
         .WithData("Key2", value2)
         .WithData("Key3", value3);
```

## সতর্কতা

1. **Sensitive Data**: Password, token, credit card number ইত্যাদি `AdditionalData` তে রাখবেন না
2. **Large Objects**: বড় object serialize করে রাখবেন না, শুধু ID বা key রাখুন
3. **PII (Personal Identifiable Information)**: User email, phone ইত্যাদি সাবধানে handle করুন

## সারসংক্ষেপ

✅ `BaseCustomException` active এবং ব্যবহারযোগ্য
✅ সব base exceptions updated
✅ `GenericNotFoundException` enhanced with context
✅ `ExceptionMiddleware` updated to handle new structure
✅ Backward compatibility maintained
✅ Build successful (no errors, only warnings)

এখন আপনি নতুন exception handling system ব্যবহার করতে পারবেন!

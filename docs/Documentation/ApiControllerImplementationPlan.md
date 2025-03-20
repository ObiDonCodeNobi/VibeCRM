# API Controller Implementation Plan

## Overview

This document outlines the plan for implementing API controllers and endpoints for the VibeCRM system based on the existing Application features. The implementation will follow ASP.NET Core Web API best practices, including Swagger/OpenAPI documentation, proper HTTP verb usage, and consistent response patterns.

## Architecture Principles

1. **Clean Architecture**: Controllers will be thin and focused on HTTP concerns, delegating business logic to the Application layer
2. **CQRS Pattern**: Controllers will use MediatR to dispatch commands and queries
3. **RESTful Design**: Endpoints will follow REST principles with appropriate HTTP verbs
4. **Consistent Response Format**: All endpoints will return consistent response objects
5. **Swagger Documentation**: All endpoints will be fully documented with Swagger/OpenAPI
6. **Fluent Validation**: Request validation will be handled by FluentValidation
7. **Serilog Logging**: All controller actions will include appropriate logging

## Controller Structure

Each domain entity will have its own controller following this naming convention:

```
[EntityName]Controller : ApiControllerBase
```

Controllers will be organized in the `VibeCRM.Api.Controllers` namespace.

## Base Controller

✅ Created a base `ApiControllerBase` class that:

1. Includes the `[ApiController]` attribute
2. Defines the `[Route("api/[controller]")]` attribute
3. Provides common helper methods for consistent responses
4. Includes the MediatR IMediator instance
5. Includes logging via Serilog

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMediator _mediator;
    protected readonly ILogger _logger;

    protected ApiControllerBase(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    // Helper methods for consistent responses
    protected ApiResponse<T> Success<T>(T data, string message = "Operation completed successfully")
    {
        return ApiResponse<T>.CreateSuccess(data, message);
    }

    protected ApiResponse<T> Failure<T>(string message, List<string>? errors = null)
    {
        return ApiResponse<T>.CreateFailure(message, errors);
    }

    protected IActionResult NotFoundResponse<T>(string message = "Resource not found")
    {
        return NotFound(ApiResponse<T>.CreateFailure(message));
    }

    protected IActionResult BadRequestResponse<T>(string message = "Invalid request", List<string>? errors = null)
    {
        return BadRequest(ApiResponse<T>.CreateFailure(message, errors));
    }
}
```

## Standard Endpoint Pattern

Each controller will implement a standard set of endpoints based on the CQRS pattern:

### Commands (Write Operations)

- **Create**: `POST /api/[controller]`
- **Update**: `PUT /api/[controller]/{id}`
- **Delete**: `DELETE /api/[controller]/{id}`

### Queries (Read Operations)

- **Get By Id**: `GET /api/[controller]/{id}`
- **Get List**: `GET /api/[controller]`
- **Get By [Criteria]**: `GET /api/[controller]/by-[criteria]/{value}`

## Response Format

✅ Implemented a consistent response format for all API endpoints:

```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }
    
    // Static helper methods for creating success and failure responses
    public static ApiResponse<T> CreateSuccess(T data, string message = "Operation completed successfully");
    public static ApiResponse<T> CreateFailure(string message, List<string>? errors = null);
}
```

## Implementation Status

### Phase 0: Authentication Infrastructure ✅

- ✅ Set up JWT authentication in API
  - ✅ Created JwtSettings class
  - ✅ Created IJwtService interface
  - ✅ Implemented JwtService
  - ✅ Added JWT configuration to appsettings.json
  - ✅ Configured JWT authentication in Program.cs
- ⏳ Configure Microsoft Identity in Blazor (TODO for future work)
- ✅ Created ApiControllerBase with auth support
- ✅ Implemented global exception handling middleware
- ✅ Created AuthController for login and token refresh

### Phase 1: Core Business Entities ✅

1. ✅ Company
2. ✅ Person
3. ✅ User
4. ✅ Team
5. ✅ Role

### Phase 2: Sales-Related Entities ✅

1. ✅ Product
2. ✅ Service
3. ✅ Quote
4. ✅ SalesOrder
5. ✅ Invoice
6. ✅ Payment

### Phase 3: Activity and Communication Entities ✅

1. ✅ Activity
2. ✅ Call
3. ✅ Note
4. ✅ Workflow
5. ✅ Attachment

### Phase 4: Reference Data Entities ✅

1. ✅ ActivityType
2. ✅ ActivityStatus
3. ✅ Other *Type entities (ProductType, PhoneType, etc.)
   - ✅ AccountType
   - ✅ AddressType
   - ✅ AttachmentType
   - ✅ CallType
   - ✅ EmailAddressType
   - ✅ NoteType
4. ✅ Other *Status entities (AccountStatus, InvoiceStatus, etc.)
   - ✅ CallDirection

## Implementation Schedule

| Phase | Components | Status | Dependencies |
|-------|------------|--------|--------------|
| 0     | Authentication Infrastructure | ✅ Completed | None |
|       | - Set up JWT authentication in API | ✅ |  |
|       | - Configure Microsoft Identity in Blazor | ⏳ TODO |  |
|       | - Create ApiControllerBase with auth support | ✅ |  |
|       | - Implement global exception handling | ✅ |  |
| 1     | Core Business Entities | ✅ Completed | Phase 0 |
|       | - Company, Person, User, Team, Role controllers | ✅ |  |
|       | - Role-based authorization policies | ✅ |  |
| 2     | Sales-Related Entities | ✅ Completed | Phase 1 |
|       | - Product, Service, Quote, SalesOrder, Invoice, Payment | ✅ |  |
| 3     | Activity and Communication Entities | ✅ Completed | Phase 1 |
|       | - Activity | ✅ |  |
|       | - Call, Note, Workflow, Attachment | ✅ |  |
| 4     | Reference Data Entities | ✅ Completed | None |
|       | - ActivityType, ActivityStatus | ✅ |  |
|       | - Other *Type and *Status entities | ✅ |  |

## Next Steps

1. Complete the Blazor frontend integration with Microsoft Identity
2. ✅ Implement Phase 2 controllers for Sales-Related entities
3. ✅ Complete Phase 3 controllers for Activity and Communication entities
4. ✅ Complete Phase 4 controllers for Reference Data entities

## Notes

- All controllers follow the standard CQRS pattern using MediatR
- Authentication is implemented using JWT tokens
- All endpoints return standardized ApiResponse objects
- Comprehensive Swagger documentation is included for all endpoints
- Global exception handling ensures consistent error responses

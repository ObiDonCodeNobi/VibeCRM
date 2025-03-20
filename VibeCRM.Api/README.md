# VibeCRM API Layer

This directory contains the API layer of VibeCRM, implementing ASP.NET Core Web API controllers that serve as the entry point for client applications. The API layer follows RESTful principles and uses Swagger/OpenAPI for documentation.

## Directory Structure

### Controllers

Organized by domain entity, each controller:

- Inherits from ApiControllerBase
- Uses MediatR to dispatch commands and queries
- Returns standardized ApiResponse objects
- Implements proper HTTP status codes
- Includes comprehensive XML documentation for Swagger

### Middleware

Contains custom middleware components:

- **ExceptionHandlingMiddleware.cs**: Global exception handling and logging
- **RequestLoggingMiddleware.cs**: Logging of all incoming requests
- **JwtMiddleware.cs**: JWT token validation and user context

### Filters

Contains action filters and authorization policies:

- **ApiExceptionFilterAttribute.cs**: Handles and formats API exceptions
- **ValidationFilterAttribute.cs**: Ensures model state validity

### Extensions

Contains extension methods for service configuration:

- **ServiceExtensions.cs**: Configures API-specific services
- **SwaggerExtensions.cs**: Configures Swagger/OpenAPI documentation
- **CorsExtensions.cs**: Configures CORS policies

## Implementation Details

### RESTful API Design

The API follows RESTful principles:

1. **Resource-based URLs**: /api/[controller]
2. **HTTP verbs**: GET, POST, PUT, DELETE
3. **Status codes**: 200, 201, 204, 400, 401, 403, 404, 500
4. **Content negotiation**: JSON by default

### API Documentation with Swagger

Comprehensive API documentation is provided using Swagger/OpenAPI:

- Detailed endpoint descriptions
- Request and response schemas
- Authentication requirements
- Example requests and responses

### Authentication and Authorization

The API uses JWT for authentication:

- Token-based authentication
- Role-based authorization
- Policy-based authorization for complex scenarios

### Response Format

All API responses follow a consistent format using ApiResponse<T>:

```json
{
  "succeeded": true,
  "message": "Operation completed successfully",
  "data": { ... },
  "errors": null
}
```

## Blazor Integration

The API layer is designed to work seamlessly with the Blazor front-end:

- Consistent response format
- Proper CORS configuration
- Authentication flow integration

## Extension Guidelines

When extending this layer:

1. Create new controllers following the established pattern
2. Use MediatR to dispatch commands and queries
3. Return consistent ApiResponse objects
4. Implement proper HTTP status codes
5. Provide comprehensive XML documentation for Swagger
6. Follow RESTful naming conventions
7. Implement proper validation and error handling
8. Use Fluent UI for any UI components
9. Maintain complete XML documentation for all public methods and endpoints

# Service Feature

## Overview
The Service feature provides functionality for managing services offered to customers in the VibeCRM system. These services can be organized by service types and include details such as name, description, and standard auditing properties.

## Feature Components

### Domain Layer
- **Service Entity**: Represents a service in the system with properties for ID, ServiceTypeId, Name, Description, and standard auditing fields.
- **IServiceRepository Interface**: Defines contract for Service data access operations.

### Infrastructure Layer
- **ServiceRepository**: Implements the IServiceRepository interface using Dapper for data access to SQL Server.

### Application Layer
- **DTOs**:
  - `ServiceDto`: Base DTO with essential service properties
  - `ServiceDetailsDto`: Detailed view with additional metadata
  - `ServiceListDto`: Streamlined version for list displays
- **Commands & Handlers**:
  - `CreateServiceCommand`: Creates a new service
  - `UpdateServiceCommand`: Updates an existing service
  - `DeleteServiceCommand`: Soft deletes a service (sets Active=0)
  - Each command has its own validator in the same folder
- **Queries & Handlers**:
  - `GetAllServicesQuery`: Retrieves all active services
  - `GetServiceByIdQuery`: Retrieves a specific service by ID
- **Mapping Profiles**:
  - `ServiceMappingProfile`: Maps between entities and DTOs
- **Validators**:
  - DTO validators in the Validators folder:
    - `ServiceDtoValidator`: For the base DTO
    - `ServiceDetailsDtoValidator`: For the detailed DTO
    - `ServiceListDtoValidator`: For the list DTO

### Dependency Registration
- The Service repository is registered in the infrastructure layer's `DependencyInjection.cs` file.
- The Service mapping profiles and validators are registered in the application layer's `ServiceCollectionExtensions.cs` file through the `AddServiceFeature` extension method.

## Implementation Notes

### Entity Design
The Service entity inherits from `BaseAuditableEntity<Guid>` and includes:
- ServiceId (Guid): Primary key
- ServiceTypeId (Guid): Foreign key to ServiceType
- Name (string): Service name
- Description (string): Optional service description
- Standard audit fields: CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active

### Soft Delete Pattern
Services use the standardized soft delete pattern with the `Active` property:
- Active=1 indicates active records
- Active=0 indicates "deleted" records
- All queries filter by `Active=1` to exclude soft-deleted records
- The `DeleteAsync` method performs soft deletion by setting `Active=0`

### Validation Rules
- **Service Name**:
  - Required
  - Maximum length of 100 characters
- **Service Description**:
  - Optional
  - Maximum length of 500 characters
- **ServiceTypeId**:
  - Required (cannot be empty GUID)
- **IDs and User References**:
  - Command IDs must be valid GUIDs
  - User references (CreatedBy, ModifiedBy) must be valid GUIDs

### Relationships
- Services have a relationship with ServiceType (many-to-one)

## Usage Examples

### Creating a Service
```csharp
var command = new CreateServiceCommand
{
    ServiceTypeId = serviceTypeGuid,
    Name = "Software Development",
    Description = "Custom software development services",
    CreatedBy = currentUserGuid,
    ModifiedBy = currentUserGuid
};

var result = await mediator.Send(command);
```

### Updating a Service
```csharp
var command = new UpdateServiceCommand
{
    Id = serviceGuid,
    ServiceTypeId = serviceTypeGuid,
    Name = "Updated Service Name",
    Description = "Updated description",
    ModifiedBy = currentUserGuid
};

var result = await mediator.Send(command);
```

### Deleting a Service
```csharp
var command = new DeleteServiceCommand(serviceGuid, currentUserGuid);
var result = await mediator.Send(command);
```

### Retrieving Services
```csharp
// Get all services
var allServices = await mediator.Send(new GetAllServicesQuery());

// Get a specific service
var service = await mediator.Send(new GetServiceByIdQuery(serviceGuid));
```

## Best Practices Followed
1. **Clean Architecture**: Separation of concerns between domain, infrastructure, and application layers
2. **CQRS Pattern**: Separate command and query responsibility using MediatR
3. **Repository Pattern**: Abstraction layer for data access operations
4. **Soft Delete**: Records are never physically deleted, only marked as inactive by setting Active=0
5. **XML Documentation**: Comprehensive documentation for all classes and methods
6. **Entity as Source of Truth**: All other components (DTOs, commands, etc.) match the entity definition
7. **FluentValidation**: Validation rules defined using FluentValidation library for all commands and DTOs
8. **Consistent Structure**: Command validators placed in command folders, DTO validators in Validators folder

## Pending Components
1. API Controllers (WebAPI layer)
2. UI Components (using Blazor and Fluent UI)
3. Query Validators (if needed, following the same pattern as command validators)

# ServiceType Feature

## Overview
The ServiceType feature provides functionality for managing service types in the VibeCRM system. Service types categorize services into different groups such as "Consulting", "Implementation", "Training", etc., making it easier to organize and find services.

## Components

### Domain Entities
- **ServiceType**: Represents a service type with properties like Id, Type, Description, OrdinalPosition, and a collection of Services.

### DTOs (Data Transfer Objects)
- **ServiceTypeDto**: Basic DTO for service type information.
- **ServiceTypeListDto**: DTO for service types in list views, includes service count.
- **ServiceTypeDetailsDto**: Detailed DTO with audit information and additional details.

### Commands
- **CreateServiceType**: Creates a new service type.
- **UpdateServiceType**: Updates an existing service type.
- **DeleteServiceType**: Soft deletes a service type by setting its Active property to false.

### Queries
- **GetAllServiceTypes**: Retrieves all active service types ordered by ordinal position.
- **GetServiceTypeById**: Retrieves a specific service type by its ID.
- **GetServiceTypeByType**: Retrieves service types by their type name (partial match).
- **GetDefaultServiceType**: Retrieves the default service type (the one with the lowest ordinal position).

### Validators
- **ServiceTypeDtoValidator**: Validates the ServiceTypeDto.
- **CreateServiceTypeCommandValidator**: Validates the CreateServiceTypeCommand.
- **UpdateServiceTypeCommandValidator**: Validates the UpdateServiceTypeCommand.
- **DeleteServiceTypeCommandValidator**: Validates the DeleteServiceTypeCommand.

### Mappings
- **ServiceTypeMappingProfile**: AutoMapper profile for mapping between ServiceType entities and DTOs.

## Usage Examples

### Creating a Service Type
```csharp
var command = new CreateServiceTypeCommand
{
    Type = "Consulting",
    Description = "Professional advisory services to help clients improve their business processes",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Updating a Service Type
```csharp
var command = new UpdateServiceTypeCommand
{
    Id = serviceTypeId,
    Type = "Implementation",
    Description = "Services for implementing software solutions",
    OrdinalPosition = 2
};

var result = await _mediator.Send(command);
```

### Deleting a Service Type
```csharp
var command = new DeleteServiceTypeCommand
{
    Id = serviceTypeId
};

var result = await _mediator.Send(command);
```

### Retrieving Service Types
```csharp
// Get all service types
var allServiceTypes = await _mediator.Send(new GetAllServiceTypesQuery());

// Get service type by ID
var serviceType = await _mediator.Send(new GetServiceTypeByIdQuery { Id = serviceTypeId });

// Get service types by type name
var consultingServiceTypes = await _mediator.Send(new GetServiceTypeByTypeQuery { Type = "Consult" });

// Get default service type
var defaultServiceType = await _mediator.Send(new GetDefaultServiceTypeQuery());
```

## Implementation Details
- Follows Clean Architecture principles and CQRS pattern.
- Uses Dapper for database operations.
- Implements soft delete functionality by setting the Active property to false instead of physically removing records.
- Uses FluentValidation for input validation.
- Provides comprehensive error handling and logging.

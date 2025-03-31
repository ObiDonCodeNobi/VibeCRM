# ServiceType Feature

## Overview
The ServiceType feature provides functionality for managing service types in the VibeCRM system. Service types categorize services into different groups such as "Consulting", "Implementation", "Training", etc., making it easier to organize and find services.

## Domain Model
The ServiceType entity is a reference entity that represents a type category for services. Each ServiceType has the following properties:

- **ServiceTypeId**: Unique identifier (UUID)
- **Type**: Name of the service type (e.g., "Consulting", "Implementation", "Training")
- **Description**: Detailed description of what the service type means
- **OrdinalPosition**: Numeric value for ordering service types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Services**: Collection of associated Service entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ServiceTypeDto**: Base DTO with core properties
- **ServiceTypeDetailsDto**: Extended DTO with audit fields and service count
- **ServiceTypeListDto**: Optimized DTO for list views

### Commands
- **CreateServiceType**: Creates a new service type
- **UpdateServiceType**: Updates an existing service type
- **DeleteServiceType**: Soft-deletes a service type by setting Active = false

### Queries
- **GetAllServiceTypes**: Retrieves all active service types ordered by ordinal position
- **GetServiceTypeById**: Retrieves a specific service type by its ID
- **GetServiceTypeByType**: Retrieves service types by their type name (partial match)
- **GetDefaultServiceType**: Retrieves the default service type (the one with the lowest ordinal position)

### Validators
- **ServiceTypeDtoValidator**: Validates the base DTO
- **ServiceTypeDetailsDtoValidator**: Validates the detailed DTO
- **ServiceTypeListDtoValidator**: Validates the list DTO
- **GetServiceTypeByIdQueryValidator**: Validates the ID query
- **GetServiceTypeByTypeQueryValidator**: Validates the type name query
- **GetAllServiceTypesQueryValidator**: Validates the "get all" query

### Mappings
- **ServiceTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new ServiceType
```csharp
var command = new CreateServiceTypeCommand
{
    Type = "Consulting",
    Description = "Professional advisory services to help clients improve their business processes",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all ServiceTypes
```csharp
var query = new GetAllServiceTypesQuery();
var serviceTypes = await _mediator.Send(query);
```

### Retrieving a ServiceType by ID
```csharp
var query = new GetServiceTypeByIdQuery { Id = serviceTypeId };
var serviceType = await _mediator.Send(query);
```

### Retrieving ServiceTypes by type name
```csharp
var query = new GetServiceTypeByTypeQuery { Type = "Consult" };
var consultingServiceTypes = await _mediator.Send(query);
```

### Retrieving the default ServiceType
```csharp
var query = new GetDefaultServiceTypeQuery();
var defaultServiceType = await _mediator.Send(query);
```

### Updating a ServiceType
```csharp
var command = new UpdateServiceTypeCommand
{
    Id = serviceTypeId,
    Type = "Implementation",
    Description = "Services for implementing software solutions",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a ServiceType
```csharp
var command = new DeleteServiceTypeCommand
{
    Id = serviceTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The ServiceType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all service types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Service types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Service Associations
Each ServiceType can be associated with multiple Service entities. The feature includes functionality to retrieve the count of services using each type.

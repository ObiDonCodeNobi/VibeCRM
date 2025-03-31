# PersonType Feature

## Overview
The PersonType feature provides functionality for managing person types in the VibeCRM system. Person types categorize people in the system (e.g., Customer, Vendor, Employee) and help organize and filter contacts.

## Domain Model
The PersonType entity is a reference entity that represents a type category for people. Each PersonType has the following properties:

- **PersonTypeId**: Unique identifier (UUID)
- **Type**: Name of the person type (e.g., "Customer", "Vendor", "Employee")
- **Description**: Detailed description of what the person type means
- **OrdinalPosition**: Numeric value for ordering person types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **People**: Collection of associated Person entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PersonTypeDto**: Base DTO with core properties
- **PersonTypeDetailsDto**: Extended DTO with audit fields and people count
- **PersonTypeListDto**: Optimized DTO for list views

### Commands
- **CreatePersonType**: Creates a new person type
- **UpdatePersonType**: Updates an existing person type
- **DeletePersonType**: Soft-deletes a person type by setting Active = false

### Queries
- **GetAllPersonTypes**: Retrieves all active person types
- **GetPersonTypeById**: Retrieves a specific person type by its ID
- **GetPersonTypeByType**: Retrieves person types by their type name
- **GetPersonTypeByOrdinalPosition**: Retrieves person types by their ordinal position
- **GetDefaultPersonType**: Retrieves the default person type (lowest ordinal position)

### Validators
- **PersonTypeDtoValidator**: Validates the base DTO
- **PersonTypeDetailsDtoValidator**: Validates the detailed DTO
- **PersonTypeListDtoValidator**: Validates the list DTO
- **CreatePersonTypeCommandValidator**: Validates the create command
- **UpdatePersonTypeCommandValidator**: Validates the update command
- **DeletePersonTypeCommandValidator**: Validates the delete command
- **GetPersonTypeByIdQueryValidator**: Validates the ID query
- **GetPersonTypeByTypeQueryValidator**: Validates the type name query
- **GetPersonTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetDefaultPersonTypeQueryValidator**: Validates the default query

### Mappings
- **PersonTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new PersonType
```csharp
var command = new CreatePersonTypeCommand
{
    Type = "Customer",
    Description = "External clients who purchase products or services",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all PersonTypes
```csharp
var query = new GetAllPersonTypesQuery();
var personTypes = await _mediator.Send(query);
```

### Retrieving a PersonType by ID
```csharp
var query = new GetPersonTypeByIdQuery { Id = personTypeId };
var personType = await _mediator.Send(query);
```

### Retrieving PersonTypes by type name
```csharp
var query = new GetPersonTypeByTypeQuery { Type = "Customer" };
var personType = await _mediator.Send(query);
```

### Retrieving the default PersonType
```csharp
var query = new GetDefaultPersonTypeQuery();
var defaultPersonType = await _mediator.Send(query);
```

### Updating a PersonType
```csharp
var command = new UpdatePersonTypeCommand
{
    Id = personTypeId,
    Type = "Premium Customer",
    Description = "High-value clients with premium service level",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a PersonType
```csharp
var command = new DeletePersonTypeCommand
{
    Id = personTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The PersonType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all person types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Person types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Person Associations
Each PersonType can be associated with multiple Person entities. The feature includes functionality to retrieve the count of people using each type.

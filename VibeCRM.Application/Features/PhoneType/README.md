# PhoneType Feature

## Overview
The PhoneType feature provides functionality for managing phone type entities in the VibeCRM application. Phone types categorize phone numbers (e.g., "Home", "Work", "Mobile", "Fax") and are used throughout the system to classify phone contact information.

## Domain Model
The PhoneType entity is a reference entity that represents a type category for phone numbers. Each PhoneType has the following properties:

- **PhoneTypeId**: Unique identifier (UUID)
- **Type**: Name of the phone type (e.g., "Home", "Work", "Mobile", "Fax")
- **Description**: Detailed description of what the phone type means
- **OrdinalPosition**: Numeric value for ordering phone types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **PhoneNumbers**: Collection of associated PhoneNumber entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PhoneTypeDto**: Base DTO with core properties
- **PhoneTypeDetailsDto**: Extended DTO with audit fields and phone number count
- **PhoneTypeListDto**: Optimized DTO for list views

### Commands
- **CreatePhoneType**: Creates a new phone type
- **UpdatePhoneType**: Updates an existing phone type
- **DeletePhoneType**: Soft-deletes a phone type by setting Active = false

### Queries
- **GetAllPhoneTypes**: Retrieves all active phone types
- **GetPhoneTypeById**: Retrieves a specific phone type by its ID
- **GetPhoneTypeByType**: Retrieves phone types by their type name
- **GetPhoneTypeByOrdinalPosition**: Retrieves phone types by their ordinal position
- **GetDefaultPhoneType**: Retrieves the default phone type (lowest ordinal position)

### Validators
- **PhoneTypeDtoValidator**: Validates the base DTO
- **PhoneTypeDetailsDtoValidator**: Validates the detailed DTO
- **PhoneTypeListDtoValidator**: Validates the list DTO
- **CreatePhoneTypeCommandValidator**: Validates the create command
- **UpdatePhoneTypeCommandValidator**: Validates the update command
- **DeletePhoneTypeCommandValidator**: Validates the delete command
- **GetPhoneTypeByIdQueryValidator**: Validates the ID query
- **GetPhoneTypeByTypeQueryValidator**: Validates the type name query
- **GetPhoneTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetDefaultPhoneTypeQueryValidator**: Validates the default query

### Mappings
- **PhoneTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new PhoneType
```csharp
var command = new CreatePhoneTypeCommand
{
    Type = "Mobile",
    Description = "Mobile phone number",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all PhoneTypes
```csharp
var query = new GetAllPhoneTypesQuery();
var phoneTypes = await _mediator.Send(query);
```

### Retrieving a PhoneType by ID
```csharp
var query = new GetPhoneTypeByIdQuery { Id = phoneTypeId };
var phoneType = await _mediator.Send(query);
```

### Retrieving PhoneTypes by type name
```csharp
var query = new GetPhoneTypeByTypeQuery { Type = "Mobile" };
var phoneType = await _mediator.Send(query);
```

### Retrieving the default PhoneType
```csharp
var query = new GetDefaultPhoneTypeQuery();
var defaultPhoneType = await _mediator.Send(query);
```

### Updating a PhoneType
```csharp
var command = new UpdatePhoneTypeCommand
{
    Id = phoneTypeId,
    Type = "Work",
    Description = "Work phone number",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a PhoneType
```csharp
var command = new DeletePhoneTypeCommand
{
    Id = phoneTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The PhoneType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all phone types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Phone types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Phone Number Associations
Each PhoneType can be associated with multiple PhoneNumber entities. The feature includes functionality to retrieve the count of phone numbers using each type.

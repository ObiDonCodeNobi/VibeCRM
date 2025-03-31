# EmailAddressType Feature

## Overview
The EmailAddressType feature provides functionality for managing email address types in the VibeCRM system. Email address types are used to categorize email addresses (e.g., "Personal", "Work") for organization and communication preferences.

## Domain Model
The EmailAddressType entity is a reference entity that represents a type category for email addresses. Each EmailAddressType has the following properties:

- **EmailAddressTypeId**: Unique identifier (UUID)
- **Type**: Name of the email address type (e.g., "Personal", "Work")
- **Description**: Detailed description of what the email address type means
- **OrdinalPosition**: Numeric value for ordering email address types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **EmailAddresses**: Collection of associated EmailAddress entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **EmailAddressTypeDto**: Base DTO with core properties
- **EmailAddressTypeDetailsDto**: Extended DTO with audit fields and email address count
- **EmailAddressTypeListDto**: Optimized DTO for list views

### Commands
- **CreateEmailAddressType**: Creates a new email address type
- **UpdateEmailAddressType**: Updates an existing email address type
- **DeleteEmailAddressType**: Soft-deletes an email address type by setting Active = false

### Queries
- **GetAllEmailAddressTypes**: Retrieves all active email address types
- **GetEmailAddressTypeById**: Retrieves a specific email address type by its ID
- **GetEmailAddressTypeByType**: Retrieves email address types by their type name
- **GetEmailAddressTypesByOrdinalPosition**: Retrieves email address types ordered by their ordinal position
- **GetDefaultEmailAddressType**: Retrieves the default email address type (lowest ordinal position)

### Validators
- **EmailAddressTypeDtoValidator**: Validates the base DTO
- **EmailAddressTypeDetailsDtoValidator**: Validates the detailed DTO
- **EmailAddressTypeListDtoValidator**: Validates the list DTO
- **GetEmailAddressTypeByIdQueryValidator**: Validates the ID query
- **GetEmailAddressTypeByTypeQueryValidator**: Validates the type name query
- **GetEmailAddressTypesByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllEmailAddressTypesQueryValidator**: Validates the "get all" query

### Mappings
- **EmailAddressTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new EmailAddressType
```csharp
var command = new CreateEmailAddressTypeCommand
{
    Type = "Work",
    Description = "Professional email address for business communications",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all EmailAddressTypes
```csharp
var query = new GetAllEmailAddressTypesQuery();
var emailAddressTypes = await _mediator.Send(query);
```

### Retrieving EmailAddressTypes by ordinal position
```csharp
var query = new GetEmailAddressTypesByOrdinalPositionQuery();
var orderedEmailAddressTypes = await _mediator.Send(query);
```

### Retrieving default EmailAddressType
```csharp
var query = new GetDefaultEmailAddressTypeQuery();
var defaultEmailAddressType = await _mediator.Send(query);
```

### Updating an EmailAddressType
```csharp
var command = new UpdateEmailAddressTypeCommand
{
    Id = emailAddressTypeId,
    Type = "Professional",
    Description = "Updated description for work email addresses",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an EmailAddressType
```csharp
var command = new DeleteEmailAddressTypeCommand
{
    Id = emailAddressTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The EmailAddressType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Email address types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### EmailAddress Associations
Each EmailAddressType can be associated with multiple EmailAddress entities. The feature includes functionality to retrieve the count of email addresses using each type.

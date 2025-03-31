# ContactType Feature

## Overview
The ContactType feature provides functionality for managing contact types in the VibeCRM system. Contact types categorize contacts (e.g., "Customer", "Vendor", "Partner", "Employee") and help organize relationships between entities in the system.

## Domain Model
The ContactType entity is a reference entity that represents a type category for contacts. Each ContactType has the following properties:

- **ContactTypeId**: Unique identifier (UUID)
- **Type**: Name of the contact type (e.g., "Customer", "Vendor", "Partner")
- **Description**: Detailed description of what the contact type means
- **OrdinalPosition**: Numeric value for ordering contact types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Contacts**: Collection of associated Contact entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ContactTypeDto**: Base DTO with core properties
- **ContactTypeDetailsDto**: Extended DTO with audit fields and contact count
- **ContactTypeListDto**: Optimized DTO for list views

### Commands
- **CreateContactType**: Creates a new contact type
- **UpdateContactType**: Updates an existing contact type
- **DeleteContactType**: Soft-deletes a contact type by setting Active = false

### Queries
- **GetAllContactTypes**: Retrieves all active contact types
- **GetContactTypeById**: Retrieves a specific contact type by its ID
- **GetContactTypeByType**: Retrieves a specific contact type by its type name
- **GetContactTypeByOrdinalPosition**: Retrieves contact types ordered by position
- **GetDefaultContactType**: Retrieves the default contact type

### Validators
- **ContactTypeDtoValidator**: Validates the base DTO
- **ContactTypeDetailsDtoValidator**: Validates the detailed DTO
- **ContactTypeListDtoValidator**: Validates the list DTO
- **CreateContactTypeCommandValidator**: Validates the create command
- **UpdateContactTypeCommandValidator**: Validates the update command
- **DeleteContactTypeCommandValidator**: Validates the delete command
- **GetContactTypeByIdQueryValidator**: Validates the ID query
- **GetContactTypeByTypeQueryValidator**: Validates the type name query
- **GetContactTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllContactTypesQueryValidator**: Validates the "get all" query
- **GetDefaultContactTypeQueryValidator**: Validates the default query

### Mappings
- **ContactTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new ContactType
```csharp
var command = new CreateContactTypeCommand
{
    Type = "Customer",
    Description = "External customer contact",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all ContactTypes
```csharp
var query = new GetAllContactTypesQuery();
var contactTypes = await _mediator.Send(query);
```

### Retrieving a ContactType by ID
```csharp
var query = new GetContactTypeByIdQuery { Id = contactTypeId };
var contactType = await _mediator.Send(query);
```

### Retrieving ContactTypes by type name
```csharp
var query = new GetContactTypeByTypeQuery { Type = "Customer" };
var contactTypes = await _mediator.Send(query);
```

### Retrieving the default ContactType
```csharp
var query = new GetDefaultContactTypeQuery();
var defaultContactType = await _mediator.Send(query);
```

### Updating a ContactType
```csharp
var command = new UpdateContactTypeCommand
{
    Id = contactTypeId,
    Type = "Premium Customer",
    Description = "High-value external customer contact",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a ContactType
```csharp
var command = new DeleteContactTypeCommand
{
    Id = contactTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The ContactType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all contact types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Contact types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Contact Associations
Each ContactType can be associated with multiple Contact entities. The feature includes functionality to retrieve the count of contacts using each type.

# AddressType Feature

## Overview
The AddressType feature provides functionality for managing address types in the VibeCRM system. Address types categorize addresses (e.g., "Home", "Work", "Billing", "Shipping") and help organize contact information for entities in the system.

## Domain Model
The AddressType entity is a reference entity that represents a type category for addresses. Each AddressType has the following properties:

- **AddressTypeId**: Unique identifier (UUID)
- **Type**: Name of the address type (e.g., "Home", "Work", "Billing")
- **Description**: Detailed description of what the address type means
- **OrdinalPosition**: Numeric value for ordering address types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Addresses**: Collection of associated Address entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **AddressTypeDto**: Base DTO with core properties
- **AddressTypeDetailsDto**: Extended DTO with audit fields and address count
- **AddressTypeListDto**: Optimized DTO for list views

### Commands
- **CreateAddressType**: Creates a new address type
- **UpdateAddressType**: Updates an existing address type
- **DeleteAddressType**: Soft-deletes an address type by setting Active = false

### Queries
- **GetAllAddressTypes**: Retrieves all active address types
- **GetAddressTypeById**: Retrieves a specific address type by its ID
- **GetAddressTypeByType**: Retrieves a specific address type by its type name
- **GetAddressTypeByOrdinalPosition**: Retrieves a specific address type by its ordinal position
- **GetDefaultAddressType**: Retrieves the default address type

### Validators
- **AddressTypeDtoValidator**: Validates the base DTO
- **AddressTypeDetailsDtoValidator**: Validates the detailed DTO
- **AddressTypeListDtoValidator**: Validates the list DTO
- **GetAddressTypeByIdQueryValidator**: Validates the ID query
- **GetAddressTypeByTypeQueryValidator**: Validates the type name query
- **GetAddressTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllAddressTypesQueryValidator**: Validates the "get all" query

### Mappings
- **AddressTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new AddressType
```csharp
var command = new CreateAddressTypeCommand
{
    Type = "Home",
    Description = "Residential address",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all AddressTypes
```csharp
var query = new GetAllAddressTypesQuery();
var addressTypes = await _mediator.Send(query);
```

### Updating an AddressType
```csharp
var command = new UpdateAddressTypeCommand
{
    Id = addressTypeId,
    Type = "Primary Residence",
    Description = "Main home address",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an AddressType
```csharp
var command = new DeleteAddressTypeCommand
{
    Id = addressTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The AddressType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Address types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Address Associations
Each AddressType can be associated with multiple Address entities. The feature includes functionality to retrieve the count of addresses using each type.

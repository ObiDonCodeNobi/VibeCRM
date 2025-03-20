# AddressType Feature

## Overview
The AddressType feature provides functionality for managing address types in the VibeCRM system. Address types categorize addresses (e.g., "Home", "Work", "Billing", "Shipping") and help organize contact information for entities in the system.

## Architecture
This feature follows Clean Architecture and CQRS principles:

- **Domain Layer**: Contains the `AddressType` entity and repository interface
- **Application Layer**: Contains DTOs, Commands, Queries, Validators, and Mapping profiles
- **Infrastructure Layer**: Contains the repository implementation

## Components

### DTOs
- **AddressTypeDto**: Basic properties of an address type
- **AddressTypeListDto**: List view with address count
- **AddressTypeDetailsDto**: Detailed view with audit fields

### Commands
- **CreateAddressType**: Creates a new address type
- **UpdateAddressType**: Updates an existing address type
- **DeleteAddressType**: Performs soft delete by setting Active = false

### Queries
- **GetAllAddressTypes**: Retrieves all address types
- **GetAddressTypeById**: Retrieves an address type by ID
- **GetAddressTypeByType**: Retrieves an address type by its type name
- **GetAddressTypeByOrdinalPosition**: Retrieves an address type by its ordinal position
- **GetDefaultAddressType**: Retrieves the default address type

### Validators
- Validators for all DTOs, commands, and queries
- Ensures data integrity and validation

### Mapping Profile
- Maps between entities and DTOs/commands
- Ensures proper data transformation

## Implementation Details

### Soft Delete
- Implemented using the `Active` property (true = active, false = deleted)
- All queries filter by `Active = true` to exclude soft-deleted records
- The `DeleteAsync` method in the repository sets `Active = false` instead of removing the record

### Audit Fields
- All entities include audit fields (CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
- These fields are automatically set in command handlers

### Ordinal Position
- Address types can be ordered using the `OrdinalPosition` property
- The `GetByOrdinalPositionAsync` method in the repository returns address types ordered by this field

## Usage Examples

### Creating an Address Type
```csharp
var command = new CreateAddressTypeCommand
{
    Type = "Home",
    Description = "Residential address",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Retrieving All Address Types
```csharp
var query = new GetAllAddressTypesQuery();
var addressTypes = await _mediator.Send(query);
```

### Updating an Address Type
```csharp
var command = new UpdateAddressTypeCommand
{
    Id = addressTypeId,
    Type = "Primary Residence",
    Description = "Main home address",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Deleting an Address Type
```csharp
var command = new DeleteAddressTypeCommand
{
    Id = addressTypeId
};

var result = await _mediator.Send(command);
```

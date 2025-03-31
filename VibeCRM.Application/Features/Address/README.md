# Address Feature

## Overview
The Address feature provides functionality for managing address information within the VibeCRM system. It follows the CQRS (Command Query Responsibility Segregation) pattern, separating commands for modifying data from queries for retrieving data.

## Domain Model
The Address entity is a core business entity that represents a physical location. Each Address has the following properties:

- **AddressId**: Unique identifier (UUID)
- **AddressTypeId**: Reference to the type of address (e.g., Home, Business, Shipping)
- **Line1**: First line of the address (street number and name)
- **Line2**: Optional second line of the address (apartment, suite, unit, etc.)
- **City**: City name
- **StateId**: Reference to the state/province
- **Zip**: Postal/zip code
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **AddressType**: Navigation property to the associated AddressType
- **State**: Navigation property to the associated State

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **AddressDto**: Base DTO with core properties
- **AddressDetailsDto**: Extended DTO with audit fields and related data
- **AddressListDto**: Optimized DTO for list views

### Commands
- **CreateAddress**: Creates a new address
- **UpdateAddress**: Updates an existing address
- **DeleteAddress**: Soft-deletes an address by setting Active = false
- **ValidateAddress**: Validates an address against external address verification service

### Queries
- **GetAllAddresses**: Retrieves all active addresses
- **GetAddressById**: Retrieves a specific address by its ID
- **GetAddressesByType**: Retrieves addresses filtered by address type
- **GetAddressesByState**: Retrieves addresses filtered by state
- **GetAddressesByCity**: Retrieves addresses filtered by city

### Validators
- **AddressDtoValidator**: Validates the base DTO
- **AddressDetailsDtoValidator**: Validates the detailed DTO
- **AddressListDtoValidator**: Validates the list DTO
- **CreateAddressCommandValidator**: Validates the create command
- **UpdateAddressCommandValidator**: Validates the update command
- **DeleteAddressCommandValidator**: Validates the delete command
- **ValidateAddressCommandValidator**: Validates the address validation command
- **GetAddressByIdQueryValidator**: Validates the ID query
- **GetAddressesByTypeQueryValidator**: Validates the type query
- **GetAddressesByStateQueryValidator**: Validates the state query
- **GetAddressesByCityQueryValidator**: Validates the city query

### Mappings
- **AddressMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Address
```csharp
var command = new CreateAddressCommand
{
    AddressTypeId = addressTypeId,
    Line1 = "123 Main St",
    City = "Springfield",
    StateId = stateId,
    Zip = "12345",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Addresses
```csharp
var query = new GetAllAddressesQuery();
var addresses = await _mediator.Send(query);
```

### Retrieving an Address by ID
```csharp
var query = new GetAddressByIdQuery { Id = addressId };
var address = await _mediator.Send(query);
```

### Retrieving Addresses by type
```csharp
var query = new GetAddressesByTypeQuery { AddressTypeId = addressTypeId };
var addresses = await _mediator.Send(query);
```

### Validating an Address
```csharp
var command = new ValidateAddressCommand
{
    Id = addressId,
    ModifiedBy = currentUserId
};

var validationResult = await _mediator.Send(command);
```

### Updating an Address
```csharp
var command = new UpdateAddressCommand
{
    Id = addressId,
    AddressTypeId = addressTypeId,
    Line1 = "456 Oak Ave",
    City = "Springfield",
    StateId = stateId,
    Zip = "12345",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an Address
```csharp
var command = new DeleteAddressCommand
{
    Id = addressId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Address feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Line1 is required and limited to 100 characters
- Line2 is optional and limited to 100 characters
- City is required and limited to 50 characters
- StateId must reference a valid state
- Zip is required and must follow the appropriate format for the country
- AddressTypeId must reference a valid address type
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Address Verification
The system supports optional address verification through external services:
- The ValidateAddress command can be used to verify address accuracy
- Verification results are stored with the address record
- Addresses can be marked as verified or unverified
- The UI displays verification status indicators for addresses

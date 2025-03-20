# Address Feature

## Overview
The Address feature provides functionality for managing address information within the VibeCRM system. It follows the CQRS (Command Query Responsibility Segregation) pattern, separating commands for modifying data from queries for retrieving data.

## Components

### DTOs (Data Transfer Objects)
- **AddressDto**: Base DTO containing essential address properties
- **AddressDetailsDto**: Extended DTO with additional details for single address views
- **AddressListDto**: DTO optimized for displaying addresses in lists

### Commands
- **CreateAddress**: Creates a new address
- **UpdateAddress**: Updates an existing address
- **DeleteAddress**: Soft-deletes an address (sets Active = false)

### Queries
- **GetAddressById**: Retrieves a specific address by its ID
- **GetAllAddresses**: Retrieves all active addresses

### Validators
- **CreateAddressCommandValidator**: Validates address creation requests
- **UpdateAddressCommandValidator**: Validates address update requests

### Mappings
- **AddressMappingProfile**: Configures AutoMapper mappings between entities and DTOs

## Implementation Details

### Soft Delete Pattern
The Address feature implements the soft delete pattern using the `Active` property:
- When an address is "deleted," its `Active` property is set to `false` rather than removing the record from the database
- All queries filter by `Active = true` to only show active records
- The `DeleteAsync` method in the repository performs a soft delete by setting `Active = false`

### Entity Structure
The Address entity includes the following properties:
- AddressId (Guid): Unique identifier
- AddressTypeId (Guid): Reference to address type
- Line1 (string): First line of the address
- Line2 (string): Optional second line of the address
- City (string): City name
- StateId (Guid): Reference to state/province
- Zip (string): Postal/zip code
- Active (bool): Indicates whether the address is active or soft-deleted
- CreatedBy (Guid): User who created the address
- CreatedDate (DateTime): When the address was created
- ModifiedBy (Guid): User who last modified the address
- ModifiedDate (DateTime): When the address was last modified

## Usage Examples

### Creating a New Address
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

### Updating an Address
```csharp
var command = new UpdateAddressCommand
{
    AddressId = addressId,
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
var command = new DeleteAddressCommand(addressId, currentUserId);
var result = await _mediator.Send(command);
```

### Retrieving an Address
```csharp
var query = new GetAddressByIdQuery(addressId);
var address = await _mediator.Send(query);
```

### Retrieving All Addresses
```csharp
var query = new GetAllAddressesQuery();
var addresses = await _mediator.Send(query);
```

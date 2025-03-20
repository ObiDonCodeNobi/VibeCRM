# EmailAddress Feature

## Overview
The EmailAddress feature provides functionality to manage email addresses within the VibeCRM system. It follows Clean Architecture and SOLID principles, implementing the CQRS pattern with MediatR for command and query handling.

## Components

### Domain
- `EmailAddress`: Entity representing an email address with properties for ID, EmailAddressTypeId, Address, and standard audit fields.

### Data Transfer Objects (DTOs)
- `EmailAddressDto`: Base DTO with essential email address properties.
- `EmailAddressDetailsDto`: Extended DTO with additional details for detailed views.
- `EmailAddressListDto`: Optimized DTO for list views.

### Validators
- `EmailAddressDtoValidator`: Validates the base EmailAddressDto.
- `EmailAddressDetailsDtoValidator`: Validates the EmailAddressDetailsDto.
- `EmailAddressListDtoValidator`: Validates the EmailAddressListDto.

### Commands
- **Create Email Address**
  - `CreateEmailAddressCommand`: Command for creating a new email address.
  - `CreateEmailAddressCommandValidator`: Validates the create command.
  - `CreateEmailAddressCommandHandler`: Handles the create command.

- **Update Email Address**
  - `UpdateEmailAddressCommand`: Command for updating an existing email address.
  - `UpdateEmailAddressCommandValidator`: Validates the update command.
  - `UpdateEmailAddressCommandHandler`: Handles the update command.

- **Delete Email Address**
  - `DeleteEmailAddressCommand`: Command for soft-deleting an email address.
  - `DeleteEmailAddressCommandValidator`: Validates the delete command.
  - `DeleteEmailAddressCommandHandler`: Handles the delete command.

### Queries
- **Get All Email Addresses**
  - `GetAllEmailAddressesQuery`: Query for retrieving all email addresses.
  - `GetAllEmailAddressesQueryHandler`: Handles the get all query.

- **Get Email Address By ID**
  - `GetEmailAddressByIdQuery`: Query for retrieving a specific email address.
  - `GetEmailAddressByIdQueryHandler`: Handles the get by ID query.

- **Search Email Addresses**
  - `SearchEmailAddressesQuery`: Query for searching email addresses.
  - `SearchEmailAddressesQueryHandler`: Handles the search query.

- **Get Email Addresses By Type**
  - `GetEmailAddressesByTypeQuery`: Query for filtering email addresses by type.
  - `GetEmailAddressesByTypeQueryHandler`: Handles the get by type query.

### Mapping
- `EmailAddressMappingProfile`: AutoMapper profile for mapping between entities and DTOs.

## Usage Examples

### Creating an Email Address
```csharp
var command = new CreateEmailAddressCommand
{
    EmailAddressTypeId = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual type ID
    Address = "example@example.com",
    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual user ID
    CreatedDate = DateTime.UtcNow
};

var result = await _mediator.Send(command);
```

### Retrieving an Email Address
```csharp
var query = new GetEmailAddressByIdQuery
{
    Id = Guid.Parse("00000000-0000-0000-0000-000000000000") // Replace with actual email address ID
};

var emailAddress = await _mediator.Send(query);
```

### Updating an Email Address
```csharp
var command = new UpdateEmailAddressCommand
{
    Id = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual email address ID
    EmailAddressTypeId = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual type ID
    Address = "updated@example.com",
    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual user ID
    ModifiedDate = DateTime.UtcNow
};

var result = await _mediator.Send(command);
```

### Deleting an Email Address
```csharp
var command = new DeleteEmailAddressCommand
{
    Id = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual email address ID
    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual user ID
    ModifiedDate = DateTime.UtcNow
};

var result = await _mediator.Send(command);
```

### Searching Email Addresses
```csharp
var query = new SearchEmailAddressesQuery
{
    SearchTerm = "example.com"
};

var emailAddresses = await _mediator.Send(query);
```

## Implementation Details

### Soft Delete
The EmailAddress feature implements soft delete using the `Active` property. When an email address is deleted:
- The `Active` property is set to `false` (0 in the database)
- The record remains in the database but is filtered out from regular queries
- All queries include a filter for `Active = 1` to exclude soft-deleted records

### Validation
All inputs are validated using FluentValidation:
- Email addresses are validated for proper format
- Required fields are enforced
- String lengths are validated

### Error Handling
The feature includes comprehensive error handling:
- Validation errors are returned with descriptive messages
- Not found scenarios are properly handled
- Duplicate email addresses are detected and prevented

## Dependencies
- **FluentValidation**: For input validation
- **AutoMapper**: For object mapping between entities and DTOs
- **MediatR**: For command and query handling
- **Dapper**: For data access against MS SQL Server

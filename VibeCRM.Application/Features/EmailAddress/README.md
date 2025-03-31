# EmailAddress Feature

## Overview
The EmailAddress feature provides functionality to manage email addresses within the VibeCRM system. It follows Clean Architecture and SOLID principles, implementing the CQRS pattern with MediatR for command and query handling.

## Domain Model
The EmailAddress entity is a core business entity that represents an email address in the CRM system. Each EmailAddress has the following properties:

- **EmailAddressId**: Unique identifier (UUID)
- **EmailAddressTypeId**: Reference to the email address type (e.g., Work, Personal, Other)
- **Address**: The actual email address string
- **IsPrimary**: Boolean flag indicating if this is the primary email address
- **PersonId**: Optional reference to the associated person
- **CompanyId**: Optional reference to the associated company
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **EmailAddressType**: Navigation property to the associated EmailAddressType
- **Person**: Navigation property to the associated Person (if applicable)
- **Company**: Navigation property to the associated Company (if applicable)

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **EmailAddressDto**: Base DTO with core properties
- **EmailAddressDetailsDto**: Extended DTO with audit fields and related data
- **EmailAddressListDto**: Optimized DTO for list views

### Commands
- **CreateEmailAddress**: Creates a new email address
- **UpdateEmailAddress**: Updates an existing email address
- **DeleteEmailAddress**: Soft-deletes an email address by setting Active = false
- **SetPrimaryEmailAddress**: Sets an email address as the primary contact method
- **ValidateEmailAddress**: Validates an email address format and optionally checks deliverability

### Queries
- **GetAllEmailAddresses**: Retrieves all active email addresses
- **GetEmailAddressById**: Retrieves a specific email address by its ID
- **GetEmailAddressesByType**: Retrieves email addresses filtered by type
- **GetEmailAddressesByPerson**: Retrieves email addresses for a specific person
- **GetEmailAddressesByCompany**: Retrieves email addresses for a specific company
- **SearchEmailAddresses**: Searches email addresses by address string

### Validators
- **EmailAddressDtoValidator**: Validates the base DTO
- **EmailAddressDetailsDtoValidator**: Validates the detailed DTO
- **EmailAddressListDtoValidator**: Validates the list DTO
- **CreateEmailAddressCommandValidator**: Validates the create command
- **UpdateEmailAddressCommandValidator**: Validates the update command
- **DeleteEmailAddressCommandValidator**: Validates the delete command
- **SetPrimaryEmailAddressCommandValidator**: Validates the set primary command
- **ValidateEmailAddressCommandValidator**: Validates the email validation command
- **GetEmailAddressByIdQueryValidator**: Validates the ID query
- **GetEmailAddressesByTypeQueryValidator**: Validates the type query
- **GetEmailAddressesByPersonQueryValidator**: Validates the person query
- **GetEmailAddressesByCompanyQueryValidator**: Validates the company query
- **SearchEmailAddressesQueryValidator**: Validates the search query

### Mappings
- **EmailAddressMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Email Address
```csharp
var command = new CreateEmailAddressCommand
{
    EmailAddressTypeId = emailAddressTypeId,
    Address = "example@example.com",
    IsPrimary = true,
    PersonId = personId, // Optional
    CompanyId = null, // Optional
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Email Addresses
```csharp
var query = new GetAllEmailAddressesQuery();
var emailAddresses = await _mediator.Send(query);
```

### Retrieving an Email Address by ID
```csharp
var query = new GetEmailAddressByIdQuery { Id = emailAddressId };
var emailAddress = await _mediator.Send(query);
```

### Searching Email Addresses
```csharp
var query = new SearchEmailAddressesQuery { SearchTerm = "example.com" };
var emailAddresses = await _mediator.Send(query);
```

### Setting an Email Address as Primary
```csharp
var command = new SetPrimaryEmailAddressCommand
{
    Id = emailAddressId,
    PersonId = personId, // If for a person
    CompanyId = null, // If for a company
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating an Email Address
```csharp
var command = new UpdateEmailAddressCommand
{
    Id = emailAddressId,
    EmailAddressTypeId = emailAddressTypeId,
    Address = "updated@example.com",
    IsPrimary = true,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an Email Address
```csharp
var command = new DeleteEmailAddressCommand
{
    Id = emailAddressId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

### Validating an Email Address
```csharp
var command = new ValidateEmailAddressCommand
{
    Address = "test@example.com",
    CheckDeliverability = true
};

var validationResult = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The EmailAddress feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Address is required and must be a valid email format
- EmailAddressTypeId must reference a valid email address type
- Either PersonId or CompanyId should be provided (but not both)
- If IsPrimary is set to true, any other email addresses for the same entity will be set to false
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Email Validation
- Basic format validation is performed using regular expressions
- Optional deliverability check can be performed using external services
- Disposable email domains can be detected and flagged

### Related Entities
- Email addresses can be associated with either a Person or a Company (but not both)
- Each Person or Company can have multiple email addresses
- Only one email address per Person or Company can be marked as primary

## Dependencies
- **FluentValidation**: For input validation
- **AutoMapper**: For object mapping between entities and DTOs
- **MediatR**: For command and query handling
- **Dapper**: For data access against MS SQL Server

# Phone Feature

## Overview
The Phone feature provides functionality for managing phone numbers within the VibeCRM system. It allows for storing, retrieving, updating, and soft-deleting phone number records associated with contacts, companies, and other entities.

## Domain Model
The Phone entity is a core business entity that represents a phone number in the CRM system. Each Phone has the following properties:

- **PhoneId**: Unique identifier (UUID)
- **PhoneNumber**: The actual phone number
- **PhoneTypeId**: Reference to the type of phone (e.g., Mobile, Work, Home)
- **CountryCode**: International country code for the phone number
- **Extension**: Optional extension number
- **PersonId**: Optional reference to the associated person
- **CompanyId**: Optional reference to the associated company
- **IsPrimary**: Boolean flag indicating if this is the primary phone number
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **PhoneType**: Navigation property to the associated PhoneType
- **Person**: Navigation property to the associated Person (if applicable)
- **Company**: Navigation property to the associated Company (if applicable)

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PhoneDto**: Base DTO with core properties
- **PhoneDetailsDto**: Extended DTO with audit fields and related data
- **PhoneListDto**: Optimized DTO for list views

### Commands
- **CreatePhone**: Creates a new phone number
- **UpdatePhone**: Updates an existing phone number
- **DeletePhone**: Soft-deletes a phone number by setting Active = false
- **SetPrimaryPhone**: Sets a phone number as the primary contact method
- **BulkCreatePhones**: Creates multiple phone numbers in a single operation

### Queries
- **GetAllPhones**: Retrieves all active phone numbers
- **GetPhoneById**: Retrieves a specific phone number by its ID
- **GetPhonesByPerson**: Retrieves phone numbers associated with a specific person
- **GetPhonesByCompany**: Retrieves phone numbers associated with a specific company
- **GetPhonesByType**: Retrieves phone numbers of a specific type
- **GetPrimaryPhoneByPerson**: Retrieves the primary phone number for a person
- **GetPrimaryPhoneByCompany**: Retrieves the primary phone number for a company

### Validators
- **PhoneDtoValidator**: Validates the base DTO
- **PhoneDetailsDtoValidator**: Validates the detailed DTO
- **PhoneListDtoValidator**: Validates the list DTO
- **CreatePhoneCommandValidator**: Validates the create command
- **UpdatePhoneCommandValidator**: Validates the update command
- **DeletePhoneCommandValidator**: Validates the delete command
- **SetPrimaryPhoneCommandValidator**: Validates the set primary command
- **BulkCreatePhonesCommandValidator**: Validates the bulk create command
- **GetPhoneByIdQueryValidator**: Validates the ID query
- **GetPhonesByPersonQueryValidator**: Validates the person query
- **GetPhonesByCompanyQueryValidator**: Validates the company query
- **GetPhonesByTypeQueryValidator**: Validates the type query

### Mappings
- **PhoneMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Phone
```csharp
var command = new CreatePhoneCommand
{
    PhoneNumber = "555-123-4567",
    PhoneTypeId = phoneTypeId,
    CountryCode = "1",
    Extension = "123",
    PersonId = personId,
    IsPrimary = true,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Phones
```csharp
var query = new GetAllPhonesQuery();
var phones = await _mediator.Send(query);
```

### Retrieving a Phone by ID
```csharp
var query = new GetPhoneByIdQuery { Id = phoneId };
var phone = await _mediator.Send(query);
```

### Retrieving Phones by Person
```csharp
var query = new GetPhonesByPersonQuery { PersonId = personId };
var phones = await _mediator.Send(query);
```

### Setting a Phone as Primary
```csharp
var command = new SetPrimaryPhoneCommand
{
    Id = phoneId,
    PersonId = personId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Phone
```csharp
var command = new UpdatePhoneCommand
{
    Id = phoneId,
    PhoneNumber = "555-987-6543",
    PhoneTypeId = newPhoneTypeId,
    Extension = "456",
    IsPrimary = true,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Phone
```csharp
var command = new DeletePhoneCommand
{
    Id = phoneId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Phone feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- PhoneNumber is required and must be in a valid format
- PhoneTypeId must reference a valid phone type
- CountryCode is required and must be in a valid format
- Extension is optional but must be numeric if provided
- Either PersonId or CompanyId must be provided (a phone must be associated with at least one entity)
- If IsPrimary is set to true, any other primary phone numbers for the same entity are set to false
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Phone Number Formatting
- Phone numbers are stored in a standardized format
- The system supports international phone numbers with country codes
- The UI displays phone numbers in a user-friendly format based on the country code
- Phone numbers can be validated against country-specific patterns

### Primary Phone Logic
- Each person or company can have one primary phone number
- Setting a phone number as primary automatically sets all other phone numbers for the same entity as non-primary
- The primary phone is used as the default contact method in communications

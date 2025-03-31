# Person Feature

## Overview
The Person feature provides functionality for managing person entities within the VibeCRM system. It follows Clean Architecture and CQRS patterns to ensure separation of concerns and maintainability.

## Domain Model
The Person entity is a core business entity that represents an individual in the CRM system. Each Person has the following properties:

- **PersonId**: Unique identifier (UUID)
- **Firstname**: Person's first name
- **Lastname**: Person's last name
- **Title**: Person's job title
- **CompanyId**: Optional reference to the associated company
- **PreferredContactMethod**: Preferred method of contact (e.g., Email, Phone)
- **DateOfBirth**: Optional date of birth
- **Notes**: Optional additional notes about the person
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Company**: Navigation property to the associated Company
- **Addresses**: Collection of associated Address entities
- **PhoneNumbers**: Collection of associated PhoneNumber entities
- **EmailAddresses**: Collection of associated EmailAddress entities
- **Activities**: Collection of associated Activity entities
- **Attachments**: Collection of associated Attachment entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PersonDto**: Base DTO with core properties
- **PersonDetailsDto**: Extended DTO with audit fields and related data
- **PersonListDto**: Optimized DTO for list views

### Commands
- **CreatePerson**: Creates a new person
- **UpdatePerson**: Updates an existing person
- **DeletePerson**: Soft-deletes a person by setting Active = false
- **AssignPersonToCompany**: Associates a person with a company
- **AssignPersonToUser**: Assigns a person to a specific user for management
- **MergePersons**: Merges duplicate person records

### Queries
- **GetAllPersons**: Retrieves all active persons
- **GetPersonById**: Retrieves a specific person by their ID
- **GetPersonsByCompany**: Retrieves persons associated with a specific company
- **GetPersonsByUser**: Retrieves persons assigned to a specific user
- **SearchPersons**: Searches persons by name, email, phone, or other criteria

### Validators
- **PersonDtoValidator**: Validates the base DTO
- **PersonDetailsDtoValidator**: Validates the detailed DTO
- **PersonListDtoValidator**: Validates the list DTO
- **CreatePersonCommandValidator**: Validates the create command
- **UpdatePersonCommandValidator**: Validates the update command
- **DeletePersonCommandValidator**: Validates the delete command
- **AssignPersonToCompanyCommandValidator**: Validates the company assignment command
- **AssignPersonToUserCommandValidator**: Validates the user assignment command
- **MergePersonsCommandValidator**: Validates the merge command
- **GetPersonByIdQueryValidator**: Validates the ID query
- **GetPersonsByCompanyQueryValidator**: Validates the company query
- **GetPersonsByUserQueryValidator**: Validates the user query
- **SearchPersonsQueryValidator**: Validates the search query

### Mappings
- **PersonMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Person
```csharp
var command = new CreatePersonCommand
{
    Firstname = "John",
    Lastname = "Doe",
    Title = "Sales Manager",
    CompanyId = companyId, // Optional
    PreferredContactMethod = "Email",
    DateOfBirth = new DateTime(1980, 1, 15), // Optional
    Notes = "Met at the technology conference", // Optional
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Persons
```csharp
var query = new GetAllPersonsQuery();
var persons = await _mediator.Send(query);
```

### Retrieving a Person by ID
```csharp
var query = new GetPersonByIdQuery { Id = personId };
var person = await _mediator.Send(query);
```

### Searching Persons
```csharp
var query = new SearchPersonsQuery { SearchTerm = "John" };
var persons = await _mediator.Send(query);
```

### Assigning a Person to a Company
```csharp
var command = new AssignPersonToCompanyCommand
{
    PersonId = personId,
    CompanyId = companyId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Person
```csharp
var command = new UpdatePersonCommand
{
    Id = personId,
    Firstname = "John",
    Lastname = "Smith",
    Title = "Senior Sales Manager",
    PreferredContactMethod = "Phone",
    Notes = "Recently promoted to senior position",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Person
```csharp
var command = new DeletePersonCommand
{
    Id = personId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

### Merging Duplicate Persons
```csharp
var command = new MergePersonsCommand
{
    PrimaryPersonId = primaryPersonId,
    SecondaryPersonIds = new List<Guid> { duplicatePersonId1, duplicatePersonId2 },
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Person feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Firstname is required and limited to 100 characters
- Lastname is required and limited to 100 characters
- Title is optional and limited to 100 characters
- CompanyId must reference a valid company if provided
- DateOfBirth must be a valid date in the past if provided
- Notes are optional and limited to 1000 characters
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Related Entities
- Persons can have multiple addresses, with one designated as primary
- Persons can have multiple phone numbers, with one designated as primary
- Persons can have multiple email addresses, with one designated as primary
- Activities can be associated with persons for tracking interactions
- Attachments can be linked to persons for storing related documents

### Duplicate Detection
- The system checks for potential duplicate persons based on name, email, and phone similarity
- When creating or updating a person, potential duplicates are flagged
- Users can choose to proceed with creation or merge with existing records using the MergePersons command

# Person Feature

## Overview
The Person feature provides functionality for managing person entities within the VibeCRM system. It follows Clean Architecture and CQRS patterns to ensure separation of concerns and maintainability.

## Components

### DTOs
- **PersonDto**: Base DTO for person entities with essential properties.
- **PersonDetailsDto**: Detailed DTO with related entities for comprehensive views.
- **PersonListDto**: Streamlined DTO for displaying persons in lists with summary information.

### Commands
- **CreatePerson**: Creates a new person in the system.
- **UpdatePerson**: Updates an existing person's information.
- **DeletePerson**: Soft-deletes a person by setting the Active flag to false.

### Queries
- **GetPersonById**: Retrieves a specific person by their unique identifier.
- **GetAllPersons**: Retrieves multiple persons with optional filtering and pagination.

### Validators
- Validation rules for all DTOs and commands to ensure data integrity.

### Mappings
- AutoMapper profiles for mapping between entities and DTOs.

## Implementation Details

### Soft Delete
Person entities use the `Active` property for soft deletion, consistent with the VibeCRM system pattern. When a person is "deleted", the `Active` property is set to `false` rather than removing the record from the database.

### Validation
All commands and DTOs are validated using FluentValidation to ensure data integrity before processing.

### Error Handling
Comprehensive error handling and logging are implemented in all command and query handlers.

## Usage Examples

### Creating a Person
```csharp
var command = new CreatePersonCommand
{
    Firstname = "John",
    Lastname = "Doe",
    Title = "Sales Manager",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await mediator.Send(command);
```

### Retrieving a Person
```csharp
var query = new GetPersonByIdQuery { Id = personId };
var person = await mediator.Send(query);
```

### Updating a Person
```csharp
var command = new UpdatePersonCommand
{
    Id = personId,
    Firstname = "John",
    Lastname = "Smith",
    ModifiedBy = currentUserId
};

var result = await mediator.Send(command);
```

### Deleting a Person
```csharp
var command = new DeletePersonCommand
{
    Id = personId,
    ModifiedBy = currentUserId
};

var success = await mediator.Send(command);
```

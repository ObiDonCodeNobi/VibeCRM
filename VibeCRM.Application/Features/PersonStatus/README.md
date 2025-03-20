# PersonStatus Feature

## Overview
The PersonStatus feature provides functionality for managing person status entities in the VibeCRM system. Person statuses represent the current state of a person in the system (e.g., "Active", "Inactive", "Lead", "Customer", etc.) and are used to categorize and filter people.

## Architecture
This feature follows the Clean Architecture and CQRS (Command Query Responsibility Segregation) patterns using MediatR. It is organized into the following components:

### Domain Layer
- `PersonStatus` entity in the Domain layer defines the core business entity.

### Application Layer
- **DTOs (Data Transfer Objects)**:
  - `PersonStatusDto`: Basic DTO for transferring person status data.
  - `PersonStatusDetailsDto`: Detailed DTO with additional information about the person status.
  - `PersonStatusListDto`: DTO optimized for list views of person statuses.

- **Commands**:
  - `CreatePersonStatusCommand`: Command for creating a new person status.
  - `UpdatePersonStatusCommand`: Command for updating an existing person status.
  - `DeletePersonStatusCommand`: Command for soft deleting a person status.

- **Command Handlers**:
  - `CreatePersonStatusCommandHandler`: Handles the creation of person statuses.
  - `UpdatePersonStatusCommandHandler`: Handles the updating of person statuses.
  - `DeletePersonStatusCommandHandler`: Handles the soft deletion of person statuses.

- **Queries**:
  - `GetPersonStatusByIdQuery`: Query for retrieving a person status by its ID.
  - `GetPersonStatusByStatusQuery`: Query for retrieving a person status by its status name.
  - `GetAllPersonStatusesQuery`: Query for retrieving all person statuses.
  - `GetPersonStatusByOrdinalPositionQuery`: Query for retrieving person statuses ordered by their ordinal position.
  - `GetDefaultPersonStatusQuery`: Query for retrieving the default person status.

- **Query Handlers**:
  - Corresponding handlers for each query that interact with the repository to fetch data.

- **Validators**:
  - Validators for all DTOs, commands, and queries using FluentValidation.

- **Mappings**:
  - `PersonStatusMappingProfile`: AutoMapper profile for mapping between PersonStatus entities and DTOs.

### Infrastructure Layer
- `IPersonStatusRepository`: Interface defining the repository contract.
- `PersonStatusRepository`: Implementation of the repository interface using Dapper ORM.

## Key Features
1. **CRUD Operations**: Create, Read, Update, and Delete (soft delete) operations for person status entities.
2. **Ordinal Position**: Person statuses can be ordered using an ordinal position property.
3. **Default Status**: Support for retrieving the default person status.
4. **Soft Delete**: Implements the soft delete pattern using the `Active` property.

## Usage Examples

### Creating a Person Status
```csharp
var command = new CreatePersonStatusCommand
{
    Status = "Active",
    Description = "Person is currently active in the system",
    OrdinalPosition = 1,
    CreatedBy = "admin"
};

var result = await _mediator.Send(command);
```

### Updating a Person Status
```csharp
var command = new UpdatePersonStatusCommand
{
    Id = personStatusId,
    Status = "Active Customer",
    Description = "Person is an active customer",
    OrdinalPosition = 1,
    ModifiedBy = "admin"
};

var result = await _mediator.Send(command);
```

### Retrieving All Person Statuses
```csharp
var query = new GetAllPersonStatusesQuery
{
    IncludeInactive = false
};

var personStatuses = await _mediator.Send(query);
```

### Retrieving Person Statuses by Ordinal Position
```csharp
var query = new GetPersonStatusByOrdinalPositionQuery();

var orderedPersonStatuses = await _mediator.Send(query);
```

## Dependencies
- **MediatR**: For implementing the CQRS pattern.
- **AutoMapper**: For mapping between entities and DTOs.
- **FluentValidation**: For validating commands, queries, and DTOs.
- **Dapper**: ORM for data access.
- **Microsoft.Extensions.Logging**: For logging within handlers and repositories.

## Best Practices
1. All handlers include comprehensive error handling and logging.
2. All public methods and classes have XML documentation.
3. Validation is implemented for all commands, queries, and DTOs.
4. The feature follows the soft delete pattern using the `Active` property.
5. All repository methods include a `CancellationToken` parameter for cancellation support.

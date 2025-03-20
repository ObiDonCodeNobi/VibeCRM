# Activity Status Feature

## Overview
The Activity Status feature manages the different statuses that can be assigned to activities in the VibeCRM system. Examples include "Scheduled", "In Progress", "Completed", etc. Each status has an ordinal position that determines its order in listings and dropdowns.

## Architecture
This feature follows Clean Architecture and CQRS patterns:

- **Domain Layer**: Contains the ActivityStatus entity
- **Application Layer**: Contains DTOs, Commands, Queries, Validators, and Mapping Profiles
- **Infrastructure Layer**: Contains the repository implementation

## Components

### DTOs
- **ActivityStatusDto**: Basic properties (Id, Status, Description, OrdinalPosition)
- **ActivityStatusListDto**: List view with additional ActivityCount property
- **ActivityStatusDetailsDto**: Detailed view with audit fields (CreatedDate, CreatedBy, etc.)

### Commands
- **CreateActivityStatus**: Creates a new activity status
- **UpdateActivityStatus**: Updates an existing activity status
- **DeleteActivityStatus**: Performs a soft delete by setting Active = false

### Queries
- **GetAllActivityStatuses**: Retrieves all active activity statuses
- **GetActivityStatusById**: Retrieves a specific activity status by ID
- **GetActivityStatusByStatus**: Retrieves activity statuses by status name
- **GetActivityStatusByOrdinalPosition**: Retrieves activity statuses ordered by position
- **GetCompletedActivityStatuses**: Retrieves all completed activity statuses
- **GetDefaultActivityStatus**: Retrieves the default activity status

### Validators
Each DTO, Command, and Query has its own validator using FluentValidation.

### Mapping Profile
The `ActivityStatusMappingProfile` handles mapping between:
- Entity to DTOs
- DTOs to Entity
- Commands to Entity

## Implementation Details

### Soft Delete Pattern
This feature implements the soft delete pattern using the `Active` property. When an entity is "deleted", the `Active` property is set to `false` rather than removing the record from the database.

### Ordinal Position
Activity statuses have an ordinal position that determines their order in listings and dropdowns. The `GetActivityStatusByOrdinalPositionAsync` method retrieves statuses ordered by this position.

### Default Status
The system has a concept of a default activity status, which can be retrieved using the `GetDefaultActivityStatus` query.

### Completed Statuses
The system can identify which activity statuses represent completion, retrievable via the `GetCompletedActivityStatuses` query.

## Usage Examples

### Creating a New Activity Status
```csharp
var command = new CreateActivityStatusCommand
{
    Status = "In Progress",
    Description = "Activity is currently being worked on",
    OrdinalPosition = 2
};

var activityStatusId = await _mediator.Send(command);
```

### Retrieving Activity Statuses
```csharp
// Get all activity statuses
var query = new GetAllActivityStatusesQuery();
var activityStatuses = await _mediator.Send(query);

// Get activity status by ID
var detailsQuery = new GetActivityStatusByIdQuery { Id = activityStatusId };
var activityStatus = await _mediator.Send(detailsQuery);
```

## Dependencies
- **FluentValidation**: For validating commands and DTOs
- **AutoMapper**: For mapping between entities and DTOs
- **MediatR**: For handling commands and queries following the CQRS pattern
- **Dapper**: For data access in the repository layer

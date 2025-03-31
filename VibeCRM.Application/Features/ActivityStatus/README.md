# Activity Status Feature

## Overview
The Activity Status feature manages the different statuses that can be assigned to activities in the VibeCRM system. Examples include "Scheduled", "In Progress", "Completed", etc. Each status has an ordinal position that determines its order in listings and dropdowns.

## Domain Model
The ActivityStatus entity is a reference entity that represents the status of an activity. Each ActivityStatus has the following properties:

- **ActivityStatusId**: Unique identifier (UUID)
- **Status**: Name of the activity status (e.g., "Scheduled", "In Progress", "Completed")
- **Description**: Detailed description of what the status means
- **OrdinalPosition**: Numeric value for ordering statuses in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Activities**: Collection of associated Activity entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ActivityStatusDto**: Base DTO with core properties
- **ActivityStatusDetailsDto**: Extended DTO with audit fields and activity count
- **ActivityStatusListDto**: Optimized DTO for list views

### Commands
- **CreateActivityStatus**: Creates a new activity status
- **UpdateActivityStatus**: Updates an existing activity status
- **DeleteActivityStatus**: Soft-deletes an activity status by setting Active = false

### Queries
- **GetAllActivityStatuses**: Retrieves all active activity statuses
- **GetActivityStatusById**: Retrieves a specific activity status by its ID
- **GetActivityStatusByStatus**: Retrieves activity statuses by their status name
- **GetActivityStatusByOrdinalPosition**: Retrieves activity statuses by their ordinal position
- **GetCompletedActivityStatuses**: Retrieves all completed activity statuses
- **GetDefaultActivityStatus**: Retrieves the default activity status (lowest ordinal position)

### Validators
- **ActivityStatusDtoValidator**: Validates the base DTO
- **ActivityStatusDetailsDtoValidator**: Validates the detailed DTO
- **ActivityStatusListDtoValidator**: Validates the list DTO
- **CreateActivityStatusCommandValidator**: Validates the create command
- **UpdateActivityStatusCommandValidator**: Validates the update command
- **DeleteActivityStatusCommandValidator**: Validates the delete command
- **GetActivityStatusByIdQueryValidator**: Validates the ID query
- **GetActivityStatusByStatusQueryValidator**: Validates the status name query
- **GetActivityStatusByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllActivityStatusesQueryValidator**: Validates the "get all" query

### Mappings
- **ActivityStatusMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new ActivityStatus
```csharp
var command = new CreateActivityStatusCommand
{
    Status = "In Progress",
    Description = "Activity is currently being worked on",
    OrdinalPosition = 2,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all ActivityStatuses
```csharp
var query = new GetAllActivityStatusesQuery();
var activityStatuses = await _mediator.Send(query);
```

### Retrieving an ActivityStatus by ID
```csharp
var query = new GetActivityStatusByIdQuery { Id = activityStatusId };
var activityStatus = await _mediator.Send(query);
```

### Retrieving ActivityStatuses by status name
```csharp
var query = new GetActivityStatusByStatusQuery { Status = "In Progress" };
var activityStatus = await _mediator.Send(query);
```

### Retrieving completed ActivityStatuses
```csharp
var query = new GetCompletedActivityStatusesQuery();
var completedStatuses = await _mediator.Send(query);
```

### Retrieving the default ActivityStatus
```csharp
var query = new GetDefaultActivityStatusQuery();
var defaultActivityStatus = await _mediator.Send(query);
```

### Updating an ActivityStatus
```csharp
var command = new UpdateActivityStatusCommand
{
    Id = activityStatusId,
    Status = "In Progress - High Priority",
    Description = "Activity is currently being worked on with high priority",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an ActivityStatus
```csharp
var command = new DeleteActivityStatusCommand
{
    Id = activityStatusId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The ActivityStatus feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Status name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Status name must be unique across all activity statuses
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Activity statuses are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Completed Statuses
The system identifies which activity statuses represent completion through the GetCompletedActivityStatuses query, which returns statuses that indicate an activity is finished.

### Activity Associations
Each ActivityStatus can be associated with multiple Activity entities. The feature includes functionality to retrieve the count of activities using each status.

## Dependencies
- **FluentValidation**: For validating commands and DTOs
- **AutoMapper**: For mapping between entities and DTOs
- **MediatR**: For handling commands and queries following the CQRS pattern
- **Dapper**: For data access in the repository layer

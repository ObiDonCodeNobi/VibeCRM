# Activity Feature

## Overview
The Activity feature provides functionality for managing activities in the VibeCRM system. Activities represent tasks, events, or interactions that users perform or participate in as part of their daily work with customers, prospects, and partners.

## Domain Model
The Activity entity is a core business entity that represents a scheduled or completed action. Each Activity has the following properties:

- **ActivityId**: Unique identifier (UUID)
- **ActivityDefinitionId**: Reference to the activity definition that categorizes this activity
- **ActivityTypeId**: Reference to the activity type that categorizes this activity
- **ActivityStatusId**: Reference to the status of this activity
- **Subject**: Brief title describing the activity purpose
- **Description**: Detailed description of the activity
- **StartDate**: Date and time when the activity is scheduled to start
- **EndDate**: Date and time when the activity is scheduled to end
- **CompletedDate**: Date and time when the activity was completed (null if not completed)
- **AssignedToUserId**: Reference to the user assigned to complete the activity
- **RelatedToCompanyId**: Optional reference to a related company
- **RelatedToPersonId**: Optional reference to a related person
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **ActivityDefinition**: Navigation property to the associated ActivityDefinition
- **ActivityType**: Navigation property to the associated ActivityType
- **ActivityStatus**: Navigation property to the associated ActivityStatus
- **AssignedToUser**: Navigation property to the assigned user
- **RelatedToCompany**: Navigation property to the related company
- **RelatedToPerson**: Navigation property to the related person

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ActivityDto**: Base DTO with core properties
- **ActivityDetailsDto**: Extended DTO with audit fields and related data
- **ActivityListDto**: Optimized DTO for list views with computed properties

### Commands
- **CreateActivity**: Creates a new activity
- **UpdateActivity**: Updates an existing activity
- **DeleteActivity**: Soft-deletes an activity by setting Active = false
- **CompleteActivity**: Marks an activity as completed by setting the CompletedDate
- **ReassignActivity**: Changes the user assigned to an activity
- **RescheduleActivity**: Updates the start and end dates of an activity

### Queries
- **GetAllActivities**: Retrieves all active activities
- **GetActivityById**: Retrieves a specific activity by its ID
- **GetActivitiesByUser**: Retrieves activities assigned to a specific user
- **GetActivitiesByCompany**: Retrieves activities related to a specific company
- **GetActivitiesByPerson**: Retrieves activities related to a specific person
- **GetActivitiesByStatus**: Retrieves activities with a specific status
- **GetActivitiesByDateRange**: Retrieves activities scheduled within a date range
- **GetOverdueActivities**: Retrieves activities that are past their end date but not completed

### Validators
- **ActivityDtoValidator**: Validates the base DTO
- **ActivityDetailsDtoValidator**: Validates the detailed DTO
- **ActivityListDtoValidator**: Validates the list DTO
- **CreateActivityCommandValidator**: Validates the create command
- **UpdateActivityCommandValidator**: Validates the update command
- **DeleteActivityCommandValidator**: Validates the delete command
- **CompleteActivityCommandValidator**: Validates the complete command
- **ReassignActivityCommandValidator**: Validates the reassign command
- **RescheduleActivityCommandValidator**: Validates the reschedule command
- **GetActivityByIdQueryValidator**: Validates the ID query
- **GetActivitiesByUserQueryValidator**: Validates the user query
- **GetActivitiesByCompanyQueryValidator**: Validates the company query
- **GetActivitiesByPersonQueryValidator**: Validates the person query
- **GetActivitiesByStatusQueryValidator**: Validates the status query
- **GetActivitiesByDateRangeQueryValidator**: Validates the date range query

### Mappings
- **ActivityMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Activity
```csharp
var command = new CreateActivityCommand
{
    ActivityDefinitionId = activityDefinitionId,
    ActivityTypeId = activityTypeId,
    ActivityStatusId = activityStatusId,
    Subject = "Follow up with customer about new product",
    Description = "Call the customer to discuss the new product features and pricing",
    StartDate = DateTime.UtcNow.AddDays(1),
    EndDate = DateTime.UtcNow.AddDays(1).AddHours(1),
    AssignedToUserId = salesRepId,
    RelatedToCompanyId = companyId,
    RelatedToPersonId = personId,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Activities
```csharp
var query = new GetAllActivitiesQuery();
var activities = await _mediator.Send(query);
```

### Retrieving an Activity by ID
```csharp
var query = new GetActivityByIdQuery { Id = activityId };
var activity = await _mediator.Send(query);
```

### Retrieving Activities by user
```csharp
var query = new GetActivitiesByUserQuery { UserId = userId };
var activities = await _mediator.Send(query);
```

### Completing an Activity
```csharp
var command = new CompleteActivityCommand
{
    Id = activityId,
    CompletedDate = DateTime.UtcNow,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating an Activity
```csharp
var command = new UpdateActivityCommand
{
    Id = activityId,
    Subject = "Updated follow-up with customer",
    Description = "Updated description for the follow-up call",
    ActivityStatusId = newStatusId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an Activity
```csharp
var command = new DeleteActivityCommand
{
    Id = activityId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Activity feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Subject is required and limited to 200 characters
- Description is limited to 1000 characters
- StartDate must be a valid date and time
- EndDate must be after StartDate
- CompletedDate must be null or a valid date and time
- ActivityDefinitionId, ActivityTypeId, and ActivityStatusId must reference valid entities
- AssignedToUserId must reference a valid user
- RelatedToCompanyId and RelatedToPersonId must reference valid entities if provided
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Activity Status Workflow
- Activities typically follow a status workflow (e.g., "Not Started" → "In Progress" → "Completed")
- The status determines how the activity is displayed in the UI and what actions are available
- Completing an activity automatically sets its status to "Completed"

### Calendar Integration
- Activities with start and end dates can be displayed on a calendar view
- The system supports filtering activities by date range for calendar display
- Activities can be created and updated directly from the calendar interface

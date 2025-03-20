# ActivityDefinition Feature

## Overview
The ActivityDefinition feature provides functionality for managing predefined activity templates that can be used to create new activities in the VibeCRM system. Activity definitions serve as templates that define the default properties of activities, such as type, status, assigned users/teams, and due date offsets.

## Components

### DTOs
- **ActivityDefinitionDto**: Base DTO containing essential properties for activity definition operations.
- **ActivityDefinitionDetailsDto**: Detailed DTO with additional properties for comprehensive views.
- **ActivityDefinitionListDto**: Simplified DTO for displaying activity definitions in lists.

### Commands
- **CreateActivityDefinition**: Creates a new activity definition in the system.
- **UpdateActivityDefinition**: Updates an existing activity definition.
- **DeleteActivityDefinition**: Soft-deletes an activity definition by setting its Active property to false.

### Queries
- **GetActivityDefinitionById**: Retrieves a specific activity definition by its unique identifier.
- **GetAllActivityDefinitions**: Retrieves all active activity definitions.

### Validators
- **ActivityDefinitionDtoValidator**: Validates the base DTO.
- **ActivityDefinitionDetailsDtoValidator**: Validates the detailed DTO.
- **ActivityDefinitionListDtoValidator**: Validates the list DTO.
- **CreateActivityDefinitionCommandValidator**: Validates the create command.
- **UpdateActivityDefinitionCommandValidator**: Validates the update command.

### Mapping Profiles
- **ActivityDefinitionMappingProfile**: Defines mappings between activity definition entities and DTOs.

## Repository Methods
- **GetAllAsync**: Retrieves all active activity definitions.
- **GetByIdAsync**: Retrieves a specific activity definition by ID.
- **AddAsync**: Adds a new activity definition.
- **UpdateAsync**: Updates an existing activity definition.
- **DeleteAsync**: Soft-deletes an activity definition.
- **GetByNameAsync**: Retrieves an activity definition by its subject/name.
- **GetByActivityTypeAsync**: Retrieves activity definitions by activity type.
- **GetByWorkflowIdAsync**: Retrieves activity definitions associated with a specific workflow.
- **GetByCreatedByAsync**: Retrieves activity definitions created by a specific user.

## Validation Rules
- Activity type and status are required.
- Subject is required and limited to 200 characters.
- Description is optional but limited to 2000 characters.
- Due date offset must be zero or positive.
- Assigned user and team are required.
- Created by and modified by user IDs are required.

## Usage Examples

### Creating a New Activity Definition
```csharp
var command = new CreateActivityDefinitionCommand
{
    ActivityTypeId = Guid.Parse("activity-type-id"),
    ActivityStatusId = Guid.Parse("activity-status-id"),
    AssignedUserId = Guid.Parse("user-id"),
    AssignedTeamId = Guid.Parse("team-id"),
    Subject = "Follow-up Call",
    Description = "Make a follow-up call to discuss the proposal",
    DueDateOffset = 3, // Due 3 days after creation
    CreatedBy = Guid.Parse("current-user-id"),
    ModifiedBy = Guid.Parse("current-user-id")
};

var result = await _mediator.Send(command);
```

### Updating an Activity Definition
```csharp
var command = new UpdateActivityDefinitionCommand
{
    Id = Guid.Parse("activity-definition-id"),
    ActivityTypeId = Guid.Parse("activity-type-id"),
    ActivityStatusId = Guid.Parse("activity-status-id"),
    AssignedUserId = Guid.Parse("user-id"),
    AssignedTeamId = Guid.Parse("team-id"),
    Subject = "Updated Follow-up Call",
    Description = "Updated description",
    DueDateOffset = 5, // Now due 5 days after creation
    ModifiedBy = Guid.Parse("current-user-id")
};

var result = await _mediator.Send(command);
```

### Deleting an Activity Definition
```csharp
var command = new DeleteActivityDefinitionCommand
{
    ActivityDefinitionId = Guid.Parse("activity-definition-id"),
    ModifiedBy = Guid.Parse("current-user-id")
};

var result = await _mediator.Send(command);
```

### Retrieving an Activity Definition
```csharp
var query = new GetActivityDefinitionByIdQuery(Guid.Parse("activity-definition-id"));
var result = await _mediator.Send(query);
```

### Retrieving All Activity Definitions
```csharp
var query = new GetAllActivityDefinitionsQuery();
var result = await _mediator.Send(query);
```

## Notes
- This feature implements soft delete using the `Active` property. When an activity definition is "deleted," its `Active` property is set to `false` rather than removing the record from the database.
- All queries automatically filter out inactive (soft-deleted) activity definitions.
- The repository implementation uses Dapper ORM for data access.

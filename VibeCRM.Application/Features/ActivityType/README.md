# ActivityType Feature

## Overview
The ActivityType feature provides functionality for managing activity types in the VibeCRM system. Activity types categorize different kinds of activities (e.g., Meeting, Call, Email) and help organize and filter activities throughout the system.

## Domain Model
The ActivityType entity is a reference entity that represents a type category for activities. Each ActivityType has the following properties:

- **ActivityTypeId**: Unique identifier (UUID)
- **Type**: Name of the activity type (e.g., "Meeting", "Call", "Email")
- **Description**: Detailed description of what the activity type means
- **OrdinalPosition**: Numeric value for ordering activity types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Activities**: Collection of associated Activity entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ActivityTypeDto**: Base DTO with core properties
- **ActivityTypeDetailsDto**: Extended DTO with audit fields and activity count
- **ActivityTypeListDto**: Optimized DTO for list views

### Commands
- **CreateActivityType**: Creates a new activity type
- **UpdateActivityType**: Updates an existing activity type
- **DeleteActivityType**: Soft-deletes an activity type by setting Active = false

### Queries
- **GetAllActivityTypes**: Retrieves all active activity types
- **GetActivityTypeById**: Retrieves a specific activity type by its ID
- **GetActivityTypeByType**: Retrieves activity types by their type name
- **GetActivityTypeByOrdinalPosition**: Retrieves activity types by their ordinal position
- **GetDefaultActivityType**: Retrieves the default activity type (lowest ordinal position)

### Validators
- **ActivityTypeDtoValidator**: Validates the base DTO
- **ActivityTypeDetailsDtoValidator**: Validates the detailed DTO
- **ActivityTypeListDtoValidator**: Validates the list DTO
- **CreateActivityTypeCommandValidator**: Validates the create command
- **UpdateActivityTypeCommandValidator**: Validates the update command
- **DeleteActivityTypeCommandValidator**: Validates the delete command
- **GetActivityTypeByIdQueryValidator**: Validates the ID query
- **GetActivityTypeByTypeQueryValidator**: Validates the type name query
- **GetActivityTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query

### Mappings
- **ActivityTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new ActivityType
```csharp
var command = new CreateActivityTypeCommand
{
    Type = "Meeting",
    Description = "Face-to-face meeting with client",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all ActivityTypes
```csharp
var query = new GetAllActivityTypesQuery();
var activityTypes = await _mediator.Send(query);
```

### Retrieving an ActivityType by ID
```csharp
var query = new GetActivityTypeByIdQuery { Id = activityTypeId };
var activityType = await _mediator.Send(query);
```

### Retrieving ActivityTypes by type name
```csharp
var query = new GetActivityTypeByTypeQuery { Type = "Meeting" };
var activityType = await _mediator.Send(query);
```

### Retrieving the default ActivityType
```csharp
var query = new GetDefaultActivityTypeQuery();
var defaultActivityType = await _mediator.Send(query);
```

### Updating an ActivityType
```csharp
var command = new UpdateActivityTypeCommand
{
    Id = activityTypeId,
    Type = "Virtual Meeting",
    Description = "Online meeting via video conference",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an ActivityType
```csharp
var command = new DeleteActivityTypeCommand
{
    Id = activityTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The ActivityType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all activity types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Activity types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Activity Associations
Each ActivityType can be associated with multiple Activity entities. The feature includes functionality to retrieve the count of activities using each type.

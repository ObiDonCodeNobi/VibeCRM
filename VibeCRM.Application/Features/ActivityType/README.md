# ActivityType Feature

## Overview
The ActivityType feature provides functionality for managing activity types in the VibeCRM system. Activity types categorize different kinds of activities (e.g., Meeting, Call, Email) and help organize and filter activities throughout the system.

## Architecture
This feature follows the Clean Architecture and CQRS pattern with MediatR:

- **Domain Layer**: Contains the ActivityType entity and repository interface
- **Application Layer**: Contains DTOs, Commands, Queries, Validators, and Mapping Profiles
- **Infrastructure Layer**: Contains the repository implementation

## Components

### DTOs
- **ActivityTypeDto**: Basic DTO with core properties (Id, Type, Description, OrdinalPosition)
- **ActivityTypeListDto**: DTO for list views, includes ActivityCount
- **ActivityTypeDetailsDto**: Detailed DTO with audit fields (CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)

### Commands
- **CreateActivityType**: Creates a new activity type
- **UpdateActivityType**: Updates an existing activity type
- **DeleteActivityType**: Performs a soft delete by setting Active = false

### Queries
- **GetAllActivityTypes**: Retrieves all activity types
- **GetActivityTypeById**: Retrieves an activity type by its ID
- **GetActivityTypeByType**: Retrieves an activity type by its type name
- **GetActivityTypeByOrdinalPosition**: Retrieves an activity type by its ordinal position
- **GetDefaultActivityType**: Retrieves the default activity type (lowest ordinal position)

### Validators
- Validators for all DTOs, Commands, and Queries to ensure data integrity

### Mapping Profile
- Maps between ActivityType entities and DTOs/Commands

## Implementation Details

### Soft Delete
The feature implements soft delete using the `Active` property. When an entity is "deleted", the `Active` property is set to `false` rather than removing the record from the database. All queries filter by `Active = 1` to only show active records.

### Audit Fields
All entities include audit fields (CreatedDate, CreatedBy, ModifiedDate, ModifiedBy) that are automatically set during creation and updates.

### Ordinal Position
Activity types have an ordinal position that determines their display order in lists and dropdowns.

## Usage Examples

### Creating an Activity Type
```csharp
var command = new CreateActivityTypeCommand
{
    Type = "Meeting",
    Description = "Face-to-face meeting with client",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Retrieving All Activity Types
```csharp
var query = new GetAllActivityTypesQuery();
var activityTypes = await _mediator.Send(query);
```

### Updating an Activity Type
```csharp
var command = new UpdateActivityTypeCommand
{
    Id = activityTypeId,
    Type = "Virtual Meeting",
    Description = "Online meeting via video conference",
    OrdinalPosition = 2
};

var result = await _mediator.Send(command);
```

### Deleting an Activity Type
```csharp
var command = new DeleteActivityTypeCommand
{
    Id = activityTypeId
};

var result = await _mediator.Send(command);
```

# CallDirection Feature

## Overview
The CallDirection feature manages call direction types within the VibeCRM system. Call directions represent the flow of a call, such as Inbound, Outbound, or Missed. This feature provides functionality for creating, retrieving, updating, and soft-deleting call direction records.

## Domain Model
The CallDirection entity is a reference entity that represents the direction of a call. Each CallDirection has the following properties:

- **CallDirectionId**: Unique identifier (UUID)
- **Direction**: Name of the direction (e.g., "Inbound", "Outbound", "Missed")
- **Description**: Detailed description of what the direction means
- **OrdinalPosition**: Numeric value for ordering directions in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Calls**: Collection of associated Call entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **CallDirectionDto**: Base DTO with core properties
- **CallDirectionDetailsDto**: Extended DTO with audit fields and call count
- **CallDirectionListDto**: Optimized DTO for list views

### Commands
- **CreateCallDirection**: Creates a new call direction
- **UpdateCallDirection**: Updates an existing call direction
- **DeleteCallDirection**: Soft-deletes a call direction by setting Active = false

### Queries
- **GetAllCallDirections**: Retrieves all active call directions
- **GetCallDirectionById**: Retrieves a specific call direction by its ID
- **GetCallDirectionByDirection**: Retrieves a specific call direction by its direction name
- **GetDefaultCallDirection**: Retrieves the default call direction
- **GetCallDirectionsByOrdinalPosition**: Retrieves call directions ordered by position

### Validators
- **CallDirectionDtoValidator**: Validates the base DTO
- **CallDirectionDetailsDtoValidator**: Validates the detailed DTO
- **CallDirectionListDtoValidator**: Validates the list DTO
- **GetCallDirectionByIdQueryValidator**: Validates the ID query
- **GetCallDirectionByDirectionQueryValidator**: Validates the direction name query
- **GetCallDirectionsByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllCallDirectionsQueryValidator**: Validates the "get all" query

### Mappings
- **CallDirectionMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new CallDirection
```csharp
var command = new CreateCallDirectionCommand
{
    Direction = "Inbound",
    Description = "Incoming call from a customer",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var callDirectionId = await _mediator.Send(command);
```

### Retrieving all CallDirections
```csharp
var query = new GetAllCallDirectionsQuery();
var callDirections = await _mediator.Send(query);
```

### Retrieving CallDirections by ordinal position
```csharp
var orderedQuery = new GetCallDirectionsByOrdinalPositionQuery();
var orderedCallDirections = await _mediator.Send(orderedQuery);
```

### Retrieving default CallDirection
```csharp
var defaultQuery = new GetDefaultCallDirectionQuery();
var defaultCallDirection = await _mediator.Send(defaultQuery);
```

### Updating a CallDirection
```csharp
var command = new UpdateCallDirectionCommand
{
    Id = callDirectionId,
    Direction = "Inbound",
    Description = "Updated description for incoming calls",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

### Deleting a CallDirection
```csharp
var command = new DeleteCallDirectionCommand
{
    Id = callDirectionId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The CallDirection feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Direction name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Call directions are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Call Associations
Each CallDirection can be associated with multiple Call entities. The feature includes functionality to retrieve the count of calls using each direction.

# CallDirection Feature

## Overview
The CallDirection feature manages call direction types within the VibeCRM system. Call directions represent the flow of a call, such as Inbound, Outbound, or Missed. This feature provides a complete implementation following Clean Architecture and CQRS principles.

## Components

### Domain
- `CallDirection` entity in the Domain layer with properties:
  - Id (Guid)
  - Direction (string)
  - Description (string)
  - OrdinalPosition (int)
  - Audit fields (CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
  - Active (bool) for soft delete functionality

### DTOs
- `CallDirectionDto`: Basic properties for general use
- `CallDirectionListDto`: Extended properties for list views, including activity count
- `CallDirectionDetailsDto`: Complete properties including audit fields

### Commands
- `CreateCallDirection`: Creates a new call direction
- `UpdateCallDirection`: Updates an existing call direction
- `DeleteCallDirection`: Soft deletes a call direction by setting Active = false

### Queries
- `GetAllCallDirections`: Retrieves all active call directions
- `GetCallDirectionById`: Retrieves a specific call direction by ID
- `GetCallDirectionByDirection`: Retrieves a call direction by its direction name
- `GetDefaultCallDirection`: Retrieves the default call direction
- `GetCallDirectionsByOrdinalPosition`: Retrieves call directions ordered by position

### Validators
- Validators for all DTOs and commands to ensure data integrity

### Mapping Profiles
- `CallDirectionMappingProfile`: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a Call Direction
```csharp
var command = new CreateCallDirectionCommand
{
    Direction = "Inbound",
    Description = "Incoming call from a customer",
    OrdinalPosition = 1,
    CreatedBy = currentUserId
};

var callDirectionId = await _mediator.Send(command);
```

### Updating a Call Direction
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

### Retrieving Call Directions
```csharp
// Get all call directions
var query = new GetAllCallDirectionsQuery();
var callDirections = await _mediator.Send(query);

// Get call directions ordered by position
var orderedQuery = new GetCallDirectionsByOrdinalPositionQuery();
var orderedCallDirections = await _mediator.Send(orderedQuery);

// Get default call direction
var defaultQuery = new GetDefaultCallDirectionQuery();
var defaultCallDirection = await _mediator.Send(defaultQuery);
```

## Implementation Notes
- Soft delete is implemented using the `Active` property
- All queries filter by `Active = true` to exclude soft-deleted records
- The `OrdinalPosition` property is used for sorting call directions in dropdowns and lists
- The default call direction is determined by the repository implementation

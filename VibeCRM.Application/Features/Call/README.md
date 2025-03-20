# Call Feature

## Overview
The Call feature provides functionality for managing call records within the VibeCRM system. Calls can be associated with companies and persons, and include metadata such as type, status, direction, and duration. This feature follows the CQRS pattern with MediatR for command and query separation.

## Key Components

### DTOs
- `CallDto`: Base DTO containing common call properties
- `CallDetailsDto`: Detailed DTO with additional information for single call views
- `CallListDto`: Simplified DTO for displaying calls in lists

### Commands
- `CreateCall`: Creates a new call in the system
- `UpdateCall`: Updates an existing call's properties
- `DeleteCall`: Soft-deletes a call by setting its Active property to false

### Queries
- `GetCallById`: Retrieves a specific call by its unique identifier
- `GetAllCalls`: Retrieves all active calls in the system

### Validators
- Ensures all call data meets the required validation rules using FluentValidation

## Implementation Details

### Soft Delete Pattern
- All delete operations are implemented as soft deletes by setting the `Active` property to `false`
- All queries automatically filter by `Active = 1` to exclude soft-deleted records

### Mapping
- AutoMapper profiles are used to map between entities and DTOs
- The mapping configuration handles the relationship between the entity's `Id` and `CallId` properties

## Usage Examples

### Creating a Call
```csharp
var command = new CreateCallCommand
{
    TypeId = Guid.Parse("call-type-id"),
    StatusId = Guid.Parse("call-status-id"),
    DirectionId = Guid.Parse("call-direction-id"),
    Description = "Customer inquiry about product features",
    Duration = 300, // 5 minutes in seconds
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await mediator.Send(command);
```

### Retrieving a Call
```csharp
var query = new GetCallByIdQuery(callId);
var call = await mediator.Send(query);
```

### Updating a Call
```csharp
var command = new UpdateCallCommand
{
    Id = callId,
    TypeId = Guid.Parse("new-call-type-id"),
    StatusId = Guid.Parse("new-call-status-id"),
    DirectionId = Guid.Parse("call-direction-id"),
    Description = "Updated description with additional details",
    Duration = 450, // 7.5 minutes in seconds
    ModifiedBy = currentUserId
};

var result = await mediator.Send(command);
```

### Deleting a Call
```csharp
var command = new DeleteCallCommand(callId, currentUserId);
var success = await mediator.Send(command);
```

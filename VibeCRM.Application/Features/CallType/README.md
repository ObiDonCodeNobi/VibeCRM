# CallType Feature

## Overview
The CallType feature provides functionality for managing call types in the VibeCRM system. Call types are used to categorize calls for organization and reporting purposes, such as Sales, Support, Follow-up, etc.

## Domain Model
The CallType entity is a reference entity that represents a type category for calls. Each CallType has the following properties:

- **CallTypeId**: Unique identifier (UUID)
- **Type**: Name of the call type (e.g., "Sales Call", "Support Call", "Follow-up")
- **Description**: Detailed description of what the call type means
- **OrdinalPosition**: Numeric value for ordering call types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Calls**: Collection of associated Call entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **CallTypeDto**: Base DTO with core properties
- **CallTypeDetailsDto**: Extended DTO with audit fields and call count
- **CallTypeListDto**: Optimized DTO for list views

### Commands
- **CreateCallType**: Creates a new call type
- **UpdateCallType**: Updates an existing call type
- **DeleteCallType**: Soft-deletes a call type by setting Active = false

### Queries
- **GetAllCallTypes**: Retrieves all active call types
- **GetCallTypeById**: Retrieves a specific call type by its ID
- **GetCallTypeByType**: Retrieves call types by their type name
- **GetDefaultCallType**: Retrieves the default call type (lowest ordinal position)
- **GetCallTypesByOrdinalPosition**: Retrieves call types ordered by position
- **GetInboundCallTypes**: Retrieves call types used for inbound calls
- **GetOutboundCallTypes**: Retrieves call types used for outbound calls

### Validators
- **CallTypeDtoValidator**: Validates the base DTO
- **CallTypeDetailsDtoValidator**: Validates the detailed DTO
- **CallTypeListDtoValidator**: Validates the list DTO
- **CreateCallTypeCommandValidator**: Validates the create command
- **UpdateCallTypeCommandValidator**: Validates the update command
- **DeleteCallTypeCommandValidator**: Validates the delete command
- **GetCallTypeByIdQueryValidator**: Validates the ID query
- **GetCallTypeByTypeQueryValidator**: Validates the type name query
- **GetCallTypesByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllCallTypesQueryValidator**: Validates the "get all" query
- **GetInboundCallTypesQueryValidator**: Validates the inbound query
- **GetOutboundCallTypesQueryValidator**: Validates the outbound query

### Mappings
- **CallTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new CallType
```csharp
var command = new CreateCallTypeCommand
{
    Type = "Sales Call",
    Description = "Call related to sales activities",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all CallTypes
```csharp
var query = new GetAllCallTypesQuery();
var callTypes = await _mediator.Send(query);
```

### Retrieving a CallType by ID
```csharp
var query = new GetCallTypeByIdQuery { Id = callTypeId };
var callType = await _mediator.Send(query);
```

### Retrieving CallTypes by type name
```csharp
var query = new GetCallTypeByTypeQuery { Type = "Sales" };
var callTypes = await _mediator.Send(query);
```

### Retrieving the default CallType
```csharp
var query = new GetDefaultCallTypeQuery();
var defaultCallType = await _mediator.Send(query);
```

### Retrieving inbound CallTypes
```csharp
var query = new GetInboundCallTypesQuery();
var inboundCallTypes = await _mediator.Send(query);
```

### Retrieving outbound CallTypes
```csharp
var query = new GetOutboundCallTypesQuery();
var outboundCallTypes = await _mediator.Send(query);
```

### Updating a CallType
```csharp
var command = new UpdateCallTypeCommand
{
    Id = callTypeId,
    Type = "Updated Sales Call",
    Description = "Updated description for sales calls",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a CallType
```csharp
var command = new DeleteCallTypeCommand
{
    Id = callTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The CallType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all call types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Call types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Call Associations
Each CallType can be associated with multiple Call entities. The feature includes functionality to retrieve the count of calls using each type.

### Inbound vs Outbound Types
The system supports categorizing call types as either inbound or outbound through specialized queries, which helps in reporting and filtering calls by direction.

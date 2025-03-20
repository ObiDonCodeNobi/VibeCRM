# CallType Feature

## Overview
The CallType feature provides functionality for managing call types in the VibeCRM system. Call types are used to categorize calls for organization and reporting purposes, such as Sales, Support, Follow-up, etc.

## Components

### DTOs
- **CallTypeDto**: Basic DTO containing essential call type properties.
- **CallTypeListDto**: Extended DTO for list displays, including activity count and direction indicators.
- **CallTypeDetailsDto**: Complete DTO including all properties and audit fields.

### Commands
- **CreateCallType**: Creates a new call type.
- **UpdateCallType**: Updates an existing call type.
- **DeleteCallType**: Soft deletes an existing call type by setting Active = false.

### Queries
- **GetAllCallTypes**: Retrieves all active call types.
- **GetCallTypeById**: Retrieves a specific call type by its ID.
- **GetCallTypeByType**: Retrieves call types by type name.
- **GetDefaultCallType**: Retrieves the default call type (typically the one with the lowest ordinal position).
- **GetCallTypesByOrdinalPosition**: Retrieves call types ordered by their ordinal position.
- **GetInboundCallTypes**: Retrieves call types used for inbound calls.
- **GetOutboundCallTypes**: Retrieves call types used for outbound calls.

### Validators
- **CallTypeDtoValidator**: Validates the basic CallTypeDto.
- **CallTypeListDtoValidator**: Validates the CallTypeListDto.
- **CallTypeDetailsDtoValidator**: Validates the CallTypeDetailsDto.
- **CreateCallTypeCommandValidator**: Validates the CreateCallTypeCommand.
- **UpdateCallTypeCommandValidator**: Validates the UpdateCallTypeCommand.
- **DeleteCallTypeCommandValidator**: Validates the DeleteCallTypeCommand.

### Mappings
- **CallTypeMappingProfile**: Defines mappings between CallType entities and DTOs.

## Usage Examples

### Creating a Call Type
```csharp
var createCommand = new CreateCallTypeCommand
{
    Type = "Sales Call",
    Description = "Call related to sales activities",
    OrdinalPosition = 1
};

var result = await _mediator.Send(createCommand);
```

### Updating a Call Type
```csharp
var updateCommand = new UpdateCallTypeCommand
{
    Id = callTypeId,
    Type = "Updated Sales Call",
    Description = "Updated description for sales calls",
    OrdinalPosition = 2
};

var result = await _mediator.Send(updateCommand);
```

### Deleting a Call Type
```csharp
var deleteCommand = new DeleteCallTypeCommand
{
    Id = callTypeId
};

var result = await _mediator.Send(deleteCommand);
```

### Retrieving Call Types
```csharp
// Get all call types
var allCallTypes = await _mediator.Send(new GetAllCallTypesQuery());

// Get call type by ID
var callTypeById = await _mediator.Send(new GetCallTypeByIdQuery { Id = callTypeId });

// Get call types by type name
var callTypesByType = await _mediator.Send(new GetCallTypeByTypeQuery { Type = "Sales" });

// Get default call type
var defaultCallType = await _mediator.Send(new GetDefaultCallTypeQuery());

// Get call types ordered by ordinal position
var orderedCallTypes = await _mediator.Send(new GetCallTypesByOrdinalPositionQuery());

// Get inbound call types
var inboundCallTypes = await _mediator.Send(new GetInboundCallTypesQuery());

// Get outbound call types
var outboundCallTypes = await _mediator.Send(new GetOutboundCallTypesQuery());
```

## Implementation Notes
- The feature follows Clean Architecture and CQRS principles.
- Soft delete is implemented using the `Active` property, ensuring that deleted records are not returned in queries.
- Call types can be categorized as inbound or outbound based on naming conventions.
- The default call type is determined by the lowest ordinal position.

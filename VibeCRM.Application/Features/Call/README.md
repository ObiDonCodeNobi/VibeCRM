# Call Feature

## Overview
The Call feature provides functionality for managing call records within the VibeCRM system. Calls can be associated with companies and persons, and include metadata such as type, status, direction, and duration. This feature follows the CQRS pattern with MediatR for command and query separation.

## Domain Model
The Call entity is a core business entity that represents a phone call interaction in the CRM system. Each Call has the following properties:

- **CallId**: Unique identifier (UUID)
- **CallTypeId**: Reference to the type of call (e.g., Sales, Support, Follow-up)
- **CallStatusId**: Reference to the status of the call (e.g., Scheduled, Completed, Missed)
- **CallDirectionId**: Reference to the direction of the call (e.g., Inbound, Outbound)
- **Subject**: Brief subject line for the call
- **Description**: Detailed notes about the call
- **ScheduledStart**: Date and time when the call is scheduled to start
- **ScheduledEnd**: Date and time when the call is scheduled to end
- **ActualStart**: Date and time when the call actually started
- **ActualEnd**: Date and time when the call actually ended
- **Duration**: Duration of the call in seconds
- **PersonId**: Optional reference to the associated person
- **CompanyId**: Optional reference to the associated company
- **AssignedToUserId**: Reference to the user assigned to the call
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **CallType**: Navigation property to the associated CallType
- **CallStatus**: Navigation property to the associated CallStatus
- **CallDirection**: Navigation property to the associated CallDirection
- **Person**: Navigation property to the associated Person (if applicable)
- **Company**: Navigation property to the associated Company (if applicable)
- **AssignedToUser**: Navigation property to the assigned user

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **CallDto**: Base DTO with core properties
- **CallDetailsDto**: Extended DTO with audit fields and related data
- **CallListDto**: Optimized DTO for list views

### Commands
- **CreateCall**: Creates a new call
- **UpdateCall**: Updates an existing call
- **DeleteCall**: Soft-deletes a call by setting Active = false
- **CompleteCall**: Marks a call as completed with actual start/end times
- **RescheduleCall**: Updates the scheduled start/end times for a call
- **AssignCall**: Assigns a call to a different user

### Queries
- **GetAllCalls**: Retrieves all active calls
- **GetCallById**: Retrieves a specific call by its ID
- **GetCallsByPerson**: Retrieves calls associated with a specific person
- **GetCallsByCompany**: Retrieves calls associated with a specific company
- **GetCallsByUser**: Retrieves calls assigned to a specific user
- **GetCallsByDateRange**: Retrieves calls scheduled within a date range
- **GetCallsByStatus**: Retrieves calls with a specific status
- **GetCallsByType**: Retrieves calls of a specific type
- **GetCallsByDirection**: Retrieves calls with a specific direction

### Validators
- **CallDtoValidator**: Validates the base DTO
- **CallDetailsDtoValidator**: Validates the detailed DTO
- **CallListDtoValidator**: Validates the list DTO
- **CreateCallCommandValidator**: Validates the create command
- **UpdateCallCommandValidator**: Validates the update command
- **DeleteCallCommandValidator**: Validates the delete command
- **CompleteCallCommandValidator**: Validates the complete command
- **RescheduleCallCommandValidator**: Validates the reschedule command
- **AssignCallCommandValidator**: Validates the assign command
- **GetCallByIdQueryValidator**: Validates the ID query
- **GetCallsByPersonQueryValidator**: Validates the person query
- **GetCallsByCompanyQueryValidator**: Validates the company query
- **GetCallsByUserQueryValidator**: Validates the user query
- **GetCallsByDateRangeQueryValidator**: Validates the date range query
- **GetCallsByStatusQueryValidator**: Validates the status query
- **GetCallsByTypeQueryValidator**: Validates the type query
- **GetCallsByDirectionQueryValidator**: Validates the direction query

### Mappings
- **CallMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Call
```csharp
var command = new CreateCallCommand
{
    CallTypeId = callTypeId,
    CallStatusId = callStatusId,
    CallDirectionId = callDirectionId,
    Subject = "Product Demo Call",
    Description = "Scheduled demo of our premium features",
    ScheduledStart = DateTime.UtcNow.AddDays(1),
    ScheduledEnd = DateTime.UtcNow.AddDays(1).AddHours(1),
    PersonId = personId,
    CompanyId = companyId,
    AssignedToUserId = salesRepId,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Calls
```csharp
var query = new GetAllCallsQuery();
var calls = await _mediator.Send(query);
```

### Retrieving a Call by ID
```csharp
var query = new GetCallByIdQuery { Id = callId };
var call = await _mediator.Send(query);
```

### Completing a Call
```csharp
var command = new CompleteCallCommand
{
    Id = callId,
    ActualStart = DateTime.UtcNow.AddHours(-1),
    ActualEnd = DateTime.UtcNow,
    Duration = 3600, // 1 hour in seconds
    Description = "Completed demo call. Client was very interested in our premium features.",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Call
```csharp
var command = new UpdateCallCommand
{
    Id = callId,
    CallTypeId = newCallTypeId,
    CallStatusId = newCallStatusId,
    Subject = "Updated Product Demo Call",
    Description = "Updated description with additional details",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Call
```csharp
var command = new DeleteCallCommand
{
    Id = callId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Call feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Subject is required and limited to 200 characters
- Description is optional but limited to 2000 characters
- CallTypeId, CallStatusId, and CallDirectionId must reference valid entities
- ScheduledStart and ScheduledEnd are required for new calls
- ScheduledEnd must be after ScheduledStart
- ActualStart and ActualEnd are required when completing a call
- ActualEnd must be after ActualStart
- Duration must be non-negative and consistent with start/end times
- AssignedToUserId must reference a valid user
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Call Duration Calculation
- Duration is stored in seconds for precision
- When completing a call, duration can be provided directly or calculated from ActualStart and ActualEnd
- The UI displays duration in a human-readable format (e.g., "1h 30m")

### Call Scheduling
- Calls can be scheduled for future dates
- The system provides calendar views for scheduled calls
- Reminders can be configured for upcoming calls
- Conflicts with other scheduled calls are detected and reported

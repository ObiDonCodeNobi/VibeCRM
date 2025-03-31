# PersonStatus Feature

## Overview
The PersonStatus feature provides functionality for managing person status entities in the VibeCRM system. Person statuses represent the current state of a person in the system (e.g., "Active", "Inactive", "Lead", "Customer", etc.) and are used to categorize and filter people.

## Domain Model
The PersonStatus entity is a reference entity that represents the status of a person. Each PersonStatus has the following properties:

- **PersonStatusId**: Unique identifier (UUID)
- **Status**: Name of the person status (e.g., "Active", "Inactive", "Lead")
- **Description**: Detailed description of what the status means
- **OrdinalPosition**: Numeric value for ordering statuses in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **People**: Collection of associated Person entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PersonStatusDto**: Base DTO with core properties
- **PersonStatusDetailsDto**: Extended DTO with audit fields and people count
- **PersonStatusListDto**: Optimized DTO for list views

### Commands
- **CreatePersonStatus**: Creates a new person status
- **UpdatePersonStatus**: Updates an existing person status
- **DeletePersonStatus**: Soft-deletes a person status by setting Active = false

### Queries
- **GetAllPersonStatuses**: Retrieves all active person statuses
- **GetPersonStatusById**: Retrieves a specific person status by its ID
- **GetPersonStatusByStatus**: Retrieves person statuses by their status name
- **GetPersonStatusByOrdinalPosition**: Retrieves person statuses by their ordinal position
- **GetDefaultPersonStatus**: Retrieves the default person status (lowest ordinal position)

### Validators
- **PersonStatusDtoValidator**: Validates the base DTO
- **PersonStatusDetailsDtoValidator**: Validates the detailed DTO
- **PersonStatusListDtoValidator**: Validates the list DTO
- **CreatePersonStatusCommandValidator**: Validates the create command
- **UpdatePersonStatusCommandValidator**: Validates the update command
- **DeletePersonStatusCommandValidator**: Validates the delete command
- **GetPersonStatusByIdQueryValidator**: Validates the ID query
- **GetPersonStatusByStatusQueryValidator**: Validates the status name query
- **GetPersonStatusByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllPersonStatusesQueryValidator**: Validates the "get all" query

### Mappings
- **PersonStatusMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new PersonStatus
```csharp
var command = new CreatePersonStatusCommand
{
    Status = "Active",
    Description = "Person is currently active in the system",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all PersonStatuses
```csharp
var query = new GetAllPersonStatusesQuery();
var personStatuses = await _mediator.Send(query);
```

### Retrieving a PersonStatus by ID
```csharp
var query = new GetPersonStatusByIdQuery { Id = personStatusId };
var personStatus = await _mediator.Send(query);
```

### Retrieving PersonStatuses by status name
```csharp
var query = new GetPersonStatusByStatusQuery { Status = "Active" };
var personStatus = await _mediator.Send(query);
```

### Retrieving the default PersonStatus
```csharp
var query = new GetDefaultPersonStatusQuery();
var defaultPersonStatus = await _mediator.Send(query);
```

### Updating a PersonStatus
```csharp
var command = new UpdatePersonStatusCommand
{
    Id = personStatusId,
    Status = "Active Customer",
    Description = "Person is an active customer",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a PersonStatus
```csharp
var command = new DeletePersonStatusCommand
{
    Id = personStatusId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The PersonStatus feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Status name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Status name must be unique across all person statuses
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Person statuses are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Person Associations
Each PersonStatus can be associated with multiple Person entities. The feature includes functionality to retrieve the count of people using each status.

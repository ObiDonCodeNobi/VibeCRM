# QuoteStatus Feature

## Overview
The QuoteStatus feature provides functionality for managing quote statuses in the VibeCRM system. Quote statuses represent the different states a quote can be in, such as Draft, Sent, Accepted, Rejected, etc.

## Domain Model
The QuoteStatus entity is a reference entity that represents the status of a quote. Each QuoteStatus has the following properties:

- **QuoteStatusId**: Unique identifier (UUID)
- **Status**: Name of the quote status (e.g., "Draft", "Sent", "Accepted", "Rejected")
- **Description**: Detailed description of what the status means
- **OrdinalPosition**: Numeric value for ordering statuses in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Quotes**: Collection of associated Quote entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **QuoteStatusDto**: Base DTO with core properties
- **QuoteStatusDetailsDto**: Extended DTO with audit fields and quote count
- **QuoteStatusListDto**: Optimized DTO for list views

### Commands
- **CreateQuoteStatus**: Creates a new quote status
- **UpdateQuoteStatus**: Updates an existing quote status
- **DeleteQuoteStatus**: Soft-deletes a quote status by setting Active = false

### Queries
- **GetAllQuoteStatuses**: Retrieves all active quote statuses
- **GetQuoteStatusById**: Retrieves a specific quote status by its ID
- **GetQuoteStatusByStatus**: Retrieves quote statuses by their status name
- **GetQuoteStatusByOrdinalPosition**: Retrieves quote statuses by their ordinal position
- **GetDefaultQuoteStatus**: Retrieves the default quote status (lowest ordinal position)

### Validators
- **QuoteStatusDtoValidator**: Validates the base DTO
- **QuoteStatusDetailsDtoValidator**: Validates the detailed DTO
- **QuoteStatusListDtoValidator**: Validates the list DTO
- **CreateQuoteStatusCommandValidator**: Validates the create command
- **UpdateQuoteStatusCommandValidator**: Validates the update command
- **DeleteQuoteStatusCommandValidator**: Validates the delete command
- **GetQuoteStatusByIdQueryValidator**: Validates the ID query
- **GetQuoteStatusByStatusQueryValidator**: Validates the status name query
- **GetQuoteStatusByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllQuoteStatusesQueryValidator**: Validates the "get all" query

### Mappings
- **QuoteStatusMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new QuoteStatus
```csharp
var command = new CreateQuoteStatusCommand
{
    Status = "Draft",
    Description = "Initial quote that has not been sent to the customer",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all QuoteStatuses
```csharp
var query = new GetAllQuoteStatusesQuery();
var quoteStatuses = await _mediator.Send(query);
```

### Retrieving a QuoteStatus by ID
```csharp
var query = new GetQuoteStatusByIdQuery { Id = quoteStatusId };
var quoteStatus = await _mediator.Send(query);
```

### Retrieving QuoteStatuses by status name
```csharp
var query = new GetQuoteStatusByStatusQuery { Status = "Draft" };
var quoteStatus = await _mediator.Send(query);
```

### Retrieving the default QuoteStatus
```csharp
var query = new GetDefaultQuoteStatusQuery();
var defaultQuoteStatus = await _mediator.Send(query);
```

### Updating a QuoteStatus
```csharp
var command = new UpdateQuoteStatusCommand
{
    Id = quoteStatusId,
    Status = "Updated Draft",
    Description = "Updated description for draft quotes",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a QuoteStatus
```csharp
var command = new DeleteQuoteStatusCommand
{
    Id = quoteStatusId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The QuoteStatus feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Status name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Status name must be unique across all quote statuses
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Quote statuses are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Quote Associations
Each QuoteStatus can be associated with multiple Quote entities. The feature includes functionality to retrieve the count of quotes using each status.

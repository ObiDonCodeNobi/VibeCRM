# QuoteStatus Feature

## Overview
The QuoteStatus feature provides functionality for managing quote statuses in the VibeCRM system. Quote statuses represent the different states a quote can be in, such as Draft, Sent, Accepted, Rejected, etc.

## Architecture
This feature follows Clean Architecture principles and the CQRS pattern:

- **Domain Layer**: Contains the QuoteStatus entity and repository interface
- **Application Layer**: Contains commands, queries, DTOs, validators, and mapping profiles
- **Infrastructure Layer**: Contains the repository implementation

## Commands
- **CreateQuoteStatus**: Creates a new quote status
- **UpdateQuoteStatus**: Updates an existing quote status
- **DeleteQuoteStatus**: Soft-deletes a quote status (sets Active = false)

## Queries
- **GetAllQuoteStatuses**: Retrieves all active quote statuses
- **GetQuoteStatusById**: Retrieves a specific quote status by its ID
- **GetQuoteStatusByOrdinalPosition**: Retrieves quote statuses by their ordinal position
- **GetQuoteStatusByStatus**: Retrieves quote statuses by their status name
- **GetDefaultQuoteStatus**: Retrieves the default quote status

## DTOs
- **QuoteStatusDto**: Basic information for quote statuses
- **QuoteStatusListDto**: Information for listing quote statuses, includes quote count
- **QuoteStatusDetailsDto**: Detailed information including audit fields

## Validation Rules
- Status name is required and cannot exceed 50 characters
- Description is required and cannot exceed 500 characters
- Ordinal position must be a non-negative number
- Status name must be unique across all quote statuses
- Quote count must be a non-negative number

## Soft Delete Implementation
This feature follows the standardized soft delete pattern used throughout the VibeCRM system:
- All entities have a boolean `Active` property that defaults to `true`
- When an entity is "deleted", the `Active` property is set to `false`
- All queries filter by `Active = 1` to only show active records
- The `DeleteAsync` method in repositories sets `Active = 0`

## Usage Examples

### Create a new quote status
```csharp
var command = new CreateQuoteStatusCommand
{
    Status = "Draft",
    Description = "Initial quote that has not been sent to the customer",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Update an existing quote status
```csharp
var command = new UpdateQuoteStatusCommand
{
    Id = quoteStatusId,
    Status = "Updated Draft",
    Description = "Updated description for draft quotes",
    OrdinalPosition = 2
};

var result = await _mediator.Send(command);
```

### Delete a quote status
```csharp
var command = new DeleteQuoteStatusCommand
{
    Id = quoteStatusId
};

var result = await _mediator.Send(command);
```

### Get all quote statuses
```csharp
var query = new GetAllQuoteStatusesQuery();
var result = await _mediator.Send(query);
```

### Get a quote status by ID
```csharp
var query = new GetQuoteStatusByIdQuery { Id = quoteStatusId };
var result = await _mediator.Send(query);
```

### Get quote statuses by ordinal position
```csharp
var query = new GetQuoteStatusByOrdinalPositionQuery { OrdinalPosition = 1 };
var result = await _mediator.Send(query);
```

### Get quote statuses by status name
```csharp
var query = new GetQuoteStatusByStatusQuery { Status = "Draft" };
var result = await _mediator.Send(query);
```

### Get the default quote status
```csharp
var query = new GetDefaultQuoteStatusQuery();
var result = await _mediator.Send(query);
```

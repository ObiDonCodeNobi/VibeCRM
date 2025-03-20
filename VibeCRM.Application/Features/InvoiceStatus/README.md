# InvoiceStatus Feature

## Overview
The InvoiceStatus feature provides functionality for managing invoice statuses in the VibeCRM system. Invoice statuses represent the various states an invoice can be in throughout its lifecycle (e.g., Draft, Sent, Paid, Overdue).

## Architecture
This feature follows the Clean Architecture and CQRS pattern using MediatR:

- **Domain Layer**: Contains the InvoiceStatus entity
- **Application Layer**: Contains commands, queries, DTOs, validators, and mapping profiles
- **Infrastructure Layer**: Contains the repository implementation

## Components

### Entity
- `InvoiceStatus`: Represents an invoice status with properties such as ID, status name, description, and ordinal position.

### DTOs
- `InvoiceStatusDto`: Basic DTO for invoice status
- `InvoiceStatusDetailsDto`: Detailed DTO including audit information
- `InvoiceStatusListDto`: DTO for listing invoice statuses with additional information like invoice count

### Commands
- `CreateInvoiceStatusCommand`: Creates a new invoice status
- `UpdateInvoiceStatusCommand`: Updates an existing invoice status
- `DeleteInvoiceStatusCommand`: Soft deletes an invoice status by setting Active = false

### Queries
- `GetAllInvoiceStatusesQuery`: Retrieves all invoice statuses
- `GetInvoiceStatusByIdQuery`: Retrieves an invoice status by its ID
- `GetInvoiceStatusByStatusQuery`: Retrieves an invoice status by its status name
- `GetInvoiceStatusByOrdinalPositionQuery`: Retrieves invoice statuses ordered by ordinal position
- `GetDefaultInvoiceStatusQuery`: Retrieves the default invoice status

### Validators
- Validators for all DTOs and commands to ensure data integrity

### Repository
- `IInvoiceStatusRepository`: Interface defining repository methods
- `InvoiceStatusRepository`: Implementation of the repository using Dapper ORM

## Usage Examples

### Creating an Invoice Status
```csharp
var command = new CreateInvoiceStatusCommand
{
    Status = "Draft",
    Description = "Invoice is in draft state",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Updating an Invoice Status
```csharp
var command = new UpdateInvoiceStatusCommand
{
    Id = invoiceStatusId,
    Status = "Updated Draft",
    Description = "Updated description",
    OrdinalPosition = 2
};

var result = await _mediator.Send(command);
```

### Deleting an Invoice Status
```csharp
var command = new DeleteInvoiceStatusCommand
{
    Id = invoiceStatusId
};

var result = await _mediator.Send(command);
```

### Retrieving Invoice Statuses
```csharp
// Get all invoice statuses
var allStatuses = await _mediator.Send(new GetAllInvoiceStatusesQuery());

// Get invoice status by ID
var statusById = await _mediator.Send(new GetInvoiceStatusByIdQuery { Id = invoiceStatusId });

// Get invoice status by status name
var statusByName = await _mediator.Send(new GetInvoiceStatusByStatusQuery { Status = "Draft" });

// Get invoice statuses ordered by ordinal position
var orderedStatuses = await _mediator.Send(new GetInvoiceStatusByOrdinalPositionQuery());

// Get default invoice status
var defaultStatus = await _mediator.Send(new GetDefaultInvoiceStatusQuery());
```

## Notes
- The feature implements soft delete using the `Active` property.
- All queries filter by `Active = true` to exclude soft-deleted records.
- The `OrdinalPosition` property allows for custom ordering of invoice statuses in UI displays.

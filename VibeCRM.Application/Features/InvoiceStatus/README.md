# InvoiceStatus Feature

## Overview
The InvoiceStatus feature provides functionality for managing invoice statuses in the VibeCRM system. Invoice statuses represent the various states an invoice can be in throughout its lifecycle (e.g., Draft, Sent, Paid, Overdue).

## Domain Model
The InvoiceStatus entity is a reference entity that represents the status of an invoice. Each InvoiceStatus has the following properties:

- **InvoiceStatusId**: Unique identifier (UUID)
- **Status**: Name of the invoice status (e.g., "Draft", "Sent", "Paid", "Overdue")
- **Description**: Detailed description of what the status means
- **OrdinalPosition**: Numeric value for ordering statuses in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Invoices**: Collection of associated Invoice entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **InvoiceStatusDto**: Base DTO with core properties
- **InvoiceStatusDetailsDto**: Extended DTO with audit fields and invoice count
- **InvoiceStatusListDto**: Optimized DTO for list views

### Commands
- **CreateInvoiceStatus**: Creates a new invoice status
- **UpdateInvoiceStatus**: Updates an existing invoice status
- **DeleteInvoiceStatus**: Soft-deletes an invoice status by setting Active = false

### Queries
- **GetAllInvoiceStatuses**: Retrieves all active invoice statuses
- **GetInvoiceStatusById**: Retrieves a specific invoice status by its ID
- **GetInvoiceStatusByStatus**: Retrieves invoice statuses by their status name
- **GetInvoiceStatusByOrdinalPosition**: Retrieves invoice statuses by their ordinal position
- **GetDefaultInvoiceStatus**: Retrieves the default invoice status (lowest ordinal position)

### Validators
- **InvoiceStatusDtoValidator**: Validates the base DTO
- **InvoiceStatusDetailsDtoValidator**: Validates the detailed DTO
- **InvoiceStatusListDtoValidator**: Validates the list DTO
- **CreateInvoiceStatusCommandValidator**: Validates the create command
- **UpdateInvoiceStatusCommandValidator**: Validates the update command
- **DeleteInvoiceStatusCommandValidator**: Validates the delete command
- **GetInvoiceStatusByIdQueryValidator**: Validates the ID query
- **GetInvoiceStatusByStatusQueryValidator**: Validates the status name query
- **GetInvoiceStatusByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllInvoiceStatusesQueryValidator**: Validates the "get all" query

### Mappings
- **InvoiceStatusMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new InvoiceStatus
```csharp
var command = new CreateInvoiceStatusCommand
{
    Status = "Draft",
    Description = "Invoice is in draft state",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all InvoiceStatuses
```csharp
var query = new GetAllInvoiceStatusesQuery();
var invoiceStatuses = await _mediator.Send(query);
```

### Retrieving an InvoiceStatus by ID
```csharp
var query = new GetInvoiceStatusByIdQuery { Id = invoiceStatusId };
var invoiceStatus = await _mediator.Send(query);
```

### Retrieving InvoiceStatuses by status name
```csharp
var query = new GetInvoiceStatusByStatusQuery { Status = "Draft" };
var invoiceStatus = await _mediator.Send(query);
```

### Retrieving the default InvoiceStatus
```csharp
var query = new GetDefaultInvoiceStatusQuery();
var defaultInvoiceStatus = await _mediator.Send(query);
```

### Updating an InvoiceStatus
```csharp
var command = new UpdateInvoiceStatusCommand
{
    Id = invoiceStatusId,
    Status = "Updated Draft",
    Description = "Updated description",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an InvoiceStatus
```csharp
var command = new DeleteInvoiceStatusCommand
{
    Id = invoiceStatusId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The InvoiceStatus feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Status name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Status name must be unique across all invoice statuses
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Invoice statuses are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Invoice Associations
Each InvoiceStatus can be associated with multiple Invoice entities. The feature includes functionality to retrieve the count of invoices using each status.

# Invoice Feature

## Overview
The Invoice feature provides functionality to manage invoices within the VibeCRM system. It follows Clean Architecture and SOLID principles, implementing the CQRS pattern with MediatR for command and query handling.

## Domain Model
The Invoice entity is a core business entity that represents a billing document in the CRM system. Each Invoice has the following properties:

- **InvoiceId**: Unique identifier (UUID)
- **Number**: Unique invoice number (e.g., "INV-2025-001")
- **SalesOrderId**: Reference to the associated sales order
- **Standard audit fields**: CreatedBy, CreatedDate, ModifiedBy, ModifiedDate

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **InvoiceDto**: Base DTO with essential invoice properties.
- **InvoiceDetailsDto**: Extended DTO with additional details for detailed views.
- **InvoiceListDto**: Optimized DTO for list views.

### Validators
- **InvoiceDtoValidator**: Validates the base InvoiceDto.
- **InvoiceDetailsDtoValidator**: Validates the InvoiceDetailsDto.
- **InvoiceListDtoValidator**: Validates the InvoiceListDto.

### Commands
- **CreateInvoice**: Creates a new invoice.
- **UpdateInvoice**: Updates an existing invoice.
- **DeleteInvoice**: Soft-deletes an invoice by setting Active = false.

### Queries
- **GetAllInvoices**: Retrieves all active invoices.
- **GetInvoiceById**: Retrieves a specific invoice by its ID.
- **GetInvoicesBySalesOrderId**: Retrieves invoices associated with a specific sales order.

### Mapping
- **InvoiceMappingProfile**: AutoMapper profile for mapping between entities and DTOs.

## Usage Examples

### Creating an Invoice
```csharp
var command = new CreateInvoiceCommand
{
    Number = "INV-2025-001",
    SalesOrderId = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual sales order ID
    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual user ID
    CreatedDate = DateTime.UtcNow
};

var result = await _mediator.Send(command);
```

### Retrieving an Invoice
```csharp
var query = new GetInvoiceByIdQuery
{
    Id = Guid.Parse("00000000-0000-0000-0000-000000000000") // Replace with actual invoice ID
};

var invoice = await _mediator.Send(query);
```

### Updating an Invoice
```csharp
var command = new UpdateInvoiceCommand
{
    Id = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual invoice ID
    Number = "INV-2025-001-UPDATED",
    SalesOrderId = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual sales order ID
    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual user ID
    ModifiedDate = DateTime.UtcNow
};

var result = await _mediator.Send(command);
```

### Deleting an Invoice
```csharp
var command = new DeleteInvoiceCommand
{
    Id = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual invoice ID
    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"), // Replace with actual user ID
    ModifiedDate = DateTime.UtcNow
};

var result = await _mediator.Send(command);
```

### Retrieving Invoices by Sales Order ID
```csharp
var query = new GetInvoicesBySalesOrderIdQuery
{
    SalesOrderId = Guid.Parse("00000000-0000-0000-0000-000000000000") // Replace with actual sales order ID
};

var invoices = await _mediator.Send(query);
```

## Implementation Details

### Soft Delete
The Invoice feature implements soft delete using the `Active` property. When an invoice is deleted:
- The `Active` property is set to `false` (0 in the database)
- The record remains in the database but is filtered out from regular queries
- All queries include a filter for `Active = 1` to exclude soft-deleted records

### Validation
All inputs are validated using FluentValidation:
- Required fields are enforced
- String lengths are validated
- ID fields are validated for non-empty values

### Error Handling
The feature includes comprehensive error handling:
- Validation errors are returned with descriptive messages
- Not found scenarios are properly handled
- All exceptions are logged and wrapped with meaningful messages

## Dependencies
- **FluentValidation**: For input validation
- **AutoMapper**: For object mapping between entities and DTOs
- **MediatR**: For command and query handling
- **Dapper**: For data access against MS SQL Server

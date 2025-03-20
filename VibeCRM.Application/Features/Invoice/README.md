# Invoice Feature

## Overview
The Invoice feature provides functionality to manage invoices within the VibeCRM system. It follows Clean Architecture and SOLID principles, implementing the CQRS pattern with MediatR for command and query handling.

## Components

### Domain
- `Invoice`: Entity representing an invoice with properties for InvoiceId, SalesOrderId, Number, and standard audit fields.

### Data Transfer Objects (DTOs)
- `InvoiceDto`: Base DTO with essential invoice properties.
- `InvoiceDetailsDto`: Extended DTO with additional details for detailed views.
- `InvoiceListDto`: Optimized DTO for list views.

### Validators
- `InvoiceDtoValidator`: Validates the base InvoiceDto.
- `InvoiceDetailsDtoValidator`: Validates the InvoiceDetailsDto.
- `InvoiceListDtoValidator`: Validates the InvoiceListDto.

### Commands
- **Create Invoice**
  - `CreateInvoiceCommand`: Command for creating a new invoice.
  - `CreateInvoiceCommandValidator`: Validates the create command.
  - `CreateInvoiceCommandHandler`: Handles the create command.

- **Update Invoice**
  - `UpdateInvoiceCommand`: Command for updating an existing invoice.
  - `UpdateInvoiceCommandValidator`: Validates the update command.
  - `UpdateInvoiceCommandHandler`: Handles the update command.

- **Delete Invoice**
  - `DeleteInvoiceCommand`: Command for soft-deleting an invoice.
  - `DeleteInvoiceCommandValidator`: Validates the delete command.
  - `DeleteInvoiceCommandHandler`: Handles the delete command.

### Queries
- **Get All Invoices**
  - `GetAllInvoicesQuery`: Query for retrieving all invoices.
  - `GetAllInvoicesQueryHandler`: Handles the get all query.

- **Get Invoice By ID**
  - `GetInvoiceByIdQuery`: Query for retrieving a specific invoice.
  - `GetInvoiceByIdQueryHandler`: Handles the get by ID query.

- **Get Invoices By Sales Order ID**
  - `GetInvoicesBySalesOrderIdQuery`: Query for retrieving invoices by sales order ID.
  - `GetInvoicesBySalesOrderIdQueryHandler`: Handles the get by sales order ID query.

### Mapping
- `InvoiceMappingProfile`: AutoMapper profile for mapping between entities and DTOs.

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

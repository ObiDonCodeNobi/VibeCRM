# Payment Feature

## Overview
The Payment feature in VibeCRM provides comprehensive functionality to manage payment records within the system. This feature follows the CQRS pattern with MediatR, adhering to Clean Architecture and SOLID principles.

## Components

### DTOs
- **PaymentDto**: Base DTO containing essential payment information
- **PaymentDetailsDto**: Extended DTO with detailed payment information including related entities
- **PaymentListDto**: Optimized DTO for displaying payments in lists

### Commands
- **CreatePayment**: Creates a new payment record
- **UpdatePayment**: Updates an existing payment record
- **DeletePayment**: Soft-deletes a payment record (sets Active = false)

### Queries
- **GetAllPayments**: Retrieves all active payments
- **GetPaymentById**: Retrieves a specific payment by its ID
- **GetPaymentsByInvoice**: Retrieves payments associated with a specific invoice
- **GetPaymentsByCompany**: Retrieves payments associated with a specific company
- **GetPaymentsByPerson**: Retrieves payments associated with a specific person
- **GetPaymentsByStatus**: Retrieves payments with a specific payment status
- **GetPaymentsByMethod**: Retrieves payments with a specific payment method
- **GetPaymentsByDateRange**: Retrieves payments within a specific date range

### Validators
- Validators for all DTOs and commands to ensure data integrity

### Mapping Profiles
- AutoMapper profiles for mapping between domain entities and DTOs

## Usage Examples

### Creating a Payment
```csharp
var createCommand = new CreatePaymentCommand
{
    InvoiceId = Guid.Parse("invoice-guid-here"),
    PaymentTypeId = Guid.Parse("payment-type-guid-here"),
    PaymentMethodTypeId = Guid.Parse("payment-method-guid-here"),
    PaymentStatusId = Guid.Parse("payment-status-guid-here"),
    PaymentDate = DateTime.UtcNow,
    Amount = 100.00m,
    ReferenceNumber = "REF12345",
    Notes = "Payment for invoice #INV-001",
    CreatedBy = Guid.Parse("user-guid-here"),
    ModifiedBy = Guid.Parse("user-guid-here")
};

var result = await _mediator.Send(createCommand);
```

### Retrieving Payments by Invoice
```csharp
var query = new GetPaymentsByInvoiceQuery(Guid.Parse("invoice-guid-here"));
var payments = await _mediator.Send(query);
```

### Updating a Payment
```csharp
var updateCommand = new UpdatePaymentCommand
{
    Id = Guid.Parse("payment-guid-here"),
    Amount = 150.00m,
    PaymentStatusId = Guid.Parse("new-status-guid-here"),
    Notes = "Updated payment amount",
    ModifiedBy = Guid.Parse("user-guid-here")
};

var result = await _mediator.Send(updateCommand);
```

### Deleting a Payment
```csharp
var deleteCommand = new DeletePaymentCommand(
    Guid.Parse("payment-guid-here"),
    Guid.Parse("user-guid-here")
);

var result = await _mediator.Send(deleteCommand);
```

## Implementation Notes
- All entities use soft delete via the `Active` property
- All queries filter by `Active = true` to exclude soft-deleted records
- Full XML documentation is provided for all classes and methods
- Validation is implemented using FluentValidation
- Repository pattern is used for data access with Dapper ORM

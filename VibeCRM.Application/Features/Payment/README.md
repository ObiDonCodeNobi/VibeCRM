# Payment Feature

## Overview
The Payment feature in VibeCRM provides comprehensive functionality to manage payment records within the system. This feature follows the CQRS pattern with MediatR, adhering to Clean Architecture and SOLID principles.

## Domain Model
The Payment entity is a core business entity that represents a financial transaction in the CRM system. Each Payment has the following properties:

- **PaymentId**: Unique identifier (UUID)
- **InvoiceId**: Reference to the associated invoice
- **PaymentTypeId**: Reference to the type of payment
- **PaymentMethodTypeId**: Reference to the method of payment
- **PaymentStatusId**: Reference to the status of the payment
- **PaymentDate**: Date when the payment was made
- **Amount**: The payment amount
- **ReferenceNumber**: External reference number (e.g., check number, transaction ID)
- **Notes**: Additional notes or comments about the payment
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Invoice**: Navigation property to the associated Invoice
- **PaymentType**: Navigation property to the associated PaymentType
- **PaymentMethodType**: Navigation property to the associated PaymentMethodType
- **PaymentStatus**: Navigation property to the associated PaymentStatus
- **PaymentLineItems**: Collection of associated PaymentLineItem entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PaymentDto**: Base DTO with core properties
- **PaymentDetailsDto**: Extended DTO with audit fields and related data
- **PaymentListDto**: Optimized DTO for list views

### Commands
- **CreatePayment**: Creates a new payment
- **UpdatePayment**: Updates an existing payment
- **DeletePayment**: Soft-deletes a payment by setting Active = false
- **ApplyPaymentToInvoice**: Applies a payment to a specific invoice
- **VoidPayment**: Marks a payment as void
- **RefundPayment**: Records a refund for a payment

### Queries
- **GetAllPayments**: Retrieves all active payments
- **GetPaymentById**: Retrieves a specific payment by its ID
- **GetPaymentsByInvoice**: Retrieves payments associated with a specific invoice
- **GetPaymentsByCompany**: Retrieves payments associated with a specific company
- **GetPaymentsByPerson**: Retrieves payments associated with a specific person
- **GetPaymentsByStatus**: Retrieves payments with a specific status
- **GetPaymentsByMethod**: Retrieves payments with a specific payment method
- **GetPaymentsByDateRange**: Retrieves payments within a date range
- **GetPaymentWithLineItems**: Retrieves a payment with its line items

### Validators
- **PaymentDtoValidator**: Validates the base DTO
- **PaymentDetailsDtoValidator**: Validates the detailed DTO
- **PaymentListDtoValidator**: Validates the list DTO
- **CreatePaymentCommandValidator**: Validates the create command
- **UpdatePaymentCommandValidator**: Validates the update command
- **DeletePaymentCommandValidator**: Validates the delete command
- **ApplyPaymentToInvoiceCommandValidator**: Validates the apply payment command
- **VoidPaymentCommandValidator**: Validates the void payment command
- **RefundPaymentCommandValidator**: Validates the refund payment command
- **GetPaymentByIdQueryValidator**: Validates the ID query
- **GetPaymentsByInvoiceQueryValidator**: Validates the invoice query
- **GetPaymentsByCompanyQueryValidator**: Validates the company query
- **GetPaymentsByPersonQueryValidator**: Validates the person query
- **GetPaymentsByStatusQueryValidator**: Validates the status query
- **GetPaymentsByMethodQueryValidator**: Validates the method query
- **GetPaymentsByDateRangeQueryValidator**: Validates the date range query

### Mappings
- **PaymentMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Payment
```csharp
var command = new CreatePaymentCommand
{
    InvoiceId = invoiceId,
    PaymentTypeId = paymentTypeId,
    PaymentMethodTypeId = paymentMethodTypeId,
    PaymentStatusId = paymentStatusId,
    PaymentDate = DateTime.UtcNow,
    Amount = 500.00m,
    ReferenceNumber = "CHK12345",
    Notes = "Payment for invoice #INV-2025-001",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Payments
```csharp
var query = new GetAllPaymentsQuery();
var payments = await _mediator.Send(query);
```

### Retrieving a Payment by ID
```csharp
var query = new GetPaymentByIdQuery { Id = paymentId };
var payment = await _mediator.Send(query);
```

### Retrieving Payments by Invoice
```csharp
var query = new GetPaymentsByInvoiceQuery { InvoiceId = invoiceId };
var payments = await _mediator.Send(query);
```

### Applying a Payment to an Invoice
```csharp
var command = new ApplyPaymentToInvoiceCommand
{
    PaymentId = paymentId,
    InvoiceId = invoiceId,
    Amount = 250.00m,
    Notes = "Partial payment applied to invoice",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Payment
```csharp
var command = new UpdatePaymentCommand
{
    Id = paymentId,
    PaymentStatusId = newPaymentStatusId,
    Amount = 550.00m,
    Notes = "Updated payment amount",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Payment
```csharp
var command = new DeletePaymentCommand
{
    Id = paymentId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Payment feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- InvoiceId must reference a valid invoice
- PaymentTypeId must reference a valid payment type
- PaymentMethodTypeId must reference a valid payment method
- PaymentStatusId must reference a valid payment status
- PaymentDate is required and must be a valid date
- Amount must be greater than zero
- ReferenceNumber is optional but limited to 50 characters
- Notes are optional but limited to 500 characters
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Payment Status Workflow
- Payments follow a status workflow (e.g., "Pending" → "Processed" → "Cleared" or "Declined")
- Status transitions are validated to ensure they follow the allowed workflow
- Certain operations are only allowed for payments in specific statuses

### Payment Application
- Payments can be applied to one or more invoices
- The system tracks which portions of a payment are applied to which invoices
- The total applied amount cannot exceed the payment amount
- The payment application affects the outstanding balance of the associated invoices

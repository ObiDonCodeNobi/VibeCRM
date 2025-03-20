# PaymentLineItem Feature

## Overview
The PaymentLineItem feature provides comprehensive functionality for managing payment line items within the VibeCRM system. Payment line items represent individual allocations of a payment to specific invoices or invoice line items, allowing for detailed tracking of how payments are applied.

## Business Purpose
Payment line items enable the system to:
- Track how payments are distributed across multiple invoices
- Apply partial payments to specific invoice line items
- Calculate remaining balances on invoices
- Generate detailed payment history reports
- Support complex payment allocation scenarios

## Components

### Domain Entity
The `PaymentLineItem` entity in the domain layer represents a single allocation of a payment amount to an invoice or invoice line item.

### DTOs
- **PaymentLineItemDto**: Base DTO containing essential payment line item properties
- **PaymentLineItemDetailsDto**: Extended DTO with additional audit information for detailed views
- **PaymentLineItemListDto**: Streamlined DTO optimized for list displays

### Commands
- **CreatePaymentLineItem**: Creates a new payment line item record
- **UpdatePaymentLineItem**: Updates an existing payment line item record
- **DeletePaymentLineItem**: Soft-deletes a payment line item record (sets Active = false)

### Queries
- **GetPaymentLineItemById**: Retrieves a specific payment line item by its ID
- **GetAllPaymentLineItems**: Retrieves all active payment line items
- **GetPaymentLineItemsByPaymentId**: Retrieves all payment line items for a specific payment
- **GetPaymentLineItemsByInvoiceId**: Retrieves all payment line items for a specific invoice

### Validators
- **PaymentLineItemDtoValidator**: Validates the base payment line item DTO
- **PaymentLineItemDetailsDtoValidator**: Validates the detailed payment line item DTO
- **PaymentLineItemListDtoValidator**: Validates the list payment line item DTO
- **CreatePaymentLineItemCommandValidator**: Validates the create command
- **UpdatePaymentLineItemCommandValidator**: Validates the update command
- **DeletePaymentLineItemCommandValidator**: Validates the delete command

### Mapping Profiles
- **PaymentLineItemMappingProfile**: Defines mappings between entities and DTOs

## Usage Examples

### Creating a Payment Line Item
```csharp
// Create a command to add a new payment line item
var createCommand = new CreatePaymentLineItemCommand
{
    PaymentId = paymentId,
    InvoiceId = invoiceId,
    Amount = 100.00m,
    Description = "Partial payment for Invoice #12345",
    CreatedBy = "system"
};

// Send the command using MediatR
var result = await _mediator.Send(createCommand);
```

### Retrieving Payment Line Items for an Invoice
```csharp
// Create a query to get all payment line items for an invoice
var query = new GetPaymentLineItemsByInvoiceIdQuery
{
    InvoiceId = invoiceId
};

// Send the query using MediatR
var paymentLineItems = await _mediator.Send(query);

// Process the results
foreach (var item in paymentLineItems)
{
    // Use the payment line item data
    Console.WriteLine($"Payment: {item.PaymentId}, Amount: {item.Amount}");
}
```

### Updating a Payment Line Item
```csharp
// Create a command to update an existing payment line item
var updateCommand = new UpdatePaymentLineItemCommand
{
    Id = paymentLineItemId,
    PaymentId = paymentId,
    InvoiceId = invoiceId,
    Amount = 150.00m,
    Description = "Updated payment allocation",
    ModifiedBy = "system"
};

// Send the command using MediatR
var result = await _mediator.Send(updateCommand);
```

### Deleting a Payment Line Item
```csharp
// Create a command to soft-delete a payment line item
var deleteCommand = new DeletePaymentLineItemCommand
{
    Id = paymentLineItemId,
    ModifiedBy = "system"
};

// Send the command using MediatR
var success = await _mediator.Send(deleteCommand);
```

## Integration Points
- **Payment Feature**: Payment line items are associated with payments
- **Invoice Feature**: Payment line items can be applied to invoices
- **Invoice Line Item Feature**: Payment line items can be applied to specific invoice line items

## Soft Delete Pattern
The PaymentLineItem feature implements the standard soft delete pattern used throughout VibeCRM:
- Records are never physically deleted from the database
- The `Active` property is set to `false` when a record is "deleted"
- All queries filter by `Active = true` to exclude soft-deleted records
- The `DeleteAsync` method in the repository performs the soft delete operation

## Validation Rules
- Payment ID is required
- Either Invoice ID or Invoice Line Item ID must be provided
- Amount must be greater than zero
- Description is required and cannot exceed 500 characters
- Notes cannot exceed 2000 characters

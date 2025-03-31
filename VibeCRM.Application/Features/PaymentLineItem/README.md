# PaymentLineItem Feature

## Overview
The PaymentLineItem feature provides comprehensive functionality for managing payment line items within the VibeCRM system. Payment line items represent individual allocations of a payment to specific invoices or invoice line items, allowing for detailed tracking of how payments are applied.

## Domain Model
The PaymentLineItem entity is a core business entity that represents a single allocation of a payment amount to an invoice or invoice line item. Each PaymentLineItem has the following properties:

- **PaymentLineItemId**: Unique identifier (UUID)
- **PaymentId**: Reference to the associated payment
- **InvoiceId**: Reference to the associated invoice
- **InvoiceLineItemId**: Optional reference to the specific invoice line item
- **Amount**: The amount allocated from the payment
- **Description**: Brief description of the payment allocation
- **Notes**: Additional notes or comments about the payment allocation
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Payment**: Navigation property to the associated Payment
- **Invoice**: Navigation property to the associated Invoice
- **InvoiceLineItem**: Navigation property to the associated InvoiceLineItem (if applicable)

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PaymentLineItemDto**: Base DTO with core properties
- **PaymentLineItemDetailsDto**: Extended DTO with audit fields and related data
- **PaymentLineItemListDto**: Optimized DTO for list views

### Commands
- **CreatePaymentLineItem**: Creates a new payment line item
- **UpdatePaymentLineItem**: Updates an existing payment line item
- **DeletePaymentLineItem**: Soft-deletes a payment line item by setting Active = false
- **BulkCreatePaymentLineItems**: Creates multiple payment line items in a single operation
- **ReallocatePaymentLineItem**: Moves a payment allocation from one invoice to another

### Queries
- **GetAllPaymentLineItems**: Retrieves all active payment line items
- **GetPaymentLineItemById**: Retrieves a specific payment line item by its ID
- **GetPaymentLineItemsByPayment**: Retrieves payment line items associated with a specific payment
- **GetPaymentLineItemsByInvoice**: Retrieves payment line items associated with a specific invoice
- **GetPaymentLineItemsByInvoiceLineItem**: Retrieves payment line items associated with a specific invoice line item

### Validators
- **PaymentLineItemDtoValidator**: Validates the base DTO
- **PaymentLineItemDetailsDtoValidator**: Validates the detailed DTO
- **PaymentLineItemListDtoValidator**: Validates the list DTO
- **CreatePaymentLineItemCommandValidator**: Validates the create command
- **UpdatePaymentLineItemCommandValidator**: Validates the update command
- **DeletePaymentLineItemCommandValidator**: Validates the delete command
- **BulkCreatePaymentLineItemsCommandValidator**: Validates the bulk create command
- **ReallocatePaymentLineItemCommandValidator**: Validates the reallocate command
- **GetPaymentLineItemByIdQueryValidator**: Validates the ID query
- **GetPaymentLineItemsByPaymentQueryValidator**: Validates the payment query
- **GetPaymentLineItemsByInvoiceQueryValidator**: Validates the invoice query
- **GetPaymentLineItemsByInvoiceLineItemQueryValidator**: Validates the invoice line item query

### Mappings
- **PaymentLineItemMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new PaymentLineItem
```csharp
var command = new CreatePaymentLineItemCommand
{
    PaymentId = paymentId,
    InvoiceId = invoiceId,
    InvoiceLineItemId = invoiceLineItemId, // Optional
    Amount = 250.00m,
    Description = "Partial payment for Invoice #INV-2025-001",
    Notes = "Applied to outstanding balance",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all PaymentLineItems
```csharp
var query = new GetAllPaymentLineItemsQuery();
var paymentLineItems = await _mediator.Send(query);
```

### Retrieving a PaymentLineItem by ID
```csharp
var query = new GetPaymentLineItemByIdQuery { Id = paymentLineItemId };
var paymentLineItem = await _mediator.Send(query);
```

### Retrieving PaymentLineItems by Payment
```csharp
var query = new GetPaymentLineItemsByPaymentQuery { PaymentId = paymentId };
var paymentLineItems = await _mediator.Send(query);
```

### Bulk Creating PaymentLineItems
```csharp
var command = new BulkCreatePaymentLineItemsCommand
{
    PaymentId = paymentId,
    LineItems = new List<PaymentLineItemCreateDto>
    {
        new PaymentLineItemCreateDto
        {
            InvoiceId = invoice1Id,
            Amount = 150.00m,
            Description = "Partial payment for Invoice #INV-2025-001"
        },
        new PaymentLineItemCreateDto
        {
            InvoiceId = invoice2Id,
            Amount = 350.00m,
            Description = "Full payment for Invoice #INV-2025-002"
        }
    },
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a PaymentLineItem
```csharp
var command = new UpdatePaymentLineItemCommand
{
    Id = paymentLineItemId,
    Amount = 300.00m,
    Description = "Updated payment allocation",
    Notes = "Adjusted to cover additional charges",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a PaymentLineItem
```csharp
var command = new DeletePaymentLineItemCommand
{
    Id = paymentLineItemId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The PaymentLineItem feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- PaymentId must reference a valid payment
- InvoiceId must reference a valid invoice
- InvoiceLineItemId (if provided) must reference a valid invoice line item
- Amount must be greater than zero
- Amount cannot exceed the remaining balance of the payment
- Amount cannot exceed the remaining balance of the invoice or invoice line item
- Description is required and limited to 200 characters
- Notes are optional but limited to 1000 characters
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Payment Allocation Logic
- The system ensures that the total of all payment line items does not exceed the payment amount
- When a payment is applied to an invoice, the invoice's outstanding balance is reduced
- When a payment line item is deleted or modified, the system recalculates the invoice's outstanding balance
- The system prevents over-allocation of payments to invoices

### Transaction Support
- All operations that affect multiple records (e.g., creating a payment and its line items) are wrapped in transactions
- If any part of a transaction fails, the entire operation is rolled back to maintain data consistency

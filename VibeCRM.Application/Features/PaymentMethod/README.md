# PaymentMethod Feature

## Overview
The PaymentMethod feature provides functionality for managing payment method entities in the VibeCRM system. Payment methods represent the various ways customers can make payments (e.g., "Credit Card", "Cash", "Check", etc.) and are used in financial transactions throughout the system.

## Domain Model
The PaymentMethod entity is a reference entity that represents a method of payment. Each PaymentMethod has the following properties:

- **PaymentMethodId**: Unique identifier (UUID)
- **Name**: Name of the payment method (e.g., "Credit Card", "Cash", "Check")
- **Description**: Detailed description of the payment method
- **OrdinalPosition**: Numeric value for ordering payment methods in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Payments**: Collection of associated Payment entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PaymentMethodDto**: Base DTO with core properties
- **PaymentMethodDetailsDto**: Extended DTO with audit fields and payment count
- **PaymentMethodListDto**: Optimized DTO for list views

### Commands
- **CreatePaymentMethod**: Creates a new payment method
- **UpdatePaymentMethod**: Updates an existing payment method
- **DeletePaymentMethod**: Soft-deletes a payment method by setting Active = false

### Queries
- **GetAllPaymentMethods**: Retrieves all active payment methods
- **GetPaymentMethodById**: Retrieves a specific payment method by its ID
- **GetPaymentMethodByName**: Retrieves a specific payment method by its name
- **GetPaymentMethodByOrdinalPosition**: Retrieves payment methods ordered by their ordinal position
- **GetDefaultPaymentMethod**: Retrieves the default payment method

### Validators
- **PaymentMethodDtoValidator**: Validates the base DTO
- **PaymentMethodDetailsDtoValidator**: Validates the detailed DTO
- **PaymentMethodListDtoValidator**: Validates the list DTO
- **GetPaymentMethodByIdQueryValidator**: Validates the ID query
- **GetPaymentMethodByNameQueryValidator**: Validates the name query
- **GetPaymentMethodByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllPaymentMethodsQueryValidator**: Validates the "get all" query

### Mappings
- **PaymentMethodMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new PaymentMethod
```csharp
var command = new CreatePaymentMethodCommand
{
    Name = "Credit Card",
    Description = "Payment via credit card (Visa, MasterCard, etc.)",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all PaymentMethods
```csharp
var query = new GetAllPaymentMethodsQuery();
var paymentMethods = await _mediator.Send(query);
```

### Retrieving PaymentMethods by ordinal position
```csharp
var query = new GetPaymentMethodByOrdinalPositionQuery();
var orderedPaymentMethods = await _mediator.Send(query);
```

### Retrieving default PaymentMethod
```csharp
var query = new GetDefaultPaymentMethodQuery();
var defaultPaymentMethod = await _mediator.Send(query);
```

### Updating a PaymentMethod
```csharp
var command = new UpdatePaymentMethodCommand
{
    Id = paymentMethodId,
    Name = "Credit Card",
    Description = "Payment via credit card (Visa, MasterCard, American Express)",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a PaymentMethod
```csharp
var command = new DeletePaymentMethodCommand
{
    Id = paymentMethodId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The PaymentMethod feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Payment methods are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Payment Associations
Each PaymentMethod can be associated with multiple Payment entities. The feature includes functionality to retrieve the count of payments using each method.

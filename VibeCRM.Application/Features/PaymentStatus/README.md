# PaymentStatus Feature

## Overview
The PaymentStatus feature provides a comprehensive implementation for managing payment statuses within the VibeCRM system. Payment statuses represent the current state of payments (e.g., "Paid", "Pending", "Overdue") and are used to track payment progress throughout the system.

## Architecture
This feature follows the Clean Architecture and CQRS patterns established in the VibeCRM system:

- **Domain Layer**: Contains the PaymentStatus entity definition
- **Application Layer**: Contains DTOs, Commands, Queries, Validators, and Mapping profiles
- **Infrastructure Layer**: Contains the PaymentStatusRepository implementation

## Components

### DTOs
- `PaymentStatusDto`: Basic DTO for transferring payment status data
- `PaymentStatusDetailsDto`: Detailed DTO with additional information like creation/modification dates
- `PaymentStatusListDto`: DTO optimized for list views with payment count

### Commands
- `CreatePaymentStatus`: Creates a new payment status
- `UpdatePaymentStatus`: Updates an existing payment status
- `DeletePaymentStatus`: Soft deletes a payment status (sets Active = false)

### Queries
- `GetPaymentStatusById`: Retrieves a payment status by its ID
- `GetPaymentStatusByStatus`: Retrieves a payment status by its status name
- `GetAllPaymentStatuses`: Retrieves all payment statuses with optional filtering

### Validators
- Command validators: Ensure command data is valid before processing
- Query validators: Validate query parameters
- DTO validators: Validate DTO properties for data consistency

### Mapping Profiles
- `PaymentStatusMappingProfile`: Configures AutoMapper mappings between entities and DTOs

## Implementation Details

### Soft Delete Pattern
- Uses the standard `Active` property for soft delete
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than removing records

### Audit Fields
- Tracks `CreatedDate`, `CreatedBy`, `ModifiedDate`, and `ModifiedBy` for all entities
- These fields are properly maintained in command handlers

### Validation
- All commands and DTOs have comprehensive validation rules
- Validation includes required fields, string lengths, and numeric ranges

## Usage Examples

### Creating a Payment Status
```csharp
var command = new CreatePaymentStatusCommand
{
    Status = "Paid",
    Description = "Payment has been received and processed",
    OrdinalPosition = 1,
    CreatedBy = userId,
    ModifiedBy = userId
};

var result = await mediator.Send(command);
```

### Retrieving a Payment Status
```csharp
var query = new GetPaymentStatusByStatusQuery
{
    Status = "Paid"
};

var paymentStatus = await mediator.Send(query);
```

### Updating a Payment Status
```csharp
var command = new UpdatePaymentStatusCommand
{
    Id = paymentStatusId,
    Status = "Paid in Full",
    Description = "Payment has been received and fully processed",
    OrdinalPosition = 1,
    ModifiedBy = userId
};

var result = await mediator.Send(command);
```

### Deleting a Payment Status
```csharp
var command = new DeletePaymentStatusCommand
{
    Id = paymentStatusId,
    ModifiedBy = userId
};

var success = await mediator.Send(command);
```

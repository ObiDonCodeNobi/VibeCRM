# PaymentStatus Feature

## Overview
The PaymentStatus feature provides a comprehensive implementation for managing payment statuses within the VibeCRM system. Payment statuses represent the current state of payments (e.g., "Paid", "Pending", "Overdue") and are used to track payment progress throughout the system.

## Domain Model
The PaymentStatus entity is a reference entity that represents the status of a payment. Each PaymentStatus has the following properties:

- **PaymentStatusId**: Unique identifier (UUID)
- **Status**: Name of the status (e.g., "Paid", "Pending", "Overdue")
- **Description**: Detailed description of what the status means
- **OrdinalPosition**: Numeric value for ordering statuses in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Payments**: Collection of associated Payment entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **PaymentStatusDto**: Base DTO with core properties
- **PaymentStatusDetailsDto**: Extended DTO with audit fields and payment count
- **PaymentStatusListDto**: Optimized DTO for list views

### Commands
- **CreatePaymentStatus**: Creates a new payment status
- **UpdatePaymentStatus**: Updates an existing payment status
- **DeletePaymentStatus**: Soft-deletes a payment status by setting Active = false

### Queries
- **GetAllPaymentStatuses**: Retrieves all active payment statuses
- **GetPaymentStatusById**: Retrieves a specific payment status by its ID
- **GetPaymentStatusByStatus**: Retrieves a specific payment status by its status name
- **GetPaymentStatusByOrdinalPosition**: Retrieves payment statuses ordered by position

### Validators
- **PaymentStatusDtoValidator**: Validates the base DTO
- **PaymentStatusDetailsDtoValidator**: Validates the detailed DTO
- **PaymentStatusListDtoValidator**: Validates the list DTO
- **GetPaymentStatusByIdQueryValidator**: Validates the ID query
- **GetPaymentStatusByStatusQueryValidator**: Validates the status name query
- **GetPaymentStatusByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllPaymentStatusesQueryValidator**: Validates the "get all" query

### Mappings
- **PaymentStatusMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new PaymentStatus
```csharp
var command = new CreatePaymentStatusCommand
{
    Status = "Paid",
    Description = "Payment has been received and processed",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all PaymentStatuses
```csharp
var query = new GetAllPaymentStatusesQuery();
var paymentStatuses = await _mediator.Send(query);
```

### Retrieving a PaymentStatus by status name
```csharp
var query = new GetPaymentStatusByStatusQuery
{
    Status = "Paid"
};

var paymentStatus = await _mediator.Send(query);
```

### Updating a PaymentStatus
```csharp
var command = new UpdatePaymentStatusCommand
{
    Id = paymentStatusId,
    Status = "Paid in Full",
    Description = "Payment has been received and fully processed",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a PaymentStatus
```csharp
var command = new DeletePaymentStatusCommand
{
    Id = paymentStatusId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The PaymentStatus feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Status name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Payment statuses are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Payment Associations
Each PaymentStatus can be associated with multiple Payment entities. The feature includes functionality to retrieve the count of payments using each status.

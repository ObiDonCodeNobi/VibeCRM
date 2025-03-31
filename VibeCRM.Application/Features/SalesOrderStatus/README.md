# SalesOrderStatus Feature

## Overview
The SalesOrderStatus feature manages the different statuses that a sales order can have throughout its lifecycle in the VibeCRM system. This feature follows Clean Architecture principles and implements the CQRS pattern for clear separation of command and query responsibilities.

## Domain Model
The SalesOrderStatus entity is a reference entity that represents the status of a sales order. Each SalesOrderStatus has the following properties:

- **SalesOrderStatusId**: Unique identifier (UUID)
- **Status**: Name of the sales order status (e.g., "New", "In Progress", "Completed")
- **Description**: Detailed description of what the status means
- **OrdinalPosition**: Numeric value for ordering statuses in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **SalesOrders**: Collection of associated SalesOrder entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **SalesOrderStatusDto**: Base DTO with core properties
- **SalesOrderStatusDetailsDto**: Extended DTO with audit fields and sales order count
- **SalesOrderStatusListDto**: Optimized DTO for list views

### Commands
- **CreateSalesOrderStatus**: Creates a new sales order status
- **UpdateSalesOrderStatus**: Updates an existing sales order status
- **DeleteSalesOrderStatus**: Soft-deletes a sales order status by setting Active = false

### Queries
- **GetAllSalesOrderStatuses**: Retrieves all active sales order statuses
- **GetSalesOrderStatusById**: Retrieves a specific sales order status by its ID
- **GetSalesOrderStatusByStatus**: Retrieves sales order statuses by their status name
- **GetSalesOrderStatusByOrdinalPosition**: Retrieves sales order statuses by their ordinal position
- **GetDefaultSalesOrderStatus**: Retrieves the default sales order status (lowest ordinal position)

### Validators
- **SalesOrderStatusDtoValidator**: Validates the base DTO
- **SalesOrderStatusDetailsDtoValidator**: Validates the detailed DTO
- **SalesOrderStatusListDtoValidator**: Validates the list DTO
- **CreateSalesOrderStatusCommandValidator**: Validates the create command
- **UpdateSalesOrderStatusCommandValidator**: Validates the update command
- **DeleteSalesOrderStatusCommandValidator**: Validates the delete command
- **GetSalesOrderStatusByIdQueryValidator**: Validates the ID query
- **GetSalesOrderStatusByStatusQueryValidator**: Validates the status name query
- **GetSalesOrderStatusByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllSalesOrderStatusesQueryValidator**: Validates the "get all" query

### Mappings
- **SalesOrderStatusMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new SalesOrderStatus
```csharp
var command = new CreateSalesOrderStatusCommand
{
    Status = "New",
    Description = "A newly created sales order",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all SalesOrderStatuses
```csharp
var query = new GetAllSalesOrderStatusesQuery();
var salesOrderStatuses = await _mediator.Send(query);
```

### Retrieving a SalesOrderStatus by ID
```csharp
var query = new GetSalesOrderStatusByIdQuery { Id = salesOrderStatusId };
var salesOrderStatus = await _mediator.Send(query);
```

### Retrieving SalesOrderStatuses by status name
```csharp
var query = new GetSalesOrderStatusByStatusQuery { Status = "In Progress" };
var salesOrderStatus = await _mediator.Send(query);
```

### Retrieving the default SalesOrderStatus
```csharp
var query = new GetDefaultSalesOrderStatusQuery();
var defaultSalesOrderStatus = await _mediator.Send(query);
```

### Updating a SalesOrderStatus
```csharp
var command = new UpdateSalesOrderStatusCommand
{
    Id = salesOrderStatusId,
    Status = "In Progress",
    Description = "A sales order that is being processed",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a SalesOrderStatus
```csharp
var command = new DeleteSalesOrderStatusCommand
{
    Id = salesOrderStatusId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The SalesOrderStatus feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Status name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Status name must be unique across all sales order statuses
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Sales order statuses are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### SalesOrder Associations
Each SalesOrderStatus can be associated with multiple SalesOrder entities. The feature includes functionality to retrieve the count of sales orders using each status.

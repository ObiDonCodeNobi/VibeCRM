# SalesOrderStatus Feature

## Overview
The SalesOrderStatus feature manages the different statuses that a sales order can have throughout its lifecycle in the VibeCRM system. This feature follows Clean Architecture principles and implements the CQRS pattern for clear separation of command and query responsibilities.

## Implementation Details

### Domain Entity
- `SalesOrderStatus`: Represents a status that a sales order can have, with properties such as Id, Status, Description, and OrdinalPosition.

### DTOs
- `SalesOrderStatusDto`: Basic information structure for sales order statuses.
- `SalesOrderStatusListDto`: Structure for listing sales order statuses with additional information like sales order count.
- `SalesOrderStatusDetailsDto`: Detailed structure including audit fields.

### Validators
- `SalesOrderStatusDtoValidator`: Validates properties of the SalesOrderStatusDto.
- `SalesOrderStatusListDtoValidator`: Validates properties of the SalesOrderStatusListDto.
- `SalesOrderStatusDetailsDtoValidator`: Validates properties of the SalesOrderStatusDetailsDto.

### Commands
- `CreateSalesOrderStatus`: Creates a new sales order status.
- `UpdateSalesOrderStatus`: Updates an existing sales order status.
- `DeleteSalesOrderStatus`: Soft-deletes an existing sales order status by setting the Active property to false.

### Queries
- `GetAllSalesOrderStatuses`: Retrieves all active sales order statuses.
- `GetSalesOrderStatusById`: Retrieves a specific sales order status by its ID.
- `GetSalesOrderStatusByOrdinalPosition`: Retrieves sales order statuses by their ordinal position.
- `GetSalesOrderStatusByStatus`: Retrieves sales order statuses by their status name.

### Mapping Profile
- `SalesOrderStatusMappingProfile`: Maps between SalesOrderStatus entities and DTOs.

## Design Decisions
- **Soft Delete**: Instead of physically removing records, the feature implements soft delete functionality by setting an `Active` property to false.
- **Validation**: FluentValidation is used for input validation to ensure data integrity.
- **Mapping**: AutoMapper is utilized for DTO mapping to simplify the transformation between domain entities and DTOs.
- **CQRS Pattern**: The feature follows the CQRS pattern, separating command and query responsibilities.
- **Repository Pattern**: The feature uses the repository pattern to abstract data access.

## Usage Examples

### Creating a Sales Order Status
```csharp
var command = new CreateSalesOrderStatusCommand
{
    Status = "New",
    Description = "A newly created sales order",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Updating a Sales Order Status
```csharp
var command = new UpdateSalesOrderStatusCommand
{
    Id = salesOrderStatusId,
    Status = "In Progress",
    Description = "A sales order that is being processed",
    OrdinalPosition = 2
};

var result = await _mediator.Send(command);
```

### Deleting a Sales Order Status
```csharp
var command = new DeleteSalesOrderStatusCommand
{
    Id = salesOrderStatusId
};

var result = await _mediator.Send(command);
```

### Retrieving All Sales Order Statuses
```csharp
var query = new GetAllSalesOrderStatusesQuery();
var result = await _mediator.Send(query);
```

### Retrieving a Sales Order Status by ID
```csharp
var query = new GetSalesOrderStatusByIdQuery
{
    Id = salesOrderStatusId
};

var result = await _mediator.Send(query);
```

## Notes
- The feature is designed to be extensible, allowing for additional functionality to be added as needed.
- All commands and queries include comprehensive validation to ensure data integrity.
- The feature follows the standardized soft delete pattern used throughout the VibeCRM system.

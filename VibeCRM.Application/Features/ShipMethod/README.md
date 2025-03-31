# ShipMethod Feature

## Overview
The ShipMethod feature provides functionality for managing shipping methods in the VibeCRM system. Shipping methods define different ways to ship products to customers, such as "Standard", "Express", "Overnight", etc., making it easier to organize and track shipping options for orders.

## Domain Model
The ShipMethod entity is a reference entity that represents a shipping method for orders. Each ShipMethod has the following properties:

- **ShipMethodId**: Unique identifier (UUID)
- **Method**: Name of the shipping method (e.g., "Standard", "Express", "Overnight")
- **Description**: Detailed description of what the shipping method means
- **OrdinalPosition**: Numeric value for ordering shipping methods in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **SalesOrders**: Collection of associated SalesOrder entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ShipMethodDto**: Base DTO with core properties
- **ShipMethodDetailsDto**: Extended DTO with audit fields and order count
- **ShipMethodListDto**: Optimized DTO for list views

### Commands
- **CreateShipMethod**: Creates a new shipping method
- **UpdateShipMethod**: Updates an existing shipping method
- **DeleteShipMethod**: Soft-deletes a shipping method by setting Active = false

### Queries
- **GetAllShipMethods**: Retrieves all active shipping methods
- **GetShipMethodById**: Retrieves a specific shipping method by its ID
- **GetShipMethodByMethod**: Retrieves shipping methods by their method name
- **GetDefaultShipMethod**: Retrieves the default shipping method (lowest ordinal position)

### Validators
- **ShipMethodDtoValidator**: Validates the base DTO
- **ShipMethodDetailsDtoValidator**: Validates the detailed DTO
- **ShipMethodListDtoValidator**: Validates the list DTO
- **CreateShipMethodCommandValidator**: Validates the create command
- **UpdateShipMethodCommandValidator**: Validates the update command
- **DeleteShipMethodCommandValidator**: Validates the delete command
- **GetShipMethodByIdQueryValidator**: Validates the ID query
- **GetShipMethodByMethodQueryValidator**: Validates the method name query
- **GetAllShipMethodsQueryValidator**: Validates the "get all" query
- **GetDefaultShipMethodQueryValidator**: Validates the default query

### Mappings
- **ShipMethodMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new ShipMethod
```csharp
var command = new CreateShipMethodCommand
{
    Method = "Express",
    Description = "2-3 day delivery service with tracking",
    OrdinalPosition = 2,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all ShipMethods
```csharp
var query = new GetAllShipMethodsQuery();
var shipMethods = await _mediator.Send(query);
```

### Retrieving a ShipMethod by ID
```csharp
var query = new GetShipMethodByIdQuery { Id = shipMethodId };
var shipMethod = await _mediator.Send(query);
```

### Retrieving ShipMethods by method name
```csharp
var query = new GetShipMethodByMethodQuery { Method = "Express" };
var expressShipMethods = await _mediator.Send(query);
```

### Retrieving the default ShipMethod
```csharp
var query = new GetDefaultShipMethodQuery();
var defaultShipMethod = await _mediator.Send(query);
```

### Updating a ShipMethod
```csharp
var command = new UpdateShipMethodCommand
{
    Id = shipMethodId,
    Method = "Overnight",
    Description = "Next-day delivery service with tracking and signature confirmation",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a ShipMethod
```csharp
var command = new DeleteShipMethodCommand
{
    Id = shipMethodId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The ShipMethod feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Method name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Method name must be unique across all shipping methods
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Shipping methods are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### SalesOrder Associations
Each ShipMethod can be associated with multiple SalesOrder entities. The feature includes functionality to retrieve the count of orders using each shipping method.

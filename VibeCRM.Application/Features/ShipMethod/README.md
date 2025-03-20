# ShipMethod Feature

## Overview
The ShipMethod feature provides functionality for managing shipping methods in the VibeCRM system. Shipping methods define different ways to ship products to customers, such as "Standard", "Express", "Overnight", etc., making it easier to organize and track shipping options for orders.

## Components

### Domain Entities
- **ShipMethod**: Represents a shipping method with properties like Id, Method, Description, OrdinalPosition, and audit fields.

### DTOs (Data Transfer Objects)
- **ShipMethodDto**: Basic DTO for shipping method information.
- **ShipMethodListDto**: DTO for shipping methods in list views, includes order count.
- **ShipMethodDetailsDto**: Detailed DTO with audit information and additional details.

### Commands
- **CreateShipMethod**: Creates a new shipping method.
- **UpdateShipMethod**: Updates an existing shipping method.
- **DeleteShipMethod**: Soft deletes a shipping method by setting its Active property to false.

### Queries
- **GetAllShipMethods**: Retrieves all active shipping methods ordered by ordinal position.
- **GetShipMethodById**: Retrieves a specific shipping method by its ID.
- **GetShipMethodByMethod**: Retrieves shipping methods by their method name.
- **GetDefaultShipMethod**: Retrieves the default shipping method (the one with the lowest ordinal position).

### Validators
- **ShipMethodDtoValidator**: Validates the ShipMethodDto.
- **ShipMethodListDtoValidator**: Validates the ShipMethodListDto.
- **ShipMethodDetailsDtoValidator**: Validates the ShipMethodDetailsDto.
- **CreateShipMethodCommandValidator**: Validates the CreateShipMethodCommand.
- **UpdateShipMethodCommandValidator**: Validates the UpdateShipMethodCommand.
- **DeleteShipMethodCommandValidator**: Validates the DeleteShipMethodCommand.
- **GetAllShipMethodsQueryValidator**: Validates the GetAllShipMethodsQuery.
- **GetShipMethodByIdQueryValidator**: Validates the GetShipMethodByIdQuery.
- **GetShipMethodByMethodQueryValidator**: Validates the GetShipMethodByMethodQuery.
- **GetDefaultShipMethodQueryValidator**: Validates the GetDefaultShipMethodQuery.

### Mappings
- **ShipMethodMappingProfile**: AutoMapper profile for mapping between ShipMethod entities and DTOs.

## Usage Examples

### Creating a Shipping Method
```csharp
var command = new CreateShipMethodCommand
{
    Method = "Express",
    Description = "2-3 day delivery service with tracking",
    OrdinalPosition = 2
};

var result = await _mediator.Send(command);
```

### Updating a Shipping Method
```csharp
var command = new UpdateShipMethodCommand
{
    Id = shipMethodId,
    Method = "Overnight",
    Description = "Next-day delivery service with tracking and signature confirmation",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Deleting a Shipping Method
```csharp
var command = new DeleteShipMethodCommand
{
    Id = shipMethodId
};

var result = await _mediator.Send(command);
```

### Retrieving Shipping Methods
```csharp
// Get all shipping methods
var allShipMethods = await _mediator.Send(new GetAllShipMethodsQuery());

// Get shipping method by ID
var shipMethod = await _mediator.Send(new GetShipMethodByIdQuery { Id = shipMethodId });

// Get shipping methods by method name
var expressShipMethods = await _mediator.Send(new GetShipMethodByMethodQuery { Method = "Express" });

// Get default shipping method
var defaultShipMethod = await _mediator.Send(new GetDefaultShipMethodQuery());
```

## Implementation Details
- Follows Clean Architecture principles and CQRS pattern.
- Uses Dapper for database operations.
- Implements soft delete functionality by setting the Active property to false instead of physically removing records.
- Uses FluentValidation for input validation.
- Provides comprehensive error handling and logging.
- Follows the standardized soft delete pattern used throughout the VibeCRM system, where all entities have a boolean `Active` property that defaults to `true`, and when an entity is "deleted", the `Active` property is set to `false`. All queries filter by `Active = 1` to only show active records.

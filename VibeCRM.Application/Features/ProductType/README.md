# ProductType Feature

## Overview
The ProductType feature provides functionality for managing product types in the VibeCRM system. Product types are used to categorize products for organization and reporting purposes.

## Architecture
This feature follows Clean Architecture principles and the CQRS pattern:

- **Domain Layer**: Contains the ProductType entity and repository interface
- **Application Layer**: Contains commands, queries, DTOs, validators, and mapping profiles
- **Infrastructure Layer**: Contains the repository implementation

## Commands
- **CreateProductType**: Creates a new product type
- **UpdateProductType**: Updates an existing product type
- **DeleteProductType**: Soft-deletes a product type (sets Active = false)

## Queries
- **GetAllProductTypes**: Retrieves all active product types
- **GetProductTypeById**: Retrieves a specific product type by its ID
- **GetProductTypeByOrdinalPosition**: Retrieves a product type by its ordinal position
- **GetProductTypeByType**: Retrieves a product type by its type name
- **GetDefaultProductType**: Retrieves the default product type (lowest ordinal position)

## DTOs
- **ProductTypeDto**: Basic information for product types
- **ProductTypeListDto**: Information for listing product types, includes product count
- **ProductTypeDetailsDto**: Detailed information including audit fields

## Validation Rules
- Type name is required and cannot exceed 50 characters
- Description is required and cannot exceed 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all product types
- Product count must be a non-negative number

## Usage Examples

### Create a new product type
```csharp
var command = new CreateProductTypeCommand
{
    Type = "Hardware",
    Description = "Physical computing components and devices",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Update an existing product type
```csharp
var command = new UpdateProductTypeCommand
{
    Id = productTypeId,
    Type = "Updated Hardware",
    Description = "Updated description for hardware products",
    OrdinalPosition = 2
};

var result = await _mediator.Send(command);
```

### Delete a product type
```csharp
var command = new DeleteProductTypeCommand
{
    Id = productTypeId
};

var result = await _mediator.Send(command);
```

### Get all product types
```csharp
var query = new GetAllProductTypesQuery();
var result = await _mediator.Send(query);
```

### Get a product type by ID
```csharp
var query = new GetProductTypeByIdQuery { Id = productTypeId };
var result = await _mediator.Send(query);
```

### Get a product type by ordinal position
```csharp
var query = new GetProductTypeByOrdinalPositionQuery { OrdinalPosition = 1 };
var result = await _mediator.Send(query);
```

### Get a product type by type name
```csharp
var query = new GetProductTypeByTypeQuery { Type = "Hardware" };
var result = await _mediator.Send(query);
```

### Get the default product type
```csharp
var query = new GetDefaultProductTypeQuery();
var result = await _mediator.Send(query);
```

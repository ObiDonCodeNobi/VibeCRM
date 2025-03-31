# ProductType Feature

## Overview
The ProductType feature provides functionality for managing product types in the VibeCRM system. Product types are used to categorize products for organization and reporting purposes.

## Domain Model
The ProductType entity is a reference entity that represents a type category for products. Each ProductType has the following properties:

- **ProductTypeId**: Unique identifier (UUID)
- **Type**: Name of the product type (e.g., "Hardware", "Software", "Service")
- **Description**: Detailed description of what the product type means
- **OrdinalPosition**: Numeric value for ordering product types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Products**: Collection of associated Product entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ProductTypeDto**: Base DTO with core properties
- **ProductTypeDetailsDto**: Extended DTO with audit fields and product count
- **ProductTypeListDto**: Optimized DTO for list views

### Commands
- **CreateProductType**: Creates a new product type
- **UpdateProductType**: Updates an existing product type
- **DeleteProductType**: Soft-deletes a product type by setting Active = false

### Queries
- **GetAllProductTypes**: Retrieves all active product types
- **GetProductTypeById**: Retrieves a specific product type by its ID
- **GetProductTypeByOrdinalPosition**: Retrieves a product type by its ordinal position
- **GetProductTypeByType**: Retrieves a product type by its type name
- **GetDefaultProductType**: Retrieves the default product type (lowest ordinal position)

### Validators
- **ProductTypeDtoValidator**: Validates the base DTO
- **ProductTypeDetailsDtoValidator**: Validates the detailed DTO
- **ProductTypeListDtoValidator**: Validates the list DTO
- **GetProductTypeByIdQueryValidator**: Validates the ID query
- **GetProductTypeByTypeQueryValidator**: Validates the type name query
- **GetProductTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllProductTypesQueryValidator**: Validates the "get all" query

### Mappings
- **ProductTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new ProductType
```csharp
var command = new CreateProductTypeCommand
{
    Type = "Hardware",
    Description = "Physical computing components and devices",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all ProductTypes
```csharp
var query = new GetAllProductTypesQuery();
var productTypes = await _mediator.Send(query);
```

### Retrieving a ProductType by ID
```csharp
var query = new GetProductTypeByIdQuery { Id = productTypeId };
var productType = await _mediator.Send(query);
```

### Retrieving a ProductType by type name
```csharp
var query = new GetProductTypeByTypeQuery { Type = "Hardware" };
var productType = await _mediator.Send(query);
```

### Retrieving the default ProductType
```csharp
var query = new GetDefaultProductTypeQuery();
var defaultProductType = await _mediator.Send(query);
```

### Updating a ProductType
```csharp
var command = new UpdateProductTypeCommand
{
    Id = productTypeId,
    Type = "Updated Hardware",
    Description = "Updated description for hardware products",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a ProductType
```csharp
var command = new DeleteProductTypeCommand
{
    Id = productTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The ProductType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all product types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Product types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Product Associations
Each ProductType can be associated with multiple Product entities. The feature includes functionality to retrieve the count of products using each type.

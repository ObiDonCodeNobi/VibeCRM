# Product Feature

## Overview
The Product feature provides comprehensive functionality for managing products within the VibeCRM system. It allows for creating, retrieving, updating, and soft-deleting product records that can be used in quotes, sales orders, and invoices.

## Domain Model
The Product entity is a core business entity that represents a sellable item in the CRM system. Each Product has the following properties:

- **ProductId**: Unique identifier (UUID)
- **Name**: The name of the product
- **Description**: Detailed description of the product
- **SKU**: Stock Keeping Unit - unique product identifier
- **UPC**: Universal Product Code (optional)
- **ProductGroupId**: Reference to the product group/category
- **UnitPrice**: Standard price per unit
- **Cost**: Cost per unit
- **QuantityOnHand**: Current inventory quantity
- **ReorderPoint**: Quantity threshold for reordering
- **Weight**: Product weight (optional)
- **Dimensions**: Product dimensions (optional)
- **IsActive**: Boolean flag indicating if the product is active for sale
- **IsTaxable**: Boolean flag indicating if the product is taxable
- **TaxRate**: Tax rate percentage if taxable
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **ProductGroup**: Navigation property to the associated ProductGroup
- **SalesOrderLineItems**: Collection of associated SalesOrderLineItem entities
- **QuoteLineItems**: Collection of associated QuoteLineItem entities
- **InvoiceLineItems**: Collection of associated InvoiceLineItem entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ProductDto**: Base DTO with core properties
- **ProductDetailsDto**: Extended DTO with audit fields and related data
- **ProductListDto**: Optimized DTO for list views

### Commands
- **CreateProduct**: Creates a new product
- **UpdateProduct**: Updates an existing product
- **DeleteProduct**: Soft-deletes a product by setting Active = false
- **UpdateProductInventory**: Updates the inventory quantity for a product
- **UpdateProductPricing**: Updates the pricing information for a product
- **ActivateProduct**: Sets a product as active for sale
- **DeactivateProduct**: Sets a product as inactive for sale

### Queries
- **GetAllProducts**: Retrieves all active products
- **GetProductById**: Retrieves a specific product by its ID
- **GetProductBySKU**: Retrieves a product by its SKU
- **GetProductsByGroup**: Retrieves products in a specific product group
- **GetProductsByPriceRange**: Retrieves products within a price range
- **GetLowStockProducts**: Retrieves products below their reorder point
- **GetActiveProducts**: Retrieves only active products
- **GetInactiveProducts**: Retrieves only inactive products

### Validators
- **ProductDtoValidator**: Validates the base DTO
- **ProductDetailsDtoValidator**: Validates the detailed DTO
- **ProductListDtoValidator**: Validates the list DTO
- **CreateProductCommandValidator**: Validates the create command
- **UpdateProductCommandValidator**: Validates the update command
- **DeleteProductCommandValidator**: Validates the delete command
- **UpdateProductInventoryCommandValidator**: Validates the update inventory command
- **UpdateProductPricingCommandValidator**: Validates the update pricing command
- **ActivateProductCommandValidator**: Validates the activate command
- **DeactivateProductCommandValidator**: Validates the deactivate command
- **GetProductByIdQueryValidator**: Validates the ID query
- **GetProductBySKUQueryValidator**: Validates the SKU query
- **GetProductsByGroupQueryValidator**: Validates the group query
- **GetProductsByPriceRangeQueryValidator**: Validates the price range query

### Mappings
- **ProductMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Product
```csharp
var command = new CreateProductCommand
{
    Name = "Premium Widget",
    Description = "High-quality widget with extended durability",
    SKU = "WDG-PREM-001",
    UPC = "123456789012",
    ProductGroupId = productGroupId,
    UnitPrice = 29.99m,
    Cost = 15.50m,
    QuantityOnHand = 100,
    ReorderPoint = 25,
    Weight = 1.5m,
    Dimensions = "10x5x3",
    IsActive = true,
    IsTaxable = true,
    TaxRate = 8.25m,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Products
```csharp
var query = new GetAllProductsQuery();
var products = await _mediator.Send(query);
```

### Retrieving a Product by ID
```csharp
var query = new GetProductByIdQuery { Id = productId };
var product = await _mediator.Send(query);
```

### Retrieving Products by Group
```csharp
var query = new GetProductsByGroupQuery { ProductGroupId = productGroupId };
var products = await _mediator.Send(query);
```

### Updating Product Inventory
```csharp
var command = new UpdateProductInventoryCommand
{
    Id = productId,
    QuantityOnHand = 85,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Product
```csharp
var command = new UpdateProductCommand
{
    Id = productId,
    Name = "Premium Widget Pro",
    Description = "Enhanced premium widget with additional features",
    UnitPrice = 34.99m,
    ReorderPoint = 30,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deactivating a Product
```csharp
var command = new DeactivateProductCommand
{
    Id = productId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Product feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Name is required and limited to 100 characters
- Description is required and limited to 500 characters
- SKU is required, limited to 50 characters, and must be unique
- UPC is optional but must be a valid UPC format if provided
- ProductGroupId must reference a valid product group
- UnitPrice and Cost must be non-negative
- QuantityOnHand must be non-negative
- ReorderPoint must be non-negative
- Weight and Dimensions are optional
- TaxRate must be between 0 and 100 if IsTaxable is true
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Inventory Management
- The system tracks current inventory levels through the QuantityOnHand property
- When inventory falls below the ReorderPoint, the product appears in low stock reports
- Inventory updates are logged for audit purposes
- The system can generate inventory valuation reports based on Cost and QuantityOnHand

### Pricing Logic
- UnitPrice represents the standard selling price
- The system supports special pricing rules (discounts, promotions, etc.)
- Margin and markup calculations are available based on UnitPrice and Cost
- Price history is maintained for reporting purposes

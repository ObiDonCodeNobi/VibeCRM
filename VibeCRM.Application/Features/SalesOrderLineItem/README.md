# SalesOrderLineItem Feature

## Overview
The SalesOrderLineItem feature provides functionality for managing line items associated with sales orders in the VibeCRM system. Each sales order line item represents a product or service included in a sales order, along with quantity, pricing, and discount information.

## Components

### DTOs
- **SalesOrderLineItemDto**: Base DTO containing all properties of a sales order line item
- **SalesOrderLineItemListDto**: Simplified DTO for list views with essential properties
- **SalesOrderLineItemDetailsDto**: Extended DTO with audit properties and calculated fields (extended price, discount value, tax amount, total price)

### Commands
- **CreateSalesOrderLineItem**: Creates a new sales order line item
- **UpdateSalesOrderLineItem**: Updates an existing sales order line item
- **DeleteSalesOrderLineItem**: Soft deletes a sales order line item by setting Active = false

### Queries
- **GetAllSalesOrderLineItems**: Retrieves all active sales order line items
- **GetSalesOrderLineItemById**: Retrieves a specific sales order line item by ID
- **GetSalesOrderLineItemsBySalesOrder**: Retrieves all line items for a specific sales order
- **GetSalesOrderLineItemsByProduct**: Retrieves all line items for a specific product
- **GetSalesOrderLineItemsByService**: Retrieves all line items for a specific service
- **GetTotalForSalesOrder**: Calculates the total amount for a sales order

### Validators
- **SalesOrderLineItemDtoValidator**: Validates SalesOrderLineItemDto properties
- **SalesOrderLineItemListDtoValidator**: Validates SalesOrderLineItemListDto properties
- **SalesOrderLineItemDetailsDtoValidator**: Validates SalesOrderLineItemDetailsDto properties

### Mappings
- **SalesOrderLineItemMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Implementation Details

### Soft Delete Pattern
The SalesOrderLineItem feature implements the standardized soft delete pattern using the `Active` property. When a line item is "deleted", its `Active` property is set to `false` rather than removing the record from the database. All queries filter by `Active = true` to exclude soft-deleted records.

### Validation Rules
- SalesOrderId is required
- Either ProductId or ServiceId must be provided
- Description is required and cannot exceed 500 characters
- Quantity must be positive
- UnitPrice must be non-negative
- DiscountPercentage must be between 0 and 100 if provided
- DiscountAmount must be non-negative if provided
- TaxRate must be non-negative if provided and item is taxable
- Notes cannot exceed 1000 characters if provided

### Calculated Fields
- **ExtendedPrice**: UnitPrice × Quantity
- **DiscountValue**: Either the fixed DiscountAmount or calculated from DiscountPercentage
- **TaxAmount**: Calculated based on price after discount and TaxRate if item is taxable
- **TotalPrice**: Price after discount plus tax amount

## Usage Examples

### Creating a Sales Order Line Item
```csharp
var command = new CreateSalesOrderLineItemCommand
{
    SalesOrderId = salesOrderId,
    ProductId = productId,
    Description = "Product Description",
    Quantity = 2,
    UnitPrice = 99.99m,
    IsTaxable = true,
    TaxRate = 8.25m
};

var result = await _mediator.Send(command);
```

### Retrieving Line Items for a Sales Order
```csharp
var query = new GetSalesOrderLineItemsBySalesOrderQuery
{
    SalesOrderId = salesOrderId
};

var lineItems = await _mediator.Send(query);
```

### Calculating Total for a Sales Order
```csharp
var query = new GetTotalForSalesOrderQuery
{
    SalesOrderId = salesOrderId
};

var total = await _mediator.Send(query);
```

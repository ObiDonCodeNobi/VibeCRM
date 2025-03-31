# SalesOrderLineItem Feature

## Overview
The SalesOrderLineItem feature provides functionality for managing line items associated with sales orders in the VibeCRM system. Each sales order line item represents a product or service included in a sales order, along with quantity, pricing, and discount information.

## Domain Model
The SalesOrderLineItem entity is a core business entity that represents an individual product or service line within a sales order. Each SalesOrderLineItem has the following properties:

- **SalesOrderLineItemId**: Unique identifier (UUID)
- **SalesOrderId**: Reference to the parent sales order
- **ProductId**: Optional reference to a product
- **ServiceId**: Optional reference to a service
- **Description**: Description of the line item
- **Quantity**: Quantity of the product or service
- **UnitPrice**: Price per unit
- **DiscountPercentage**: Optional percentage discount
- **DiscountAmount**: Optional fixed discount amount
- **IsTaxable**: Boolean flag indicating if the item is taxable
- **TaxRate**: Tax rate percentage if taxable
- **LineNumber**: Order of the line item within the sales order
- **Notes**: Additional notes or comments
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **SalesOrder**: Navigation property to the parent SalesOrder entity
- **Product**: Navigation property to the associated Product entity (if applicable)
- **Service**: Navigation property to the associated Service entity (if applicable)

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **SalesOrderLineItemDto**: Base DTO with core properties
- **SalesOrderLineItemDetailsDto**: Extended DTO with audit fields and calculated values
- **SalesOrderLineItemListDto**: Optimized DTO for list views

### Commands
- **CreateSalesOrderLineItem**: Creates a new sales order line item
- **UpdateSalesOrderLineItem**: Updates an existing sales order line item
- **DeleteSalesOrderLineItem**: Soft-deletes a sales order line item by setting Active = false
- **BulkCreateSalesOrderLineItems**: Creates multiple sales order line items in a single operation

### Queries
- **GetAllSalesOrderLineItems**: Retrieves all active sales order line items
- **GetSalesOrderLineItemById**: Retrieves a specific sales order line item by its ID
- **GetSalesOrderLineItemsBySalesOrder**: Retrieves all line items for a specific sales order
- **GetSalesOrderLineItemsByProduct**: Retrieves all line items for a specific product
- **GetSalesOrderLineItemsByService**: Retrieves all line items for a specific service
- **GetTotalForSalesOrder**: Calculates the total amount for a sales order

### Validators
- **SalesOrderLineItemDtoValidator**: Validates the base DTO
- **SalesOrderLineItemDetailsDtoValidator**: Validates the detailed DTO
- **SalesOrderLineItemListDtoValidator**: Validates the list DTO
- **CreateSalesOrderLineItemCommandValidator**: Validates the create command
- **UpdateSalesOrderLineItemCommandValidator**: Validates the update command
- **DeleteSalesOrderLineItemCommandValidator**: Validates the delete command
- **BulkCreateSalesOrderLineItemsCommandValidator**: Validates the bulk create command
- **GetSalesOrderLineItemByIdQueryValidator**: Validates the ID query
- **GetSalesOrderLineItemsBySalesOrderQueryValidator**: Validates the sales order query
- **GetSalesOrderLineItemsByProductQueryValidator**: Validates the product query
- **GetSalesOrderLineItemsByServiceQueryValidator**: Validates the service query
- **GetTotalForSalesOrderQueryValidator**: Validates the total query

### Mappings
- **SalesOrderLineItemMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new SalesOrderLineItem
```csharp
var command = new CreateSalesOrderLineItemCommand
{
    SalesOrderId = salesOrderId,
    ProductId = productId,
    Description = "Premium Widget",
    Quantity = 2,
    UnitPrice = 99.99m,
    DiscountPercentage = 10.0m,
    IsTaxable = true,
    TaxRate = 8.25m,
    LineNumber = 1,
    Notes = "Customer requested expedited shipping",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Creating multiple SalesOrderLineItems
```csharp
var command = new BulkCreateSalesOrderLineItemsCommand
{
    SalesOrderId = salesOrderId,
    LineItems = new List<SalesOrderLineItemDto>
    {
        new SalesOrderLineItemDto
        {
            ProductId = product1Id,
            Description = "Premium Widget",
            Quantity = 2,
            UnitPrice = 99.99m,
            IsTaxable = true,
            TaxRate = 8.25m,
            LineNumber = 1
        },
        new SalesOrderLineItemDto
        {
            ProductId = product2Id,
            Description = "Standard Widget",
            Quantity = 5,
            UnitPrice = 49.99m,
            DiscountPercentage = 5.0m,
            IsTaxable = true,
            TaxRate = 8.25m,
            LineNumber = 2
        }
    },
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all SalesOrderLineItems for a SalesOrder
```csharp
var query = new GetSalesOrderLineItemsBySalesOrderQuery
{
    SalesOrderId = salesOrderId
};

var lineItems = await _mediator.Send(query);
```

### Retrieving a SalesOrderLineItem by ID
```csharp
var query = new GetSalesOrderLineItemByIdQuery { Id = lineItemId };
var lineItem = await _mediator.Send(query);
```

### Calculating the Total for a SalesOrder
```csharp
var query = new GetTotalForSalesOrderQuery
{
    SalesOrderId = salesOrderId
};

var total = await _mediator.Send(query);
```

### Updating a SalesOrderLineItem
```csharp
var command = new UpdateSalesOrderLineItemCommand
{
    Id = lineItemId,
    Quantity = 3,
    UnitPrice = 89.99m,
    DiscountPercentage = 15.0m,
    Notes = "Customer requested additional quantity at a discounted price",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a SalesOrderLineItem
```csharp
var command = new DeleteSalesOrderLineItemCommand
{
    Id = lineItemId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The SalesOrderLineItem feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- SalesOrderId is required and must reference a valid sales order
- Either ProductId or ServiceId must be provided, or a Description must be entered
- Description is required and limited to 500 characters
- Quantity must be greater than zero
- UnitPrice must be greater than or equal to zero
- DiscountPercentage must be between 0 and 100 if provided
- DiscountAmount must be greater than or equal to zero if provided
- TaxRate must be greater than or equal to zero if IsTaxable is true
- LineNumber must be a positive integer
- Notes are optional but limited to 1000 characters
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Calculated Fields
The SalesOrderLineItem entity includes methods for calculating:
- **ExtendedPrice**: UnitPrice × Quantity
- **DiscountValue**: Either the fixed DiscountAmount or calculated from DiscountPercentage and ExtendedPrice
- **TaxAmount**: Calculated based on (ExtendedPrice - DiscountValue) × TaxRate if IsTaxable is true
- **TotalPrice**: ExtendedPrice - DiscountValue + TaxAmount

### SalesOrder Associations
Each SalesOrderLineItem is associated with exactly one SalesOrder. The feature includes functionality to retrieve all line items for a specific sales order and calculate totals for reporting and invoicing purposes.

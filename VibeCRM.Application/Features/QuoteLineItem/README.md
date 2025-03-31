# QuoteLineItem Feature

## Overview
The QuoteLineItem feature provides functionality for managing line items within quotes in the VibeCRM system. Quote line items represent individual products, services, or charges that make up a quote, including details such as quantity, pricing, discounts, and taxes.

## Domain Model
The QuoteLineItem entity represents a line item within a quote. Each QuoteLineItem has the following properties:

- **QuoteLineItemId**: Unique identifier (UUID)
- **QuoteId**: Reference to the parent quote
- **ProductId**: Optional reference to a product
- **ServiceId**: Optional reference to a service
- **Description**: Description of the line item
- **Quantity**: Quantity of the product or service
- **UnitPrice**: Price per unit
- **DiscountPercentage**: Optional percentage discount
- **DiscountAmount**: Optional fixed discount amount
- **TaxPercentage**: Optional tax percentage
- **LineNumber**: Order of the line item within the quote
- **Notes**: Additional notes or comments
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Quote**: Navigation property to the parent Quote entity
- **Product**: Navigation property to the associated Product entity (if applicable)
- **Service**: Navigation property to the associated Service entity (if applicable)

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **QuoteLineItemDto**: Base DTO with essential quote line item properties
- **QuoteLineItemDetailsDto**: Extended DTO with audit information and calculated values
- **QuoteLineItemListDto**: Simplified DTO for listing quote line items

### Commands
- **CreateQuoteLineItem**: Creates a new quote line item
- **UpdateQuoteLineItem**: Updates an existing quote line item
- **DeleteQuoteLineItem**: Soft-deletes a quote line item by setting Active = false

### Queries
- **GetQuoteLineItemById**: Retrieves a specific quote line item by its ID
- **GetAllQuoteLineItems**: Retrieves all active quote line items
- **GetQuoteLineItemsByQuote**: Retrieves all line items for a specific quote
- **GetQuoteLineItemsByProduct**: Retrieves all line items for a specific product
- **GetQuoteLineItemsByService**: Retrieves all line items for a specific service
- **GetQuoteLineItemsByDateRange**: Retrieves line items created within a date range
- **GetTotalForQuote**: Calculates the total amount for a quote

### Validators
- **QuoteLineItemDtoValidator**: Validates the base DTO
- **QuoteLineItemDetailsDtoValidator**: Validates the detailed DTO
- **QuoteLineItemListDtoValidator**: Validates the list DTO
- **CreateQuoteLineItemCommandValidator**: Validates the create command
- **UpdateQuoteLineItemCommandValidator**: Validates the update command
- **DeleteQuoteLineItemCommandValidator**: Validates the delete command
- **GetQuoteLineItemByIdQueryValidator**: Validates the ID query
- **GetQuoteLineItemsByQuoteQueryValidator**: Validates the quote query
- **GetQuoteLineItemsByProductQueryValidator**: Validates the product query
- **GetQuoteLineItemsByServiceQueryValidator**: Validates the service query
- **GetQuoteLineItemsByDateRangeQueryValidator**: Validates the date range query
- **GetTotalForQuoteQueryValidator**: Validates the total query

### Mappings
- **QuoteLineItemMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new QuoteLineItem
```csharp
var command = new CreateQuoteLineItemCommand
{
    QuoteId = quoteId,
    ProductId = productId,
    Description = "Product description",
    Quantity = 2,
    UnitPrice = 100.00m,
    TaxPercentage = 7.5m,
    LineNumber = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all QuoteLineItems for a Quote
```csharp
var query = new GetQuoteLineItemsByQuoteQuery
{
    QuoteId = quoteId
};

var quoteLineItems = await _mediator.Send(query);
```

### Calculating the Total for a Quote
```csharp
var query = new GetTotalForQuoteQuery
{
    QuoteId = quoteId
};

var total = await _mediator.Send(query);
```

### Updating a QuoteLineItem
```csharp
var command = new UpdateQuoteLineItemCommand
{
    Id = quoteLineItemId,
    QuoteId = quoteId,
    ProductId = productId,
    Description = "Updated product description",
    Quantity = 3,
    UnitPrice = 95.00m,
    TaxPercentage = 7.5m,
    LineNumber = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a QuoteLineItem
```csharp
var command = new DeleteQuoteLineItemCommand
{
    Id = quoteLineItemId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The QuoteLineItem feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- QuoteId is required and must reference a valid Quote
- Either ProductId or ServiceId must be provided, or a Description must be entered
- Quantity must be greater than zero
- UnitPrice must be greater than or equal to zero
- DiscountPercentage must be between 0 and 100 if provided
- DiscountAmount must be greater than or equal to zero if provided
- TaxPercentage must be greater than or equal to zero if provided
- LineNumber must be a positive integer
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Calculated Fields
The QuoteLineItem entity includes methods for calculating:
- Extended price (quantity × unit price)
- Discount amount (based on either percentage or fixed amount)
- Tax amount (based on tax percentage)
- Total price (extended price - discount + tax)

### Quote Associations
Each QuoteLineItem is associated with exactly one Quote. The feature includes functionality to retrieve all line items for a specific quote and calculate totals.

# QuoteLineItem Feature

## Overview

The QuoteLineItem feature provides functionality for managing line items within quotes in the VibeCRM system. Quote line items represent individual products, services, or charges that make up a quote, including details such as quantity, pricing, discounts, and taxes.

## Domain Entity

The `QuoteLineItem` entity represents a line item within a quote and includes the following key properties:

- `QuoteLineItemId`: Unique identifier for the quote line item
- `QuoteId`: Reference to the parent quote
- `ProductId`: Optional reference to a product
- `ServiceId`: Optional reference to a service
- `Description`: Description of the line item
- `Quantity`: Quantity of the product or service
- `UnitPrice`: Price per unit
- `DiscountPercentage`: Optional percentage discount
- `DiscountAmount`: Optional fixed discount amount
- `TaxPercentage`: Optional tax percentage
- `LineNumber`: Order of the line item within the quote
- `Notes`: Additional notes or comments

The entity also includes methods for calculating:
- Extended price (quantity × unit price)
- Discount amount
- Tax amount
- Total price

## Components

### DTOs

- `QuoteLineItemDto`: Base DTO with essential quote line item properties
- `QuoteLineItemDetailsDto`: Extended DTO with audit information and calculated values
- `QuoteLineItemListDto`: Simplified DTO for listing quote line items

### Commands

#### CreateQuoteLineItem
- `CreateQuoteLineItemCommand`: Command for creating a new quote line item
- `CreateQuoteLineItemCommandHandler`: Handler that processes the create command
- `CreateQuoteLineItemCommandValidator`: Validator that ensures the create command data is valid

#### UpdateQuoteLineItem
- `UpdateQuoteLineItemCommand`: Command for updating an existing quote line item
- `UpdateQuoteLineItemCommandHandler`: Handler that processes the update command
- `UpdateQuoteLineItemCommandValidator`: Validator that ensures the update command data is valid

#### DeleteQuoteLineItem
- `DeleteQuoteLineItemCommand`: Command for soft-deleting a quote line item
- `DeleteQuoteLineItemCommandHandler`: Handler that processes the delete command
- `DeleteQuoteLineItemCommandValidator`: Validator that ensures the delete command data is valid

### Queries

#### GetQuoteLineItemById
- `GetQuoteLineItemByIdQuery`: Query to retrieve a specific quote line item by ID
- `GetQuoteLineItemByIdQueryHandler`: Handler that processes the query
- `GetQuoteLineItemByIdQueryValidator`: Validator that ensures the query parameters are valid

#### GetAllQuoteLineItems
- `GetAllQuoteLineItemsQuery`: Query to retrieve all active quote line items
- `GetAllQuoteLineItemsQueryHandler`: Handler that processes the query

#### GetQuoteLineItemsByQuote
- `GetQuoteLineItemsByQuoteQuery`: Query to retrieve all line items for a specific quote
- `GetQuoteLineItemsByQuoteQueryHandler`: Handler that processes the query
- `GetQuoteLineItemsByQuoteQueryValidator`: Validator that ensures the query parameters are valid

#### GetQuoteLineItemsByProduct
- `GetQuoteLineItemsByProductQuery`: Query to retrieve all line items for a specific product
- `GetQuoteLineItemsByProductQueryHandler`: Handler that processes the query
- `GetQuoteLineItemsByProductQueryValidator`: Validator that ensures the query parameters are valid

#### GetQuoteLineItemsByService
- `GetQuoteLineItemsByServiceQuery`: Query to retrieve all line items for a specific service
- `GetQuoteLineItemsByServiceQueryHandler`: Handler that processes the query
- `GetQuoteLineItemsByServiceQueryValidator`: Validator that ensures the query parameters are valid

#### GetQuoteLineItemsByDateRange
- `GetQuoteLineItemsByDateRangeQuery`: Query to retrieve line items created within a date range
- `GetQuoteLineItemsByDateRangeQueryHandler`: Handler that processes the query
- `GetQuoteLineItemsByDateRangeQueryValidator`: Validator that ensures the query parameters are valid

#### GetTotalForQuote
- `GetTotalForQuoteQuery`: Query to calculate the total amount for a quote
- `GetTotalForQuoteQueryHandler`: Handler that processes the query
- `GetTotalForQuoteQueryValidator`: Validator that ensures the query parameters are valid

### Mappings

- `QuoteLineItemMappingProfile`: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a Quote Line Item

```csharp
var createCommand = new CreateQuoteLineItemCommand
{
    QuoteId = quoteId,
    ProductId = productId,
    Description = "Product description",
    Quantity = 2,
    UnitPrice = 100.00m,
    TaxPercentage = 7.5m,
    LineNumber = 1,
    CreatedBy = userId
};

var result = await mediator.Send(createCommand);
```

### Updating a Quote Line Item

```csharp
var updateCommand = new UpdateQuoteLineItemCommand
{
    Id = quoteLineItemId,
    QuoteId = quoteId,
    ProductId = productId,
    Description = "Updated product description",
    Quantity = 3,
    UnitPrice = 95.00m,
    TaxPercentage = 7.5m,
    LineNumber = 1,
    ModifiedBy = userId
};

var result = await mediator.Send(updateCommand);
```

### Retrieving Quote Line Items for a Quote

```csharp
var query = new GetQuoteLineItemsByQuoteQuery
{
    QuoteId = quoteId
};

var quoteLineItems = await mediator.Send(query);
```

### Calculating the Total for a Quote

```csharp
var query = new GetTotalForQuoteQuery
{
    QuoteId = quoteId
};

var total = await mediator.Send(query);
```

## Implementation Notes

- The feature follows the CQRS pattern using MediatR
- Soft delete is implemented using the `Active` property
- All validation is performed using FluentValidation
- Entity-DTO mapping is handled by AutoMapper
- Comprehensive logging is included in all handlers

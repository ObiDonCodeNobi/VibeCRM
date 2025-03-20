# Quote Entity Navigation Properties Update

## Overview
This document summarizes the changes made to update the Quote entity navigation properties in the VibeCRM system. The primary goal was to enhance the Quote entity by adding missing navigation properties, establishing bidirectional relationships with related entities, updating repository interfaces and implementations, and ensuring that all related entities are loaded correctly in the CQRS feature updates.

## Changes Made

### Domain Layer
- Verified that the Quote entity already had the necessary navigation properties:
  - `QuoteStatusId` (nullable Guid)
  - `QuoteStatus` (navigation property)
  - `LineItems` (collection of QuoteLineItem)
  - `SalesOrders` (collection of SalesOrder)

### Repository Interface
- Updated the `IQuoteRepository` interface to include methods for loading related entities:
  - `LoadQuoteStatusAsync`: Loads the quote status for a quote
  - `LoadLineItemsAsync`: Loads the line items for a quote
  - `LoadSalesOrdersAsync`: Loads the sales orders for a quote

### Repository Implementation
- Made the following methods public in the `QuoteRepository` implementation:
  - `LoadQuoteStatusAsync`
  - `LoadLineItemsAsync`
  - `LoadSalesOrdersAsync`

### DTOs
- Updated `QuoteDetailsDto` to include:
  - `QuoteStatusId` (nullable)
  - `QuoteStatusName` (nullable)
  - `ICollection<QuoteLineItemDto> LineItems`
  - `ICollection<SalesOrderListDto> SalesOrders`
- Updated `QuoteListDto` to include:
  - `QuoteStatusId` (nullable)
  - `QuoteStatusName` (nullable)
  - `LineItemCount`
  - `TotalAmount`
- Created `QuoteLineItemDto` to represent individual line items in quotes

### Mapping Profiles
- Updated `QuoteMappingProfile` to include mappings for:
  - Quote to QuoteDetailsDto (including QuoteStatus, LineItems, and SalesOrders)
  - Quote to QuoteListDto (including QuoteStatus, LineItemCount, and TotalAmount)
  - QuoteLineItem to QuoteLineItemDto
  - SalesOrder to SalesOrderListDto

### Command Classes
- Updated `CreateQuoteCommand` to include:
  - `QuoteStatusId` (nullable)
- Updated `UpdateQuoteCommand` to include:
  - `QuoteStatusId` (nullable)

### Command Handlers
- Updated `CreateQuoteCommandHandler` to:
  - Handle the new QuoteStatusId property
  - Load related entities after creating a quote
- Updated `UpdateQuoteCommandHandler` to:
  - Handle the new QuoteStatusId property
  - Load related entities after updating a quote

### Query Handlers
- Updated `GetQuoteByIdQueryHandler` to use `GetByIdWithRelatedEntitiesAsync` to load related entities
- Updated `GetAllQuotesQueryHandler` to load related entities for each quote
- Updated `GetQuotesByCompanyQueryHandler` to load related entities for quotes retrieved by company
- Updated `GetQuotesByNumberQueryHandler` to load related entities for quotes retrieved by number
- Updated `GetQuotesByActivityQueryHandler` to load related entities for quotes retrieved by activity

### Validators
- Updated `QuoteDetailsDtoValidator` to include validation rules for:
  - QuoteStatusName
  - LineItems
  - SalesOrders
- Created `QuoteLineItemDtoValidator` to validate QuoteLineItemDto properties

## Benefits
1. **Improved Data Access**: The updated navigation properties allow for more efficient data retrieval and manipulation.
2. **Enhanced User Experience**: Applications can now display more comprehensive information about quotes, including their status, line items, and associated sales orders.
3. **Maintainable Code**: The standardized approach to loading related entities ensures consistency across the codebase.
4. **Better Data Integrity**: The bidirectional relationships help maintain data integrity between related entities.

## Next Steps
1. **Testing**: Conduct thorough unit and integration testing to ensure that the changes do not introduce any regressions or issues.
2. **Documentation**: Update any relevant API documentation to reflect the changes made to the Quote entity and its relationships.
3. **Review Other Related Features**: Check if other features or components in the application need updates or adjustments due to the changes made in the Quote entity and its navigation properties.

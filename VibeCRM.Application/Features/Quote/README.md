# Quote Feature

## Overview
The Quote feature in VibeCRM provides comprehensive functionality for managing quotes within the CRM system. It allows for creating, retrieving, updating, and soft-deleting quote records that represent sales proposals to customers.

## Domain Model
The Quote entity is a core business entity that represents a sales proposal in the CRM system. Each Quote has the following properties:

- **QuoteId**: Unique identifier (UUID)
- **Number**: Unique quote number (e.g., Q-2025-001)
- **Title**: Title or name of the quote
- **Description**: Detailed description of the quote
- **CompanyId**: Reference to the associated company
- **ContactPersonId**: Reference to the primary contact person
- **QuoteDate**: Date the quote was created
- **ExpirationDate**: Date the quote expires
- **Status**: Current status of the quote (e.g., Draft, Sent, Accepted, Rejected)
- **TotalAmount**: Total amount of the quote
- **DiscountPercentage**: Percentage discount applied to the quote
- **DiscountAmount**: Fixed discount amount applied to the quote
- **TaxPercentage**: Tax percentage applied to the quote
- **TaxAmount**: Calculated tax amount
- **GrandTotal**: Final total after discounts and taxes
- **Terms**: Terms and conditions of the quote
- **Notes**: Additional notes or comments
- **ActivityId**: Optional reference to the associated activity
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Company**: Navigation property to the associated Company
- **ContactPerson**: Navigation property to the associated Person
- **Activity**: Navigation property to the associated Activity
- **QuoteLineItems**: Collection of associated QuoteLineItem entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **QuoteDto**: Base DTO with core properties
- **QuoteDetailsDto**: Extended DTO with audit fields and related data
- **QuoteListDto**: Optimized DTO for list views

### Commands
- **CreateQuote**: Creates a new quote
- **UpdateQuote**: Updates an existing quote
- **DeleteQuote**: Soft-deletes a quote by setting Active = false
- **ConvertQuoteToSalesOrder**: Converts a quote to a sales order
- **SendQuote**: Sends a quote to a customer via email
- **AcceptQuote**: Marks a quote as accepted
- **RejectQuote**: Marks a quote as rejected
- **DuplicateQuote**: Creates a new quote based on an existing one

### Queries
- **GetAllQuotes**: Retrieves all active quotes
- **GetQuoteById**: Retrieves a specific quote by its ID
- **GetQuotesByNumber**: Retrieves quotes by their number
- **GetQuotesByCompany**: Retrieves quotes associated with a specific company
- **GetQuotesByContactPerson**: Retrieves quotes associated with a specific contact person
- **GetQuotesByActivity**: Retrieves quotes associated with a specific activity
- **GetQuotesByStatus**: Retrieves quotes with a specific status
- **GetQuotesByDateRange**: Retrieves quotes within a specific date range
- **GetExpiredQuotes**: Retrieves quotes that have expired

### Validators
- **QuoteDtoValidator**: Validates the base DTO
- **QuoteDetailsDtoValidator**: Validates the detailed DTO
- **QuoteListDtoValidator**: Validates the list DTO
- **CreateQuoteCommandValidator**: Validates the create command
- **UpdateQuoteCommandValidator**: Validates the update command
- **DeleteQuoteCommandValidator**: Validates the delete command
- **ConvertQuoteToSalesOrderCommandValidator**: Validates the convert command
- **SendQuoteCommandValidator**: Validates the send command
- **AcceptQuoteCommandValidator**: Validates the accept command
- **RejectQuoteCommandValidator**: Validates the reject command
- **DuplicateQuoteCommandValidator**: Validates the duplicate command
- **GetQuoteByIdQueryValidator**: Validates the ID query
- **GetQuotesByNumberQueryValidator**: Validates the number query
- **GetQuotesByCompanyQueryValidator**: Validates the company query
- **GetQuotesByContactPersonQueryValidator**: Validates the contact person query
- **GetQuotesByActivityQueryValidator**: Validates the activity query
- **GetQuotesByStatusQueryValidator**: Validates the status query
- **GetQuotesByDateRangeQueryValidator**: Validates the date range query

### Mappings
- **QuoteMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Quote
```csharp
var command = new CreateQuoteCommand
{
    Number = "Q-2025-001",
    Title = "Annual Software Subscription",
    Description = "Proposal for annual software subscription and support",
    CompanyId = companyId,
    ContactPersonId = contactPersonId,
    QuoteDate = DateTime.Now,
    ExpirationDate = DateTime.Now.AddMonths(1),
    Status = "Draft",
    TotalAmount = 10000.00m,
    DiscountPercentage = 10.0m,
    TaxPercentage = 8.25m,
    Terms = "Net 30",
    Notes = "Customer requested expedited delivery",
    ActivityId = activityId,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Quotes
```csharp
var query = new GetAllQuotesQuery();
var quotes = await _mediator.Send(query);
```

### Retrieving a Quote by ID
```csharp
var query = new GetQuoteByIdQuery { Id = quoteId };
var quote = await _mediator.Send(query);
```

### Retrieving Quotes by Company
```csharp
var query = new GetQuotesByCompanyQuery { CompanyId = companyId };
var quotes = await _mediator.Send(query);
```

### Converting a Quote to a Sales Order
```csharp
var command = new ConvertQuoteToSalesOrderCommand
{
    QuoteId = quoteId,
    ModifiedBy = currentUserId
};

var salesOrderId = await _mediator.Send(command);
```

### Updating a Quote
```csharp
var command = new UpdateQuoteCommand
{
    Id = quoteId,
    Title = "Updated Software Subscription",
    Description = "Updated proposal with additional services",
    ExpirationDate = DateTime.Now.AddMonths(2),
    Status = "Revised",
    DiscountPercentage = 15.0m,
    Notes = "Customer requested additional services",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Quote
```csharp
var command = new DeleteQuoteCommand
{
    Id = quoteId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Quote feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Number is required, limited to 50 characters, and must be unique
- Title is required and limited to 100 characters
- CompanyId must reference a valid company
- ContactPersonId must reference a valid person
- QuoteDate is required and cannot be in the future
- ExpirationDate must be after QuoteDate
- Status must be one of the predefined status values
- TotalAmount, DiscountPercentage, DiscountAmount, TaxPercentage, and TaxAmount must be non-negative
- Terms and Notes are optional but limited to 500 characters each
- ActivityId must reference a valid activity if provided
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Quote Lifecycle
- Quotes progress through various statuses: Draft, Sent, Accepted, Rejected, Expired
- Status transitions are controlled by specific commands with appropriate validation
- When a quote is accepted, it can be converted to a sales order
- Expired quotes are automatically identified based on their expiration date

### Quote Calculations
- The system automatically calculates DiscountAmount based on TotalAmount and DiscountPercentage
- The system automatically calculates TaxAmount based on (TotalAmount - DiscountAmount) and TaxPercentage
- GrandTotal is calculated as TotalAmount - DiscountAmount + TaxAmount
- These calculations are performed both in the UI and in the backend for consistency

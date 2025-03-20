# Quote Feature

## Overview
The Quote feature in VibeCRM provides comprehensive functionality for managing quotes within the CRM system. It follows the Clean Architecture and CQRS pattern using MediatR, with a focus on maintainability and extensibility.

## Architecture
This feature follows the Onion Architecture principles and is structured according to the CQRS pattern:

- **DTOs**: Data Transfer Objects for transferring quote data between layers
- **Commands**: For creating, updating, and deleting quotes
- **Queries**: For retrieving quotes based on various criteria
- **Validators**: For validating the input data using FluentValidation
- **Mappings**: For mapping between domain entities and DTOs using AutoMapper

## Components

### DTOs
- **QuoteDto**: Base DTO containing essential quote information
- **QuoteDetailsDto**: Extended DTO with audit information
- **QuoteListDto**: Simplified DTO for listing quotes in UI components

### Commands
- **CreateQuote**: Creates a new quote in the system
- **UpdateQuote**: Updates an existing quote
- **DeleteQuote**: Soft-deletes a quote by setting the Active flag to false

### Queries
- **GetAllQuotes**: Retrieves all active quotes
- **GetQuoteById**: Retrieves a specific quote by its ID
- **GetQuotesByNumber**: Retrieves quotes by their number
- **GetQuotesByCompany**: Retrieves quotes associated with a specific company
- **GetQuotesByActivity**: Retrieves quotes associated with a specific activity

### Validators
Each command and query has its own validator to ensure data integrity and business rule compliance.

### Mappings
The `QuoteMappingProfile` defines mappings between the Quote domain entity and various DTOs.

## Soft Delete Pattern
This feature implements the standard soft delete pattern using the `Active` property:
- When a quote is "deleted", the `Active` property is set to `false`
- All queries filter by `Active = true` to only show active quotes
- The original record remains in the database for historical purposes

## Usage Examples

### Creating a Quote
```csharp
var createCommand = new CreateQuoteCommand
{
    Number = "Q-2025-001",
    CreatedBy = userId
};

var result = await _mediator.Send(createCommand);
```

### Updating a Quote
```csharp
var updateCommand = new UpdateQuoteCommand
{
    Id = quoteId,
    Number = "Q-2025-001-REV",
    ModifiedBy = userId
};

var result = await _mediator.Send(updateCommand);
```

### Deleting a Quote
```csharp
var deleteCommand = new DeleteQuoteCommand
{
    Id = quoteId,
    ModifiedBy = userId
};

var success = await _mediator.Send(deleteCommand);
```

### Retrieving Quotes
```csharp
// Get all quotes
var allQuotes = await _mediator.Send(new GetAllQuotesQuery());

// Get quote by ID
var quote = await _mediator.Send(new GetQuoteByIdQuery { Id = quoteId });

// Get quotes by company
var companyQuotes = await _mediator.Send(new GetQuotesByCompanyQuery { CompanyId = companyId });

// Get quotes by activity
var activityQuotes = await _mediator.Send(new GetQuotesByActivityQuery { ActivityId = activityId });

// Get quotes by number
var numberQuotes = await _mediator.Send(new GetQuotesByNumberQuery { Number = "Q-2025" });
```

## Validation Rules
- Quote number is required and cannot exceed 50 characters
- IDs (Quote ID, Company ID, Activity ID) must be valid GUIDs
- User IDs for audit fields (CreatedBy, ModifiedBy) are required
- Quotes must exist and be active for update and delete operations

## Dependencies
- **MediatR**: For implementing the CQRS pattern
- **FluentValidation**: For input validation
- **AutoMapper**: For object mapping
- **Serilog**: For logging
- **Dapper**: For data access

## Error Handling
All command and query handlers include comprehensive error handling and logging to ensure system reliability and facilitate troubleshooting.

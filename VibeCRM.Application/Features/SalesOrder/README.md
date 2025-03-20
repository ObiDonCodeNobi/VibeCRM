# SalesOrder Feature

## Overview
The SalesOrder feature provides a comprehensive implementation for managing sales orders within the VibeCRM system. It follows Clean Architecture principles, CQRS pattern with MediatR, and implements the standardized soft delete pattern using the `Active` property.

## Structure
The feature is organized into the following components:

### DTOs (Data Transfer Objects)
- **SalesOrderDto**: Basic representation for create/update operations
- **SalesOrderDetailsDto**: Detailed representation including related entities
- **SalesOrderListDto**: Simplified representation for list views

### Validators
All DTOs and commands/queries have corresponding validators implemented using FluentValidation:
- **SalesOrderDtoValidator**
- **SalesOrderDetailsDtoValidator**
- **SalesOrderListDtoValidator**
- Command validators (Create, Update, Delete)
- Query validators (GetAll, GetById, GetByNumber, etc.)

### Commands
- **CreateSalesOrder**: Creates a new sales order
  - Command
  - Validator
  - Handler
- **UpdateSalesOrder**: Updates an existing sales order
  - Command
  - Validator
  - Handler
- **DeleteSalesOrder**: Performs a soft delete on an existing sales order
  - Command
  - Validator
  - Handler

### Queries
- **GetAllSalesOrders**: Retrieves all sales orders
  - Query
  - Validator
  - Handler
- **GetSalesOrderById**: Retrieves a specific sales order by ID
  - Query
  - Validator
  - Handler
- **GetSalesOrderByNumber**: Retrieves a sales order by its number
  - Query
  - Validator
  - Handler
- **GetSalesOrderByCompany**: Retrieves sales orders associated with a company
  - Query
  - Validator
  - Handler
- **GetSalesOrderByQuote**: Retrieves sales orders associated with a quote
  - Query
  - Validator
  - Handler
- **GetSalesOrderByActivity**: Retrieves sales orders associated with an activity
  - Query
  - Validator
  - Handler
- **GetSalesOrderByOrderDateRange**: Retrieves sales orders within a date range
  - Query
  - Validator
  - Handler

### Mappings
- **SalesOrderMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Implementation Details

### Soft Delete Pattern
All operations follow the standardized soft delete pattern:
- Entities have an `Active` boolean property (default = true)
- Delete operations set `Active = 0` rather than removing records
- All queries filter by `Active = 1` to exclude soft-deleted records

### Validation
Comprehensive validation rules are implemented for all DTOs, commands, and queries to ensure data integrity:
- Required fields validation
- String length validation
- Numeric range validation
- Date validation
- Relationship validation

### Error Handling
Proper exception handling with detailed logging is implemented in all handlers:
- ArgumentNullException for null requests
- ArgumentException for invalid parameters
- InvalidOperationException for business rule violations
- Structured logging with context information

### Documentation
Complete XML documentation is provided for all classes and methods, including:
- Class summaries
- Method descriptions
- Parameter documentation
- Return value documentation
- Exception documentation

## Usage Examples

### Creating a Sales Order
```csharp
var command = new CreateSalesOrderCommand
{
    Number = "SO-2025-001",
    SalesOrderStatusId = Guid.Parse("..."),
    ShipMethodId = Guid.Parse("..."),
    BillToAddressId = Guid.Parse("..."),
    ShipToAddressId = Guid.Parse("..."),
    TaxCodeId = Guid.Parse("..."),
    OrderDate = DateTime.UtcNow,
    SubTotal = 1000.00m,
    TaxAmount = 100.00m,
    DueAmount = 1100.00m,
    CreatedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving Sales Orders by Company
```csharp
var query = new GetSalesOrderByCompanyQuery
{
    CompanyId = companyId
};

var salesOrders = await _mediator.Send(query);
```

### Updating a Sales Order
```csharp
var command = new UpdateSalesOrderCommand
{
    Id = salesOrderId,
    Number = "SO-2025-001-REV",
    SalesOrderStatusId = newStatusId,
    // ... other properties
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Sales Order
```csharp
var command = new DeleteSalesOrderCommand
{
    Id = salesOrderId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Dependencies
- **MediatR**: For implementing the CQRS pattern
- **FluentValidation**: For implementing validation rules
- **AutoMapper**: For object-to-object mapping
- **Serilog**: For structured logging
- **Dapper**: For data access (used in repositories)

## Notes
- All queries and commands follow consistent patterns for maintainability
- The feature adheres to SOLID principles and Clean Architecture
- Soft delete is implemented using the `Active` property as per system standards
- The implementation follows the Onion Architecture pattern

# SalesOrder Feature

## Overview
The SalesOrder feature provides a comprehensive implementation for managing sales orders within the VibeCRM system. It allows for creating, retrieving, updating, and soft-deleting sales order records that represent confirmed customer purchases.

## Domain Model
The SalesOrder entity is a core business entity that represents a confirmed customer order in the CRM system. Each SalesOrder has the following properties:

- **SalesOrderId**: Unique identifier (UUID)
- **Number**: Unique sales order number (e.g., SO-2025-001)
- **QuoteId**: Optional reference to the associated quote
- **CompanyId**: Reference to the associated company
- **ContactPersonId**: Reference to the primary contact person
- **SalesOrderStatusId**: Reference to the sales order status
- **ShipMethodId**: Reference to the shipping method
- **BillToAddressId**: Reference to the billing address
- **ShipToAddressId**: Reference to the shipping address
- **TaxCodeId**: Reference to the tax code
- **OrderDate**: Date the order was placed
- **ShipDate**: Date the order was shipped (optional)
- **ExpectedDeliveryDate**: Expected delivery date (optional)
- **SubTotal**: Subtotal amount before tax
- **DiscountAmount**: Discount amount applied
- **TaxAmount**: Tax amount applied
- **ShippingAmount**: Shipping charges
- **DueAmount**: Total amount due
- **Notes**: Additional notes or comments
- **ActivityId**: Optional reference to the associated activity
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Company**: Navigation property to the associated Company
- **ContactPerson**: Navigation property to the associated Person
- **SalesOrderStatus**: Navigation property to the associated SalesOrderStatus
- **ShipMethod**: Navigation property to the associated ShipMethod
- **BillToAddress**: Navigation property to the billing Address
- **ShipToAddress**: Navigation property to the shipping Address
- **TaxCode**: Navigation property to the associated TaxCode
- **Quote**: Navigation property to the associated Quote (if applicable)
- **Activity**: Navigation property to the associated Activity (if applicable)
- **SalesOrderLineItems**: Collection of associated SalesOrderLineItem entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **SalesOrderDto**: Base DTO with core properties
- **SalesOrderDetailsDto**: Extended DTO with audit fields and related data
- **SalesOrderListDto**: Optimized DTO for list views

### Commands
- **CreateSalesOrder**: Creates a new sales order
- **UpdateSalesOrder**: Updates an existing sales order
- **DeleteSalesOrder**: Soft-deletes a sales order by setting Active = false
- **CreateSalesOrderFromQuote**: Creates a new sales order based on an existing quote
- **UpdateSalesOrderStatus**: Updates the status of a sales order
- **ShipSalesOrder**: Records shipping information for a sales order
- **CancelSalesOrder**: Cancels a sales order

### Queries
- **GetAllSalesOrders**: Retrieves all active sales orders
- **GetSalesOrderById**: Retrieves a specific sales order by its ID
- **GetSalesOrderByNumber**: Retrieves a sales order by its number
- **GetSalesOrdersByCompany**: Retrieves sales orders associated with a specific company
- **GetSalesOrdersByContactPerson**: Retrieves sales orders associated with a specific contact person
- **GetSalesOrdersByQuote**: Retrieves sales orders associated with a specific quote
- **GetSalesOrdersByActivity**: Retrieves sales orders associated with a specific activity
- **GetSalesOrdersByStatus**: Retrieves sales orders with a specific status
- **GetSalesOrdersByOrderDateRange**: Retrieves sales orders within a specific order date range
- **GetSalesOrdersByShipDateRange**: Retrieves sales orders within a specific ship date range
- **GetSalesOrdersReadyToShip**: Retrieves sales orders that are ready to be shipped

### Validators
- **SalesOrderDtoValidator**: Validates the base DTO
- **SalesOrderDetailsDtoValidator**: Validates the detailed DTO
- **SalesOrderListDtoValidator**: Validates the list DTO
- **CreateSalesOrderCommandValidator**: Validates the create command
- **UpdateSalesOrderCommandValidator**: Validates the update command
- **DeleteSalesOrderCommandValidator**: Validates the delete command
- **CreateSalesOrderFromQuoteCommandValidator**: Validates the create from quote command
- **UpdateSalesOrderStatusCommandValidator**: Validates the update status command
- **ShipSalesOrderCommandValidator**: Validates the ship command
- **CancelSalesOrderCommandValidator**: Validates the cancel command
- **GetSalesOrderByIdQueryValidator**: Validates the ID query
- **GetSalesOrderByNumberQueryValidator**: Validates the number query
- **GetSalesOrdersByCompanyQueryValidator**: Validates the company query
- **GetSalesOrdersByContactPersonQueryValidator**: Validates the contact person query
- **GetSalesOrdersByQuoteQueryValidator**: Validates the quote query
- **GetSalesOrdersByActivityQueryValidator**: Validates the activity query
- **GetSalesOrdersByStatusQueryValidator**: Validates the status query
- **GetSalesOrdersByOrderDateRangeQueryValidator**: Validates the order date range query
- **GetSalesOrdersByShipDateRangeQueryValidator**: Validates the ship date range query

### Mappings
- **SalesOrderMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new SalesOrder
```csharp
var command = new CreateSalesOrderCommand
{
    Number = "SO-2025-001",
    CompanyId = companyId,
    ContactPersonId = contactPersonId,
    SalesOrderStatusId = statusId,
    ShipMethodId = shipMethodId,
    BillToAddressId = billToAddressId,
    ShipToAddressId = shipToAddressId,
    TaxCodeId = taxCodeId,
    OrderDate = DateTime.Now,
    ExpectedDeliveryDate = DateTime.Now.AddDays(7),
    SubTotal = 1000.00m,
    DiscountAmount = 100.00m,
    TaxAmount = 90.00m,
    ShippingAmount = 25.00m,
    DueAmount = 1015.00m,
    Notes = "Customer requested expedited shipping",
    ActivityId = activityId,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Creating a SalesOrder from a Quote
```csharp
var command = new CreateSalesOrderFromQuoteCommand
{
    QuoteId = quoteId,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var salesOrderId = await _mediator.Send(command);
```

### Retrieving all SalesOrders
```csharp
var query = new GetAllSalesOrdersQuery();
var salesOrders = await _mediator.Send(query);
```

### Retrieving a SalesOrder by ID
```csharp
var query = new GetSalesOrderByIdQuery { Id = salesOrderId };
var salesOrder = await _mediator.Send(query);
```

### Retrieving SalesOrders by Company
```csharp
var query = new GetSalesOrdersByCompanyQuery { CompanyId = companyId };
var salesOrders = await _mediator.Send(query);
```

### Updating a SalesOrder's Status
```csharp
var command = new UpdateSalesOrderStatusCommand
{
    Id = salesOrderId,
    SalesOrderStatusId = newStatusId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Shipping a SalesOrder
```csharp
var command = new ShipSalesOrderCommand
{
    Id = salesOrderId,
    ShipDate = DateTime.Now,
    TrackingNumber = "1Z999AA10123456784",
    ShippingNotes = "Shipped via UPS Ground",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a SalesOrder
```csharp
var command = new UpdateSalesOrderCommand
{
    Id = salesOrderId,
    ShipMethodId = newShipMethodId,
    ExpectedDeliveryDate = DateTime.Now.AddDays(5),
    Notes = "Customer changed shipping method to express",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a SalesOrder
```csharp
var command = new DeleteSalesOrderCommand
{
    Id = salesOrderId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The SalesOrder feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Number is required, limited to 50 characters, and must be unique
- CompanyId must reference a valid company
- ContactPersonId must reference a valid person
- SalesOrderStatusId must reference a valid sales order status
- ShipMethodId must reference a valid ship method
- BillToAddressId must reference a valid address
- ShipToAddressId must reference a valid address
- TaxCodeId must reference a valid tax code
- OrderDate is required and cannot be in the future
- ExpectedDeliveryDate must be after OrderDate if provided
- ShipDate must be after OrderDate if provided
- SubTotal, DiscountAmount, TaxAmount, ShippingAmount, and DueAmount must be non-negative
- DueAmount must equal SubTotal - DiscountAmount + TaxAmount + ShippingAmount
- Notes are optional but limited to 500 characters
- QuoteId must reference a valid quote if provided
- ActivityId must reference a valid activity if provided
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Sales Order Lifecycle
- Sales orders progress through various statuses: Draft, Confirmed, In Process, Shipped, Delivered, Canceled
- Status transitions are controlled by specific commands with appropriate validation
- When a sales order is shipped, shipping information is recorded
- Canceled sales orders remain in the system but are marked with a canceled status

### Quote to Sales Order Conversion
- The system supports creating a sales order directly from a quote
- When converting, line items, pricing, and customer information are copied from the quote
- The quote and sales order remain linked for reporting and tracking purposes

# VibeCRM Entity Navigation Properties Update Plan

## Overview

This document outlines the plan to update business entities in the VibeCRM system to ensure they have appropriate navigation properties. The updates will follow Clean Architecture and SOLID principles while maintaining the existing Onion Architecture pattern.

## Current State Analysis

After reviewing the business entities in the VibeCRM.Domain.Entities.BusinessEntities namespace, several entities have been identified with missing or incomplete navigation properties. These gaps can impact the system's ability to efficiently navigate between related entities and may lead to suboptimal query performance.

## Update Requirements

### 1. Entity Updates

#### SalesOrder Entity
- **Missing Navigation Properties**:
  - SalesOrderStatus
  - ShipMethod
  - BillToAddress
  - ShipToAddress
  - TaxCode
  - SalesOrderLineItems (collection)

#### Quote Entity
- **Missing Navigation Properties**:
  - QuoteLineItems (collection)
  - QuoteStatus (if applicable)
  - Related SalesOrders (inverse navigation from SalesOrder.Quote)

#### Product Entity - 
- **Implemented Navigation Properties**:
  - ProductType 
  - QuoteLineItems (collection) 
  - SalesOrderLineItems (collection) 
  - ProductGroups (collection) 

#### Address Entity - 
- **Implemented Navigation Properties**:
  - State 

#### Person Entity
- **Missing Navigation Properties**:
  - Any foreign key relationships not currently navigable

#### Service Entity
- Needs full review to ensure all navigation properties are present

#### Workflow Entity
- Needs full review to ensure all navigation properties are present

#### Invoice Entity
- Needs full review to ensure all navigation properties are present

#### InvoiceLineItem Entity
- Needs full review to ensure all navigation properties are present

### 2. Repository Updates

For each entity with updated navigation properties, the corresponding repositories will need to be updated:

1. **Domain/Core Repository Interfaces**:
   - Update method signatures if needed
   - Ensure consistency with entity changes

2. **Application Repository Interfaces**:
   - Update query methods to include new navigation properties
   - Ensure DTOs reflect the updated entity structure

3. **CQRS Repository Interfaces**:
   - Update command repositories if entity structure changes affect write operations
   - Update query repositories to include new navigation properties in query results

4. **Repository Implementations**:
   - Update SQL queries to include joins for new navigation properties
   - Ensure proper mapping of navigation properties in query results

### 3. CQRS Feature Updates

For each affected entity, the following CQRS components will need to be updated:

1. **Commands**:
   - Update command handlers to handle new navigation properties
   - Update validation rules if they depend on navigation properties

2. **Queries**:
   - Update query handlers to include new navigation properties in results
   - Update projections to map new navigation properties to DTOs

3. **DTOs**:
   - Update DTOs to include properties that represent the new navigation properties
   - Ensure DTO mapping profiles are updated

4. **Validators**:
   - Update validators to include validation rules for new navigation properties

## Implementation Plan

### Phase 1: Entity Updates

1. **Update Entity Classes**:
   - Add missing navigation properties to each entity
   - Ensure proper XML documentation for all new properties
   - Initialize collection properties in constructors

2. **Update Entity Framework Configurations**:
   - Update entity configurations to include the new navigation properties
   - Define relationship configurations (one-to-many, many-to-many)

### Phase 2: Repository Updates

1. **Update Repository Interfaces**:
   - Add or modify methods to support new navigation properties
   - Ensure consistency across all repository layers

2. **Update Repository Implementations**:
   - Modify SQL queries to include new navigation properties
   - Update mapping logic to handle new navigation properties
   
### Phase 3: CQRS Feature Updates

1. **Update DTOs and Mapping Profiles**:
   - Add properties to DTOs that correspond to new navigation properties
   - Update AutoMapper profiles to map new navigation properties

2. **Update Command Handlers**:
   - Modify command handlers to handle new navigation properties
   - Update validation logic if necessary

3. **Update Query Handlers**:
   - Modify query handlers to include new navigation properties in results
   - Update projections to include new navigation properties

## Detailed Entity Updates

### SalesOrder Entity

```csharp
// Add the following navigation properties
public SalesOrderStatus? SalesOrderStatus { get; set; }
public ShipMethod? ShipMethod { get; set; }
public TaxCode? TaxCode { get; set; }
public Address? BillToAddress { get; set; }
public Address? ShipToAddress { get; set; }
public Quote? Quote { get; set; }
public ICollection<SalesOrderLineItem> LineItems { get; set; } = new HashSet<SalesOrderLineItem>();
public ICollection<Company_SalesOrder> Companies { get; set; } = new HashSet<Company_SalesOrder>();
public ICollection<SalesOrder_Activity> Activities { get; set; } = new HashSet<SalesOrder_Activity>();
```

### Quote Entity

```csharp
// Add the following navigation properties
public QuoteStatus? QuoteStatus { get; set; }
public ICollection<QuoteLineItem> LineItems { get; set; } = new HashSet<QuoteLineItem>();
public ICollection<SalesOrder> SalesOrders { get; set; } = new HashSet<SalesOrder>();
public ICollection<Company_Quote> Companies { get; set; } = new HashSet<Company_Quote>();
public ICollection<Quote_Activity> Activities { get; set; } = new HashSet<Quote_Activity>();
```

### Product Entity - 

```csharp
// Added the following navigation properties
public ProductType? ProductType { get; set; }
public ICollection<QuoteLineItem> QuoteLineItems { get; set; } = new HashSet<QuoteLineItem>();
public ICollection<SalesOrderLineItem> SalesOrderLineItems { get; set; } = new HashSet<SalesOrderLineItem>();
public ICollection<ProductGroup> ProductGroups { get; set; } = new HashSet<ProductGroup>();
```

### Address Entity - 

```csharp
// Added the following navigation property
public State? State { get; set; }
```

## Repository Update Examples

### SalesOrderRepository

```csharp
// Update methods to load navigation properties
public async Task LoadSalesOrderStatusAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
public async Task LoadShipMethodAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
public async Task LoadTaxCodeAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
public async Task LoadAddressesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
public async Task LoadLineItemsAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
public async Task LoadCompaniesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
public async Task LoadActivitiesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
public async Task LoadQuoteAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
public async Task<SalesOrder> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
```

### ProductRepository - 

```csharp
// Implemented the following methods to load related entities
public async Task LoadProductTypeAsync(Product product, CancellationToken cancellationToken = default);
public async Task LoadQuoteLineItemsAsync(Product product, CancellationToken cancellationToken = default);
public async Task LoadSalesOrderLineItemsAsync(Product product, CancellationToken cancellationToken = default);
public async Task LoadProductGroupsAsync(Product product, CancellationToken cancellationToken = default);
public async Task<Product?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
```

### AddressRepository - 

```csharp
// Implemented the following methods to load related entities
public async Task LoadStateAsync(Address address, CancellationToken cancellationToken = default);
public async Task LoadAddressTypeAsync(Address address, CancellationToken cancellationToken = default);
public async Task<Address?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
public async Task<IEnumerable<Address>> GetByStateIdAsync(Guid stateId, CancellationToken cancellationToken = default);
```

### QuoteRepository - 

```csharp
// Implemented the following methods to load related entities
public async Task LoadQuoteStatusAsync(Quote quote, CancellationToken cancellationToken = default);
public async Task LoadLineItemsAsync(Quote quote, CancellationToken cancellationToken = default);
public async Task LoadSalesOrdersAsync(Quote quote, CancellationToken cancellationToken = default);
public async Task LoadCompaniesAsync(Quote quote, CancellationToken cancellationToken = default);
public async Task LoadActivitiesAsync(Quote quote, CancellationToken cancellationToken = default);
public async Task<Quote?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
public async Task<IEnumerable<Quote>> GetByQuoteStatusIdAsync(Guid quoteStatusId, CancellationToken cancellationToken = default);
```

## CQRS Feature Update Examples

### Product Feature - 

1. **DTOs**:
   - Created `ProductDto`, `ProductDetailsDto`, and `ProductListDto` with properties for navigation properties 
   - Added mapping profiles in `ProductMappingProfile` 

2. **Commands**:
   - Implemented `CreateProductCommand`, `UpdateProductCommand`, and `DeleteProductCommand` 
   - Added validation rules in corresponding validators 

3. **Queries**:
   - Implemented `GetProductByIdQuery`, `GetAllProductsQuery`, `GetProductsByProductTypeQuery`, and `GetProductsByProductGroupQuery` 
   - Added handlers to load and include related entities in query results 

## Implementation Progress

### Completed Entities
-  Product Entity (including repository and CQRS features)
-  Address Entity (including repository updates)
-  QuoteLineItem Entity (updated to include Product navigation property)
-  Quote Entity (including repository updates for loading related entities)
-  SalesOrder Entity (including repository updates for loading related entities)

### In Progress
- Person Entity

### Pending
- Service Entity
- Workflow Entity
- Invoice Entity
- InvoiceLineItem Entity

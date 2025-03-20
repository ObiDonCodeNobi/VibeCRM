# PaymentMethod Feature

## Overview
The PaymentMethod feature provides functionality for managing payment method entities in the VibeCRM system. Payment methods represent the various ways customers can make payments (e.g., "Credit Card", "Cash", "Check", etc.) and are used in financial transactions throughout the system.

## Architecture
This feature follows the Clean Architecture and CQRS (Command Query Responsibility Segregation) patterns using MediatR. It is organized into the following components:

### Domain Layer
- `PaymentMethod` entity in the Domain layer defines the core business entity.

### Application Layer
- **DTOs (Data Transfer Objects)**:
  - `PaymentMethodDto`: Basic DTO for transferring payment method data.
  - `PaymentMethodDetailsDto`: Detailed DTO with additional information about the payment method.
  - `PaymentMethodListDto`: DTO optimized for list views of payment methods.

- **Commands**:
  - `CreatePaymentMethodCommand`: Command for creating a new payment method.
  - `UpdatePaymentMethodCommand`: Command for updating an existing payment method.
  - `DeletePaymentMethodCommand`: Command for soft deleting a payment method.

- **Command Handlers**:
  - `CreatePaymentMethodCommandHandler`: Handles the creation of payment methods.
  - `UpdatePaymentMethodCommandHandler`: Handles the updating of payment methods.
  - `DeletePaymentMethodCommandHandler`: Handles the soft deletion of payment methods.

- **Queries**:
  - `GetPaymentMethodByIdQuery`: Query for retrieving a payment method by its ID.
  - `GetPaymentMethodByNameQuery`: Query for retrieving a payment method by its name.
  - `GetAllPaymentMethodsQuery`: Query for retrieving all payment methods.
  - `GetPaymentMethodByOrdinalPositionQuery`: Query for retrieving payment methods ordered by their ordinal position.
  - `GetDefaultPaymentMethodQuery`: Query for retrieving the default payment method.

- **Query Handlers**:
  - Corresponding handlers for each query that interact with the repository to fetch data.

- **Validators**:
  - Validators for all DTOs, commands, and queries using FluentValidation.

- **Mappings**:
  - `PaymentMethodMappingProfile`: AutoMapper profile for mapping between PaymentMethod entities and DTOs.

### Infrastructure Layer
- `IPaymentMethodRepository`: Interface defining the repository contract.
- `PaymentMethodRepository`: Implementation of the repository interface using Dapper ORM.

## Key Features
1. **CRUD Operations**: Create, Read, Update, and Delete (soft delete) operations for payment method entities.
2. **Ordinal Position**: Payment methods can be ordered using an ordinal position property for display purposes.
3. **Default Method**: Support for retrieving the default payment method.
4. **Soft Delete**: Implements the soft delete pattern using the `Active` property.

## Usage Examples

### Creating a Payment Method
```csharp
var command = new CreatePaymentMethodCommand
{
    Name = "Credit Card",
    Description = "Payment via credit card (Visa, MasterCard, etc.)",
    OrdinalPosition = 1,
    CreatedBy = "admin"
};

var result = await _mediator.Send(command);
```

### Updating a Payment Method
```csharp
var command = new UpdatePaymentMethodCommand
{
    Id = paymentMethodId,
    Name = "Credit Card",
    Description = "Payment via credit card (Visa, MasterCard, American Express)",
    OrdinalPosition = 1,
    ModifiedBy = "admin"
};

var result = await _mediator.Send(command);
```

### Retrieving All Payment Methods
```csharp
var query = new GetAllPaymentMethodsQuery
{
    IncludeInactive = false
};

var paymentMethods = await _mediator.Send(query);
```

### Retrieving Payment Methods by Ordinal Position
```csharp
var query = new GetPaymentMethodByOrdinalPositionQuery();

var orderedPaymentMethods = await _mediator.Send(query);
```

## Dependencies
- **MediatR**: For implementing the CQRS pattern.
- **AutoMapper**: For mapping between entities and DTOs.
- **FluentValidation**: For validating commands, queries, and DTOs.
- **Dapper**: ORM for data access.
- **Microsoft.Extensions.Logging**: For logging within handlers and repositories.

## Best Practices
1. All handlers include comprehensive error handling and logging.
2. All public methods and classes have XML documentation.
3. Validation is implemented for all commands, queries, and DTOs.
4. The feature follows the soft delete pattern using the `Active` property.
5. All repository methods include a `CancellationToken` parameter for cancellation support.
6. SOLID principles are followed throughout the implementation.
7. Clean Architecture principles are maintained with clear separation of concerns.

# AccountType Feature

## Overview
The AccountType feature provides functionality for managing account types in the VibeCRM system. Account types are used to categorize accounts for organization and reporting purposes.

## Domain Model
The AccountType entity is a reference entity that represents a type category for accounts. Each AccountType has the following properties:

- **AccountTypeId**: Unique identifier (UUID)
- **Type**: Name of the account type (e.g., "Customer", "Prospect", "Partner")
- **Description**: Detailed description of what the account type means
- **OrdinalPosition**: Numeric value for ordering account types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Accounts**: Collection of associated Account entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **AccountTypeDto**: Base DTO with core properties
- **AccountTypeDetailsDto**: Extended DTO with audit fields and account count
- **AccountTypeListDto**: Optimized DTO for list views

### Commands
- **CreateAccountType**: Creates a new account type
- **UpdateAccountType**: Updates an existing account type
- **DeleteAccountType**: Soft-deletes an account type by setting Active = false

### Queries
- **GetAllAccountTypes**: Retrieves all active account types
- **GetAccountTypeById**: Retrieves a specific account type by its ID
- **GetAccountTypeByType**: Retrieves account types by their type name
- **GetAccountTypeByOrdinalPosition**: Retrieves account types ordered by their ordinal position
- **GetDefaultAccountType**: Retrieves the default account type (lowest ordinal position)

### Validators
- **AccountTypeDtoValidator**: Validates the base DTO
- **AccountTypeDetailsDtoValidator**: Validates the detailed DTO
- **AccountTypeListDtoValidator**: Validates the list DTO
- **CreateAccountTypeCommandValidator**: Validates the create command
- **UpdateAccountTypeCommandValidator**: Validates the update command
- **DeleteAccountTypeCommandValidator**: Validates the delete command
- **GetAccountTypeByIdQueryValidator**: Validates the ID query
- **GetAccountTypeByTypeQueryValidator**: Validates the type name query
- **GetAccountTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query

### Mappings
- **AccountTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new AccountType
```csharp
var command = new CreateAccountTypeCommand
{
    Type = "Customer",
    Description = "Organizations that purchase products or services",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all AccountTypes
```csharp
var query = new GetAllAccountTypesQuery();
var accountTypes = await _mediator.Send(query);
```

### Retrieving an AccountType by ID
```csharp
var query = new GetAccountTypeByIdQuery { Id = accountTypeId };
var accountType = await _mediator.Send(query);
```

### Retrieving AccountTypes by type name
```csharp
var query = new GetAccountTypeByTypeQuery { Type = "Customer" };
var accountType = await _mediator.Send(query);
```

### Retrieving the default AccountType
```csharp
var query = new GetDefaultAccountTypeQuery();
var defaultAccountType = await _mediator.Send(query);
```

### Updating an AccountType
```csharp
var command = new UpdateAccountTypeCommand
{
    Id = accountTypeId,
    Type = "Strategic Customer",
    Description = "High-value customers with strategic importance",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an AccountType
```csharp
var command = new DeleteAccountTypeCommand
{
    Id = accountTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The AccountType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all account types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Account types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Account Associations
Each AccountType can be associated with multiple Account entities. The feature includes functionality to retrieve the count of accounts using each type.

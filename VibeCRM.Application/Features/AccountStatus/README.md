# AccountStatus Feature

## Overview
The AccountStatus feature manages the status types for company accounts within the VibeCRM system. It provides a way to categorize companies based on their current status (e.g., "Active", "Prospect", "Inactive", etc.) and includes functionality for creating, retrieving, updating, and soft-deleting account status records.

## Domain Model
The AccountStatus entity is a TypeStatus entity that represents a status category for company accounts. Each AccountStatus has the following properties:

- **AccountStatusId**: Unique identifier (UUID)
- **Status**: Name of the status (e.g., "Active", "Prospect")
- **Description**: Detailed description of what the status means
- **OrdinalPosition**: Numeric value for ordering statuses in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Companies**: Collection of associated Company entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **AccountStatusDto**: Base DTO with core properties
- **AccountStatusDetailsDto**: Extended DTO with audit fields and company count
- **AccountStatusListDto**: Optimized DTO for list views

### Commands
- **CreateAccountStatus**: Creates a new account status
- **UpdateAccountStatus**: Updates an existing account status
- **DeleteAccountStatus**: Soft-deletes an account status by setting Active = false

### Queries
- **GetAllAccountStatuses**: Retrieves all active account statuses
- **GetAccountStatusById**: Retrieves a specific account status by its ID
- **GetAccountStatusByStatus**: Retrieves a specific account status by its status name
- **GetAccountStatusByOrdinalPosition**: Retrieves a specific account status by its ordinal position

### Validators
- **AccountStatusDtoValidator**: Validates the base DTO
- **AccountStatusDetailsDtoValidator**: Validates the detailed DTO
- **AccountStatusListDtoValidator**: Validates the list DTO
- **GetAccountStatusByIdQueryValidator**: Validates the ID query
- **GetAccountStatusByStatusQueryValidator**: Validates the status name query
- **GetAccountStatusByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllAccountStatusesQueryValidator**: Validates the "get all" query

### Mappings
- **AccountStatusMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new AccountStatus
```csharp
var command = new CreateAccountStatusCommand
{
    Status = "Prospect",
    Description = "A potential customer who has not yet committed to our services",
    OrdinalPosition = 2,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all AccountStatuses
```csharp
var query = new GetAllAccountStatusesQuery();
var accountStatuses = await _mediator.Send(query);
```

### Updating an AccountStatus
```csharp
var command = new UpdateAccountStatusCommand
{
    Id = accountStatusId,
    Status = "Hot Prospect",
    Description = "A potential customer who has shown significant interest",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an AccountStatus
```csharp
var command = new DeleteAccountStatusCommand
{
    Id = accountStatusId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The AccountStatus feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Status name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Account statuses are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Company Associations
Each AccountStatus can be associated with multiple Company entities. The feature includes functionality to retrieve the count of companies using each status.

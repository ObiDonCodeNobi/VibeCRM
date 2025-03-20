# AccountType Feature

This feature implements the functionality for managing account types in the VibeCRM system. Account types are used to categorize accounts for organization and reporting purposes.

## Structure

The feature follows the Clean Architecture principles and CQRS pattern with the following components:

### Commands
- **CreateAccountType**: Creates a new account type
- **UpdateAccountType**: Updates an existing account type
- **DeleteAccountType**: Soft deletes an account type (sets Active = false)

### Queries
- **GetAllAccountTypes**: Retrieves all active account types
- **GetAccountTypeById**: Retrieves a specific account type by its ID
- **GetAccountTypeByType**: Retrieves account types by their type name
- **GetAccountTypeByOrdinalPosition**: Retrieves account types ordered by their ordinal position

### DTOs
- **AccountTypeDto**: Basic DTO for account type data
- **AccountTypeListDto**: Extended DTO for account type data in list views, includes company count

### Validators
- Validators for all commands and DTOs to ensure data integrity

### Mappings
- AutoMapper profile for mapping between entities and DTOs

## Implementation Details

- Follows the soft delete pattern using the `Active` property
- Uses Dapper ORM for data access
- Implements comprehensive logging with Serilog
- Includes full XML documentation for all classes and methods

## Usage

The feature is accessed through MediatR handlers that process commands and queries. The handlers interact with the repository layer, which uses Dapper to communicate with the database.

## Dependencies

- MediatR for CQRS implementation
- AutoMapper for object mapping
- FluentValidation for validation
- Serilog for logging
- Dapper for data access

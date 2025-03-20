# EmailAddressType Feature

## Overview
The EmailAddressType feature provides functionality for managing email address types in the VibeCRM system. Email address types are used to categorize email addresses (e.g., "Personal", "Work") for organization and communication preferences.

## Components

### Domain Entity
- `EmailAddressType`: Represents an email address type in the system.

### DTOs
- `EmailAddressTypeDto`: Basic DTO for email address type information.
- `EmailAddressTypeDetailsDto`: Detailed DTO including audit information and email address count.
- `EmailAddressTypeListDto`: DTO for list views including email address count.

### Commands
- `CreateEmailAddressType`: Creates a new email address type.
- `UpdateEmailAddressType`: Updates an existing email address type.
- `DeleteEmailAddressType`: Soft deletes an email address type by setting Active = false.

### Queries
- `GetAllEmailAddressTypes`: Retrieves all active email address types.
- `GetEmailAddressTypeById`: Retrieves a specific email address type by its ID.
- `GetEmailAddressTypeByType`: Retrieves email address types by their type name.
- `GetEmailAddressTypesByOrdinalPosition`: Retrieves email address types ordered by their ordinal position.
- `GetDefaultEmailAddressType`: Retrieves the default email address type (lowest ordinal position).

### Validators
- Command validators: Ensure command data is valid before processing.
- Query validators: Validate query parameters.
- DTO validators: Validate DTO properties.

### Mapping Profiles
- `EmailAddressTypeMappingProfile`: Configures AutoMapper mappings between entities and DTOs.

## Repository
The `IEmailAddressTypeRepository` interface and its implementation provide data access methods:
- Standard CRUD operations (AddAsync, UpdateAsync, GetByIdAsync, GetAllAsync, DeleteAsync)
- Specialized methods:
  - GetByOrdinalPositionAsync: Get email address types ordered by ordinal position
  - GetByTypeAsync: Get email address types by type name
  - GetDefaultAsync: Get the default email address type

## Implementation Notes
- Soft delete is implemented using the Active property (true = active, false = deleted)
- All repository methods include a CancellationToken parameter
- DeleteAsync expects a Guid ID parameter, not an entity object
- All queries filter by Active = 1 to exclude soft-deleted records

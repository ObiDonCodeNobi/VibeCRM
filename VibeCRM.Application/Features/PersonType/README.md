# PersonType Feature

## Overview
The PersonType feature provides functionality for managing person types in the VibeCRM system. Person types categorize people in the system (e.g., Customer, Vendor, Employee) and help organize and filter contacts.

## Components

### DTOs
- **PersonTypeDto**: Basic DTO for person type information
- **PersonTypeDetailsDto**: Detailed DTO including audit information and people count
- **PersonTypeListDto**: DTO optimized for list views and dropdowns

### Commands
- **CreatePersonType**: Creates a new person type
  - CreatePersonTypeCommand
  - CreatePersonTypeCommandHandler
  - CreatePersonTypeCommandValidator
- **UpdatePersonType**: Updates an existing person type
  - UpdatePersonTypeCommand
  - UpdatePersonTypeCommandHandler
  - UpdatePersonTypeCommandValidator
- **DeletePersonType**: Soft-deletes a person type
  - DeletePersonTypeCommand
  - DeletePersonTypeCommandHandler
  - DeletePersonTypeCommandValidator

### Queries
- **GetAllPersonTypes**: Retrieves all active person types
  - GetAllPersonTypesQuery
  - GetAllPersonTypesQueryHandler
  - GetAllPersonTypesQueryValidator
- **GetPersonTypeById**: Retrieves a specific person type by ID
  - GetPersonTypeByIdQuery
  - GetPersonTypeByIdQueryHandler
  - GetPersonTypeByIdQueryValidator
- **GetPersonTypeByOrdinalPosition**: Retrieves person types ordered by ordinal position
  - GetPersonTypeByOrdinalPositionQuery
  - GetPersonTypeByOrdinalPositionQueryHandler
  - GetPersonTypeByOrdinalPositionQueryValidator

### Validators
- **DTO Validators**:
  - PersonTypeDtoValidator
  - PersonTypeDetailsDtoValidator
  - PersonTypeListDtoValidator

### Mappings
- **PersonTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a Person Type
```csharp
var command = new CreatePersonTypeCommand
{
    Type = "Customer",
    Description = "External clients who purchase products or services",
    OrdinalPosition = 1,
    CreatedBy = userId,
    ModifiedBy = userId
};

var personTypeId = await _mediator.Send(command);
```

### Retrieving Person Types
```csharp
// Get all person types
var query = new GetAllPersonTypesQuery();
var personTypes = await _mediator.Send(query);

// Get person type by ID
var detailsQuery = new GetPersonTypeByIdQuery { Id = personTypeId };
var personTypeDetails = await _mediator.Send(detailsQuery);

// Get person types ordered by ordinal position
var orderedQuery = new GetPersonTypeByOrdinalPositionQuery();
var orderedPersonTypes = await _mediator.Send(orderedQuery);
```

### Updating a Person Type
```csharp
var updateCommand = new UpdatePersonTypeCommand
{
    Id = personTypeId,
    Type = "Premium Customer",
    Description = "High-value clients with premium service level",
    OrdinalPosition = 1,
    ModifiedBy = userId
};

var success = await _mediator.Send(updateCommand);
```

### Deleting a Person Type
```csharp
var deleteCommand = new DeletePersonTypeCommand
{
    Id = personTypeId,
    ModifiedBy = userId
};

var success = await _mediator.Send(deleteCommand);
```

## Design Decisions
- Follows Clean Architecture and CQRS pattern
- Uses soft delete functionality via the Active property
- Implements ordinal position for custom sorting
- Maintains audit trail with CreatedBy, CreatedDate, ModifiedBy, and ModifiedDate properties
- Each command and query has its own validator for proper validation
- DTOs have dedicated validators to ensure data integrity

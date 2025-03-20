# PhoneType Feature

## Overview
The PhoneType feature provides functionality for managing phone type entities in the VibeCRM application. Phone types categorize phone numbers (e.g., "Home", "Work", "Mobile", "Fax") and are used throughout the system to classify phone contact information.

## Architecture
This feature follows Clean Architecture and CQRS principles:

- **Commands**: Handle create, update, and delete operations
- **Queries**: Handle data retrieval operations
- **DTOs**: Define data transfer objects for various use cases
- **Validators**: Ensure data integrity through validation rules
- **Mappings**: Configure AutoMapper profiles for entity-DTO conversions

## Directory Structure
```
PhoneType/
├── Commands/
│   ├── CreatePhoneType/
│   │   ├── CreatePhoneTypeCommand.cs
│   │   ├── CreatePhoneTypeCommandHandler.cs
│   │   └── CreatePhoneTypeCommandValidator.cs
│   ├── UpdatePhoneType/
│   │   ├── UpdatePhoneTypeCommand.cs
│   │   ├── UpdatePhoneTypeCommandHandler.cs
│   │   └── UpdatePhoneTypeCommandValidator.cs
│   └── DeletePhoneType/
│       ├── DeletePhoneTypeCommand.cs
│       ├── DeletePhoneTypeCommandHandler.cs
│       └── DeletePhoneTypeCommandValidator.cs
├── DTOs/
│   ├── PhoneTypeDto.cs
│   ├── PhoneTypeListDto.cs
│   └── PhoneTypeDetailsDto.cs
├── Mappings/
│   └── PhoneTypeMappingProfile.cs
├── Queries/
│   ├── GetAllPhoneTypes/
│   │   ├── GetAllPhoneTypesQuery.cs
│   │   ├── GetAllPhoneTypesQueryHandler.cs
│   │   └── GetAllPhoneTypesQueryValidator.cs
│   ├── GetPhoneTypeById/
│   │   ├── GetPhoneTypeByIdQuery.cs
│   │   ├── GetPhoneTypeByIdQueryHandler.cs
│   │   └── GetPhoneTypeByIdQueryValidator.cs
│   ├── GetPhoneTypeByOrdinalPosition/
│   │   ├── GetPhoneTypeByOrdinalPositionQuery.cs
│   │   ├── GetPhoneTypeByOrdinalPositionQueryHandler.cs
│   │   └── GetPhoneTypeByOrdinalPositionQueryValidator.cs
│   ├── GetPhoneTypeByType/
│   │   ├── GetPhoneTypeByTypeQuery.cs
│   │   ├── GetPhoneTypeByTypeQueryHandler.cs
│   │   └── GetPhoneTypeByTypeQueryValidator.cs
│   └── GetDefaultPhoneType/
│       ├── GetDefaultPhoneTypeQuery.cs
│       ├── GetDefaultPhoneTypeQueryHandler.cs
│       └── GetDefaultPhoneTypeQueryValidator.cs
└── Validators/
    ├── PhoneTypeDtoValidator.cs
    ├── PhoneTypeListDtoValidator.cs
    └── PhoneTypeDetailsDtoValidator.cs
```

## Key Components

### DTOs
- **PhoneTypeDto**: Basic phone type information (Id, Type, Description, OrdinalPosition)
- **PhoneTypeListDto**: List view with phone count information
- **PhoneTypeDetailsDto**: Detailed view with audit information

### Commands
- **CreatePhoneType**: Creates a new phone type
- **UpdatePhoneType**: Updates an existing phone type
- **DeletePhoneType**: Soft-deletes a phone type (sets Active = false)

### Queries
- **GetAllPhoneTypes**: Retrieves all phone types
- **GetPhoneTypeById**: Retrieves a specific phone type by ID
- **GetPhoneTypeByOrdinalPosition**: Retrieves a phone type by its ordinal position
- **GetPhoneTypeByType**: Retrieves a phone type by its type name
- **GetDefaultPhoneType**: Retrieves the default phone type (lowest ordinal position)

## Usage Examples

### Creating a Phone Type
```csharp
var command = new CreatePhoneTypeCommand
{
    Type = "Mobile",
    Description = "Mobile phone number",
    OrdinalPosition = 1
};

var result = await _mediator.Send(command);
```

### Updating a Phone Type
```csharp
var command = new UpdatePhoneTypeCommand
{
    Id = phoneTypeId,
    Type = "Work",
    Description = "Work phone number",
    OrdinalPosition = 2
};

var result = await _mediator.Send(command);
```

### Deleting a Phone Type
```csharp
var command = new DeletePhoneTypeCommand
{
    Id = phoneTypeId
};

var result = await _mediator.Send(command);
```

### Retrieving Phone Types
```csharp
// Get all phone types
var allPhoneTypes = await _mediator.Send(new GetAllPhoneTypesQuery());

// Get phone type by ID
var phoneType = await _mediator.Send(new GetPhoneTypeByIdQuery { Id = phoneTypeId });

// Get default phone type
var defaultPhoneType = await _mediator.Send(new GetDefaultPhoneTypeQuery());
```

## Validation
All commands and queries include validation to ensure data integrity:
- Type name is required and must be unique
- Description is required
- Ordinal position must be a non-negative number
- IDs must reference existing entities

## Notes
- Phone types use soft delete functionality (Active flag)
- Default phone type is determined by the lowest ordinal position
- Phone types with existing references cannot be deleted

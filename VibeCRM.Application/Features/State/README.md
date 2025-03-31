# State Feature

## Overview
The State feature provides functionality for managing states or provinces in the VibeCRM system. States are used for address management and geographical organization of customers, accounts, and other entities.

## Domain Model
The State entity is a reference entity that represents a state or province. Each State has the following properties:

- **StateId**: Unique identifier (UUID)
- **Name**: Full name of the state (e.g., "California", "Texas", "Ontario")
- **Abbreviation**: Standard abbreviation for the state (e.g., "CA", "TX", "ON")
- **CountryId**: Reference to the associated Country entity
- **OrdinalPosition**: Numeric value for ordering states in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Addresses**: Collection of associated Address entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **StateDto**: Base DTO with core properties
- **StateDetailsDto**: Extended DTO with audit fields and address count
- **StateListDto**: Optimized DTO for list views

### Commands
- **CreateState**: Creates a new state
- **UpdateState**: Updates an existing state
- **DeleteState**: Soft-deletes a state by setting Active = false

### Queries
- **GetAllStates**: Retrieves all active states
- **GetStateById**: Retrieves a specific state by its ID
- **GetStateByName**: Retrieves states by their name
- **GetStateByAbbreviation**: Retrieves a state by its abbreviation
- **GetStatesByCountry**: Retrieves all states for a specific country

### Validators
- **StateDtoValidator**: Validates the base DTO
- **StateDetailsDtoValidator**: Validates the detailed DTO
- **StateListDtoValidator**: Validates the list DTO
- **CreateStateCommandValidator**: Validates the create command
- **UpdateStateCommandValidator**: Validates the update command
- **DeleteStateCommandValidator**: Validates the delete command
- **GetStateByIdQueryValidator**: Validates the ID query
- **GetStateByNameQueryValidator**: Validates the name query
- **GetStateByAbbreviationQueryValidator**: Validates the abbreviation query
- **GetStatesByCountryQueryValidator**: Validates the country query

### Mappings
- **StateMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new State
```csharp
var command = new CreateStateCommand
{
    Name = "California",
    Abbreviation = "CA",
    CountryId = countryId,
    OrdinalPosition = 5,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all States
```csharp
var query = new GetAllStatesQuery();
var states = await _mediator.Send(query);
```

### Retrieving a State by ID
```csharp
var query = new GetStateByIdQuery { Id = stateId };
var state = await _mediator.Send(query);
```

### Retrieving States by name
```csharp
var query = new GetStateByNameQuery { Name = "California" };
var state = await _mediator.Send(query);
```

### Retrieving a State by abbreviation
```csharp
var query = new GetStateByAbbreviationQuery { Abbreviation = "CA" };
var state = await _mediator.Send(query);
```

### Retrieving States by country
```csharp
var query = new GetStatesByCountryQuery { CountryId = countryId };
var states = await _mediator.Send(query);
```

### Updating a State
```csharp
var command = new UpdateStateCommand
{
    Id = stateId,
    Name = "California",
    Abbreviation = "CA",
    CountryId = countryId,
    OrdinalPosition = 5,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a State
```csharp
var command = new DeleteStateCommand
{
    Id = stateId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The State feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- State name is required and limited to 100 characters
- Abbreviation is required and limited to 10 characters
- Country ID is required and must reference a valid country
- Ordinal position must be a non-negative number
- State name and abbreviation must be unique within the same country
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
States are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Address Associations
Each State can be associated with multiple Address entities. The feature includes functionality to retrieve the count of addresses in each state.

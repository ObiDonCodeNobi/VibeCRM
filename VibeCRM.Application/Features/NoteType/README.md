# NoteType Feature

## Overview
The NoteType feature implements the functionality for categorizing notes in the VibeCRM system, allowing for organization and reporting of different types of notes.

## Domain Model
The NoteType entity is a reference entity that represents a category for notes. Each NoteType has the following properties:

- **NoteTypeId**: Unique identifier (UUID)
- **Type**: Name of the note type (e.g., "Meeting Notes", "Call Summary", "Follow-up")
- **Description**: Detailed description of what the note type means
- **OrdinalPosition**: Numeric value for ordering note types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Notes**: Collection of associated Note entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **NoteTypeDto**: Base DTO with core properties
- **NoteTypeDetailsDto**: Extended DTO with audit fields and note count
- **NoteTypeListDto**: Optimized DTO for list views

### Commands
- **CreateNoteType**: Creates a new note type
- **UpdateNoteType**: Updates an existing note type
- **DeleteNoteType**: Soft-deletes a note type by setting Active = false

### Queries
- **GetAllNoteTypes**: Retrieves all active note types
- **GetNoteTypeById**: Retrieves a specific note type by its ID
- **GetNoteTypeByType**: Retrieves note types by their type name
- **GetNoteTypeByOrdinalPosition**: Retrieves note types by their ordinal position
- **GetDefaultNoteType**: Retrieves the default note type (lowest ordinal position)

### Validators
- **NoteTypeDtoValidator**: Validates the base DTO
- **NoteTypeDetailsDtoValidator**: Validates the detailed DTO
- **NoteTypeListDtoValidator**: Validates the list DTO
- **CreateNoteTypeCommandValidator**: Validates the create command
- **UpdateNoteTypeCommandValidator**: Validates the update command
- **DeleteNoteTypeCommandValidator**: Validates the delete command
- **GetNoteTypeByIdQueryValidator**: Validates the ID query
- **GetNoteTypeByTypeQueryValidator**: Validates the type name query
- **GetNoteTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetAllNoteTypesQueryValidator**: Validates the "get all" query

### Mappings
- **NoteTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new NoteType
```csharp
var command = new CreateNoteTypeCommand
{
    Type = "Meeting Notes",
    Description = "Notes taken during meetings with clients or team members",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all NoteTypes
```csharp
var query = new GetAllNoteTypesQuery();
var noteTypes = await _mediator.Send(query);
```

### Retrieving a NoteType by ID
```csharp
var query = new GetNoteTypeByIdQuery { Id = noteTypeId };
var noteType = await _mediator.Send(query);
```

### Retrieving NoteTypes by type name
```csharp
var query = new GetNoteTypeByTypeQuery { Type = "Meeting Notes" };
var noteType = await _mediator.Send(query);
```

### Retrieving the default NoteType
```csharp
var query = new GetDefaultNoteTypeQuery();
var defaultNoteType = await _mediator.Send(query);
```

### Updating a NoteType
```csharp
var command = new UpdateNoteTypeCommand
{
    Id = noteTypeId,
    Type = "Meeting Minutes",
    Description = "Detailed minutes from meetings with clients or team members",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a NoteType
```csharp
var command = new DeleteNoteTypeCommand
{
    Id = noteTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The NoteType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 50 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all note types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Note types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Note Associations
Each NoteType can be associated with multiple Note entities. The feature includes functionality to retrieve the count of notes using each type.

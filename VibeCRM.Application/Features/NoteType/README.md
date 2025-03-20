# NoteType Feature

This feature implements the NoteType functionality in the VibeCRM system, allowing for the categorization of notes for organization and reporting.

## Overview

The NoteType feature provides a complete implementation for managing note types in the system, including:

- Creating new note types
- Updating existing note types
- Soft deleting note types (setting Active = false)
- Retrieving note types by various criteria
- Validating note type data

## Components

### DTOs

- **NoteTypeDto**: Basic note type information
- **NoteTypeDetailsDto**: Detailed note type information including audit fields
- **NoteTypeListDto**: Note type information for list views, including note count

### Commands

- **CreateNoteTypeCommand**: Creates a new note type
- **UpdateNoteTypeCommand**: Updates an existing note type
- **DeleteNoteTypeCommand**: Soft deletes a note type (sets Active = false)

### Queries

- **GetAllNoteTypesQuery**: Retrieves all active note types
- **GetNoteTypeByIdQuery**: Retrieves a specific note type by ID
- **GetNoteTypeByTypeQuery**: Retrieves note types by type name
- **GetNoteTypeByOrdinalPositionQuery**: Retrieves note types ordered by ordinal position

### Validators

- **NoteTypeDtoValidator**: Validates note type data

### Mapping

- **NoteTypeMappingProfile**: Configures AutoMapper mappings between entities and DTOs

## Usage Examples

### Creating a Note Type

```csharp
var command = new CreateNoteTypeCommand
{
    Type = "Meeting Notes",
    Description = "Notes taken during meetings with clients or team members",
    OrdinalPosition = 1,
    CreatedBy = "user@example.com"
};

var result = await mediator.Send(command);
```

### Updating a Note Type

```csharp
var command = new UpdateNoteTypeCommand
{
    Id = noteTypeId,
    Type = "Meeting Minutes",
    Description = "Detailed minutes from meetings with clients or team members",
    OrdinalPosition = 1,
    ModifiedBy = "user@example.com"
};

var result = await mediator.Send(command);
```

### Deleting a Note Type

```csharp
var command = new DeleteNoteTypeCommand
{
    Id = noteTypeId,
    ModifiedBy = "user@example.com"
};

var result = await mediator.Send(command);
```

### Retrieving Note Types

```csharp
// Get all note types
var allNoteTypes = await mediator.Send(new GetAllNoteTypesQuery());

// Get note type by ID
var noteType = await mediator.Send(new GetNoteTypeByIdQuery { Id = noteTypeId });

// Get note types by type name
var noteTypes = await mediator.Send(new GetNoteTypeByTypeQuery { Type = "Meeting Notes" });

// Get note types ordered by ordinal position
var orderedNoteTypes = await mediator.Send(new GetNoteTypeByOrdinalPositionQuery());
```

## Implementation Details

- The feature follows the CQRS pattern using MediatR
- Soft delete is implemented using the Active property (not IsDeleted)
- All repository methods include CancellationToken parameters
- Comprehensive logging is implemented throughout
- All commands and queries include proper validation
- Exception handling is implemented in all handlers

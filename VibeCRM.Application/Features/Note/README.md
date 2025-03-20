# Note Feature

## Overview
The Note feature provides functionality for managing notes in the VibeCRM system. Notes can be associated with companies and persons, allowing users to record important information about their business relationships.

## Components

### DTOs
- **NoteDto**: Base DTO containing the core properties of a note.
- **NoteDetailsDto**: Extended DTO with additional details for comprehensive views.
- **NoteListDto**: Simplified DTO optimized for list displays.

### Commands
- **CreateNote**: Creates a new note in the system.
- **UpdateNote**: Updates an existing note.
- **DeleteNote**: Soft-deletes a note by setting its Active property to false.

### Queries
- **GetNoteById**: Retrieves a specific note by its ID.
- **GetNotesByCompany**: Retrieves all notes associated with a specific company.
- **GetNotesByPerson**: Retrieves all notes associated with a specific person.
- **GetNotesByNoteType**: Retrieves all notes of a specific note type.

### Validators
Each command has a corresponding validator that enforces business rules:
- **CreateNoteCommandValidator**: Validates note creation requests.
- **UpdateNoteCommandValidator**: Validates note update requests.
- **DeleteNoteCommandValidator**: Validates note deletion requests.

### Mappings
- **NoteMappingProfile**: Configures AutoMapper mappings between Note entities and DTOs.

## Usage Examples

### Creating a Note
```csharp
var createCommand = new CreateNoteCommand
{
    NoteTypeId = Guid.Parse("..."),
    Subject = "Meeting Summary",
    NoteText = "Discussed new project requirements...",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await mediator.Send(createCommand);
```

### Updating a Note
```csharp
var updateCommand = new UpdateNoteCommand
{
    NoteId = existingNoteId,
    NoteTypeId = Guid.Parse("..."),
    Subject = "Updated Meeting Summary",
    NoteText = "Revised project requirements...",
    ModifiedBy = currentUserId
};

var result = await mediator.Send(updateCommand);
```

### Retrieving Notes for a Company
```csharp
var query = new GetNotesByCompanyQuery(companyId);
var notes = await mediator.Send(query);
```

## Soft Delete Implementation
Notes use the standard soft delete pattern implemented throughout VibeCRM:
- Notes have an `Active` boolean property (default = true)
- When a note is "deleted", the `Active` property is set to false
- All queries filter by `Active = 1` to only show active notes
- The `DeleteAsync` method performs a soft delete by setting `Active = 0`

This approach allows for data recovery and maintains referential integrity while hiding "deleted" records from normal queries.

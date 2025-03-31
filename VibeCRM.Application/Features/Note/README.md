# Note Feature

## Overview
The Note feature provides functionality for managing notes in the VibeCRM system. Notes can be associated with companies and persons, allowing users to record important information about their business relationships.

## Domain Model
The Note entity is a core business entity that represents a textual note in the CRM system. Each Note has the following properties:

- **NoteId**: Unique identifier (UUID)
- **NoteTypeId**: Reference to the note type (e.g., Meeting, Call, General)
- **Subject**: Brief subject line for the note
- **NoteText**: The main content of the note
- **CompanyId**: Optional reference to the associated company
- **PersonId**: Optional reference to the associated person
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **NoteType**: Navigation property to the associated NoteType
- **Company**: Navigation property to the associated Company (if applicable)
- **Person**: Navigation property to the associated Person (if applicable)

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **NoteDto**: Base DTO with core properties
- **NoteDetailsDto**: Extended DTO with audit fields and related data
- **NoteListDto**: Optimized DTO for list views

### Commands
- **CreateNote**: Creates a new note
- **UpdateNote**: Updates an existing note
- **DeleteNote**: Soft-deletes a note by setting Active = false
- **PinNote**: Marks a note as pinned for quick access
- **AssignNoteToCompany**: Associates a note with a company
- **AssignNoteToPerson**: Associates a note with a person

### Queries
- **GetAllNotes**: Retrieves all active notes
- **GetNoteById**: Retrieves a specific note by its ID
- **GetNotesByCompany**: Retrieves notes associated with a specific company
- **GetNotesByPerson**: Retrieves notes associated with a specific person
- **GetNotesByNoteType**: Retrieves notes filtered by note type
- **GetPinnedNotes**: Retrieves all pinned notes
- **SearchNotes**: Searches notes by subject or content

### Validators
- **NoteDtoValidator**: Validates the base DTO
- **NoteDetailsDtoValidator**: Validates the detailed DTO
- **NoteListDtoValidator**: Validates the list DTO
- **CreateNoteCommandValidator**: Validates the create command
- **UpdateNoteCommandValidator**: Validates the update command
- **DeleteNoteCommandValidator**: Validates the delete command
- **PinNoteCommandValidator**: Validates the pin command
- **AssignNoteToCompanyCommandValidator**: Validates the company assignment command
- **AssignNoteToPersonCommandValidator**: Validates the person assignment command
- **GetNoteByIdQueryValidator**: Validates the ID query
- **GetNotesByCompanyQueryValidator**: Validates the company query
- **GetNotesByPersonQueryValidator**: Validates the person query
- **GetNotesByNoteTypeQueryValidator**: Validates the note type query
- **SearchNotesQueryValidator**: Validates the search query

### Mappings
- **NoteMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Note
```csharp
var command = new CreateNoteCommand
{
    NoteTypeId = noteTypeId,
    Subject = "Meeting Summary",
    NoteText = "Discussed new project requirements and timeline. Client agreed to proceed with the proposal.",
    CompanyId = companyId, // Optional
    PersonId = personId, // Optional
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Notes
```csharp
var query = new GetAllNotesQuery();
var notes = await _mediator.Send(query);
```

### Retrieving a Note by ID
```csharp
var query = new GetNoteByIdQuery { Id = noteId };
var note = await _mediator.Send(query);
```

### Searching Notes
```csharp
var query = new SearchNotesQuery { SearchTerm = "project requirements" };
var notes = await _mediator.Send(query);
```

### Retrieving Notes for a Company
```csharp
var query = new GetNotesByCompanyQuery { CompanyId = companyId };
var notes = await _mediator.Send(query);
```

### Updating a Note
```csharp
var command = new UpdateNoteCommand
{
    Id = noteId,
    NoteTypeId = noteTypeId,
    Subject = "Updated Meeting Summary",
    NoteText = "Revised project requirements and timeline. Client requested additional features.",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Note
```csharp
var command = new DeleteNoteCommand
{
    Id = noteId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

### Pinning a Note
```csharp
var command = new PinNoteCommand
{
    Id = noteId,
    IsPinned = true,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Note feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Subject is required and limited to 200 characters
- NoteText is required and limited to 4000 characters
- NoteTypeId must reference a valid note type
- Either CompanyId or PersonId should be provided (but both can be null for system-wide notes)
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Related Entities
- Notes can be associated with either a Company, a Person, both, or neither
- Notes are categorized by NoteType for organization and filtering
- Pinned notes are given priority in the UI for quick access

### Rich Text Support
- The NoteText field supports rich text formatting using HTML
- The system sanitizes HTML input to prevent XSS attacks
- Formatting is preserved when displaying notes in the UI

# Attachment Feature

## Overview
The Attachment feature provides functionality for managing file attachments within the VibeCRM system. Attachments can be associated with companies, persons, and potentially other entities in the system. This feature follows the CQRS pattern with MediatR for command and query separation.

## Key Components

### DTOs
- `AttachmentDto`: Base DTO containing common attachment properties
- `AttachmentDetailsDto`: Detailed DTO with additional information for single attachment views
- `AttachmentListDto`: Simplified DTO for displaying attachments in lists

### Commands
- `CreateAttachment`: Creates a new attachment in the system
- `UpdateAttachment`: Updates an existing attachment's properties
- `DeleteAttachment`: Soft-deletes an attachment by setting its Active property to false

### Queries
- `GetAttachmentById`: Retrieves a specific attachment by its unique identifier
- `GetAllAttachments`: Retrieves all active attachments in the system

### Validators
- Ensures all attachment data meets the required validation rules using FluentValidation

## Implementation Details

### Soft Delete Pattern
- All delete operations are implemented as soft deletes by setting the `Active` property to `false`
- All queries automatically filter by `Active = 1` to exclude soft-deleted records

### Mapping
- AutoMapper profiles are used to map between entities and DTOs
- The mapping configuration handles the relationship between the entity's `Id` and `AttachmentId` properties

## Usage Examples

### Creating an Attachment
```csharp
var command = new CreateAttachmentCommand
{
    AttachmentTypeId = Guid.Parse("attachment-type-id"),
    Subject = "Contract Document",
    Path = "path/to/file.pdf",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await mediator.Send(command);
```

### Retrieving an Attachment
```csharp
var query = new GetAttachmentByIdQuery(attachmentId);
var attachment = await mediator.Send(query);
```

### Updating an Attachment
```csharp
var command = new UpdateAttachmentCommand
{
    Id = attachmentId,
    AttachmentTypeId = Guid.Parse("new-attachment-type-id"),
    Subject = "Updated Contract Document",
    Path = "path/to/updated-file.pdf",
    ModifiedBy = currentUserId
};

var result = await mediator.Send(command);
```

### Deleting an Attachment
```csharp
var command = new DeleteAttachmentCommand(attachmentId, currentUserId);
var success = await mediator.Send(command);
```

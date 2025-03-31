# Attachment Feature

## Overview
The Attachment feature provides functionality for managing file attachments within the VibeCRM system. Attachments can be associated with companies, persons, and potentially other entities in the system. This feature follows the CQRS pattern with MediatR for command and query separation.

## Domain Model
The Attachment entity is a core business entity that represents a file attached to another entity in the system. Each Attachment has the following properties:

- **AttachmentId**: Unique identifier (UUID)
- **AttachmentTypeId**: Reference to the type of attachment (e.g., Document, Image, Contract)
- **Subject**: Brief title describing the attachment
- **Description**: Optional detailed description of the attachment
- **Path**: File path or storage location of the attachment
- **FileSize**: Size of the file in bytes
- **ContentType**: MIME type of the file
- **FileName**: Original name of the uploaded file
- **RelatedToEntityId**: Optional reference to the entity this attachment is related to
- **RelatedToEntityType**: Optional type of entity this attachment is related to
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **AttachmentType**: Navigation property to the associated AttachmentType

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **AttachmentDto**: Base DTO with core properties
- **AttachmentDetailsDto**: Extended DTO with audit fields and related data
- **AttachmentListDto**: Optimized DTO for list views

### Commands
- **CreateAttachment**: Creates a new attachment
- **UpdateAttachment**: Updates an existing attachment
- **DeleteAttachment**: Soft-deletes an attachment by setting Active = false
- **UploadAttachment**: Handles file upload and creates a new attachment
- **DownloadAttachment**: Retrieves the file content for download

### Queries
- **GetAllAttachments**: Retrieves all active attachments
- **GetAttachmentById**: Retrieves a specific attachment by its ID
- **GetAttachmentsByType**: Retrieves attachments filtered by attachment type
- **GetAttachmentsByEntity**: Retrieves attachments related to a specific entity
- **GetAttachmentsByContentType**: Retrieves attachments by MIME type

### Validators
- **AttachmentDtoValidator**: Validates the base DTO
- **AttachmentDetailsDtoValidator**: Validates the detailed DTO
- **AttachmentListDtoValidator**: Validates the list DTO
- **CreateAttachmentCommandValidator**: Validates the create command
- **UpdateAttachmentCommandValidator**: Validates the update command
- **DeleteAttachmentCommandValidator**: Validates the delete command
- **UploadAttachmentCommandValidator**: Validates the upload command
- **DownloadAttachmentCommandValidator**: Validates the download command
- **GetAttachmentByIdQueryValidator**: Validates the ID query
- **GetAttachmentsByTypeQueryValidator**: Validates the type query
- **GetAttachmentsByEntityQueryValidator**: Validates the entity query
- **GetAttachmentsByContentTypeQueryValidator**: Validates the content type query

### Mappings
- **AttachmentMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Attachment
```csharp
var command = new CreateAttachmentCommand
{
    AttachmentTypeId = attachmentTypeId,
    Subject = "Contract Document",
    Description = "Signed contract for new services",
    Path = "path/to/file.pdf",
    FileSize = 1024000,
    ContentType = "application/pdf",
    FileName = "contract.pdf",
    RelatedToEntityId = companyId,
    RelatedToEntityType = "Company",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Uploading an Attachment
```csharp
var command = new UploadAttachmentCommand
{
    AttachmentTypeId = attachmentTypeId,
    Subject = "Product Image",
    File = fileStream,
    FileName = "product.jpg",
    RelatedToEntityId = productId,
    RelatedToEntityType = "Product",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Attachments
```csharp
var query = new GetAllAttachmentsQuery();
var attachments = await _mediator.Send(query);
```

### Retrieving an Attachment by ID
```csharp
var query = new GetAttachmentByIdQuery { Id = attachmentId };
var attachment = await _mediator.Send(query);
```

### Retrieving Attachments by entity
```csharp
var query = new GetAttachmentsByEntityQuery 
{ 
    EntityId = companyId,
    EntityType = "Company"
};
var attachments = await _mediator.Send(query);
```

### Downloading an Attachment
```csharp
var command = new DownloadAttachmentCommand { Id = attachmentId };
var fileResult = await _mediator.Send(command);
```

### Updating an Attachment
```csharp
var command = new UpdateAttachmentCommand
{
    Id = attachmentId,
    AttachmentTypeId = attachmentTypeId,
    Subject = "Updated Contract Document",
    Description = "Revised contract with new terms",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an Attachment
```csharp
var command = new DeleteAttachmentCommand
{
    Id = attachmentId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Attachment feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Subject is required and limited to 200 characters
- Description is optional and limited to 1000 characters
- Path is required and must be a valid file path
- FileSize must be a positive number
- ContentType is required and must be a valid MIME type
- FileName is required and must be a valid file name
- AttachmentTypeId must reference a valid attachment type
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### File Storage
- Files can be stored in the file system or in cloud storage
- The Path property stores the relative path or URI to access the file
- The system supports configurable storage providers
- File access is controlled through the DownloadAttachment command

### Security Considerations
- File uploads are validated for size and content type
- Antivirus scanning is performed on uploaded files
- Access to attachments is controlled based on the related entity's permissions
- File paths are sanitized to prevent path traversal attacks

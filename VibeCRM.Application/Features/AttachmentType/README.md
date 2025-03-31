# AttachmentType Feature

## Overview
The AttachmentType feature provides functionality for managing attachment types in the VibeCRM system. Attachment types categorize attachments (such as documents, images, contracts, etc.) for organization and reporting purposes.

## Domain Model
The AttachmentType entity is a reference entity that represents a type category for attachments. Each AttachmentType has the following properties:

- **AttachmentTypeId**: Unique identifier (UUID)
- **Type**: Name of the attachment type (e.g., "Document", "Image", "Contract")
- **Description**: Detailed description of what the attachment type means
- **OrdinalPosition**: Numeric value for ordering attachment types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Attachments**: Collection of associated Attachment entities
- **SupportedFileExtensions**: Collection of file extensions supported by this attachment type

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **AttachmentTypeDto**: Base DTO with core properties
- **AttachmentTypeDetailsDto**: Extended DTO with audit fields and attachment count
- **AttachmentTypeListDto**: Optimized DTO for list views

### Commands
- **CreateAttachmentType**: Creates a new attachment type
- **UpdateAttachmentType**: Updates an existing attachment type
- **DeleteAttachmentType**: Soft-deletes an attachment type by setting Active = false

### Queries
- **GetAllAttachmentTypes**: Retrieves all active attachment types
- **GetAttachmentTypeById**: Retrieves a specific attachment type by its ID
- **GetAttachmentTypeByType**: Retrieves a specific attachment type by its type name
- **GetAttachmentTypeByOrdinalPosition**: Retrieves attachment types ordered by position
- **GetDefaultAttachmentType**: Retrieves the default attachment type
- **GetAttachmentTypeByFileExtension**: Retrieves attachment types that support a specific file extension

### Validators
- **AttachmentTypeDtoValidator**: Validates the base DTO
- **AttachmentTypeDetailsDtoValidator**: Validates the detailed DTO
- **AttachmentTypeListDtoValidator**: Validates the list DTO
- **CreateAttachmentTypeCommandValidator**: Validates the create command
- **UpdateAttachmentTypeCommandValidator**: Validates the update command
- **DeleteAttachmentTypeCommandValidator**: Validates the delete command
- **GetAttachmentTypeByIdQueryValidator**: Validates the ID query
- **GetAttachmentTypeByTypeQueryValidator**: Validates the type name query
- **GetAttachmentTypeByFileExtensionQueryValidator**: Validates the file extension query
- **GetAllAttachmentTypesQueryValidator**: Validates the "get all" query
- **GetAttachmentTypeByOrdinalPositionQueryValidator**: Validates the ordinal position query
- **GetDefaultAttachmentTypeQueryValidator**: Validates the default query

### Mappings
- **AttachmentTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new AttachmentType
```csharp
var command = new CreateAttachmentTypeCommand
{
    Type = "Document",
    Description = "General document attachments",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all AttachmentTypes
```csharp
var query = new GetAllAttachmentTypesQuery();
var attachmentTypes = await _mediator.Send(query);
```

### Retrieving an AttachmentType by ID
```csharp
var query = new GetAttachmentTypeByIdQuery { Id = attachmentTypeId };
var attachmentType = await _mediator.Send(query);
```

### Retrieving AttachmentTypes by file extension
```csharp
var query = new GetAttachmentTypeByFileExtensionQuery { FileExtension = ".pdf" };
var attachmentTypes = await _mediator.Send(query);
```

### Retrieving the default AttachmentType
```csharp
var query = new GetDefaultAttachmentTypeQuery();
var defaultAttachmentType = await _mediator.Send(query);
```

### Updating an AttachmentType
```csharp
var command = new UpdateAttachmentTypeCommand
{
    Id = attachmentTypeId,
    Type = "Updated Document",
    Description = "Updated description for document attachments",
    OrdinalPosition = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting an AttachmentType
```csharp
var command = new DeleteAttachmentTypeCommand
{
    Id = attachmentTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The AttachmentType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Type name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Type name must be unique across all attachment types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Ordering
Attachment types are ordered by their OrdinalPosition property in list views to ensure consistent display order.

### Attachment Associations
Each AttachmentType can be associated with multiple Attachment entities. The feature includes functionality to retrieve the count of attachments using each type.

### File Extension Support
The feature supports filtering attachment types by file extension, allowing the system to determine which attachment types are valid for specific file types.

# AttachmentType Feature

## Overview
The AttachmentType feature provides functionality for managing attachment types in the VibeCRM system. Attachment types categorize attachments (such as documents, images, contracts, etc.) for organization and reporting purposes.

## Architecture
This feature follows Clean Architecture and CQRS principles:
- **Domain Layer**: Contains the AttachmentType entity definition
- **Application Layer**: Implements CQRS with MediatR, handling commands and queries
- **Infrastructure Layer**: Manages data access using the AttachmentTypeRepository

## Components

### DTOs
- **AttachmentTypeDto**: Basic properties for attachment types
- **AttachmentTypeListDto**: List view including an AttachmentCount property
- **AttachmentTypeDetailsDto**: Detailed view with audit fields

### Commands and Handlers
- **CreateAttachmentType**: Command and handler for creating new attachment types
- **UpdateAttachmentType**: Command and handler for updating existing attachment types
- **DeleteAttachmentType**: Command and handler for performing a soft delete using the Active property

### Queries and Handlers
- **GetAllAttachmentTypes**: Query and handler to retrieve all attachment types
- **GetAttachmentTypeById**: Query and handler to retrieve an attachment type by its ID
- **GetAttachmentTypeByType**: Query and handler to retrieve an attachment type by its type name
- **GetAttachmentTypeByOrdinalPosition**: Query and handler to retrieve attachment types ordered by their ordinal position
- **GetDefaultAttachmentType**: Query and handler to retrieve the default attachment type
- **GetAttachmentTypeByFileExtension**: Query and handler to retrieve attachment types that support a specific file extension

### Validators
- **AttachmentTypeDtoValidator**: Validates the basic AttachmentTypeDto
- **AttachmentTypeListDtoValidator**: Validates the AttachmentTypeListDto
- **AttachmentTypeDetailsDtoValidator**: Validates the AttachmentTypeDetailsDto
- **CreateAttachmentTypeCommandValidator**: Validates the CreateAttachmentTypeCommand
- **UpdateAttachmentTypeCommandValidator**: Validates the UpdateAttachmentTypeCommand
- **DeleteAttachmentTypeCommandValidator**: Validates the DeleteAttachmentTypeCommand
- **GetAttachmentTypeByIdQueryValidator**: Validates the GetAttachmentTypeByIdQuery
- **GetAttachmentTypeByTypeQueryValidator**: Validates the GetAttachmentTypeByTypeQuery
- **GetAttachmentTypeByFileExtensionQueryValidator**: Validates the GetAttachmentTypeByFileExtensionQuery
- **GetAllAttachmentTypesQueryValidator**: Validates the GetAllAttachmentTypesQuery (parameter-less)
- **GetAttachmentTypeByOrdinalPositionQueryValidator**: Validates the GetAttachmentTypeByOrdinalPositionQuery (parameter-less)
- **GetDefaultAttachmentTypeQueryValidator**: Validates the GetDefaultAttachmentTypeQuery (parameter-less)

### Mapping Profile
- **AttachmentTypeMappingProfile**: Maps between entities and DTOs/commands, ensuring the use of fully qualified entity names to avoid namespace conflicts

## Implementation Details

### Soft Delete
The feature implements soft delete functionality using the Active property instead of removing records from the database. When an entity is "deleted", the Active property is set to false, and all queries filter by Active = 1 to exclude soft-deleted records.

### Audit Fields
All entities include audit fields (CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) which are properly set during create and update operations.

### Ordinal Position
The GetByOrdinalPositionAsync method returns attachment types ordered by their ordinal position, allowing for customized sorting in the UI.

### File Extension Support
The GetByFileExtensionAsync method returns attachment types that support a specific file extension, allowing for filtering attachment types based on file types.

## Usage Examples

### Creating a New Attachment Type
```csharp
var command = new CreateAttachmentTypeCommand
{
    Type = "Document",
    Description = "General document attachments",
    OrdinalPosition = 1,
    CreatedBy = "system"
};

var attachmentTypeId = await _mediator.Send(command);
```

### Updating an Attachment Type
```csharp
var command = new UpdateAttachmentTypeCommand
{
    Id = attachmentTypeId,
    Type = "Updated Document",
    Description = "Updated description for document attachments",
    OrdinalPosition = 2,
    ModifiedBy = "system"
};

var result = await _mediator.Send(command);
```

### Retrieving All Attachment Types
```csharp
var query = new GetAllAttachmentTypesQuery();
var attachmentTypes = await _mediator.Send(query);
```

### Retrieving an Attachment Type by ID
```csharp
var query = new GetAttachmentTypeByIdQuery { Id = attachmentTypeId };
var attachmentType = await _mediator.Send(query);
```

### Retrieving Attachment Types by File Extension
```csharp
var query = new GetAttachmentTypeByFileExtensionQuery { FileExtension = ".pdf" };
var attachmentTypes = await _mediator.Send(query);
```

### Soft Deleting an Attachment Type
```csharp
var command = new DeleteAttachmentTypeCommand
{
    Id = attachmentTypeId,
    ModifiedBy = "system"
};

var result = await _mediator.Send(command);
```

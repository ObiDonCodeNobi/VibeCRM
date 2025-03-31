# ProductGroup Feature

## Overview
The ProductGroup feature provides a comprehensive implementation for managing product groups in the VibeCRM system. Product groups are hierarchical categories used to organize products, enabling efficient product management and navigation.

## Domain Model
The ProductGroup entity is a core business entity that represents a category for organizing products in the CRM system. Each ProductGroup has the following properties:

- **ProductGroupId**: Unique identifier (UUID)
- **Name**: The name of the product group
- **Description**: Detailed description of the product group
- **ParentId**: Optional reference to the parent product group (for hierarchical structure)
- **DisplayOrder**: Numeric value for ordering product groups in displays
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Parent**: Navigation property to the parent ProductGroup (if applicable)
- **Children**: Collection of child ProductGroup entities
- **Products**: Collection of associated Product entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ProductGroupDto**: Base DTO with core properties
- **ProductGroupDetailsDto**: Extended DTO with audit fields and related data
- **ProductGroupListDto**: Optimized DTO for list views

### Commands
- **CreateProductGroup**: Creates a new product group
- **UpdateProductGroup**: Updates an existing product group
- **DeleteProductGroup**: Soft-deletes a product group by setting Active = false
- **ReorderProductGroup**: Updates the display order of a product group
- **MoveProductGroup**: Moves a product group to a different parent

### Queries
- **GetAllProductGroups**: Retrieves all active product groups
- **GetProductGroupById**: Retrieves a specific product group by its ID
- **GetProductGroupsByParentId**: Retrieves child product groups for a specific parent
- **GetRootProductGroups**: Retrieves top-level product groups (those without a parent)
- **GetProductGroupsWithProducts**: Retrieves product groups with their associated products
- **GetProductGroupHierarchy**: Retrieves the complete product group hierarchy

### Validators
- **ProductGroupDtoValidator**: Validates the base DTO
- **ProductGroupDetailsDtoValidator**: Validates the detailed DTO
- **ProductGroupListDtoValidator**: Validates the list DTO
- **CreateProductGroupCommandValidator**: Validates the create command
- **UpdateProductGroupCommandValidator**: Validates the update command
- **DeleteProductGroupCommandValidator**: Validates the delete command
- **ReorderProductGroupCommandValidator**: Validates the reorder command
- **MoveProductGroupCommandValidator**: Validates the move command
- **GetProductGroupByIdQueryValidator**: Validates the ID query
- **GetProductGroupsByParentIdQueryValidator**: Validates the parent ID query

### Mappings
- **ProductGroupMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new ProductGroup
```csharp
var command = new CreateProductGroupCommand
{
    Name = "Electronics",
    Description = "Electronic products and accessories",
    ParentId = null, // Top-level group
    DisplayOrder = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Creating a child ProductGroup
```csharp
var command = new CreateProductGroupCommand
{
    Name = "Smartphones",
    Description = "Mobile phones and accessories",
    ParentId = electronicsGroupId, // Child of Electronics group
    DisplayOrder = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all ProductGroups
```csharp
var query = new GetAllProductGroupsQuery();
var productGroups = await _mediator.Send(query);
```

### Retrieving a ProductGroup by ID
```csharp
var query = new GetProductGroupByIdQuery { Id = productGroupId };
var productGroup = await _mediator.Send(query);
```

### Retrieving child ProductGroups
```csharp
var query = new GetProductGroupsByParentIdQuery { ParentId = parentGroupId };
var childGroups = await _mediator.Send(query);
```

### Moving a ProductGroup
```csharp
var command = new MoveProductGroupCommand
{
    Id = productGroupId,
    NewParentId = newParentGroupId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a ProductGroup
```csharp
var command = new UpdateProductGroupCommand
{
    Id = productGroupId,
    Name = "Updated Electronics",
    Description = "Updated description for electronic products",
    DisplayOrder = 2,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a ProductGroup
```csharp
var command = new DeleteProductGroupCommand
{
    Id = productGroupId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The ProductGroup feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Name is required and limited to 100 characters
- Name must be unique within the same parent group
- Description is optional but limited to 500 characters
- DisplayOrder must be a non-negative number
- ParentId must reference a valid product group if provided
- A product group cannot be its own parent
- A product group cannot be moved to one of its descendants (to prevent circular references)
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Hierarchical Structure
- Product groups can be organized in a hierarchical tree structure
- Each product group can have one parent and multiple children
- The system supports unlimited nesting levels
- The UI displays the hierarchy using indentation or tree controls
- Deleting a parent product group requires handling its children (either delete, move, or prevent)

### Display Order
- Product groups are ordered by their DisplayOrder property within the same parent
- The system provides functionality to reorder product groups
- The UI allows drag-and-drop reordering of product groups

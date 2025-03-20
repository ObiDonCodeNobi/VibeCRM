# ProductGroup Feature

## Overview
The ProductGroup feature provides a comprehensive implementation for managing product groups in the VibeCRM system. Product groups are hierarchical categories used to organize products, enabling efficient product management and navigation.

## Architecture
This feature follows:
- **Onion Architecture** - Separation of concerns with domain at the center
- **Clean Architecture** - Independence of frameworks, testability, and UI independence
- **SOLID Principles** - Single responsibility, open-closed, Liskov substitution, interface segregation, and dependency inversion
- **CQRS Pattern** - Separation of command and query responsibilities using MediatR
- **Soft Delete Pattern** - Using the `Active` property to implement soft deletion

## Components

### DTOs
- **ProductGroupDto** - Base DTO containing essential product group properties
- **ProductGroupDetailsDto** - Extended DTO with additional details and audit information
- **ProductGroupListDto** - Specialized DTO for listing product groups in UI components

### Commands
- **CreateProductGroup** - Creates a new product group
  - `CreateProductGroupCommand` - Command definition
  - `CreateProductGroupCommandHandler` - Command handler
  - `CreateProductGroupCommandValidator` - Validation rules

- **UpdateProductGroup** - Updates an existing product group
  - `UpdateProductGroupCommand` - Command definition
  - `UpdateProductGroupCommandHandler` - Command handler
  - `UpdateProductGroupCommandValidator` - Validation rules

- **DeleteProductGroup** - Soft deletes a product group
  - `DeleteProductGroupCommand` - Command definition
  - `DeleteProductGroupCommandHandler` - Command handler
  - `DeleteProductGroupCommandValidator` - Validation rules

### Queries
- **GetAllProductGroups** - Retrieves all active product groups
  - `GetAllProductGroupsQuery` - Query definition
  - `GetAllProductGroupsQueryHandler` - Query handler
  - `GetAllProductGroupsQueryValidator` - Validation rules

- **GetProductGroupById** - Retrieves a specific product group by ID
  - `GetProductGroupByIdQuery` - Query definition
  - `GetProductGroupByIdQueryHandler` - Query handler
  - `GetProductGroupByIdQueryValidator` - Validation rules

- **GetProductGroupsByParentId** - Retrieves child product groups for a specific parent
  - `GetProductGroupsByParentIdQuery` - Query definition
  - `GetProductGroupsByParentIdQueryHandler` - Query handler
  - `GetProductGroupsByParentIdQueryValidator` - Validation rules

- **GetRootProductGroups** - Retrieves top-level product groups (those without a parent)
  - `GetRootProductGroupsQuery` - Query definition
  - `GetRootProductGroupsQueryHandler` - Query handler
  - `GetRootProductGroupsQueryValidator` - Validation rules

### Validators
- **ProductGroupDtoValidator** - Validates the base product group DTO
- **ProductGroupDetailsDtoValidator** - Validates the detailed product group DTO
- **ProductGroupListDtoValidator** - Validates the product group list DTO

### Mappings
- **ProductGroupMappingProfile** - AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a Product Group
```csharp
var command = new CreateProductGroupCommand
{
    Name = "Electronics",
    Description = "Electronic products and accessories",
    DisplayOrder = 1,
    CreatedBy = userId,
    ModifiedBy = userId
};

var result = await _mediator.Send(command);
```

### Updating a Product Group
```csharp
var command = new UpdateProductGroupCommand
{
    Id = productGroupId,
    Name = "Updated Electronics",
    Description = "Updated description for electronic products",
    DisplayOrder = 2,
    ModifiedBy = userId
};

var result = await _mediator.Send(command);
```

### Deleting a Product Group
```csharp
var command = new DeleteProductGroupCommand
{
    Id = productGroupId,
    ModifiedBy = userId
};

var result = await _mediator.Send(command);
```

### Retrieving Product Groups
```csharp
// Get all product groups
var allGroups = await _mediator.Send(new GetAllProductGroupsQuery());

// Get a specific product group
var group = await _mediator.Send(new GetProductGroupByIdQuery { Id = productGroupId });

// Get child product groups
var childGroups = await _mediator.Send(new GetProductGroupsByParentIdQuery { ParentId = parentId });

// Get root-level product groups
var rootGroups = await _mediator.Send(new GetRootProductGroupsQuery());
```

## Validation Rules
- Product group names must be unique
- Names cannot exceed 100 characters
- Descriptions cannot exceed 500 characters
- Display order must be a non-negative number
- Parent product group references must be valid
- A product group cannot be its own parent

## Soft Delete Implementation
This feature follows the VibeCRM standardized soft delete pattern:
- All entities have an `Active` boolean property (default = true)
- All queries filter with `WHERE Active = 1` to exclude soft-deleted records
- The delete operation sets `Active = 0` rather than removing records
- This allows for data recovery and maintains referential integrity

## XML Documentation
All components include comprehensive XML documentation for:
- Classes
- Methods
- Properties
- Parameters
- Return values
- Exceptions

This ensures the code is self-documenting and provides complete information through IntelliSense.

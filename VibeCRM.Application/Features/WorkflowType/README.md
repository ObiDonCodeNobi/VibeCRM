# WorkflowType Feature

## Overview
The WorkflowType feature provides functionality for managing workflow type definitions in the VibeCRM system. Workflow types define templates for business processes that can be instantiated as workflows, providing structure and consistency for common business activities.

## Domain Model
The WorkflowType entity is a reference entity that defines a template for workflows. Each WorkflowType has the following properties:

- **WorkflowTypeId**: Unique identifier (UUID)
- **Name**: Name of the workflow type (e.g., "Customer Onboarding", "Support Ticket")
- **Description**: Detailed description of the workflow type's purpose and process
- **OrdinalPosition**: Numeric value for ordering workflow types in dropdowns and lists
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **Workflows**: Collection of associated Workflow entities
- **WorkflowTypeSteps**: Collection of associated WorkflowTypeStep entities defining the template steps

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **WorkflowTypeDto**: Base DTO with core properties
- **WorkflowTypeDetailsDto**: Extended DTO with audit fields and workflow count
- **WorkflowTypeListDto**: Optimized DTO for list views
- **WorkflowTypeWithStepsDto**: Extended DTO that includes the template steps

### Commands
- **CreateWorkflowType**: Creates a new workflow type
- **UpdateWorkflowType**: Updates an existing workflow type
- **DeleteWorkflowType**: Soft-deletes a workflow type by setting Active = false
- **AddWorkflowTypeStep**: Adds a step to a workflow type template
- **UpdateWorkflowTypeStep**: Updates a step in a workflow type template
- **RemoveWorkflowTypeStep**: Removes a step from a workflow type template

### Queries
- **GetAllWorkflowTypes**: Retrieves all active workflow types
- **GetWorkflowTypeById**: Retrieves a specific workflow type by its ID
- **GetWorkflowTypeByName**: Retrieves a workflow type by its name
- **GetWorkflowTypeWithSteps**: Retrieves a workflow type with its template steps
- **GetWorkflowTypesByOrdinalPosition**: Retrieves workflow types ordered by their ordinal position

### Validators
- **WorkflowTypeDtoValidator**: Validates the base DTO
- **WorkflowTypeDetailsDtoValidator**: Validates the detailed DTO
- **WorkflowTypeListDtoValidator**: Validates the list DTO
- **WorkflowTypeWithStepsDtoValidator**: Validates the DTO with steps
- **CreateWorkflowTypeCommandValidator**: Validates the create command
- **UpdateWorkflowTypeCommandValidator**: Validates the update command
- **DeleteWorkflowTypeCommandValidator**: Validates the delete command
- **AddWorkflowTypeStepCommandValidator**: Validates the add step command
- **UpdateWorkflowTypeStepCommandValidator**: Validates the update step command
- **RemoveWorkflowTypeStepCommandValidator**: Validates the remove step command
- **GetWorkflowTypeByIdQueryValidator**: Validates the ID query
- **GetWorkflowTypeByNameQueryValidator**: Validates the name query
- **GetWorkflowTypeWithStepsQueryValidator**: Validates the steps query

### Mappings
- **WorkflowTypeMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new WorkflowType
```csharp
var command = new CreateWorkflowTypeCommand
{
    Name = "Customer Onboarding",
    Description = "Process for onboarding new customers to our services",
    OrdinalPosition = 1,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all WorkflowTypes
```csharp
var query = new GetAllWorkflowTypesQuery();
var workflowTypes = await _mediator.Send(query);
```

### Retrieving a WorkflowType with steps
```csharp
var query = new GetWorkflowTypeWithStepsQuery { Id = workflowTypeId };
var workflowTypeWithSteps = await _mediator.Send(query);
```

### Adding a step to a WorkflowType
```csharp
var command = new AddWorkflowTypeStepCommand
{
    WorkflowTypeId = workflowTypeId,
    Name = "Initial Contact",
    Description = "Make initial contact with the customer",
    OrdinalPosition = 1,
    IsRequired = true,
    EstimatedDurationInMinutes = 30,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a WorkflowType
```csharp
var command = new UpdateWorkflowTypeCommand
{
    Id = workflowTypeId,
    Name = "Enhanced Customer Onboarding",
    Description = "Updated process for onboarding new customers",
    OrdinalPosition = 1,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a WorkflowType
```csharp
var command = new DeleteWorkflowTypeCommand
{
    Id = workflowTypeId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The WorkflowType feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Name is required and limited to 100 characters
- Description is required and limited to 500 characters
- Ordinal position must be a non-negative number
- Name must be unique across all workflow types
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Workflow Type Steps
- Each workflow type can have multiple template steps
- Steps define the standard process to follow for that workflow type
- Steps have properties like Name, Description, OrdinalPosition, IsRequired, and EstimatedDurationInMinutes
- When a workflow is created from a workflow type, the template steps are copied to create workflow steps

### Workflow Instantiation
- When a new workflow is created, it references the workflow type
- The workflow type's template steps are used to create the initial workflow steps
- This ensures consistency in process execution across similar business activities

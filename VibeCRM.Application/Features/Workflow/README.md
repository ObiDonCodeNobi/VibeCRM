# Workflow Feature

## Overview
The Workflow feature provides functionality for managing business workflows in the VibeCRM system. It follows the Clean Architecture and CQRS patterns, implementing a complete set of commands, queries, DTOs, validators, and mapping profiles.

## Components

### Domain Layer
- **Workflow Entity**: Represents a business workflow with properties such as WorkflowId, WorkflowTypeId, Subject, Description, StartDate, and CompletedDate.
- **IWorkflowRepository**: Interface defining methods for workflow data access.

### Application Layer

#### DTOs
- **WorkflowDto**: Base DTO containing core workflow properties.
- **WorkflowDetailsDto**: Extended DTO with additional properties for detailed views.
- **WorkflowListDto**: Simplified DTO optimized for list views with a computed Status property.

#### Commands
- **CreateWorkflow**: Command and handler for creating new workflows.
- **UpdateWorkflow**: Command and handler for updating existing workflows.
- **DeleteWorkflow**: Command and handler for soft-deleting workflows.

#### Queries
- **GetWorkflowById**: Query and handler for retrieving a specific workflow by ID.
- **GetAllWorkflows**: Query and handler for retrieving all active workflows.
- **GetWorkflowsByWorkflowType**: Query and handler for retrieving workflows filtered by workflow type.

#### Validators
- **WorkflowDtoValidator**: Validates the base WorkflowDto.
- **WorkflowDetailsDtoValidator**: Validates the WorkflowDetailsDto.
- **WorkflowListDtoValidator**: Validates the WorkflowListDto.
- Command and query-specific validators for each operation.

#### Mappings
- **WorkflowMappingProfile**: AutoMapper profile for mapping between workflow entities and DTOs.

### Infrastructure Layer
- **WorkflowRepository**: Implementation of IWorkflowRepository using Dapper for data access.

## Usage Examples

### Creating a Workflow
```csharp
var command = new CreateWorkflowCommand
{
    WorkflowTypeId = Guid.Parse("workflow-type-id"),
    Subject = "New Project Onboarding",
    Description = "Workflow for onboarding new client projects",
    StartDate = DateTime.UtcNow,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving a Workflow
```csharp
var query = new GetWorkflowByIdQuery(Guid.Parse("workflow-id"));
var workflow = await _mediator.Send(query);
```

### Updating a Workflow
```csharp
var command = new UpdateWorkflowCommand
{
    Id = Guid.Parse("workflow-id"),
    Subject = "Updated Subject",
    Description = "Updated description",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Workflow
```csharp
var command = new DeleteWorkflowCommand(Guid.Parse("workflow-id"), currentUserId);
var success = await _mediator.Send(command);
```

### Getting Workflows by Type
```csharp
var query = new GetWorkflowsByWorkflowTypeQuery(Guid.Parse("workflow-type-id"));
var workflows = await _mediator.Send(query);
```

## Notes
- All repository methods filter by `Active = 1` to exclude soft-deleted records.
- The `DeleteAsync` method performs a soft delete by setting `Active = 0`.
- Workflows have a computed Status property based on StartDate and CompletedDate values.

# Workflow Feature

## Overview
The Workflow feature provides functionality for managing business workflows in the VibeCRM system. Workflows represent structured business processes that guide users through a series of steps to complete tasks or achieve specific outcomes.

## Domain Model
The Workflow entity is a core business entity that represents a business process instance. Each Workflow has the following properties:

- **WorkflowId**: Unique identifier (UUID)
- **WorkflowTypeId**: Reference to the workflow type that defines this workflow
- **Subject**: Brief title describing the workflow purpose
- **Description**: Detailed description of the workflow
- **StartDate**: Date and time when the workflow was initiated
- **CompletedDate**: Date and time when the workflow was completed (null if not completed)
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **WorkflowSteps**: Collection of associated WorkflowStep entities
- **WorkflowType**: Navigation property to the associated WorkflowType

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **WorkflowDto**: Base DTO with core properties
- **WorkflowDetailsDto**: Extended DTO with audit fields and related data
- **WorkflowListDto**: Optimized DTO for list views with a computed Status property

### Commands
- **CreateWorkflow**: Creates a new workflow
- **UpdateWorkflow**: Updates an existing workflow
- **DeleteWorkflow**: Soft-deletes a workflow by setting Active = false
- **CompleteWorkflow**: Marks a workflow as completed by setting the CompletedDate

### Queries
- **GetAllWorkflows**: Retrieves all active workflows
- **GetWorkflowById**: Retrieves a specific workflow by its ID
- **GetWorkflowsByWorkflowType**: Retrieves workflows filtered by workflow type
- **GetActiveWorkflows**: Retrieves workflows that have started but not completed
- **GetCompletedWorkflows**: Retrieves workflows that have been completed

### Validators
- **WorkflowDtoValidator**: Validates the base DTO
- **WorkflowDetailsDtoValidator**: Validates the detailed DTO
- **WorkflowListDtoValidator**: Validates the list DTO
- **CreateWorkflowCommandValidator**: Validates the create command
- **UpdateWorkflowCommandValidator**: Validates the update command
- **DeleteWorkflowCommandValidator**: Validates the delete command
- **CompleteWorkflowCommandValidator**: Validates the complete command
- **GetWorkflowByIdQueryValidator**: Validates the ID query
- **GetWorkflowsByWorkflowTypeQueryValidator**: Validates the workflow type query

### Mappings
- **WorkflowMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Workflow
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

### Retrieving all Workflows
```csharp
var query = new GetAllWorkflowsQuery();
var workflows = await _mediator.Send(query);
```

### Retrieving a Workflow by ID
```csharp
var query = new GetWorkflowByIdQuery { Id = workflowId };
var workflow = await _mediator.Send(query);
```

### Retrieving Workflows by type
```csharp
var query = new GetWorkflowsByWorkflowTypeQuery { WorkflowTypeId = workflowTypeId };
var workflows = await _mediator.Send(query);
```

### Completing a Workflow
```csharp
var command = new CompleteWorkflowCommand
{
    Id = workflowId,
    CompletedDate = DateTime.UtcNow,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Workflow
```csharp
var command = new UpdateWorkflowCommand
{
    Id = workflowId,
    Subject = "Updated Project Onboarding",
    Description = "Updated workflow description",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Workflow
```csharp
var command = new DeleteWorkflowCommand
{
    Id = workflowId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Workflow feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Subject is required and limited to 200 characters
- Description is limited to 1000 characters
- WorkflowTypeId must reference a valid workflow type
- StartDate must be a valid date and time
- CompletedDate must be null or after StartDate
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Status Calculation
The workflow status is calculated based on the StartDate and CompletedDate:
- If CompletedDate is not null, status is "Completed"
- If StartDate is in the future, status is "Scheduled"
- Otherwise, status is "In Progress"

### Workflow Steps
- Each workflow can have multiple workflow steps
- Steps represent individual tasks or milestones within the workflow
- Steps have their own status tracking and assignment capabilities
- The workflow is considered complete when all required steps are completed

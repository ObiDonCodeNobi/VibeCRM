# Team Feature

## Overview
The Team feature provides functionality for managing teams within the VibeCRM system. Teams are groups of employees that work together on various projects and tasks. Each team has a designated team lead.

## Domain Model
The Team entity is an organizational entity that represents a group of employees. Each Team has the following properties:

- **TeamId**: Unique identifier (UUID)
- **Name**: Name of the team (e.g., "Sales Team", "Development Team")
- **Description**: Detailed description of the team's purpose and responsibilities
- **TeamLeadEmployeeId**: Reference to the employee who leads the team
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **TeamMembers**: Collection of associated TeamMember entities linking to employees

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **TeamDto**: Base DTO with core properties
- **TeamDetailsDto**: Extended DTO with audit fields and member count
- **TeamListDto**: Optimized DTO for list views with member count

### Commands
- **CreateTeam**: Creates a new team
- **UpdateTeam**: Updates an existing team
- **DeleteTeam**: Soft-deletes a team by setting Active = false
- **AddTeamMember**: Adds an employee to a team
- **RemoveTeamMember**: Removes an employee from a team

### Queries
- **GetAllTeams**: Retrieves all active teams
- **GetTeamById**: Retrieves a specific team by its ID
- **GetTeamByName**: Retrieves teams by their name
- **GetTeamsByUserId**: Retrieves all teams that a specific user is a member of
- **GetTeamMembers**: Retrieves all members of a specific team

### Validators
- **TeamDtoValidator**: Validates the base DTO
- **TeamDetailsDtoValidator**: Validates the detailed DTO
- **TeamListDtoValidator**: Validates the list DTO
- **CreateTeamCommandValidator**: Validates the create command
- **UpdateTeamCommandValidator**: Validates the update command
- **DeleteTeamCommandValidator**: Validates the delete command
- **AddTeamMemberCommandValidator**: Validates the add member command
- **RemoveTeamMemberCommandValidator**: Validates the remove member command
- **GetTeamByIdQueryValidator**: Validates the ID query
- **GetTeamByNameQueryValidator**: Validates the name query
- **GetTeamsByUserIdQueryValidator**: Validates the user ID query
- **GetTeamMembersQueryValidator**: Validates the team members query

### Mappings
- **TeamMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Team
```csharp
var command = new CreateTeamCommand
{
    Name = "Sales Team",
    Description = "Team responsible for sales activities",
    TeamLeadEmployeeId = teamLeadId,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Teams
```csharp
var query = new GetAllTeamsQuery();
var teams = await _mediator.Send(query);
```

### Retrieving a Team by ID
```csharp
var query = new GetTeamByIdQuery { Id = teamId };
var team = await _mediator.Send(query);
```

### Retrieving Teams by name
```csharp
var query = new GetTeamByNameQuery { Name = "Sales" };
var teams = await _mediator.Send(query);
```

### Retrieving Teams by user ID
```csharp
var query = new GetTeamsByUserIdQuery { UserId = userId };
var teams = await _mediator.Send(query);
```

### Adding a member to a Team
```csharp
var command = new AddTeamMemberCommand
{
    TeamId = teamId,
    EmployeeId = employeeId,
    CreatedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Team
```csharp
var command = new UpdateTeamCommand
{
    Id = teamId,
    Name = "Updated Sales Team",
    Description = "Updated description for the sales team",
    TeamLeadEmployeeId = newTeamLeadId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Team
```csharp
var command = new DeleteTeamCommand
{
    Id = teamId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Team feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Team name is required and limited to 100 characters
- Description is limited to 500 characters
- Team lead employee ID must reference a valid employee
- Team name must be unique across all teams
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Team Membership
- Team membership is managed through a separate TeamMember entity
- Each employee can be a member of multiple teams
- Each team can have multiple members
- The TeamMember entity tracks when an employee joined a team

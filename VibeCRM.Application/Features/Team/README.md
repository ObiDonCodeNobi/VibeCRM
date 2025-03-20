# Team Feature

## Overview
The Team feature provides functionality for managing teams within the VibeCRM system. Teams are groups of employees that work together on various projects and tasks. Each team has a designated team lead.

## Components

### Domain Entity
- `Team`: Represents a team in the system with properties like TeamId, TeamLeadEmployeeId, Name, and Description.

### DTOs
- `TeamDto`: Basic data transfer object for team information.
- `TeamListDto`: DTO for listing teams, includes member count.
- `TeamDetailsDto`: Detailed DTO that includes audit information and member count.

### Commands
- `CreateTeamCommand`: Creates a new team.
- `UpdateTeamCommand`: Updates an existing team.
- `DeleteTeamCommand`: Soft deletes a team by setting Active to false.

### Queries
- `GetAllTeamsQuery`: Retrieves all active teams.
- `GetTeamByIdQuery`: Retrieves a specific team by its ID.
- `GetTeamByNameQuery`: Retrieves a specific team by its name.
- `GetTeamsByUserIdQuery`: Retrieves all teams that a specific user is a member of.

### Validators
- Validators for all DTOs and commands to ensure data integrity.

### Mapping Profiles
- `TeamMappingProfile`: Maps between Team entity and DTOs.

## Usage Examples

### Creating a Team
```csharp
var createTeamCommand = new CreateTeamCommand
{
    TeamLeadEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
    Name = "Sales Team",
    Description = "Team responsible for sales activities"
};

var result = await _mediator.Send(createTeamCommand);
```

### Updating a Team
```csharp
var updateTeamCommand = new UpdateTeamCommand
{
    Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
    TeamLeadEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
    Name = "Updated Sales Team",
    Description = "Updated description for the sales team"
};

var result = await _mediator.Send(updateTeamCommand);
```

### Deleting a Team
```csharp
var deleteTeamCommand = new DeleteTeamCommand
{
    Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
};

var result = await _mediator.Send(deleteTeamCommand);
```

### Retrieving Teams
```csharp
// Get all teams
var allTeams = await _mediator.Send(new GetAllTeamsQuery());

// Get team by ID
var teamById = await _mediator.Send(new GetTeamByIdQuery { Id = Guid.Parse("00000000-0000-0000-0000-000000000000") });

// Get team by name
var teamByName = await _mediator.Send(new GetTeamByNameQuery { Name = "Sales Team" });

// Get teams by user ID
var teamsByUserId = await _mediator.Send(new GetTeamsByUserIdQuery { UserId = Guid.Parse("00000000-0000-0000-0000-000000000000") });
```

## Notes
- Teams are soft-deleted by setting the `Active` property to false.
- All queries filter out inactive (deleted) teams.
- The `MemberCount` property in DTOs would need to be populated by additional repository methods in a real implementation.

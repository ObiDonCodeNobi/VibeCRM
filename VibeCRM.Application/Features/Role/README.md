# Role Feature

## Overview
The Role feature provides functionality for managing roles in the VibeCRM system. Roles are used to define permissions and access control for users within the application.

## Domain Model
The Role entity is a security entity that represents a set of permissions and access rights. Each Role has the following properties:

- **RoleId**: Unique identifier (UUID)
- **Name**: Name of the role (e.g., "Administrator", "Manager", "User")
- **Description**: Detailed description of the role's permissions and responsibilities
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **UserRoles**: Collection of associated UserRole entities linking to users

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **RoleDto**: Base DTO with core properties
- **RoleDetailsDto**: Extended DTO with audit fields and user count
- **RoleListDto**: Optimized DTO for list views

### Commands
- **CreateRole**: Creates a new role
- **UpdateRole**: Updates an existing role
- **DeleteRole**: Soft-deletes a role by setting Active = false
- **AssignRoleToUser**: Assigns a role to a user
- **RemoveRoleFromUser**: Removes a role from a user

### Queries
- **GetAllRoles**: Retrieves all active roles
- **GetRoleById**: Retrieves a specific role by its ID
- **GetRoleByName**: Retrieves a role by its name
- **GetRolesByUserId**: Retrieves all roles assigned to a specific user

### Validators
- **RoleDtoValidator**: Validates the base DTO
- **RoleDetailsDtoValidator**: Validates the detailed DTO
- **RoleListDtoValidator**: Validates the list DTO
- **CreateRoleCommandValidator**: Validates the create command
- **UpdateRoleCommandValidator**: Validates the update command
- **DeleteRoleCommandValidator**: Validates the delete command
- **AssignRoleToUserCommandValidator**: Validates the assign role command
- **RemoveRoleFromUserCommandValidator**: Validates the remove role command
- **GetRoleByIdQueryValidator**: Validates the ID query
- **GetRoleByNameQueryValidator**: Validates the name query
- **GetRolesByUserIdQueryValidator**: Validates the user ID query

### Mappings
- **RoleMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Role
```csharp
var command = new CreateRoleCommand
{
    Name = "Administrator",
    Description = "System administrator with full access",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Roles
```csharp
var query = new GetAllRolesQuery();
var roles = await _mediator.Send(query);
```

### Retrieving a Role by ID
```csharp
var query = new GetRoleByIdQuery { Id = roleId };
var role = await _mediator.Send(query);
```

### Retrieving a Role by name
```csharp
var query = new GetRoleByNameQuery { Name = "Administrator" };
var role = await _mediator.Send(query);
```

### Retrieving Roles by user ID
```csharp
var query = new GetRolesByUserIdQuery { UserId = userId };
var roles = await _mediator.Send(query);
```

### Assigning a Role to a User
```csharp
var command = new AssignRoleToUserCommand
{
    RoleId = roleId,
    UserId = userId,
    CreatedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Role
```csharp
var command = new UpdateRoleCommand
{
    Id = roleId,
    Name = "Admin",
    Description = "Updated description for system administrator",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Role
```csharp
var command = new DeleteRoleCommand
{
    Id = roleId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Role feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Role name is required and limited to 50 characters
- Description is limited to 500 characters
- Role name must be unique across all roles
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### User Role Associations
- User-role associations are managed through a separate UserRole entity
- Each user can have multiple roles
- Each role can be assigned to multiple users
- The UserRole entity tracks when a role was assigned to a user

### Security Implications
- Roles are a critical part of the application's security model
- Role assignments determine what actions users can perform
- The system includes predefined roles that cannot be deleted
- Role management is typically restricted to administrators

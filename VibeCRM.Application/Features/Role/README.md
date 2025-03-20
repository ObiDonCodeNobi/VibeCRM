# Role Feature

## Overview
The Role feature provides functionality for managing roles in the VibeCRM system. Roles are used to define permissions and access control for users within the application.

## Components

### DTOs
- **RoleDto**: Basic data transfer object for role information
- **RoleDetailsDto**: Detailed DTO including audit properties
- **RoleListDto**: DTO for listing roles

### Commands
- **CreateRoleCommand**: Creates a new role
- **UpdateRoleCommand**: Updates an existing role
- **DeleteRoleCommand**: Soft deletes a role by setting Active = false

### Queries
- **GetRoleByIdQuery**: Retrieves a role by its unique identifier
- **GetAllRolesQuery**: Retrieves all active roles
- **GetRoleByNameQuery**: Retrieves a role by its name
- **GetRolesByUserIdQuery**: Retrieves all roles assigned to a specific user

### Validators
- **RoleDtoValidator**: Validates the basic role DTO
- **RoleDetailsDtoValidator**: Validates the detailed role DTO
- **RoleListDtoValidator**: Validates the role list DTO
- **CreateRoleCommandValidator**: Validates the create role command
- **UpdateRoleCommandValidator**: Validates the update role command
- **DeleteRoleCommandValidator**: Validates the delete role command
- **GetRoleByIdQueryValidator**: Validates the get role by ID query
- **GetAllRolesQueryValidator**: Validates the get all roles query
- **GetRoleByNameQueryValidator**: Validates the get role by name query
- **GetRolesByUserIdQueryValidator**: Validates the get roles by user ID query

### Mapping Profiles
- **RoleMappingProfile**: Defines mappings between Role entities and DTOs

## Usage Examples

### Creating a Role
```csharp
var command = new CreateRoleCommand
{
    Name = "Administrator",
    Description = "System administrator with full access",
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
    Description = "Updated description",
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

var result = await _mediator.Send(command);
```

### Retrieving a Role by ID
```csharp
var query = new GetRoleByIdQuery { Id = roleId };
var result = await _mediator.Send(query);
```

### Retrieving All Roles
```csharp
var query = new GetAllRolesQuery();
var results = await _mediator.Send(query);
```

### Retrieving a Role by Name
```csharp
var query = new GetRoleByNameQuery { Name = "Administrator" };
var result = await _mediator.Send(query);
```

### Retrieving Roles by User ID
```csharp
var query = new GetRolesByUserIdQuery { UserId = userId };
var results = await _mediator.Send(query);
```

## Implementation Details
- Uses soft delete pattern with the Active property
- Follows CQRS pattern with MediatR
- Implements comprehensive validation with FluentValidation
- Uses AutoMapper for entity-DTO mapping
- Includes detailed XML documentation for all components
- Follows Clean Architecture and SOLID principles

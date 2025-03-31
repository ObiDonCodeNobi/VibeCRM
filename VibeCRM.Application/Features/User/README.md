# User Feature

## Overview
The User feature provides functionality for managing user accounts in the VibeCRM system. Users represent individuals who can log into and interact with the system, with different roles and permissions.

## Domain Model
The User entity is a security entity that represents a system user. Each User has the following properties:

- **UserId**: Unique identifier (UUID)
- **Username**: Unique login name for the user
- **Email**: Email address associated with the user account
- **PasswordHash**: Securely stored hash of the user's password
- **FirstName**: User's first name
- **LastName**: User's last name
- **LastLoginDate**: Date and time of the user's last successful login
- **FailedLoginAttempts**: Count of consecutive failed login attempts
- **LockoutEndDate**: Date and time when account lockout expires (if locked)
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **UserRoles**: Collection of associated UserRole entities linking to roles
- **PersonId**: Optional reference to a Person entity for linking to CRM contacts

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **UserDto**: Base DTO with core properties
- **UserDetailsDto**: Extended DTO with audit fields and role information
- **UserListDto**: Optimized DTO for list views
- **UserLoginDto**: DTO for login requests
- **UserRegistrationDto**: DTO for user registration
- **UserPasswordChangeDto**: DTO for password change requests

### Commands
- **CreateUser**: Creates a new user
- **UpdateUser**: Updates an existing user
- **DeleteUser**: Soft-deletes a user by setting Active = false
- **ChangePassword**: Changes a user's password
- **ResetPassword**: Resets a user's password
- **LockUser**: Locks a user account
- **UnlockUser**: Unlocks a user account
- **AssignRoleToUser**: Assigns a role to a user
- **RemoveRoleFromUser**: Removes a role from a user

### Queries
- **GetAllUsers**: Retrieves all active users
- **GetUserById**: Retrieves a specific user by ID
- **GetUserByUsername**: Retrieves a user by username
- **GetUserByEmail**: Retrieves a user by email address
- **GetUsersByRole**: Retrieves all users with a specific role
- **AuthenticateUser**: Authenticates a user with username/password

### Validators
- **UserDtoValidator**: Validates the base DTO
- **UserDetailsDtoValidator**: Validates the detailed DTO
- **UserListDtoValidator**: Validates the list DTO
- **UserLoginDtoValidator**: Validates the login DTO
- **UserRegistrationDtoValidator**: Validates the registration DTO
- **UserPasswordChangeDtoValidator**: Validates the password change DTO
- **CreateUserCommandValidator**: Validates the create command
- **UpdateUserCommandValidator**: Validates the update command
- **DeleteUserCommandValidator**: Validates the delete command
- **ChangePasswordCommandValidator**: Validates the change password command
- **ResetPasswordCommandValidator**: Validates the reset password command
- **GetUserByIdQueryValidator**: Validates the ID query
- **GetUserByUsernameQueryValidator**: Validates the username query
- **GetUserByEmailQueryValidator**: Validates the email query
- **GetUsersByRoleQueryValidator**: Validates the role query
- **AuthenticateUserQueryValidator**: Validates the authentication query

### Mappings
- **UserMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new User
```csharp
var command = new CreateUserCommand
{
    Username = "johndoe",
    Email = "john.doe@example.com",
    Password = "SecurePassword123!",
    FirstName = "John",
    LastName = "Doe",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Users
```csharp
var query = new GetAllUsersQuery();
var users = await _mediator.Send(query);
```

### Retrieving a User by ID
```csharp
var query = new GetUserByIdQuery { Id = userId };
var user = await _mediator.Send(query);
```

### Retrieving a User by username
```csharp
var query = new GetUserByUsernameQuery { Username = "johndoe" };
var user = await _mediator.Send(query);
```

### Authenticating a User
```csharp
var query = new AuthenticateUserQuery 
{ 
    Username = "johndoe", 
    Password = "SecurePassword123!" 
};
var authResult = await _mediator.Send(query);
```

### Changing a User's password
```csharp
var command = new ChangePasswordCommand
{
    Id = userId,
    CurrentPassword = "OldPassword123!",
    NewPassword = "NewSecurePassword456!",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a User
```csharp
var command = new UpdateUserCommand
{
    Id = userId,
    Email = "john.doe.updated@example.com",
    FirstName = "Johnny",
    LastName = "Doe",
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a User
```csharp
var command = new DeleteUserCommand
{
    Id = userId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The User feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Username is required, must be unique, and limited to 50 characters
- Email is required, must be unique, and must be a valid email format
- Password must meet complexity requirements (minimum length, special characters, etc.)
- First name and last name are required and limited to 100 characters each
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Security Considerations
- Passwords are never stored in plain text, only as secure hashes
- Account lockout is implemented after multiple failed login attempts
- Password reset functionality requires email verification
- User management operations are restricted to administrators
- Authentication uses JWT tokens for stateless API access

### Role Management
- Users can have multiple roles
- Roles determine what actions a user can perform in the system
- Role assignments are managed through a separate UserRole entity

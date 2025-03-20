# VibeCRM Application Layer

This directory contains the application layer of VibeCRM, implementing the CQRS (Command Query Responsibility Segregation) pattern using MediatR. The application layer orchestrates the flow of data between the presentation and domain layers.

## Directory Structure

### Behaviors

Contains cross-cutting concerns implemented as MediatR pipeline behaviors:

- **ValidationBehavior.cs**: Implements request validation using FluentValidation.
- **LoggingBehavior.cs**: Provides logging for all requests and responses.
- **PerformanceBehavior.cs**: Measures and logs performance metrics for requests.

### Common

Contains common application components:

- **Exceptions**: Application-specific exceptions.
- **Interfaces**: Interfaces for application services.
- **Models**: DTOs and other model classes used across the application layer.

### Features

Organized by domain entity, each feature folder contains:

- **Commands**: Write operations (Create, Update, Delete) with:
  - Command class (data)
  - Command handler (processing logic)
  - Command validator (using FluentValidation)

- **Queries**: Read operations with:
  - Query class (request parameters)
  - Query handler (retrieval logic)
  - DTOs (Data Transfer Objects for responses)

- **DTOs**: Data Transfer Objects for the specific feature.
- **Validators**: FluentValidation validators for commands and queries.

### Interfaces

Contains interfaces for infrastructure services required by the application layer:

- **Persistence**: Repository interfaces.
- **Services**: External service interfaces.

## Implementation Details

### CQRS with MediatR

The application layer uses MediatR to implement the CQRS pattern:

1. **Commands**: Represent intentions to change state (create, update, delete).
2. **Queries**: Represent requests for data without changing state.
3. **Handlers**: Process commands and queries, orchestrating domain logic.

### Validation with FluentValidation

All commands and queries are validated using FluentValidation:

- Validation rules are defined in separate validator classes
- ValidationBehavior ensures all requests are validated before processing

### DTOs and Mapping

- DTOs are used to transfer data between layers
- AutoMapper profiles define mappings between domain entities and DTOs

## Extension Guidelines

When extending this layer:

1. Follow the established folder structure for new features
2. Implement both command and query handlers for each operation
3. Create comprehensive validation rules for all commands
4. Provide complete XML documentation for all public members
5. Keep handlers focused on orchestration, delegating domain logic to the domain layer
6. Use MediatR notifications for cross-cutting concerns and event-driven functionality

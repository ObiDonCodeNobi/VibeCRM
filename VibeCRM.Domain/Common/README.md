# Domain Common Layer

This directory contains the core abstractions, interfaces, and base classes that form the foundation of the VibeCRM domain model. These components implement patterns from Domain-Driven Design and follow Clean Architecture principles.

## Directory Structure

### Root Files

- **BaseEntity.cs**: Generic base class for all domain entities with typed identifiers. Implements core entity behavior including domain events.
- **BaseAuditableEntity.cs**: Extends BaseEntity with audit fields (CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) and soft delete functionality.
- **IHasDomainEvents.cs**: Interface for entities that can raise domain events.
- **ValueObject.cs**: Base class for implementing value objects with value equality semantics.

### Events

Contains domain event definitions that enable a reactive, event-driven architecture:

- **DomainEvent.cs**: Base class for all domain events that can be raised by entities.

### Exceptions

Contains custom domain-specific exceptions:

- **EntityNotFoundException.cs**: Exception thrown when an entity cannot be found by its identifier.

### Interfaces

Contains core interfaces that define contracts for domain components:

- **IEntity.cs**: Defines the contract for all entities, including both generic and non-generic versions.
- **ISoftDelete.cs**: Interface for entities that support soft deletion.

## Usage Guidelines

1. **Entity Creation**: All domain entities should inherit from either `BaseEntity<TId>` or `BaseAuditableEntity<TId>`.
2. **Value Objects**: Use `ValueObject` as the base class for all immutable value objects.
3. **Domain Events**: Implement domain logic that reacts to state changes using the domain events pattern.
4. **Soft Delete**: Use the `ISoftDelete` interface for entities that should support soft deletion.

## Design Principles

This layer follows these key principles:

- **SOLID Principles**: Particularly the Single Responsibility and Interface Segregation principles.
- **Clean Architecture**: Domain layer contains business rules and is independent of external concerns.
- **Domain-Driven Design**: Entities encapsulate both state and behavior.
- **Onion Architecture**: The domain layer forms the core of the application with no external dependencies.

## Extension Guidelines

When extending this layer:

1. Keep domain logic free of infrastructure concerns
2. Add new base classes only when they represent truly shared behavior
3. Use interfaces to define contracts rather than concrete implementations
4. Maintain comprehensive XML documentation for all public members

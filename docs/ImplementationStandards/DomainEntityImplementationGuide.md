# Domain Entity Implementation Guide

*Document Date: March 2, 2025*

## Overview

This guide provides detailed instructions for implementing domain entities that directly map to the database schema in the VibeCRM system. Following these guidelines will ensure consistent entity modeling throughout the codebase.

## Entity Base Classes

### 1. Base Entity Interfaces

```csharp
/// <summary>
/// Base interface for all entities with a typed identifier
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier</typeparam>
public interface IEntity<TId>
{
    /// <summary>
    /// Gets or sets the entity's identifier
    /// </summary>
    TId Id { get; set; }
}

/// <summary>
/// Non-generic interface for entity operations that don't require knowing the specific ID type
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the entity's identifier as an object
    /// </summary>
    object GetId();
    
    /// <summary>
    /// Gets the type of the entity's identifier
    /// </summary>
    Type GetIdType();
}

/// <summary>
/// Interface for entities that support domain events
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gets the collection of domain events
    /// </summary>
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    
    /// <summary>
    /// Adds a domain event to the entity
    /// </summary>
    /// <param name="domainEvent">The domain event to add</param>
    void AddDomainEvent(DomainEvent domainEvent);
    
    /// <summary>
    /// Removes a domain event from the entity
    /// </summary>
    /// <param name="domainEvent">The domain event to remove</param>
    void RemoveDomainEvent(DomainEvent domainEvent);
    
    /// <summary>
    /// Clears all domain events from the entity
    /// </summary>
    void ClearDomainEvents();
}
```

### 2. Base Entity Implementation

```csharp
/// <summary>
/// Base class for all entities with typed identifiers
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier</typeparam>
public abstract class BaseEntity<TId> : IEntity<TId>, IEntity, IHasDomainEvents
{
    private List<DomainEvent> _domainEvents;
    
    /// <summary>
    /// Gets or sets the entity's identifier
    /// </summary>
    public TId Id { get; set; }
    
    /// <summary>
    /// Gets the collection of domain events
    /// </summary>
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
    
    /// <summary>
    /// Adds a domain event to the entity
    /// </summary>
    /// <param name="domainEvent">The domain event to add</param>
    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents ??= new List<DomainEvent>();
        _domainEvents.Add(domainEvent);
    }
    
    /// <summary>
    /// Removes a domain event from the entity
    /// </summary>
    /// <param name="domainEvent">The domain event to remove</param>
    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }
    
    /// <summary>
    /// Clears all domain events from the entity
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
    
    /// <summary>
    /// Gets the entity's identifier as an object
    /// </summary>
    /// <returns>The entity's identifier</returns>
    public object GetId() => Id;
    
    /// <summary>
    /// Gets the type of the entity's identifier
    /// </summary>
    /// <returns>The type of the entity's identifier</returns>
    public Type GetIdType() => typeof(TId);
}
```

### 3. Auditable Entity Base

```csharp
/// <summary>
/// Base class for entities that require audit information
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier</typeparam>
public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Gets or sets the user or system that created the entity
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the entity was last modified
    /// </summary>
    public DateTime? LastModified { get; set; }
    
    /// <summary>
    /// Gets or sets the user or system that last modified the entity
    /// </summary>
    public string LastModifiedBy { get; set; }
    
    /// <summary>
    /// Gets or sets whether this entity is active
    /// </summary>
    public bool Active { get; set; } = true;
}
```

## Standard Entity Implementation

### 1. Single-Key Entity Pattern

Entities with a single primary key should follow this pattern:

```csharp
/// <summary>
/// Represents a company in the system
/// </summary>
public class Company : BaseAuditableEntity<Guid>
{
    /// <summary>
    /// Gets or sets the company identifier that directly maps to the CompanyId database column
    /// </summary>
    public Guid CompanyId { get => Id; set => Id = value; }
    
    /// <summary>
    /// Gets or sets the company name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the company website
    /// </summary>
    public string Website { get; set; }
    
    /// <summary>
    /// Gets or sets the company phone number
    /// </summary>
    public string Phone { get; set; }
}
```

Key implementation details:
- The generic `Id` property from `BaseEntity<TId>` is used internally
- A table-specific ID property (e.g., `CompanyId`) is exposed that maps to the database column
- The table-specific ID property directly forwards to the base `Id` property

### 2. Composite-Key Entity Pattern

Entities with composite keys should not inherit from `BaseEntity<TId>` and should implement `IEntity` directly:

```csharp
/// <summary>
/// Represents a many-to-many relationship between companies and contacts
/// </summary>
public class CompanyContact : IEntity, IHasDomainEvents
{
    private List<DomainEvent> _domainEvents;
    
    /// <summary>
    /// Gets or sets the company identifier
    /// </summary>
    public Guid CompanyId { get; set; }
    
    /// <summary>
    /// Gets or sets the contact identifier
    /// </summary>
    public Guid ContactId { get; set; }
    
    /// <summary>
    /// Gets or sets the relationship type
    /// </summary>
    public string RelationshipType { get; set; }
    
    /// <summary>
    /// Gets or sets the date the relationship was established
    /// </summary>
    public DateTime EstablishedDate { get; set; }
    
    /// <summary>
    /// Gets or sets whether this entity is active
    /// </summary>
    public bool Active { get; set; } = true;
    
    /// <summary>
    /// Gets the collection of domain events
    /// </summary>
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
    
    /// <summary>
    /// Gets the composite identifier for this entity
    /// </summary>
    /// <returns>An anonymous object containing the composite key</returns>
    public object GetId() => new { CompanyId, ContactId };
    
    /// <summary>
    /// Gets the type of the composite identifier
    /// </summary>
    /// <returns>The type of the composite identifier</returns>
    public Type GetIdType() => typeof(object);
    
    /// <summary>
    /// Adds a domain event to the entity
    /// </summary>
    /// <param name="domainEvent">The domain event to add</param>
    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents ??= new List<DomainEvent>();
        _domainEvents.Add(domainEvent);
    }
    
    /// <summary>
    /// Removes a domain event from the entity
    /// </summary>
    /// <param name="domainEvent">The domain event to remove</param>
    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }
    
    /// <summary>
    /// Clears all domain events from the entity
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}
```

Key implementation details:
- Junction entities implement `IEntity` and `IHasDomainEvents` directly
- The composite key is returned as an anonymous object by `GetId()`
- All the primary key components are exposed as properties
- The `Active` flag is included for soft delete support

## Entity Relationships

### 1. One-to-Many Relationships

For one-to-many relationships, use navigation properties in the parent entity:

```csharp
/// <summary>
/// Represents a company in the system
/// </summary>
public class Company : BaseAuditableEntity<Guid>
{
    public Company()
    {
        Contacts = new List<Contact>();
        Activities = new List<Activity>();
    }
    
    /// <summary>
    /// Gets or sets the company identifier that directly maps to the CompanyId database column
    /// </summary>
    public Guid CompanyId { get => Id; set => Id = value; }
    
    // Other properties...
    
    /// <summary>
    /// Gets or sets the collection of contacts associated with this company
    /// </summary>
    public ICollection<Contact> Contacts { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of activities associated with this company
    /// </summary>
    public ICollection<Activity> Activities { get; set; }
}
```

### 2. Many-to-Many Relationships

For many-to-many relationships, use both the junction entity and navigation properties:

```csharp
/// <summary>
/// Represents a contact in the system
/// </summary>
public class Contact : BaseAuditableEntity<Guid>
{
    public Contact()
    {
        Companies = new List<Company>();
        CompanyContacts = new List<CompanyContact>();
    }
    
    /// <summary>
    /// Gets or sets the contact identifier that directly maps to the ContactId database column
    /// </summary>
    public Guid ContactId { get => Id; set => Id = value; }
    
    // Other properties...
    
    /// <summary>
    /// Gets or sets the collection of companies associated with this contact
    /// </summary>
    public ICollection<Company> Companies { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of company-contact relationships
    /// </summary>
    public ICollection<CompanyContact> CompanyContacts { get; set; }
}
```

## Enumeration Classes

Use enumeration classes instead of enum types for better type safety and flexibility:

```csharp
/// <summary>
/// Represents an activity status in the system
/// </summary>
public class ActivityStatus : BaseEntity<Guid>
{
    /// <summary>
    /// Gets or sets the activity status identifier that directly maps to the ActivityStatusId database column
    /// </summary>
    public Guid ActivityStatusId { get => Id; set => Id = value; }
    
    /// <summary>
    /// Gets or sets the activity status name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the display order of the status
    /// </summary>
    public int DisplayOrder { get; set; }
    
    /// <summary>
    /// Gets or sets whether this is a default status
    /// </summary>
    public bool IsDefault { get; set; }
    
    /// <summary>
    /// Gets or sets whether activities with this status are considered complete
    /// </summary>
    public bool IsComplete { get; set; }
}
```

## Value Objects

Use value objects for complex properties that don't have identity:

```csharp
/// <summary>
/// Represents a physical address
/// </summary>
public class Address : ValueObject
{
    /// <summary>
    /// Gets or sets the street address
    /// </summary>
    public string Street { get; set; }
    
    /// <summary>
    /// Gets or sets the city
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// Gets or sets the state or province
    /// </summary>
    public string State { get; set; }
    
    /// <summary>
    /// Gets or sets the postal code
    /// </summary>
    public string PostalCode { get; set; }
    
    /// <summary>
    /// Gets or sets the country
    /// </summary>
    public string Country { get; set; }
    
    /// <summary>
    /// Gets the components for equality comparison
    /// </summary>
    /// <returns>The components to use for equality comparison</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return PostalCode;
        yield return Country;
    }
}
```

## Best Practices

1. **Match Database Schema Exactly**
   - Entity property names should match database column names
   - Primary key properties should use table-specific names (e.g., `CompanyId`)
   - Include an `Active` property on all entities for soft delete support

2. **Use Proper Types**
   - Use `Guid` for all ID properties (per system requirements)
   - Use appropriate .NET types for other properties
   - Consider using value objects for complex properties

3. **Include Navigation Properties**
   - Add navigation properties for relationships
   - Initialize collection properties in constructors

4. **Document Thoroughly**
   - Add XML documentation to all classes and properties
   - Document relationships and constraints
   - Explain any non-obvious behavior

5. **Follow Domain-Driven Design**
   - Include domain logic in entities where appropriate
   - Use value objects for concepts without identity
   - Implement domain events for important state changes

## Implementation Example: Activity Entity

A complete example of a properly implemented entity:

```csharp
/// <summary>
/// Represents an activity in the system
/// </summary>
public class Activity : BaseAuditableEntity<Guid>
{
    /// <summary>
    /// Initializes a new instance of the Activity class
    /// </summary>
    public Activity()
    {
        Companies = new List<Company>();
        CompanyActivities = new List<CompanyActivity>();
        Contacts = new List<Contact>();
        ContactActivities = new List<ContactActivity>();
    }
    
    /// <summary>
    /// Gets or sets the activity identifier that directly maps to the ActivityId database column
    /// </summary>
    public Guid ActivityId { get => Id; set => Id = value; }
    
    /// <summary>
    /// Gets or sets the activity type identifier
    /// </summary>
    public Guid ActivityTypeId { get; set; }
    
    /// <summary>
    /// Gets or sets the activity type
    /// </summary>
    public ActivityType ActivityType { get; set; }
    
    /// <summary>
    /// Gets or sets the activity status identifier
    /// </summary>
    public Guid ActivityStatusId { get; set; }
    
    /// <summary>
    /// Gets or sets the activity status
    /// </summary>
    public ActivityStatus ActivityStatus { get; set; }
    
    /// <summary>
    /// Gets or sets the activity subject
    /// </summary>
    public string Subject { get; set; }
    
    /// <summary>
    /// Gets or sets the activity description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Gets or sets the scheduled start date and time
    /// </summary>
    public DateTime? ScheduledStart { get; set; }
    
    /// <summary>
    /// Gets or sets the scheduled end date and time
    /// </summary>
    public DateTime? ScheduledEnd { get; set; }
    
    /// <summary>
    /// Gets or sets the actual start date and time
    /// </summary>
    public DateTime? ActualStart { get; set; }
    
    /// <summary>
    /// Gets or sets the actual end date and time
    /// </summary>
    public DateTime? ActualEnd { get; set; }
    
    /// <summary>
    /// Gets or sets the owner identifier
    /// </summary>
    public Guid? OwnerId { get; set; }
    
    /// <summary>
    /// Gets or sets the priority (1-5, where 1 is highest)
    /// </summary>
    public int Priority { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of companies associated with this activity
    /// </summary>
    public ICollection<Company> Companies { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of company-activity relationships
    /// </summary>
    public ICollection<CompanyActivity> CompanyActivities { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of contacts associated with this activity
    /// </summary>
    public ICollection<Contact> Contacts { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of contact-activity relationships
    /// </summary>
    public ICollection<ContactActivity> ContactActivities { get; set; }
    
    /// <summary>
    /// Sets the activity as completed
    /// </summary>
    /// <param name="completionDate">The completion date and time</param>
    /// <param name="userId">The user ID who completed the activity</param>
    public void Complete(DateTime completionDate, Guid userId)
    {
        // Domain logic for completing an activity
        ActualEnd = completionDate;
        LastModified = completionDate;
        LastModifiedBy = userId.ToString();
        
        // Raise domain event
        AddDomainEvent(new ActivityCompletedEvent(ActivityId, completionDate, userId));
    }
}
```

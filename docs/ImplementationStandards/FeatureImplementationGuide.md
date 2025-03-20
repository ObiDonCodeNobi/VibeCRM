# VibeCRM Feature Implementation Guide

## Table of Contents

1. [Introduction](#introduction)
2. [Architecture Overview](#architecture-overview)
3. [Feature Implementation Process](#feature-implementation-process)
4. [Entity Design Guidelines](#entity-design-guidelines)
5. [Repository Implementation](#repository-implementation)
6. [Application Layer Components](#application-layer-components)
   - [DTOs](#dtos)
   - [Validators](#validators)
   - [Commands and Queries](#commands-and-queries)
   - [Mapping Profiles](#mapping-profiles)
   - [Service Registration](#service-registration)
7. [API Layer Components](#api-layer-components)
8. [UI Implementation](#ui-implementation)
9. [Common Pitfalls and Solutions](#common-pitfalls-and-solutions)
10. [Testing Guidelines](#testing-guidelines)
11. [Deployment Considerations](#deployment-considerations)

## Introduction

This guide provides comprehensive instructions for implementing new features in the VibeCRM system. It covers all layers of the application architecture and details best practices to ensure consistency and maintainability. The guide is designed for developers of all experience levels working on the VibeCRM codebase.

## Architecture Overview

VibeCRM follows Clean Architecture and Onion Architecture principles, with a clear separation of concerns:

1. **Domain Layer**: Contains entities, enums, exceptions, interfaces, and logic specific to the domain
2. **Application Layer**: Contains business logic, commands/queries via MediatR (CQRS pattern), validators, and DTOs
3. **Infrastructure Layer**: Contains data access implementations, external service integrations, and cross-cutting concerns
4. **API Layer**: Contains controllers, middleware, and API-specific configurations
5. **UI Layer**: Contains Blazor components and pages using Fluent UI

The application follows these key principles:
- SOLID principles
- Dependency Inversion
- Command Query Responsibility Segregation (CQRS)
- Domain-Driven Design (DDD)

## Feature Implementation Process

### Step 1: Entity Design

1. Create or extend existing domain entities in `VibeCRM.Domain.Entities`
2. Ensure all entities inherit from appropriate base classes:
   - `BaseAuditableEntity<Guid>` for most entities
   - Implement `ISoftDelete` for soft deletion support
3. Always use UUID (Guid) for entity IDs
4. Use proper XML documentation for all properties and methods

### Step 2: Repository Interface

1. Define the repository interface in `VibeCRM.Domain.Interfaces.Repositories`
2. Extend appropriate base repository interface (e.g., `IRepositoryBase<TEntity>`)
3. Add feature-specific data access methods

### Step 3: Repository Implementation

1. Implement the repository in `VibeCRM.Infrastructure.Persistence.Repositories`
2. Use Dapper ORM for data access
3. Follow the established SQL query patterns for consistency
4. Implement soft delete functionality using the `Active` flag

### Step 4: Application Layer Components

1. Create the feature folder structure in `VibeCRM.Application.Features.{FeatureName}`
2. Implement DTOs, Validators, Commands/Queries, and Mapping profiles
3. Register services in the centralized `ServiceCollectionExtensions` class

### Step 5: API Controllers

1. Implement API controllers in `VibeCRM.Api.Controllers`
2. Use the MediatR pattern to dispatch commands and queries
3. Implement proper validation and error handling
4. Configure Swagger/OpenAPI documentation

### Step 6: UI Components

1. Implement Blazor components using Fluent UI
2. Create pages for viewing and managing the feature
3. Implement proper validation and error handling

## Entity Design Guidelines

### Naming Conventions

- Entity names should be singular nouns (e.g., `Person`, `Company`)
- Junction entities should be named as `EntityA_EntityB` (e.g., `Person_Attachment`)
- Property names should use PascalCase
- Table names in the database match the entity names

### Required Properties

All entities must include:

1. `Id` property of type `Guid` (primary key)
2. `Active` property of type `bool` (for soft delete)
3. Audit properties (inherited from `BaseAuditableEntity<Guid>`):
   - `CreatedBy`
   - `CreatedDate`
   - `ModifiedBy`
   - `ModifiedDate`

### Relationships

- Use navigation properties to define relationships between entities
- For one-to-many relationships, use `ICollection<T>` for the "many" side
- For many-to-many relationships, create a junction entity

## Repository Implementation

### Base Repository Structure

All repositories should:

1. Implement the appropriate interface
2. Extend the base repository implementation
3. Include proper exception handling
4. Use asynchronous methods with cancellation token support
5. Implement soft delete using the `Active` property

### Dapper Implementation

Use Dapper ORM for all data access:

```csharp
public async Task<EntityType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
{
    var query = @"
        SELECT *
        FROM EntityTable
        WHERE Id = @Id
        AND Active = 1";

    var parameters = new { Id = id };
    
    using var connection = _dbConnectionFactory.CreateConnection();
    return await connection.QuerySingleOrDefaultAsync<EntityType>(query, parameters);
}
```

### Soft Delete Pattern

Always implement soft delete using the `Active` property:

```csharp
public async Task DeleteAsync(Guid id, string userId, CancellationToken cancellationToken = default)
{
    var query = @"
        UPDATE EntityTable
        SET Active = 0,
            ModifiedDate = @ModifiedDate,
            ModifiedBy = @ModifiedBy
        WHERE Id = @Id
        AND Active = 1";

    var parameters = new
    {
        Id = id,
        ModifiedDate = DateTime.UtcNow,
        ModifiedBy = userId
    };
    
    using var connection = _dbConnectionFactory.CreateConnection();
    await connection.ExecuteAsync(query, parameters);
}
```

## Application Layer Components

### DTOs

Create the following DTOs for each feature:

1. **Base DTO**: Contains common properties
2. **List DTO**: Used for listing entities (optimized for grid display)
3. **Details DTO**: Contains all entity details for single-entity views
4. **Create/Update DTOs**: Contains properties needed for create/update operations

Example:
```csharp
public class PersonAttachmentListDto
{
    public Guid PersonId { get; set; }
    public Guid AttachmentId { get; set; }
    public bool Active { get; set; }
    public string? AttachmentName { get; set; }
    public string? AttachmentTypeName { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public long FileSize { get; set; }
}
```

### Validators

Create FluentValidation validators for each DTO and command/query:

```csharp
public class PersonAttachmentDetailsDtoValidator : AbstractValidator<PersonAttachmentDetailsDto>
{
    public PersonAttachmentDetailsDtoValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty().WithMessage("PersonId is required");
            
        RuleFor(x => x.AttachmentId)
            .NotEmpty().WithMessage("AttachmentId is required");
            
        RuleFor(x => x.AttachmentName)
            .MaximumLength(255).WithMessage("AttachmentName cannot exceed 255 characters");
    }
}
```

### Commands and Queries

Follow the CQRS pattern:

1. **Commands**: Modify data (Create, Update, Delete)
2. **Queries**: Retrieve data (Get, List, Search)

Create separate command/query handlers that implement `IRequestHandler<TRequest, TResponse>`:

```csharp
public class CreatePersonAttachmentCommandHandler
    : IRequestHandler<CreatePersonAttachmentCommand, PersonAttachmentDto>
{
    private readonly IPersonAttachmentRepository _repository;
    private readonly IMapper _mapper;
    
    // Implementation...
}
```

### Mapping Profiles

Create AutoMapper profiles for each feature:

```csharp
public class PersonAttachmentMappingProfile : Profile
{
    public PersonAttachmentMappingProfile()
    {
        CreateMap<Person_Attachment, PersonAttachmentDto>();
        
        CreateMap<Person_Attachment, PersonAttachmentDetailsDto>()
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => 
                src.Person != null ? $"{src.Person.Firstname} {src.Person.Lastname}".Trim() : null))
            .ForMember(dest => dest.AttachmentName, opt => opt.MapFrom(src => 
                src.Attachment != null ? src.Attachment.Filename : null));
                
        // Additional mappings...
    }
}
```

### Service Registration

Register all feature services in the centralized `ServiceCollectionExtensions` class:

```csharp
// In VibeCRM.Application.Extensions.ServiceCollectionExtensions.cs
public static IServiceCollection AddApplicationServices(this IServiceCollection services)
{
    // Register MediatR
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    
    // Register feature services
    services.AddPersonAttachmentFeature();
    services.AddPersonFeature();
    // Additional features...
    
    return services;
}

public static IServiceCollection AddPersonAttachmentFeature(this IServiceCollection services)
{
    // Register validators
    services.AddScoped<IValidator<CreatePersonAttachmentCommand>, CreatePersonAttachmentCommandValidator>();
    
    // Register AutoMapper profiles
    services.AddSingleton(provider => new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new PersonAttachmentMappingProfile());
    }).CreateMapper());
    
    return services;
}
```

## API Layer Components

### Controller Implementation

```csharp
[ApiController]
[Route("api/persons/{personId}/attachments")]
public class PersonAttachmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public PersonAttachmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<PersonAttachmentListDto>>> GetAttachments(
        Guid personId,
        [FromQuery] GetPersonAttachmentsQuery query,
        CancellationToken cancellationToken)
    {
        query.PersonId = personId;
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
    
    // Additional endpoints...
}
```

## UI Implementation

### Blazor Component Structure

```csharp
@page "/persons/{PersonId:guid}/attachments"
@using VibeCRM.Application.Features.PersonAttachment.DTOs
@using VibeCRM.Application.Features.PersonAttachment.Queries.GetPersonAttachments
@inject HttpClient Http

<FluentPageTitle>Person Attachments</FluentPageTitle>

<FluentStack>
    <FluentCard>
        <FluentDataGrid TGridItem="PersonAttachmentListDto" Items="@attachments">
            <PropertyColumn Property="@(x => x.AttachmentName)" Title="Name" Sortable="true" />
            <PropertyColumn Property="@(x => x.AttachmentTypeName)" Title="Type" Sortable="true" />
            <PropertyColumn Property="@(x => x.FileSize)" Title="Size" Sortable="true" />
            <PropertyColumn Property="@(x => x.ModifiedDate)" Title="Modified" Sortable="true" Format="MMM dd, yyyy" />
            <TemplateColumn Title="Actions">
                <FluentButton OnClick="@(() => ViewAttachment(context))">View</FluentButton>
                <FluentButton OnClick="@(() => DeleteAttachment(context))">Delete</FluentButton>
            </TemplateColumn>
        </FluentDataGrid>
    </FluentCard>
</FluentStack>

@code {
    [Parameter] public Guid PersonId { get; set; }
    private List<PersonAttachmentListDto> attachments = new();
    
    protected override async Task OnInitializedAsync()
    {
        await LoadAttachments();
    }
    
    private async Task LoadAttachments()
    {
        var result = await Http.GetFromJsonAsync<PagedResult<PersonAttachmentListDto>>(
            $"api/persons/{PersonId}/attachments");
            
        if (result != null)
        {
            attachments = result.Items.ToList();
        }
    }
    
    // Additional methods...
}
```

## Common Pitfalls and Solutions

### 1. Entity Definition vs. Code Mismatch

**Problem**: Property names in code don't match entity definitions, causing compilation or runtime errors.

**Solution**: 
- Always treat entity definitions as the source of truth
- Never modify entity definitions to match other code
- Update all other code to match entity definitions
- Use the entity property names consistently across the codebase

**Example**: 
```csharp
// Incorrect
attachments.OrderBy(a => a.Attachment != null ? a.Attachment.FileSize : 0)

// Correct (if the entity uses Path.Length for file size)
attachments.OrderBy(a => a.Attachment != null ? a.Attachment.Path.Length : 0)
```

### 2. AutoMapper Registration Issues

**Problem**: Incorrect AutoMapper registration causing runtime errors.

**Solution**:
- Use the standard MapperConfiguration approach
- Register all profiles in the centralized service registration class
- Ensure all mappings are complete and handle null values properly

**Example**:
```csharp
// Incorrect
services.AddAutoMapper(typeof(PersonAttachmentMappingProfile).Assembly);

// Correct
services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new PersonAttachmentMappingProfile());
}).CreateMapper());
```

### 3. Null Reference Exceptions

**Problem**: Null reference exceptions when handling optional relationships.

**Solution**:
- Use nullable reference types properly (`string?`, `Entity?`)
- Add null checks in all mapping and handling code
- Update method signatures to indicate nullable returns with `?`
- Ensure all validators handle nullable fields appropriately

**Example**:
```csharp
// Incorrect
public Task<PersonAttachmentDetailsDto> Handle(GetPersonAttachmentQuery request, CancellationToken cancellationToken)

// Correct
public Task<PersonAttachmentDetailsDto?> Handle(GetPersonAttachmentQuery request, CancellationToken cancellationToken)
```

### 4. Soft Delete Implementation Errors

**Problem**: Inconsistent implementation of soft delete patterns.

**Solution**:
- Always use the `Active` boolean property for soft delete (not `IsDeleted`)
- All queries should filter by `Active = 1` to exclude soft-deleted records
- For delete operations, set `Active = 0` instead of removing records
- Ensure count queries also include the `Active = 1` condition

**Example**:
```csharp
// Incorrect
await _repository.RemoveRelationshipAsync(entity.PersonId, entity.AttachmentId, cancellationToken);

// Correct
entity.Active = false;
await _repository.UpdateAsync(entity, cancellationToken);
```

### 5. Service Registration Duplication

**Problem**: Multiple feature-specific service registration extensions creating confusion.

**Solution**:
- Use a centralized `ServiceCollectionExtensions` class in `Application.Extensions`
- Create feature-specific extension methods within the centralized class
- Register all features via a single call to `AddApplicationServices()`

**Example**:
```csharp
// In Program.cs
builder.Services.AddApplicationServices();

// Instead of multiple calls like:
builder.Services.AddPersonAttachmentServices();
builder.Services.AddPersonServices();
builder.Services.AddCompanyServices();
```

## Testing Guidelines

### Unit Testing

1. Test each validator, command handler, and query handler independently
2. Mock repositories and other dependencies
3. Test both happy path and error scenarios
4. Test edge cases such as empty collections and null values

### Integration Testing

1. Test API endpoints using a test database
2. Test the entire request-to-response flow
3. Verify correct HTTP status codes and response formats
4. Test authentication and authorization

## Deployment Considerations

1. Update database schema with new entity tables
2. Run migrations in the correct order
3. Update connection strings and configuration settings
4. Test deployed application thoroughly

## Conclusion

By following this guide, you can ensure consistent implementation of features in the VibeCRM system. Consistency across the codebase improves maintainability, reduces bugs, and makes onboarding new developers easier. If you encounter issues not covered in this guide, please update it with new findings and solutions.

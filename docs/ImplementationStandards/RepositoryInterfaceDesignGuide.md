# Repository Interface Design Guide

*Document Date: March 2, 2025*

## Overview

This guide provides detailed instructions for designing repository interfaces that align with the CQRS pattern, clean architecture, and database-first design principles in the VibeCRM system. Following these guidelines will ensure consistent repository interface design throughout the codebase.

## Three-Layer Repository Interface Pattern

The repository interface pattern in VibeCRM is structured into three distinct layers:

1. **Domain Repository Interfaces**
   - Located in `Domain.Common.Interfaces.Repositories`
   - Define core domain operations
   - Work with domain entities only
   - Focus on basic CRUD and domain-specific operations

2. **Application Base Repository Interfaces**
   - Located in `Application.Common.Interfaces.Repositories`
   - Extend domain interfaces
   - Add application-specific operations
   - Return DTOs instead of entities
   - Support pagination and filtering

3. **CQRS Repository Interfaces**
   - Located in `Application.Common.Interfaces.Repositories`
   - Split into Command and Query interfaces
   - Command interfaces handle write operations
   - Query interfaces handle read operations
   - Both work with DTOs
   - Support advanced features like pagination, filtering, and sorting

## Domain Repository Interfaces

Domain repository interfaces define the core operations for each entity. They should be simple and focused on the essential CRUD operations:

```csharp
/// <summary>
/// Interface for repository operations on the Company entity
/// </summary>
public interface ICompanyRepository
{
    /// <summary>
    /// Adds a new company to the repository
    /// </summary>
    /// <param name="company">The company to add</param>
    void Add(Company company);
    
    /// <summary>
    /// Updates an existing company in the repository
    /// </summary>
    /// <param name="company">The company to update</param>
    void Update(Company company);
    
    /// <summary>
    /// Removes a company from the repository
    /// </summary>
    /// <param name="company">The company to remove</param>
    void Remove(Company company);
    
    /// <summary>
    /// Finds a company by its identifier
    /// </summary>
    /// <param name="companyId">The identifier of the company</param>
    /// <returns>The company if found, otherwise null</returns>
    Company FindById(Guid companyId);
}
```

## Application Base Repository Interfaces

These interfaces extend the domain interfaces to add application-specific functionality. They return DTOs and support additional features like pagination:

```csharp
/// <summary>
/// Interface for application-specific repository operations on the Company entity
/// </summary>
public interface ICompanyApplicationRepository : ICompanyRepository
{
    /// <summary>
    /// Gets a paginated list of companies
    /// </summary>
    /// <param name="pageNumber">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>A paginated list of company DTOs</returns>
    PaginatedList<CompanyDto> GetCompanies(int pageNumber, int pageSize);
    
    /// <summary>
    /// Searches for companies by name
    /// </summary>
    /// <param name="name">The name to search for</param>
    /// <returns>A list of matching company DTOs</returns>
    List<CompanyDto> SearchByName(string name);
}
```

## CQRS Repository Interfaces

CQRS repository interfaces are split into command and query interfaces. They are designed to handle specific tasks related to either writing or reading data:

### Command Interfaces

Command interfaces handle write operations and should focus on tasks that modify data:

```csharp
/// <summary>
/// Command interface for operations on the Company entity
/// </summary>
public interface ICompanyCommandRepository
{
    /// <summary>
    /// Creates a new company
    /// </summary>
    /// <param name="companyDto">The company data to create</param>
    void CreateCompany(CompanyDto companyDto);
    
    /// <summary>
    /// Updates an existing company
    /// </summary>
    /// <param name="companyDto">The company data to update</param>
    void UpdateCompany(CompanyDto companyDto);
    
    /// <summary>
    /// Deletes a company by its identifier
    /// </summary>
    /// <param name="companyId">The identifier of the company to delete</param>
    void DeleteCompany(Guid companyId);
}
```

### Query Interfaces

Query interfaces handle read operations and should focus on retrieving data efficiently:

```csharp
/// <summary>
/// Query interface for operations on the Company entity
/// </summary>
public interface ICompanyQueryRepository
{
    /// <summary>
    /// Gets a company by its identifier
    /// </summary>
    /// <param name="companyId">The identifier of the company</param>
    /// <returns>The company DTO if found, otherwise null</returns>
    CompanyDto GetCompanyById(Guid companyId);
    
    /// <summary>
    /// Gets all companies
    /// </summary>
    /// <returns>A list of all company DTOs</returns>
    List<CompanyDto> GetAllCompanies();
}
```

## Best Practices

1. **Separation of Concerns**
   - Keep command and query operations separate
   - Ensure interfaces are focused on a single responsibility

2. **Return DTOs for Application Interfaces**
   - Use DTOs to decouple the application layer from the domain layer
   - Support pagination and filtering in application interfaces

3. **Consistent Naming Conventions**
   - Use clear and descriptive names for interfaces and methods
   - Follow the established naming patterns for consistency

4. **Comprehensive Documentation**
   - Add XML documentation to all interfaces and methods
   - Describe the purpose and behavior of each method

5. **Adhere to SOLID Principles**
   - Ensure interfaces are small and focused (Interface Segregation Principle)
   - Design interfaces that can be easily extended (Open/Closed Principle)

6. **Database-First Design**
   - Design database schema before implementing repository interfaces
   - Ensure database schema aligns with domain entity implementation principles

7. **Clean Architecture**
   - Keep repository interfaces independent of infrastructure and presentation layers
   - Ensure repository interfaces are testable and maintainable

## Implementation Example: Company Repository Interfaces

A complete example of repository interfaces for the `Company` entity:

```csharp
// Domain Repository Interface
public interface ICompanyRepository
{
    void Add(Company company);
    void Update(Company company);
    void Remove(Company company);
    Company FindById(Guid companyId);
}

// Application Base Repository Interface
public interface ICompanyApplicationRepository : ICompanyRepository
{
    PaginatedList<CompanyDto> GetCompanies(int pageNumber, int pageSize);
    List<CompanyDto> SearchByName(string name);
}

// Command Repository Interface
public interface ICompanyCommandRepository
{
    void CreateCompany(CompanyDto companyDto);
    void UpdateCompany(CompanyDto companyDto);
    void DeleteCompany(Guid companyId);
}

// Query Repository Interface
public interface ICompanyQueryRepository
{
    CompanyDto GetCompanyById(Guid companyId);
    List<CompanyDto> GetAllCompanies();
}
```

This guide provides the foundational structure for designing repository interfaces within the VibeCRM system. By adhering to these guidelines, developers can ensure a consistent and maintainable architecture that supports the CQRS pattern, clean architecture, and database-first design principles.

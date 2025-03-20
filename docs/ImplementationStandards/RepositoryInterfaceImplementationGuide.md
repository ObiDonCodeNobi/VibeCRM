# Repository Interface Implementation Guide

*Document Date: March 2, 2025*

## Overview

This guide provides detailed instructions for implementing repository interfaces in the VibeCRM system. Following these guidelines will ensure that repository implementations are consistent, maintainable, and aligned with the CQRS pattern, clean architecture, and database-first design principles.

## Implementation Steps

### 1. Define Repository Interfaces

Before implementing a repository, ensure that you have defined the necessary interfaces as outlined in the Repository Interface Design Guide. The interfaces should be divided into:
- **Domain Repository Interfaces** for core CRUD operations.
- **Application Base Repository Interfaces** for application-specific operations.
- **CQRS Repository Interfaces** for command and query separation.

### 2. Implement Domain Repositories

Domain repositories should focus on core CRUD operations and work directly with domain entities:

```csharp
/// <summary>
/// Implementation of the Company repository for domain operations
/// </summary>
public class CompanyRepository : ICompanyRepository
{
    private readonly IDbConnection _connection;
    
    /// <summary>
    /// Initializes a new instance of the CompanyRepository class
    /// </summary>
    /// <param name="connection">The database connection</param>
    public CompanyRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    /// Meaningful XML Comments
    public void Add(Company company)
    {
        const string sql = "INSERT INTO Companies (CompanyId, Name, Website, Phone) VALUES (@Id, @Name, @Website, @Phone);";
        _connection.Execute(sql, company);
    }
    
    /// Meaningful XML Comments
    public void Update(Company company)
    {
        const string sql = "UPDATE Companies SET Name = @Name, Website = @Website, Phone = @Phone WHERE CompanyId = @Id;";
        _connection.Execute(sql, company);
    }
    
    /// Meaningful XML Comments
    public void Remove(Company company)
    {
        const string sql = "DELETE FROM Companies WHERE CompanyId = @Id;";
        _connection.Execute(sql, company);
    }
    
    /// Meaningful XML Comments
    public Company FindById(Guid companyId)
    {
        const string sql = "SELECT * FROM Companies WHERE CompanyId = @Id;";
        return _connection.QuerySingleOrDefault<Company>(sql, new { Id = companyId });
    }
}
```

### 3. Implement Application Base Repositories

Application repositories extend domain repositories and add application-specific logic, often returning DTOs:

```csharp
/// <summary>
/// Implementation of the Company application repository
/// </summary>
public class CompanyApplicationRepository : CompanyRepository, ICompanyApplicationRepository
{
    public CompanyApplicationRepository(IDbConnection connection) : base(connection)
    {
    }
    
    /// Meaningful XML Comments
    public PaginatedList<CompanyDto> GetCompanies(int pageNumber, int pageSize)
    {
        const string sql = "SELECT * FROM Companies ORDER BY Name OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";
        var companies = _connection.Query<CompanyDto>(sql, new { Offset = (pageNumber - 1) * pageSize, PageSize = pageSize }).ToList();
        return new PaginatedList<CompanyDto>(companies, pageNumber, pageSize);
    }
    
    /// Meaningful XML Comments
    public List<CompanyDto> SearchByName(string name)
    {
        const string sql = "SELECT * FROM Companies WHERE Name LIKE '%' + @Name + '%';";
        return _connection.Query<CompanyDto>(sql, new { Name = name }).ToList();
    }
}
```

### 4. Implement CQRS Repositories

CQRS repositories should be split into command and query implementations, each handling their respective responsibilities:

#### Command Repository Implementation

```csharp
/// <summary>
/// Implementation of the Company command repository
/// </summary>
public class CompanyCommandRepository : ICompanyCommandRepository
{
    private readonly IDbConnection _connection;
    
    public CompanyCommandRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    /// Meaningful XML Comments
    public void CreateCompany(CompanyDto companyDto)
    {
        const string sql = "INSERT INTO Companies (CompanyId, Name, Website, Phone) VALUES (@Id, @Name, @Website, @Phone);";
        _connection.Execute(sql, companyDto);
    }
    
    /// Meaningful XML Comments
    public void UpdateCompany(CompanyDto companyDto)
    {
        const string sql = "UPDATE Companies SET Name = @Name, Website = @Website, Phone = @Phone WHERE CompanyId = @Id;";
        _connection.Execute(sql, companyDto);
    }
    
    /// Meaningful XML Comments
    public void DeleteCompany(Guid companyId)
    {
        const string sql = "DELETE FROM Companies WHERE CompanyId = @Id;";
        _connection.Execute(sql, new { Id = companyId });
    }
}
```

#### Query Repository Implementation

```csharp
/// <summary>
/// Implementation of the Company query repository
/// </summary>
public class CompanyQueryRepository : ICompanyQueryRepository
{
    private readonly IDbConnection _connection;
    
    public CompanyQueryRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    /// Meaningful XML Comments
    public CompanyDto GetCompanyById(Guid companyId)
    {
        const string sql = "SELECT * FROM Companies WHERE CompanyId = @Id;";
        return _connection.QuerySingleOrDefault<CompanyDto>(sql, new { Id = companyId });
    }
    
    /// Meaningful XML Comments
    public List<CompanyDto> GetAllCompanies()
    {
        const string sql = "SELECT * FROM Companies;";
        return _connection.Query<CompanyDto>(sql).ToList();
    }
}
```

## Best Practices

1. **Adhere to Interface Contracts**
   - Ensure all methods are implemented as defined in the interfaces
   - Use XML comments to provide documentation for each method

2. **Use Dapper for Data Access**
   - Leverage Dapper for efficient database operations
   - Ensure SQL queries are parameterized to prevent SQL injection

3. **Separation of Concerns**
   - Keep command and query operations in separate classes
   - Ensure each class has a single responsibility

4. **Testability**
   - Design repositories to be easily testable
   - Use dependency injection for database connections
   - Write unit tests for all repository methods

5. **Performance Optimization**
   - Optimize SQL queries for performance
   - Use indexes appropriately to speed up data retrieval

## Conclusion

By following this guide, developers can implement repository interfaces that are consistent with the architectural principles of the VibeCRM system. This ensures maintainability, scalability, and alignment with the overall system design.

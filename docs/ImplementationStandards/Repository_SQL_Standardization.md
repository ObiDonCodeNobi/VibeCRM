# Repository SQL Query Standardization Checklist

## Standards to Apply
1. SQL queries should be defined as constants at the top of the class
2. Queries should be grouped in a `#region SQL Queries` block
3. Use consistent naming pattern: {Action}{Entity}Query (e.g., GetByCompanyQuery)
4. Use verbatim string literals (@"...") for multi-line queries
5. Use proper SQL formatting and indentation
6. Use aliases for table names in JOINs
7. Include comments for complex queries
8. Include meaningful column selections (not just SELECT *)
9. Add proper error handling and logging for each query
10. Add soft delete checks (IsDeleted = 0) where applicable
11. Include useful computed columns (e.g., FullName, FormattedAddress)
12. Use CASE statements for status and type mappings

# SQL Query Standardization for VibeCRM Repositories

This document outlines the SQL query standards and best practices for repository implementations in the VibeCRM project.

## 1. Query Structure and Formatting

### Basic Query Structure
```sql
SELECT 
    t.Column1,
    t.Column2,
    -- Computed columns last
    CONCAT(u.FirstName, ' ', u.LastName) AS UserFullName
FROM dbo.TableName t
INNER JOIN dbo.RequiredTable rt ON t.Id = rt.TableId
LEFT JOIN dbo.[User] u ON t.UserId = u.Id
WHERE t.Active = 1
    AND t.OtherCondition = @Param
ORDER BY t.CreatedDate DESC;
```

### Naming Conventions
- Use table aliases that are meaningful (e.g., `emp` for Employee, `ord` for Order)
- Prefix computed columns with their purpose (e.g., `Computed`, `Derived`, `Total`)
- Use PascalCase for column aliases
- Use snake_case for junction table names (e.g., `Company_Employee`)

## 2. Common Query Patterns

### Get By ID
```sql
SELECT 
    t.*,
    -- Computed columns
    CONCAT(cu.FirstName, ' ', cu.LastName) AS CreatedByUserFullName,
    CONCAT(mu.FirstName, ' ', mu.LastName) AS ModifiedByUserFullName
FROM dbo.TableName t
LEFT JOIN dbo.[User] cu ON t.CreatedBy = cu.UserId
LEFT JOIN dbo.[User] mu ON t.ModifiedBy = mu.UserId
WHERE t.Id = @Id 
    AND t.Active = 1;
```

### Get All Active
```sql
SELECT 
    t.*,
    -- Computed columns
    CONCAT(cu.FirstName, ' ', cu.LastName) AS CreatedByUserFullName,
    CONCAT(mu.FirstName, ' ', mu.LastName) AS ModifiedByUserFullName
FROM dbo.TableName t
LEFT JOIN dbo.[User] cu ON t.CreatedBy = cu.UserId
LEFT JOIN dbo.[User] mu ON t.ModifiedBy = mu.UserId
WHERE t.Active = 1
ORDER BY t.CreatedDate DESC;
```

### Get By Date Range
```sql
SELECT 
    t.*,
    -- Computed columns
    CONCAT(cu.FirstName, ' ', cu.LastName) AS CreatedByUserFullName,
    CONCAT(mu.FirstName, ' ', mu.LastName) AS ModifiedByUserFullName
FROM dbo.TableName t
LEFT JOIN dbo.[User] cu ON t.CreatedBy = cu.UserId
LEFT JOIN dbo.[User] mu ON t.ModifiedBy = mu.UserId
WHERE t.Active = 1
    AND t.CreatedDate BETWEEN @StartDate AND @EndDate
ORDER BY t.CreatedDate DESC;
```

### Junction Table Queries
```sql
SELECT 
    j.*,
    -- Left entity columns
    l.Name AS LeftEntityName,
    -- Right entity columns
    r.Name AS RightEntityName,
    -- Computed columns
    CONCAT(cu.FirstName, ' ', cu.LastName) AS CreatedByUserFullName,
    CONCAT(mu.FirstName, ' ', mu.LastName) AS ModifiedByUserFullName
FROM dbo.Left_Right j
INNER JOIN dbo.LeftEntity l ON j.LeftId = l.Id
INNER JOIN dbo.RightEntity r ON j.RightId = r.Id
LEFT JOIN dbo.[User] cu ON j.CreatedBy = cu.UserId
LEFT JOIN dbo.[User] mu ON j.ModifiedBy = mu.UserId
WHERE j.Active = 1;
```

## 3. Performance Considerations

### Indexing
- Always include WHERE clause columns in indexes
- Consider covering indexes for frequently used queries
- Include ORDER BY columns in indexes where possible

### Join Guidelines
- Use INNER JOIN for required relationships
- Use LEFT JOIN for optional relationships
- Avoid RIGHT JOIN and FULL JOIN
- Join order: most restrictive first

### Filtering
- Always include Active = 1 in WHERE clause
- Use parameterized queries
- Avoid wildcard searches at start of LIKE patterns
- Use EXISTS instead of IN for subqueries

## 4. Error Handling and Logging

### Query Logging
```sql
-- Before executing query
_logger.Debug("Executing {QueryType} for {EntityType} with parameters: {@Parameters}", 
    "GetById", typeof(T).Name, new { Id = id });

-- After successful execution
_logger.Debug("Retrieved {EntityType} with ID {Id}", typeof(T).Name, id);

-- Error handling
catch (Exception ex)
{
    _logger.Error(ex, "Error executing {QueryType} for {EntityType} with ID {Id}", 
        "GetById", typeof(T).Name, id);
    throw;
}
```

### Null Handling
```sql
-- Use COALESCE for default values
COALESCE(t.Description, 'No description') AS Description

-- Use NULLIF for conditional null
NULLIF(t.Value, 0) AS NonZeroValue

-- Handle concatenation nulls
CONCAT_WS(' ', t.FirstName, t.MiddleName, t.LastName) AS FullName
```

## 5. Repository Implementation

### Query Constants
```csharp
private const string GetByIdQuery = @"
    SELECT 
        t.*,
        -- Computed columns
        CONCAT(cu.FirstName, ' ', cu.LastName) AS CreatedByUserFullName,
        CONCAT(mu.FirstName, ' ', mu.LastName) AS ModifiedByUserFullName
    FROM dbo.TableName t
    LEFT JOIN dbo.[User] cu ON t.CreatedBy = cu.UserId
    LEFT JOIN dbo.[User] mu ON t.ModifiedBy = mu.UserId
    WHERE t.Id = @Id 
        AND t.Active = 1;";
```

### Query Execution
```csharp
public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
{
    try
    {
        using var connection = await GetOpenConnectionAsync(cancellationToken);
        var command = new CommandDefinition(
            GetByIdQuery,
            new { Id = id },
            cancellationToken: cancellationToken
        );
        return await connection.QuerySingleOrDefaultAsync<T>(command);
    }
    catch (Exception ex)
    {
        _logger.Error(ex, "Error retrieving {EntityType} with ID {Id}", 
            typeof(T).Name, id);
        throw;
    }
}
```

## 6. Testing Considerations

### Query Testing
- Test with various data sizes
- Test edge cases (nulls, empty strings)
- Test date range boundaries
- Test with concurrent operations

### Performance Testing
- Test with realistic data volumes
- Monitor query execution plans
- Test with concurrent users
- Measure and log query execution times

## 7. Documentation

### XML Comments
```csharp
/// <summary>
/// Gets an entity by its unique identifier with computed metadata.
/// </summary>
/// <param name="id">The unique identifier of the entity.</param>
/// <param name="cancellationToken">A token to cancel the operation if needed.</param>
/// <returns>The entity with computed metadata if found; otherwise null.</returns>
/// <exception cref="Exception">Thrown when database operation fails.</exception>
public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
```

### Query Comments
```sql
-- Query to retrieve entity by ID with computed metadata
-- Includes user information for audit fields
-- Returns: Single entity or null if not found
SELECT 
    t.*,
    -- Computed user full names for audit
    CONCAT(cu.FirstName, ' ', cu.LastName) AS CreatedByUserFullName,
    CONCAT(mu.FirstName, ' ', mu.LastName) AS ModifiedByUserFullName

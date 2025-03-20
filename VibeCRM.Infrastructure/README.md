# VibeCRM Infrastructure Layer

This directory contains the infrastructure layer of VibeCRM, implementing the technical capabilities required by the application layer. It provides concrete implementations of interfaces defined in the domain and application layers.

## Directory Structure

### Persistence

Contains database-related implementations:

- **ConnectionFactory**: Manages database connections to MS SQL Server.
- **Repositories**: Implements repository interfaces defined in the domain layer using Dapper ORM.
- **Scripts**: Contains SQL scripts for database setup and migrations.

### Services

Contains implementations of external service interfaces:

- **Authentication**: JWT authentication service implementation.
- **Email**: Email service implementation.
- **FileStorage**: File storage service implementation.
- **Logging**: Serilog implementation for application logging.

### DependencyInjection

Contains extension methods for registering infrastructure services:

- **DependencyInjection.cs**: Registers all infrastructure services with the DI container.
- **PersistenceServiceRegistration.cs**: Registers database-related services.
- **ExternalServiceRegistration.cs**: Registers external service implementations.

## Implementation Details

### Data Access with Dapper

The infrastructure layer uses Dapper as the ORM of choice:

1. **BaseRepository**: Provides common CRUD operations for all entities.
2. **Specialized Repositories**: Implement domain-specific repository interfaces.
3. **SQL Queries**: Uses parameterized SQL queries for data access.

### Resilience with Polly

The infrastructure layer implements resilience patterns using Polly:

1. **Retry Policies**: Automatically retry operations that fail due to transient errors.
   ```csharp
   // Example retry policy configuration
   var retryPolicy = Policy
       .Handle<SqlException>(ex => ex.IsTransient())
       .Or<TimeoutException>()
       .WaitAndRetry(
           retryCount: 3,
           sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
           onRetry: (exception, timeSpan, retryCount, context) =>
           {
               _logger.LogWarning(exception, "Error executing SQL command. Retry attempt {RetryCount}", retryCount);
           });
   ```

2. **Circuit Breaker**: Prevents cascading failures by stopping operations after consecutive failures.
   ```csharp
   var circuitBreakerPolicy = Policy
       .Handle<SqlException>()
       .CircuitBreaker(
           exceptionsAllowedBeforeBreaking: 5,
           durationOfBreak: TimeSpan.FromMinutes(1),
           onBreak: (ex, breakDelay) => _logger.LogError(ex, "Circuit broken for {BreakDelay}", breakDelay),
           onReset: () => _logger.LogInformation("Circuit reset"));
   ```

3. **Timeout Policies**: Ensure operations don't hang indefinitely.
   ```csharp
   var timeoutPolicy = Policy.Timeout(
       timeout: TimeSpan.FromSeconds(30),
       onTimeout: (context, timespan, task) =>
       {
           _logger.LogWarning("Operation timed out after {Timespan}", timespan);
       });
   ```

4. **Policy Wrapping**: Combines multiple policies for comprehensive resilience.
   ```csharp
   var resiliencePolicy = Policy.Wrap(retryPolicy, circuitBreakerPolicy, timeoutPolicy);
   ```

5. **Implementation in Repositories**: All repository methods use these policies to ensure resilient data access.

### Authentication with JWT

JWT authentication is implemented with:

- Token generation and validation
- Role-based authorization
- Secure token storage

### Logging with Serilog

Comprehensive logging is implemented using Serilog:

- Structured logging for better searchability
- Multiple sinks (console, file, database)
- Log enrichment with contextual information

## Configuration

Infrastructure services are configured through:

- **appsettings.json**: Contains configuration for all infrastructure services.
- **Environment variables**: Allows overriding configuration in different environments.
- **Options pattern**: Strongly-typed configuration objects.

## Extension Guidelines

When extending this layer:

1. Implement interfaces defined in the domain or application layers
2. Keep implementations technology-specific but hidden behind abstractions
3. Use dependency injection for all services
4. Follow the repository pattern for all data access
5. Use Dapper for all database operations
6. Provide comprehensive error handling and logging
7. Maintain complete XML documentation for all public members
8. Ensure all repositories inherit from the appropriate base repository class
9. Register all new services in the DependencyInjection class

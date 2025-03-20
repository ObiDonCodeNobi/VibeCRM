# Company Feature

## Overview
The Company feature provides functionality for managing company records within the VibeCRM system. Companies are core business entities that can be associated with people, activities, calls, and other entities. This feature follows the CQRS pattern with MediatR for command and query separation.

## Key Components

### DTOs
- `CompanyDto`: Base DTO containing common company properties
- `CompanyDetailsDto`: Detailed DTO with additional information for single company views
- `CompanyListDto`: Simplified DTO for displaying companies in lists

### Commands
- `CreateCompany`: Creates a new company in the system
- `UpdateCompany`: Updates an existing company's properties
- `DeleteCompany`: Soft-deletes a company by setting its Active property to false

### Queries
- `GetCompanyById`: Retrieves a specific company by its unique identifier
- `GetAllCompanies`: Retrieves all active companies in the system

### Validators
- Ensures all company data meets the required validation rules using FluentValidation

## Implementation Details

### Soft Delete Pattern
- All delete operations are implemented as soft deletes by setting the `Active` property to `false`
- All queries automatically filter by `Active = 1` to exclude soft-deleted records

### Mapping
- AutoMapper profiles are used to map between entities and DTOs
- The mapping configuration handles the relationship between the entity's `Id` and `CompanyId` properties

## Usage Examples

### Creating a Company
```csharp
var command = new CreateCompanyCommand
{
    Name = "Contoso Ltd.",
    Description = "A global technology company",
    AccountTypeId = Guid.Parse("account-type-id"),
    AccountStatusId = Guid.Parse("account-status-id"),
    Website = "https://contoso.com",
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await mediator.Send(command);
```

### Retrieving a Company
```csharp
var query = new GetCompanyByIdQuery(companyId);
var company = await mediator.Send(query);
```

### Updating a Company
```csharp
var command = new UpdateCompanyCommand
{
    Id = companyId,
    Name = "Contoso Corporation",
    Description = "A global technology and services corporation",
    AccountTypeId = Guid.Parse("account-type-id"),
    AccountStatusId = Guid.Parse("account-status-id"),
    Website = "https://contoso.com",
    ModifiedBy = currentUserId
};

var result = await mediator.Send(command);
```

### Deleting a Company
```csharp
var command = new DeleteCompanyCommand(companyId, currentUserId);
var success = await mediator.Send(command);
```

### Retrieving All Companies
```csharp
var query = new GetAllCompaniesQuery();
var companies = await mediator.Send(query);
```

### Searching Companies by Name
```csharp
// Using the repository directly
var companies = await companyRepository.SearchByNameAsync("Contoso");
```

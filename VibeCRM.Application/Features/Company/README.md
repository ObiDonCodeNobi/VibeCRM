# Company Feature

## Overview
The Company feature provides functionality for managing company records within the VibeCRM system. Companies are core business entities that can be associated with people, activities, calls, and other entities. This feature follows the CQRS pattern with MediatR for command and query separation.

## Domain Model
The Company entity is a core business entity that represents an organization in the CRM system. Each Company has the following properties:

- **CompanyId**: Unique identifier (UUID)
- **Name**: Company name
- **Description**: Detailed description of the company
- **AccountTypeId**: Reference to the account type (e.g., Customer, Partner, Vendor)
- **AccountStatusId**: Reference to the account status (e.g., Active, Inactive, Prospect)
- **Website**: Company website URL
- **LogoPath**: Path to the company logo image
- **AnnualRevenue**: Annual revenue amount
- **EmployeeCount**: Number of employees
- **YearFounded**: Year the company was founded
- **Industry**: Industry the company operates in
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **AccountType**: Navigation property to the associated AccountType
- **AccountStatus**: Navigation property to the associated AccountStatus
- **Addresses**: Collection of associated Address entities
- **Contacts**: Collection of associated Person entities
- **Activities**: Collection of associated Activity entities
- **Attachments**: Collection of associated Attachment entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **CompanyDto**: Base DTO with core properties
- **CompanyDetailsDto**: Extended DTO with audit fields and related data
- **CompanyListDto**: Optimized DTO for list views

### Commands
- **CreateCompany**: Creates a new company
- **UpdateCompany**: Updates an existing company
- **DeleteCompany**: Soft-deletes a company by setting Active = false
- **AssignCompanyToUser**: Assigns a company to a specific user
- **ChangeCompanyStatus**: Changes the status of a company
- **ChangeCompanyType**: Changes the type of a company

### Queries
- **GetAllCompanies**: Retrieves all active companies
- **GetCompanyById**: Retrieves a specific company by its ID
- **GetCompaniesByStatus**: Retrieves companies filtered by status
- **GetCompaniesByType**: Retrieves companies filtered by type
- **GetCompaniesByUser**: Retrieves companies assigned to a specific user
- **SearchCompanies**: Searches companies by name, description, or other criteria

### Validators
- **CompanyDtoValidator**: Validates the base DTO
- **CompanyDetailsDtoValidator**: Validates the detailed DTO
- **CompanyListDtoValidator**: Validates the list DTO
- **CreateCompanyCommandValidator**: Validates the create command
- **UpdateCompanyCommandValidator**: Validates the update command
- **DeleteCompanyCommandValidator**: Validates the delete command
- **AssignCompanyToUserCommandValidator**: Validates the assign command
- **ChangeCompanyStatusCommandValidator**: Validates the status change command
- **ChangeCompanyTypeCommandValidator**: Validates the type change command
- **GetCompanyByIdQueryValidator**: Validates the ID query
- **GetCompaniesByStatusQueryValidator**: Validates the status query
- **GetCompaniesByTypeQueryValidator**: Validates the type query
- **GetCompaniesByUserQueryValidator**: Validates the user query
- **SearchCompaniesQueryValidator**: Validates the search query

### Mappings
- **CompanyMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Company
```csharp
var command = new CreateCompanyCommand
{
    Name = "Contoso Ltd.",
    Description = "A global technology company",
    AccountTypeId = accountTypeId,
    AccountStatusId = accountStatusId,
    Website = "https://contoso.com",
    Industry = "Technology",
    AnnualRevenue = 10000000,
    EmployeeCount = 500,
    YearFounded = 2005,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Companies
```csharp
var query = new GetAllCompaniesQuery();
var companies = await _mediator.Send(query);
```

### Retrieving a Company by ID
```csharp
var query = new GetCompanyByIdQuery { Id = companyId };
var company = await _mediator.Send(query);
```

### Searching Companies
```csharp
var query = new SearchCompaniesQuery { SearchTerm = "Contoso" };
var companies = await _mediator.Send(query);
```

### Changing a Company's Status
```csharp
var command = new ChangeCompanyStatusCommand
{
    Id = companyId,
    AccountStatusId = newStatusId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Company
```csharp
var command = new UpdateCompanyCommand
{
    Id = companyId,
    Name = "Contoso Corporation",
    Description = "A global technology and services corporation",
    AccountTypeId = accountTypeId,
    AccountStatusId = accountStatusId,
    Website = "https://contoso.com",
    Industry = "Technology",
    AnnualRevenue = 12000000,
    EmployeeCount = 550,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deleting a Company
```csharp
var command = new DeleteCompanyCommand
{
    Id = companyId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Company feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Name is required and limited to 200 characters
- Description is optional and limited to 1000 characters
- Website is optional but must be a valid URL format if provided
- AccountTypeId must reference a valid account type
- AccountStatusId must reference a valid account status
- AnnualRevenue must be a non-negative number
- EmployeeCount must be a non-negative number
- YearFounded must be a valid year (not in the future)
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Related Entities
- Companies can have multiple addresses, with one designated as primary
- Companies can have multiple contacts (people), with relationships defined
- Activities can be associated with companies for tracking interactions
- Attachments can be linked to companies for storing related documents

### Duplicate Detection
- The system checks for potential duplicate companies based on name similarity
- When creating or updating a company, potential duplicates are flagged
- Users can choose to proceed with creation or merge with an existing record

# Service Feature

## Overview
The Service feature provides functionality for managing services offered to customers in the VibeCRM system. It allows for creating, retrieving, updating, and soft-deleting service records that represent billable services provided to clients.

## Domain Model
The Service entity is a core business entity that represents a billable service in the CRM system. Each Service has the following properties:

- **ServiceId**: Unique identifier (UUID)
- **ServiceTypeId**: Reference to the service type
- **Name**: The name of the service
- **Description**: Detailed description of the service
- **Rate**: Standard hourly or unit rate for the service
- **IsTaxable**: Boolean flag indicating if the service is taxable
- **StandardTaxRate**: Default tax rate percentage if taxable
- **IsActive**: Boolean flag indicating if the service is active for sale
- **Active**: Boolean flag for soft delete functionality (true = active, false = deleted)
- **ServiceType**: Navigation property to the associated ServiceType
- **SalesOrderLineItems**: Collection of associated SalesOrderLineItem entities
- **QuoteLineItems**: Collection of associated QuoteLineItem entities

## Feature Components

### DTOs
DTOs for this feature are located in the VibeCRM.Shared project for integration with the frontend:
- **ServiceDto**: Base DTO with core properties
- **ServiceDetailsDto**: Extended DTO with audit fields and related data
- **ServiceListDto**: Optimized DTO for list views

### Commands
- **CreateService**: Creates a new service
- **UpdateService**: Updates an existing service
- **DeleteService**: Soft-deletes a service by setting Active = false
- **ActivateService**: Sets a service as active for sale
- **DeactivateService**: Sets a service as inactive for sale
- **UpdateServiceRate**: Updates the rate information for a service

### Queries
- **GetAllServices**: Retrieves all active services
- **GetServiceById**: Retrieves a specific service by its ID
- **GetServicesByType**: Retrieves services of a specific service type
- **GetActiveServices**: Retrieves only active services
- **GetInactiveServices**: Retrieves only inactive services
- **GetTaxableServices**: Retrieves services that are taxable

### Validators
- **ServiceDtoValidator**: Validates the base DTO
- **ServiceDetailsDtoValidator**: Validates the detailed DTO
- **ServiceListDtoValidator**: Validates the list DTO
- **CreateServiceCommandValidator**: Validates the create command
- **UpdateServiceCommandValidator**: Validates the update command
- **DeleteServiceCommandValidator**: Validates the delete command
- **ActivateServiceCommandValidator**: Validates the activate command
- **DeactivateServiceCommandValidator**: Validates the deactivate command
- **UpdateServiceRateCommandValidator**: Validates the update rate command
- **GetServiceByIdQueryValidator**: Validates the ID query
- **GetServicesByTypeQueryValidator**: Validates the type query

### Mappings
- **ServiceMappingProfile**: AutoMapper profile for mapping between entities and DTOs

## Usage Examples

### Creating a new Service
```csharp
var command = new CreateServiceCommand
{
    ServiceTypeId = serviceTypeId,
    Name = "Software Development",
    Description = "Custom software development services",
    Rate = 150.00m,
    IsTaxable = true,
    StandardTaxRate = 8.25m,
    IsActive = true,
    CreatedBy = currentUserId,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Retrieving all Services
```csharp
var query = new GetAllServicesQuery();
var services = await _mediator.Send(query);
```

### Retrieving a Service by ID
```csharp
var query = new GetServiceByIdQuery { Id = serviceId };
var service = await _mediator.Send(query);
```

### Retrieving Services by Type
```csharp
var query = new GetServicesByTypeQuery { ServiceTypeId = serviceTypeId };
var services = await _mediator.Send(query);
```

### Updating a Service's Rate
```csharp
var command = new UpdateServiceRateCommand
{
    Id = serviceId,
    Rate = 175.00m,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Updating a Service
```csharp
var command = new UpdateServiceCommand
{
    Id = serviceId,
    Name = "Advanced Software Development",
    Description = "Enterprise-level custom software development services",
    IsTaxable = true,
    StandardTaxRate = 9.0m,
    ModifiedBy = currentUserId
};

var result = await _mediator.Send(command);
```

### Deactivating a Service
```csharp
var command = new DeactivateServiceCommand
{
    Id = serviceId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

### Deleting a Service
```csharp
var command = new DeleteServiceCommand
{
    Id = serviceId,
    ModifiedBy = currentUserId
};

var success = await _mediator.Send(command);
```

## Implementation Notes

### Soft Delete Pattern
The Service feature implements the standard VibeCRM soft delete pattern:
- Uses the `Active` property (not `IsDeleted`) for soft delete functionality
- All queries filter by `Active = 1` to exclude soft-deleted records
- The `DeleteAsync` method sets `Active = 0` rather than physically removing records

### Validation Rules
- Name is required and limited to 100 characters
- Name must be unique
- Description is optional but limited to 500 characters
- ServiceTypeId must reference a valid service type
- Rate must be non-negative
- StandardTaxRate must be between 0 and 100 if IsTaxable is true
- Audit fields (CreatedBy, ModifiedBy) are required for commands

### Service Activation
- The system distinguishes between soft-deleted services (`Active = false`) and inactive services (`IsActive = false`)
- Inactive services still exist in the system but are not available for selection in new quotes or sales orders
- This allows for temporarily removing services from availability without losing historical data

### Rate Management
- The system maintains the standard rate for each service
- Historical rates are preserved for existing quotes and sales orders
- Rate changes only affect new quotes and sales orders
- The system can generate reports showing rate changes over time

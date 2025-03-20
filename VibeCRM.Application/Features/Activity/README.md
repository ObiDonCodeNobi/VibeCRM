# Activity Feature

## Overview
The Activity feature manages the operations related to activities within the VibeCRM system. It includes functionality for creating, updating, deleting, and retrieving activities.

## Components
- **Commands**: Handles operations for creating, updating, and deleting activities.
  - CreateActivityCommand
  - UpdateActivityCommand
  - DeleteActivityCommand
- **DTOs**: Data Transfer Objects for transferring activity data.
  - ActivityDto
  - ActivityDetailsDto
  - ActivityListDto
- **Mappings**: AutoMapper profiles for mapping between entities and DTOs.
  - ActivityMappingProfile
- **Queries**: Handles data retrieval operations.
  - GetAllActivitiesQuery
  - GetActivityByIdQuery
- **Validators**: Validates data integrity and business rules.
  - ActivityDtoValidator
  - ActivityDetailsDtoValidator
  - ActivityListDtoValidator

## Business Rules
1. Activities must have a unique identifier.
2. Activities should be soft-deleted using the `Active` property.
3. All operations must adhere to the CQRS pattern.

## Dependencies
- MediatR for handling commands and queries.
- AutoMapper for object mapping.
- FluentValidation for data validation.

## Usage Examples
### Creating an Activity
```csharp
var command = new CreateActivityCommand { /* properties */ };
var result = await mediator.Send(command);
```

### Retrieving Activities
```csharp
var query = new GetAllActivitiesQuery();
var activities = await mediator.Send(query);
```

## API Endpoints
- POST /api/activities - Create a new activity
- GET /api/activities - Retrieve all activities
- GET /api/activities/{id} - Retrieve an activity by ID

## Database Schema
Activities are stored in the database with the following schema:
- Id (UUID)
- Name (string)
- Description (string)
- Active (boolean)
- CreatedDate (DateTime)
- ModifiedDate (DateTime)

# VibeCRM TODO List

This document tracks planned features and improvements for future implementation in the VibeCRM system.

## Planned Features

### 1. Notification System

**Priority:** High  
**Status:** Planned  
**Target Release:** TBD  

#### Description
Implement a comprehensive notification system to alert users about important events related to activities, contacts, opportunities, and other entities in the CRM system.

#### Benefits
- Increases user engagement and timely response to critical activities
- Improves follow-up rates and reduces missed opportunities
- Provides a centralized way to keep users informed of changes
- Cross-cutting functionality that can be leveraged by multiple features

#### Components

1. **Notification Types and Templates**
   - System-defined notification types with customizable templates
   - Support for variables/placeholders in templates
   - Priority levels (Normal, High, Urgent)

2. **Notification Delivery Channels**
   - In-app notifications with a notification center UI
   - Email notifications
   - Potential SMS notifications (future)

3. **User Preferences**
   - Per-user, per-notification type settings
   - Channel selection (receive via in-app, email, or both)
   - Frequency settings (immediate, digest)

4. **Notification Center UI**
   - Dedicated page to view all notifications
   - Mark as read/unread functionality
   - Filtering and sorting options
   - Delete/archive capabilities

5. **Integration with Existing Features**
   - Activity due date reminders
   - Activity assignments
   - Activity status changes
   - Follow-up reminders

#### Database Schema Additions

**1. NotificationType Table**
```sql
CREATE TABLE dbo.NotificationType (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    TemplateSubject NVARCHAR(200) NOT NULL,
    TemplateBody NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DisplayOrder INT NOT NULL DEFAULT 0,
    CreatedOn DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedById UNIQUEIDENTIFIER NOT NULL,
    ModifiedOn DATETIME2 NULL,
    ModifiedById UNIQUEIDENTIFIER NULL
)
```

**2. Notification Table**
```sql
CREATE TABLE dbo.Notification (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    NotificationTypeId UNIQUEIDENTIFIER NOT NULL,
    Subject NVARCHAR(200) NOT NULL,
    Body NVARCHAR(MAX) NOT NULL,
    RelatedEntityId UNIQUEIDENTIFIER NULL,
    RelatedEntityType NVARCHAR(50) NULL,
    IsRead BIT NOT NULL DEFAULT 0,
    IsSystem BIT NOT NULL DEFAULT 0,
    Priority INT NOT NULL DEFAULT 0, -- 0=Normal, 1=High, etc.
    CreatedOn DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ExpiresOn DATETIME2 NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedById UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Notification_NotificationType FOREIGN KEY (NotificationTypeId) 
        REFERENCES dbo.NotificationType(Id)
)
```

**3. NotificationRecipient Table**
```sql
CREATE TABLE dbo.NotificationRecipient (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    NotificationId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0,
    ReadOn DATETIME2 NULL,
    DeliveryChannels NVARCHAR(100) NOT NULL DEFAULT 'InApp', -- CSV: 'InApp,Email,SMS'
    DeliveryStatus NVARCHAR(50) NULL, -- Pending, Delivered, Failed
    DeliveryAttempts INT NOT NULL DEFAULT 0,
    LastDeliveryAttempt DATETIME2 NULL,
    CreatedOn DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedById UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_NotificationRecipient_Notification FOREIGN KEY (NotificationId) 
        REFERENCES dbo.Notification(Id)
)
```

**4. UserNotificationPreference Table**
```sql
CREATE TABLE dbo.UserNotificationPreference (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    NotificationTypeId UNIQUEIDENTIFIER NOT NULL,
    InAppEnabled BIT NOT NULL DEFAULT 1,
    EmailEnabled BIT NOT NULL DEFAULT 1,
    SMSEnabled BIT NOT NULL DEFAULT 0,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedOn DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedById UNIQUEIDENTIFIER NOT NULL,
    ModifiedOn DATETIME2 NULL,
    ModifiedById UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_UserNotificationPreference_NotificationType FOREIGN KEY (NotificationTypeId) 
        REFERENCES dbo.NotificationType(Id),
    CONSTRAINT UQ_UserNotificationPreference UNIQUE (UserId, NotificationTypeId)
)
```

#### Implementation Considerations
- Follow Observer pattern for notification event handling
- Use background services for notification processing and delivery
- Implement notification batching to avoid overwhelming users
- Consider real-time notifications via SignalR for in-app updates
- Ensure proper indexing for notification queries

---

## Technical Debt and Improvements

### 1. Repository Implementation Standardization

**Priority:** Medium  
**Status:** In Progress  

Standardize all repository implementations to follow the patterns defined in the Feature Implementation Guide:
- Fix any repositories not properly inheriting from RepositoryBase
- Ensure consistent parameter naming across all repository methods
- Add proper XML documentation to all repository methods
- Address warnings about method hiding (use override when appropriate)
- Fix nullability warnings to ensure consistent return types

### 2. WorkflowStep Entity Standardization

**Priority:** High  
**Status:** Planned  
**Target Completion:** Q2 2025

The WorkflowStep entity needs to be updated to align with the standardized soft delete implementation:
- Add explicit `Active` property to the WorkflowStep entity class with default value of `true`
- Ensure the property is properly documented with XML comments
- Verify that the WorkflowStepRepository is consistently using `Active = 1` in all SQL queries
- Confirm that the DeleteAsync method in the repository is setting `Active = 0` instead of performing a hard delete
- Update any related tests to reflect the standardized approach

This update is part of the ongoing effort to standardize the soft delete implementation across the VibeCRM repository using the `Active` property instead of `IsDeleted`.

#### Database Schema Update

The database schema needs to be updated to include the WorkflowSteps table with the standardized Active column:

```sql
CREATE TABLE [dbo].[WorkflowSteps] (
    [WorkflowStepsId] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
    [WorkflowId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](100) NOT NULL,
    [Description] [nvarchar](500) NULL,
    [StepOrder] [int] NOT NULL,
    [StatusId] [uniqueidentifier] NULL,
    [AssignedUserId] [uniqueidentifier] NULL,
    [AssignedTeamId] [uniqueidentifier] NULL,
    [IsCompleted] [bit] NOT NULL DEFAULT 0,
    [CompletedDate] [datetime2](7) NULL,
    [DueDate] [datetime2](7) NULL,
    [RequiresApproval] [bit] NOT NULL DEFAULT 0,
    [IsOptional] [bit] NOT NULL DEFAULT 0,
    [EstimatedDuration] [decimal](18, 2) NULL,
    [Prerequisites] [nvarchar](max) NULL,
    [Instructions] [nvarchar](max) NULL,
    [AllowedRoles] [nvarchar](max) NULL,
    [ValidationRules] [nvarchar](max) NULL,
    [IsAutomated] [bit] NOT NULL DEFAULT 0,
    [AutomationConfig] [nvarchar](max) NULL,
    [Active] [bit] NOT NULL DEFAULT 1,
    [CreatedDate] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
    [CreatedBy] [uniqueidentifier] NOT NULL,
    [ModifiedDate] [datetime2](7) NULL,
    [ModifiedBy] [uniqueidentifier] NULL,
    CONSTRAINT [PK_WorkflowSteps] PRIMARY KEY CLUSTERED ([WorkflowStepsId] ASC),
    CONSTRAINT [FK_WorkflowSteps_Workflows] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflows] ([Id]),
    CONSTRAINT [FK_WorkflowSteps_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id]),
    CONSTRAINT [FK_WorkflowSteps_Users_Assigned] FOREIGN KEY ([AssignedUserId]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_WorkflowSteps_Teams] FOREIGN KEY ([AssignedTeamId]) REFERENCES [dbo].[Teams] ([Id]),
    CONSTRAINT [FK_WorkflowSteps_Users_Created] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_WorkflowSteps_Users_Modified] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[Users] ([Id])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- Supporting tables for workflow step comments and attachments
CREATE TABLE [dbo].[WorkflowStepComments] (
    [WorkflowStepsId] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
    [WorkflowStepId] [uniqueidentifier] NOT NULL,
    [Comment] [nvarchar](max) NOT NULL,
    [Active] [bit] NOT NULL DEFAULT 1,
    [CreatedDate] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
    [CreatedBy] [uniqueidentifier] NOT NULL,
    [ModifiedDate] [datetime2](7) NULL,
    [ModifiedBy] [uniqueidentifier] NULL,
    CONSTRAINT [PK_WorkflowStepComments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkflowStepComments_WorkflowSteps] FOREIGN KEY ([WorkflowStepId]) REFERENCES [dbo].[WorkflowSteps] ([Id]),
    CONSTRAINT [FK_WorkflowStepComments_Users_Created] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_WorkflowStepComments_Users_Modified] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[Users] ([Id])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkflowStepAttachments] (
    [Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
    [WorkflowStepId] [uniqueidentifier] NOT NULL,
    [AttachmentId] [uniqueidentifier] NOT NULL,
    [Active] [bit] NOT NULL DEFAULT 1,
    [CreatedDate] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
    [CreatedBy] [uniqueidentifier] NOT NULL,
    [ModifiedDate] [datetime2](7) NULL,
    [ModifiedBy] [uniqueidentifier] NULL,
    CONSTRAINT [PK_WorkflowStepAttachments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkflowStepAttachments_WorkflowSteps] FOREIGN KEY ([WorkflowStepId]) REFERENCES [dbo].[WorkflowSteps] ([Id]),
    CONSTRAINT [FK_WorkflowStepAttachments_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_WorkflowStepAttachments_Users_Created] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_WorkflowStepAttachments_Users_Modified] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[Users] ([Id])
) ON [PRIMARY]
GO

-- Create indexes for better performance
CREATE NONCLUSTERED INDEX [IX_WorkflowSteps_WorkflowId] ON [dbo].[WorkflowSteps] ([WorkflowId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_WorkflowSteps_StatusId] ON [dbo].[WorkflowSteps] ([StatusId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_WorkflowSteps_AssignedUserId] ON [dbo].[WorkflowSteps] ([AssignedUserId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_WorkflowSteps_AssignedTeamId] ON [dbo].[WorkflowSteps] ([AssignedTeamId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_WorkflowSteps_Active] ON [dbo].[WorkflowSteps] ([Active] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_WorkflowStepComments_WorkflowStepId] ON [dbo].[WorkflowStepComments] ([WorkflowStepId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_WorkflowStepAttachments_WorkflowStepId] ON [dbo].[WorkflowStepAttachments] ([WorkflowStepId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_WorkflowStepAttachments_AttachmentId] ON [dbo].[WorkflowStepAttachments] ([AttachmentId] ASC)
GO
```

This schema includes:
1. The main `WorkflowSteps` table with all properties from the entity class
2. Supporting tables for comments and attachments
3. Appropriate foreign key constraints
4. Indexes for performance optimization
5. The standardized `Active` column with a default value of 1
6. Audit fields (CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)

### 3. Person_Quote Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Person_Quote entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Person_Quote entity class with default value of `true`
- Updated the PersonQuoteRepository to consistently use `Active = 1` in all SQL queries
- Modified the DeleteAsync method in the repository to set `Active = 0` instead of `IsDeleted = 1`
- Created a SQL script to update the database schema (Update_PersonQuote_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column in the Person_Quote table:

```sql
-- Add Active column to Person_Quote table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Person_Quote') AND name = 'Active')
BEGIN
    ALTER TABLE Person_Quote ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 1 for non-deleted records and Active = 0 for deleted records
    UPDATE Person_Quote SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END
```

This update is part of the ongoing effort to standardize the soft delete implementation across the VibeCRM repository using the `Active` property instead of `IsDeleted`.

### 4. Person_Call Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Person_Call entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Person_Call entity class with default value of `true`
- Updated the PersonCallRepository to consistently use `Active = 1` in all SQL queries instead of `IsDeleted = 0`
- Created a SQL script to update the database schema (Update_PersonCall_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column in the Person_Call table:

```sql
-- Add Active column to Person_Call table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Person_Call') AND name = 'Active')
BEGIN
    ALTER TABLE Person_Call ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 1 for non-deleted records and Active = 0 for deleted records
    UPDATE Person_Call SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END
```

This update continues the systematic effort to standardize the soft delete implementation across the VibeCRM repository using the `Active` property instead of `IsDeleted`.

### 5. Person_Address Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Person_Address entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Person_Address entity class with default value of `true`
- Updated the PersonAddressRepository to consistently use `Active = 1` in all SQL queries instead of `IsDeleted = 0`
- Created a SQL script to update the database schema (Update_PersonAddress_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column in the Person_Address table:

```sql
-- Add Active column to Person_Address table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Person_Address') AND name = 'Active')
BEGIN
    ALTER TABLE Person_Address ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 1 for non-deleted records and Active = 0 for deleted records
    UPDATE Person_Address SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END
```

This update continues the systematic effort to standardize the soft delete implementation across the VibeCRM repository using the `Active` property instead of `IsDeleted`.

### 6. Product_Category Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Product_Category entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Product_Category entity class with default value of `true`
- Updated the ProductCategoryRepository to consistently use `Active = 1` in all SQL queries instead of `IsDeleted = 0`
- Added an override of the DeleteAsync method to implement soft delete by setting `Active = 0`
- Created a SQL script to update the database schema (Update_ProductCategory_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column in the Product_Category table:

```sql
-- Add Active column to Product_Category table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Product_Category') AND name = 'Active')
BEGIN
    ALTER TABLE Product_Category ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 1 for non-deleted records and Active = 0 for deleted records
    UPDATE Product_Category SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END
```

### 7. Person_EmailAddress Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Person_EmailAddress entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Person_EmailAddress entity class with default value of `true`
- Updated the PersonEmailAddressRepository to consistently use `Active = 1` in all SQL queries instead of `IsDeleted = 0`
- Added an override of the DeleteAsync method to implement soft delete by setting `Active = 0`
- Created a SQL script to update the database schema (Update_PersonEmailAddress_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column in the Person_EmailAddress table:

```sql
-- Add Active column to Person_EmailAddress table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Person_EmailAddress') AND name = 'Active')
BEGIN
    ALTER TABLE Person_EmailAddress ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 1 for non-deleted records and Active = 0 for deleted records
    UPDATE Person_EmailAddress SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END
```

### 8. Person_Phone Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Person_Phone entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Person_Phone entity class with default value of `true`
- Updated the PersonPhoneRepository to consistently use `Active = 1` in all SQL queries instead of `IsDeleted = 0`
- Added an override of the DeleteAsync method to implement soft delete by setting `Active = 0`
- Created a SQL script to update the database schema (Update_PersonPhone_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column in the Person_Phone table:

```sql
-- Add Active column to Person_Phone table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Person_Phone') AND name = 'Active')
BEGIN
    ALTER TABLE Person_Phone ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 1 for non-deleted records and Active = 0 for deleted records
    UPDATE Person_Phone SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END
```

This update continues the systematic effort to standardize the soft delete implementation across the VibeCRM repository using the `Active` property instead of `IsDeleted`.

### 9. Person_SalesOrder Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Person_SalesOrder entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Person_SalesOrder entity class with default value of `true`
- Updated the PersonSalesOrderRepository to consistently use `Active = 1` in all SQL queries instead of `IsDeleted = 0`
- Added an override of the DeleteAsync method to implement soft delete by setting `Active = 0`
- Created a SQL script to update the database schema (Update_PersonSalesOrder_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column in the Person_SalesOrder table:

```sql
-- Add Active column to Person_SalesOrder table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Person_SalesOrder') AND name = 'Active')
BEGIN
    ALTER TABLE Person_SalesOrder ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 1 for non-deleted records and Active = 0 for deleted records
    UPDATE Person_SalesOrder SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END
```

### 10. Person_Workflow Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Person_Workflow entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Person_Workflow entity class with default value of `true`
- Updated the PersonWorkflowRepository to consistently use `Active = 1` in all SQL queries instead of `IsDeleted = 0`
- Added an override of the DeleteAsync method to implement soft delete by setting `Active = 0`
- Created a SQL script to update the database schema (Update_PersonWorkflow_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column in the Person_Workflow table:

```sql
-- Add Active column to Person_Workflow table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Person_Workflow') AND name = 'Active')
BEGIN
    ALTER TABLE Person_Workflow ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 1 for non-deleted records and Active = 0 for deleted records
    UPDATE Person_Workflow SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END
```

### 11. Invoice_Payment Entity Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The Invoice_Payment entity has been updated to align with the standardized soft delete implementation:
- Added explicit `Active` property to the Invoice_Payment entity class with default value of `true`
- Completely removed the `IsDeleted` property (not just marked as obsolete)
- Updated the InvoicePaymentRepository to consistently use `Active = 1` in all SQL queries
- Added an override of the DeleteAsync method to implement soft delete by setting `Active = 0`
- Created a SQL script to update the database schema (Update_InvoicePayment_AddActiveColumn.sql)

#### Database Schema Update

The database schema has been updated to include the Active column and remove the IsDeleted column from the Invoice_Payment table:

```sql
-- Add Active column to Invoice_Payment table if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Invoice_Payment') AND name = 'Active')
BEGIN
    ALTER TABLE Invoice_Payment ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 0 where IsDeleted = 1
    UPDATE Invoice_Payment SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END

-- Remove IsDeleted column if it exists
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Invoice_Payment') AND name = 'IsDeleted')
BEGIN
    ALTER TABLE Invoice_Payment DROP COLUMN IsDeleted;
END
```

This update represents a complete transition to the standardized soft delete approach, fully removing the legacy IsDeleted property.

### 12. WorkflowDefinitionStep Repository Standardization

**Priority:** High  
**Status:** Completed  
**Completion Date:** Current Date

The WorkflowDefinitionStepRepository has been updated to align with the standardized soft delete implementation:
- Updated all SQL queries to use `Active = 1` instead of `IsDeleted = 0` for both WorkflowStep and Workflow tables
- Created SQL scripts to update the database schema for both WorkflowStep and Workflow tables:
  - Added Active column if it doesn't exist
  - Updated Active values based on existing IsDeleted values
  - Removed the IsDeleted column completely

#### Database Schema Update

The database schema has been updated to include the Active column and remove the IsDeleted column from both the WorkflowStep and Workflow tables:

```sql
-- For WorkflowStep table
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('WorkflowStep') AND name = 'Active')
BEGIN
    ALTER TABLE WorkflowStep ADD Active BIT NOT NULL DEFAULT 1;
    
    -- Update existing records: set Active = 0 where IsDeleted = 1
    UPDATE WorkflowStep SET Active = CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END;
END

-- Remove IsDeleted column if it exists
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('WorkflowStep') AND name = 'IsDeleted')
BEGIN
    ALTER TABLE WorkflowStep DROP COLUMN IsDeleted;
END

-- Similar updates for Workflow table
```

This update represents a complete transition to the standardized soft delete approach, fully removing the legacy IsDeleted property from both tables.

### 13. Improve Unit Test Coverage

**Priority:** Medium  
**Status:** Planned  

Increase unit test coverage across all repositories and handlers:
- Create standardized test patterns for CQRS commands and queries
- Implement integration tests for repository implementations
- Add mocking for external dependencies in unit tests
- Create test data factories for common entity types

---

## Future Feature Ideas

- **Dashboard Customization**: Allow users to create custom dashboards with widgets
- **Email Integration**: Two-way email integration with activity tracking
- **Document Management**: Document storage, versioning, and sharing
- **Mobile App**: Native or PWA mobile application
- **AI-powered Insights**: Predictive analytics for sales opportunities
- **Multi-language Support**: Internationalization and localization

---

## Soft Delete Standardization

### Progress Summary
- Standardized soft delete across 43 repositories 
- Updated database schemas for 7 entities to add Active column and remove IsDeleted column
- All repositories now use Active = 1 in WHERE clauses and set Active = 0 for soft delete operations
- RepositoryBase.DeleteAsync method provides standardized implementation for all repositories

### Standardization Pattern
```csharp
// Entity property
public bool Active { get; set; } = true;

// Repository query filtering
WHERE entity.Active = 1

// Soft delete method
public async Task<bool> DeleteAsync(Guid id)
{
    UPDATE {TableName} 
    SET Active = 0, 
        ModifiedDate = @ModifiedDate, 
        ModifiedBy = @ModifiedBy
    WHERE Id = @Id AND Active = 1
}
```

### Completed
- Person_Phone: Added Active property, updated repository to use Active = 1 instead of IsDeleted = 0
- Person_Attachment: Added Active property, updated repository to use Active = 1 instead of IsDeleted = 0
- Person_SalesOrder: Added Active property, updated repository to use Active = 1 instead of IsDeleted = 0
- Person_Workflow: Added Active property, updated repository to use Active = 1 instead of IsDeleted = 0
- QuoteActivity: Updated repository to use Active = 1 instead of IsDeleted = 0
- QuoteAttachment: Already using Active = 1 in all SQL queries
- WorkflowDefinitionStep: Added Active property to entity, verified repository uses Active = 1 in queries
- WorkflowDefinition: Already using Active = 1 in all SQL queries
- Invoice_Payment: Added Active property, updated repository to use Active = 1 instead of IsDeleted = 0, and removed IsDeleted property
- WorkflowStep: Created SQL script to add Active column and remove IsDeleted column
- Workflow: Created SQL script to add Active column and remove IsDeleted column

### Pending
- Review all remaining junction entities and repositories for standardization
- Create entity framework migrations for database schema changes
- Create unit tests to verify soft delete functionality across all entities
- Update documentation to reflect standardized approach

### Next Steps
1. Complete review of all remaining repositories
2. Develop automated tests to validate soft delete behavior
3. Create migration plan for handling existing data
4. Update any remaining code that might reference IsDeleted directly

### Benefits
- Consistent API for all repositories
- Simplified queries
- Improved maintainability
- Standardized approach for all developers

### 6. Junction Entities Soft Delete Standardization

**Priority:** High  
**Status:** In Progress  
**Target Completion:** Q3 2025

#### Description
Junction entities in the VibeCRM system need to be updated to align with the standardized soft delete implementation. This involves adding the `Active` property to all junction entities that don't already have it, and ensuring that the associated repositories filter by `Active = 1` in all queries.

#### Assessment Results
- A comprehensive assessment of junction entities has been completed
- 14 entities already implement the standardized soft delete pattern with an `Active` property
- 40+ entities still need to be updated

#### Progress
- Created a comprehensive assessment document: `JunctionEntitiesSoftDeleteAssessment.md`
- Developed a template SQL migration script for junction tables
- Created a PowerShell script to generate migration scripts for all junction entities
- Developed a standardized junction entity class template with proper `Active` property implementation

#### Required Updates for Each Entity
1. **Entity Class Updates**:
   ```csharp
   /// <summary>
   /// Gets or sets a value indicating whether this record is active.
   /// When false, the record is considered soft-deleted.
   /// </summary>
   public bool Active { get; set; } = true;
   ```

2. **Repository Updates**:
   - Update all SQL queries to filter with `WHERE Active = 1` 
   - Ensure delete operations use `UPDATE {Table} SET Active = 0, ...`

3. **Database Schema Updates**:
   - Create SQL migration scripts for each entity to:
     - Add `Active` column with default value of 1
     - Update existing records if there is an `IsDeleted` column
     - Remove `IsDeleted` column if it exists

#### Next Steps
1. Execute the `Generate-JunctionMigrationScripts.ps1` script to create individual migration scripts
2. Systematically update each junction entity class to include the `Active` property
3. Update corresponding repositories to use the standardized soft delete pattern
4. Run the migration scripts to update the database schema
5. Verify functionality with comprehensive testing

This standardization effort is part of the ongoing initiative to maintain a consistent approach to soft delete functionality across the entire VibeCRM system.

### 7. Junction Entities Standardization Automation Tools

**Priority:** High  
**Status:** Completed  
**Completion Date:** 2025-03-02

#### Description
To streamline the soft delete standardization process for junction entities, a set of automation tools have been developed that allow for efficient and consistent implementation across all junction entities.

#### Tools Created

1. **Template_Junction_AddActiveColumn.sql**
   - Template SQL script for adding the `Active` column to junction tables
   - Handles migration from `IsDeleted` to `Active` pattern
   - Includes validation checks to prevent errors

2. **Generate-JunctionMigrationScripts.ps1**
   - PowerShell script that generates SQL migration scripts for multiple junction entities
   - Uses the template to create customized scripts for each entity
   - Automatically names scripts according to convention

3. **Create-JunctionRepositoryFiles.ps1**
   - Creates repository interface and implementation files for junction entities
   - Generates properly structured code with appropriate methods
   - Follows consistent naming and implementation patterns

4. **Standardize-JunctionEntity.ps1**
   - Master script that orchestrates the entire standardization process
   - Can generate migration scripts, repository files, and execute migrations
   - Includes verification of entity class properties
   - Provides detailed reporting on standardization status

#### Usage

The automation tools can be used in several ways:

```powershell
# Complete standardization with WhatIf mode (no changes made)
.\Standardize-JunctionEntity.ps1 -JunctionEntityName Entity1_Entity2 -CreateRepositoryFiles -ExecuteMigration -WhatIf

# Generate migration script only
.\Standardize-JunctionEntity.ps1 -JunctionEntityName Entity1_Entity2

# Create repository files only
.\Standardize-JunctionEntity.ps1 -JunctionEntityName Entity1_Entity2 -CreateRepositoryFiles

# Execute standardization for a specific junction entity
.\Standardize-JunctionEntity.ps1 -JunctionEntityName Entity1_Entity2 -CreateRepositoryFiles -ExecuteMigration
```

#### Benefits

- Consistent implementation across all junction entities
- Significant time savings in standardization process
- Reduced risk of human error
- Comprehensive verification and reporting
- Allows for selective execution of specific standardization steps

These tools help ensure that the soft delete standardization is applied consistently and efficiently across all junction entities in the VibeCRM system.

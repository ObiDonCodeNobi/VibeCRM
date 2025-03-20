-- SQL Script to add Active column to all junction tables
-- This script adds the Active bit column defaulting to 1 (true) to all junction tables
-- The Active column is used for soft delete functionality

-- Company_Activity
ALTER TABLE [dbo].[Company_Activity]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_Address
ALTER TABLE [dbo].[Company_Address]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_Attachment
ALTER TABLE [dbo].[Company_Attachment]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_Call
ALTER TABLE [dbo].[Company_Call]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_EmailAddress
ALTER TABLE [dbo].[Company_EmailAddress]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_Note
ALTER TABLE [dbo].[Company_Note]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_Person
ALTER TABLE [dbo].[Company_Person]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_Phone
ALTER TABLE [dbo].[Company_Phone]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_Quote
ALTER TABLE [dbo].[Company_Quote]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Company_SalesOrder
ALTER TABLE [dbo].[Company_SalesOrder]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Person_Activity
ALTER TABLE [dbo].[Person_Activity]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Person_Address
ALTER TABLE [dbo].[Person_Address]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Person_Attachment
ALTER TABLE [dbo].[Person_Attachment]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Person_Call
ALTER TABLE [dbo].[Person_Call]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Person_EmailAddress
ALTER TABLE [dbo].[Person_EmailAddress]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Person_Note
ALTER TABLE [dbo].[Person_Note]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Person_Phone
ALTER TABLE [dbo].[Person_Phone]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Quote_Activity
ALTER TABLE [dbo].[Quote_Activity]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- SalesOrder_Activity
ALTER TABLE [dbo].[SalesOrder_Activity]
ADD [Active] BIT NOT NULL DEFAULT 1;
GO

-- Add ModifiedDate column to all junction tables for tracking when soft delete occurs
-- Company_Activity
ALTER TABLE [dbo].[Company_Activity]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_Address
ALTER TABLE [dbo].[Company_Address]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_Attachment
ALTER TABLE [dbo].[Company_Attachment]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_Call
ALTER TABLE [dbo].[Company_Call]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_EmailAddress
ALTER TABLE [dbo].[Company_EmailAddress]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_Note
ALTER TABLE [dbo].[Company_Note]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_Person
ALTER TABLE [dbo].[Company_Person]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_Phone
ALTER TABLE [dbo].[Company_Phone]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_Quote
ALTER TABLE [dbo].[Company_Quote]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Company_SalesOrder
ALTER TABLE [dbo].[Company_SalesOrder]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Person_Activity
ALTER TABLE [dbo].[Person_Activity]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Person_Address
ALTER TABLE [dbo].[Person_Address]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Person_Attachment
ALTER TABLE [dbo].[Person_Attachment]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Person_Call
ALTER TABLE [dbo].[Person_Call]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Person_EmailAddress
ALTER TABLE [dbo].[Person_EmailAddress]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Person_Note
ALTER TABLE [dbo].[Person_Note]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Person_Phone
ALTER TABLE [dbo].[Person_Phone]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- Quote_Activity
ALTER TABLE [dbo].[Quote_Activity]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

-- SalesOrder_Activity
ALTER TABLE [dbo].[SalesOrder_Activity]
ADD [ModifiedDate] DATETIME2(7) NULL;
GO

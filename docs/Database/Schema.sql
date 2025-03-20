
CREATE TABLE [dbo].[AccountStatus](
	[AccountStatusId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_AccountStatus] PRIMARY KEY CLUSTERED 
(
	[AccountStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountType](
	[AccountTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[AccountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[ActivityId] [uniqueidentifier] NOT NULL,
	[ActivityTypeId] [uniqueidentifier] NOT NULL,
	[ActivityStatusId] [uniqueidentifier] NOT NULL,
	[AssignedUserId] [uniqueidentifier] NULL,
	[AssignedTeamId] [uniqueidentifier] NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[DueDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[CompletedDate] [datetime] NULL,
	[CompletedBy] [uniqueidentifier] NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity_Attachment]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_Attachment](
	[ActivityId] [uniqueidentifier] NOT NULL,
	[AttachmentId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity_Note]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_Note](
	[ActivityId] [uniqueidentifier] NOT NULL,
	[NoteId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityDefinition]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityDefinition](
	[ActivityDefinitionId] [uniqueidentifier] NOT NULL,
	[ActivityTypeId] [uniqueidentifier] NOT NULL,
	[ActivityStatusId] [uniqueidentifier] NOT NULL,
	[AssignedUserId] [uniqueidentifier] NOT NULL,
	[AssignedTeamId] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[DueDateOffset] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ActivityDefinition] PRIMARY KEY CLUSTERED 
(
	[ActivityDefinitionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityStatus]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityStatus](
	[ActivityStatusId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ActivityStatus] PRIMARY KEY CLUSTERED 
(
	[ActivityStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityType](
	[ActivityTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ActivityType] PRIMARY KEY CLUSTERED 
(
	[ActivityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressId] [uniqueidentifier] NOT NULL,
	[AddressTypeId] [uniqueidentifier] NOT NULL,
	[Line1] [nvarchar](255) NOT NULL,
	[Line2] [nvarchar](255) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[StateId] [uniqueidentifier] NOT NULL,
	[Zip] [nvarchar](15) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddressType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressType](
	[AddressTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED 
(
	[AddressTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachment](
	[AttachmentId] [uniqueidentifier] NOT NULL,
	[AttachmentTypeId] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Path] [nvarchar](255) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[AttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttachmentType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttachmentType](
	[AttachmentTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_AttachmentType] PRIMARY KEY CLUSTERED 
(
	[AttachmentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Call]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Call](
	[CallId] [uniqueidentifier] NOT NULL,
	[TypeId] [uniqueidentifier] NOT NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[DirectionId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Duration] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Call] PRIMARY KEY CLUSTERED 
(
	[CallId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CallDirection]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CallDirection](
	[CallDirectionId] [uniqueidentifier] NOT NULL,
	[Direction] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CallDirection] PRIMARY KEY CLUSTERED 
(
	[CallDirectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CallStatus]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CallStatus](
	[CallStatusId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CallStatus] PRIMARY KEY CLUSTERED 
(
	[CallStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CallType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CallType](
	[CallTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CallType] PRIMARY KEY CLUSTERED 
(
	[CallTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[ParentCompanyId] [uniqueidentifier] NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[AccountTypeId] [uniqueidentifier] NOT NULL,
	[AccountStatusId] [uniqueidentifier] NOT NULL,
	[PrimaryContactId] [uniqueidentifier] NOT NULL,
	[PrimaryPhoneId] [uniqueidentifier] NOT NULL,
	[PrimaryAddressId] [uniqueidentifier] NOT NULL,
	[Website] [nvarchar](255) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Activity]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Activity](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Address]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Address](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[AddressId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Attachment]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Attachment](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[AttachmentId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Call]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Call](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[CallId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_EmailAddress]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_EmailAddress](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[EmailAddressId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Note]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Note](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[NoteId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Person]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Person](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Phone]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Phone](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[PhoneId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Quote]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Quote](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[QuoteId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_SalesOrder]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_SalesOrder](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[SalesOrderId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[ManagerEmployeeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailAddress]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailAddress](
	[EmailAddressId] [uniqueidentifier] NOT NULL,
	[EmailAddressTypeId] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_EmailAddress] PRIMARY KEY CLUSTERED 
(
	[EmailAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailAddressType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailAddressType](
	[EmailAddressTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_EmailAddressType] PRIMARY KEY CLUSTERED 
(
	[EmailAddressTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[ReportsTo] [uniqueidentifier] NOT NULL,
	[EmployeeNumber] [varchar](50) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[HireDate] [datetime] NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [uniqueidentifier] NOT NULL,
	[SalesOrderId] [uniqueidentifier] NULL,
	[Number] [nvarchar](50) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_Activity]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_Activity](
	[InvoiceId] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_InvoiceLineItem]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_InvoiceLineItem](
	[InvoiceId] [uniqueidentifier] NOT NULL,
	[InvoiceLineItemId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceLineItem]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceLineItem](
	[InvoiceLineItemId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NULL,
	[ServiceId] [uniqueidentifier] NULL,
	[UnitPrice] [decimal](19, 4) NOT NULL,
	[TaxCodeId] [uniqueidentifier] NOT NULL,
	[TaxPercentage] [decimal](18, 2) NOT NULL,
	[UnitTotal] [decimal](19, 4) NOT NULL,
	[LineItemTotal] [decimal](19, 4) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_InvoiceLineItem] PRIMARY KEY CLUSTERED 
(
	[InvoiceLineItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lead]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lead](
	[LeadId] [uniqueidentifier] NOT NULL,
	[LeadStatusId] [uniqueidentifier] NOT NULL,
	[LeadSourceId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
(
	[LeadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeadSource]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeadSource](
	[LeadSourceId] [uniqueidentifier] NOT NULL,
	[Level] [int] NOT NULL,
	[Source] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_LeadSource] PRIMARY KEY CLUSTERED 
(
	[LeadSourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeadStatus]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeadStatus](
	[LeadStatusId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_LeadStatus] PRIMARY KEY CLUSTERED 
(
	[LeadStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Note]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Note](
	[NoteId] [uniqueidentifier] NOT NULL,
	[NoteTypeId] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](255) NOT NULL,
	[NoteText] [nvarchar](max) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteType](
	[NoteTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_NoteType] PRIMARY KEY CLUSTERED 
(
	[NoteTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [uniqueidentifier] NOT NULL,
	[InvoiceId] [uniqueidentifier] NOT NULL,
	[PaymentMethodId] [uniqueidentifier] NOT NULL,
	[PaymentStatusId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentLineItem]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentLineItem](
	[PaymentLineItemId] [uniqueidentifier] NOT NULL,
	[InvoiceLineItemId] [uniqueidentifier] NULL,
	[PaymentId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PaymentLineItem] PRIMARY KEY CLUSTERED 
(
	[PaymentLineItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[PaymentMethodId] [uniqueidentifier] NOT NULL,
	[Method] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED 
(
	[PaymentMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentStatus]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentStatus](
	[PaymentStatusId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED 
(
	[PaymentStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonId] [uniqueidentifier] NOT NULL,
	[Firstname] [nvarchar](50) NOT NULL,
	[MiddleInitial] [nvarchar](1) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_Activity]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_Activity](
	[PersonId] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_Address]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_Address](
	[PersonId] [uniqueidentifier] NOT NULL,
	[AddressId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_Attachment]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_Attachment](
	[PersonId] [uniqueidentifier] NOT NULL,
	[AttachmentId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_Call]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_Call](
	[PersonId] [uniqueidentifier] NOT NULL,
	[CallId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_EmailAddress]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_EmailAddress](
	[PersonId] [uniqueidentifier] NOT NULL,
	[EmailAddressId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_Note]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_Note](
	[PersonId] [uniqueidentifier] NOT NULL,
	[NoteId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_Phone]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_Phone](
	[PersonId] [uniqueidentifier] NOT NULL,
	[PhoneId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_Quote]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_Quote](
	[PersonId] [uniqueidentifier] NOT NULL,
	[QuoteId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_SalesOrder]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_SalesOrder](
	[PersonId] [uniqueidentifier] NOT NULL,
	[SalesOrderId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phone](
	[PhoneId] [uniqueidentifier] NOT NULL,
	[AreaCode] [int] NOT NULL,
	[Prefix] [int] NOT NULL,
	[LineNumber] [int] NOT NULL,
	[Extension] [int] NULL,
	[PhoneTypeId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED 
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneType](
	[PhoneTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_PhoneType] PRIMARY KEY CLUSTERED 
(
	[PhoneTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductTypeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductGroup]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductGroup](
	[ProductGroupId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ProductGroup] PRIMARY KEY CLUSTERED 
(
	[ProductGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductGroup_Product]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductGroup_Product](
	[ProductGroupId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[ProductTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[ProductTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quote]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quote](
	[QuoteId] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Quote] PRIMARY KEY CLUSTERED 
(
	[QuoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quote_Activity]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quote_Activity](
	[QuoteId] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quote_QuoteLineItem]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quote_QuoteLineItem](
	[QuoteId] [uniqueidentifier] NOT NULL,
	[QuoteLineItemId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteLineItem]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteLineItem](
	[QuoteLineItemId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_QuoteLineItem] PRIMARY KEY CLUSTERED 
(
	[QuoteLineItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrder]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder](
	[SalesOrderId] [uniqueidentifier] NOT NULL,
	[SalesOrderStatusId] [uniqueidentifier] NOT NULL,
	[ShipMethodId] [uniqueidentifier] NOT NULL,
	[BillToAddressId] [uniqueidentifier] NOT NULL,
	[ShipToAddressId] [uniqueidentifier] NOT NULL,
	[TaxCodeId] [uniqueidentifier] NOT NULL,
	[QuoteId] [uniqueidentifier] NULL,
	[Number] [nvarchar](50) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[ShipDate] [datetime] NULL,
	[SubTotal] [decimal](19, 4) NOT NULL,
	[TaxAmount] [decimal](19, 4) NOT NULL,
	[DueAmount] [decimal](19, 4) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[SalesOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrder_Activity]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder_Activity](
	[SalesOrderId] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrder_SalesOrderLineItem]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder_SalesOrderLineItem](
	[SalesOrderId] [uniqueidentifier] NOT NULL,
	[SalesOrderLineItemId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderLineItem]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderLineItem](
	[SalesOrderLineItemId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesOrderLineItem] PRIMARY KEY CLUSTERED 
(
	[SalesOrderLineItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderLineItem_Product]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderLineItem_Product](
	[SalesOrderLineItemId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderLineItem_Service]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderLineItem_Service](
	[SalesOrderLineItemId] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderStatus]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderStatus](
	[SalesOrderStatusId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_SalesOrderStatus] PRIMARY KEY CLUSTERED 
(
	[SalesOrderStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceId] [uniqueidentifier] NOT NULL,
	[ServiceTypeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[ServiceTypeId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[ServiceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipMethod]    Script Date: 2/16/2025 1:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipMethod](
	[ShipMethodId] [uniqueidentifier] NOT NULL,
	[Method] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ShipMethod] PRIMARY KEY CLUSTERED 
(
	[ShipMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 2/16/2025 1:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [uniqueidentifier] NOT NULL,
	[Abbreviation] [nvarchar](2) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxCode]    Script Date: 2/16/2025 1:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxCode](
	[TaxCodeId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[StateId] [uniqueidentifier] NOT NULL,
	[RateCounty] [nvarchar](50) NULL,
	[RateZip] [nvarchar](12) NULL,
	[Rate] [decimal](4, 2) NOT NULL,
	[OrdinalPosition] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_TaxCode] PRIMARY KEY CLUSTERED 
(
	[TaxCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 2/16/2025 1:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[TeamId] [uniqueidentifier] NOT NULL,
	[TeamLeadEmployeeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team_Employee]    Script Date: 2/16/2025 1:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team_Employee](
	[TeamId] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/16/2025 1:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[LoginName] [varchar](255) NOT NULL,
	[LoginPassword] [varchar](255) NOT NULL,
	[LastLogin] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Role]    Script Date: 2/16/2025 1:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Role](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workflow]    Script Date: 2/16/2025 1:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workflow](
	[WorkflowId] [uniqueidentifier] NOT NULL,
	[WorkflowTypeId] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[StartDate] [datetime] NULL,
	[CompletedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Workflow] PRIMARY KEY CLUSTERED 
(
	[WorkflowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkflowDefinition]    Script Date: 2/16/2025 1:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkflowDefinition](
	[WorkflowDefinitionId] [uniqueidentifier] NOT NULL,
	[WorkflowTypeId] [uniqueidentifier] NOT NULL,
	[AssignedUserId] [uniqueidentifier] NOT NULL,
	[AssignedTeamId] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_WorkflowDefinition] PRIMARY KEY CLUSTERED 
(
	[WorkflowDefinitionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountStatus]  WITH CHECK ADD  CONSTRAINT [FK_AccountStatus_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[AccountStatus] CHECK CONSTRAINT [FK_AccountStatus_User]
GO
ALTER TABLE [dbo].[AccountStatus]  WITH CHECK ADD  CONSTRAINT [FK_AccountStatus_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[AccountStatus] CHECK CONSTRAINT [FK_AccountStatus_User1]
GO
ALTER TABLE [dbo].[AccountType]  WITH CHECK ADD  CONSTRAINT [FK_AccountType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[AccountType] CHECK CONSTRAINT [FK_AccountType_User]
GO
ALTER TABLE [dbo].[AccountType]  WITH CHECK ADD  CONSTRAINT [FK_AccountType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[AccountType] CHECK CONSTRAINT [FK_AccountType_User1]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_ActivityStatus] FOREIGN KEY([ActivityStatusId])
REFERENCES [dbo].[ActivityStatus] ([ActivityStatusId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_ActivityStatus]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_ActivityType] FOREIGN KEY([ActivityTypeId])
REFERENCES [dbo].[ActivityType] ([ActivityTypeId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_ActivityType]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Team] FOREIGN KEY([AssignedTeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_Team]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_User] FOREIGN KEY([AssignedUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_User]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_User1] FOREIGN KEY([AssignedUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_User1]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_User2] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_User2]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_User3] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_User3]
GO
ALTER TABLE [dbo].[Activity_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Attachment_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[Activity_Attachment] CHECK CONSTRAINT [FK_Activity_Attachment_Activity]
GO
ALTER TABLE [dbo].[Activity_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Attachment_Attachment] FOREIGN KEY([AttachmentId])
REFERENCES [dbo].[Attachment] ([AttachmentId])
GO
ALTER TABLE [dbo].[Activity_Attachment] CHECK CONSTRAINT [FK_Activity_Attachment_Attachment]
GO
ALTER TABLE [dbo].[Activity_Note]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Note_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[Activity_Note] CHECK CONSTRAINT [FK_Activity_Note_Activity]
GO
ALTER TABLE [dbo].[Activity_Note]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Note_Note] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([NoteId])
GO
ALTER TABLE [dbo].[Activity_Note] CHECK CONSTRAINT [FK_Activity_Note_Note]
GO
ALTER TABLE [dbo].[ActivityDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ActivityDefinition_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ActivityDefinition] CHECK CONSTRAINT [FK_ActivityDefinition_User]
GO
ALTER TABLE [dbo].[ActivityDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ActivityDefinition_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ActivityDefinition] CHECK CONSTRAINT [FK_ActivityDefinition_User1]
GO
ALTER TABLE [dbo].[ActivityStatus]  WITH CHECK ADD  CONSTRAINT [FK_ActivityStatus_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ActivityStatus] CHECK CONSTRAINT [FK_ActivityStatus_User]
GO
ALTER TABLE [dbo].[ActivityStatus]  WITH CHECK ADD  CONSTRAINT [FK_ActivityStatus_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ActivityStatus] CHECK CONSTRAINT [FK_ActivityStatus_User1]
GO
ALTER TABLE [dbo].[ActivityType]  WITH CHECK ADD  CONSTRAINT [FK_ActivityType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ActivityType] CHECK CONSTRAINT [FK_ActivityType_User]
GO
ALTER TABLE [dbo].[ActivityType]  WITH CHECK ADD  CONSTRAINT [FK_ActivityType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ActivityType] CHECK CONSTRAINT [FK_ActivityType_User1]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_AddressType] FOREIGN KEY([AddressTypeId])
REFERENCES [dbo].[AddressType] ([AddressTypeId])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_AddressType]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_State]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_User]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_User1]
GO
ALTER TABLE [dbo].[AddressType]  WITH CHECK ADD  CONSTRAINT [FK_AddressType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[AddressType] CHECK CONSTRAINT [FK_AddressType_User]
GO
ALTER TABLE [dbo].[AddressType]  WITH CHECK ADD  CONSTRAINT [FK_AddressType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[AddressType] CHECK CONSTRAINT [FK_AddressType_User1]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_AttachmentType] FOREIGN KEY([AttachmentTypeId])
REFERENCES [dbo].[AttachmentType] ([AttachmentTypeId])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_AttachmentType]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_User]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_User1]
GO
ALTER TABLE [dbo].[AttachmentType]  WITH CHECK ADD  CONSTRAINT [FK_AttachmentType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[AttachmentType] CHECK CONSTRAINT [FK_AttachmentType_User]
GO
ALTER TABLE [dbo].[AttachmentType]  WITH CHECK ADD  CONSTRAINT [FK_AttachmentType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[AttachmentType] CHECK CONSTRAINT [FK_AttachmentType_User1]
GO
ALTER TABLE [dbo].[Call]  WITH CHECK ADD  CONSTRAINT [FK_Call_CallDirection] FOREIGN KEY([DirectionId])
REFERENCES [dbo].[CallDirection] ([CallDirectionId])
GO
ALTER TABLE [dbo].[Call] CHECK CONSTRAINT [FK_Call_CallDirection]
GO
ALTER TABLE [dbo].[Call]  WITH CHECK ADD  CONSTRAINT [FK_Call_CallStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[CallStatus] ([CallStatusId])
GO
ALTER TABLE [dbo].[Call] CHECK CONSTRAINT [FK_Call_CallStatus]
GO
ALTER TABLE [dbo].[Call]  WITH CHECK ADD  CONSTRAINT [FK_Call_CallType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[CallType] ([CallTypeId])
GO
ALTER TABLE [dbo].[Call] CHECK CONSTRAINT [FK_Call_CallType]
GO
ALTER TABLE [dbo].[Call]  WITH CHECK ADD  CONSTRAINT [FK_Call_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Call] CHECK CONSTRAINT [FK_Call_User]
GO
ALTER TABLE [dbo].[Call]  WITH CHECK ADD  CONSTRAINT [FK_Call_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Call] CHECK CONSTRAINT [FK_Call_User1]
GO
ALTER TABLE [dbo].[CallDirection]  WITH CHECK ADD  CONSTRAINT [FK_CallDirection_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[CallDirection] CHECK CONSTRAINT [FK_CallDirection_User]
GO
ALTER TABLE [dbo].[CallDirection]  WITH CHECK ADD  CONSTRAINT [FK_CallDirection_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[CallDirection] CHECK CONSTRAINT [FK_CallDirection_User1]
GO
ALTER TABLE [dbo].[CallStatus]  WITH CHECK ADD  CONSTRAINT [FK_CallStatus_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[CallStatus] CHECK CONSTRAINT [FK_CallStatus_User]
GO
ALTER TABLE [dbo].[CallStatus]  WITH CHECK ADD  CONSTRAINT [FK_CallStatus_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[CallStatus] CHECK CONSTRAINT [FK_CallStatus_User1]
GO
ALTER TABLE [dbo].[CallType]  WITH CHECK ADD  CONSTRAINT [FK_CallType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[CallType] CHECK CONSTRAINT [FK_CallType_User]
GO
ALTER TABLE [dbo].[CallType]  WITH CHECK ADD  CONSTRAINT [FK_CallType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[CallType] CHECK CONSTRAINT [FK_CallType_User1]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_AccountStatus] FOREIGN KEY([AccountStatusId])
REFERENCES [dbo].[AccountStatus] ([AccountStatusId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_AccountStatus]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_AccountType] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[AccountType] ([AccountTypeId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_AccountType]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Address] FOREIGN KEY([PrimaryAddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Address]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Company] FOREIGN KEY([ParentCompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Company]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Person] FOREIGN KEY([PrimaryContactId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Person]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Phone] FOREIGN KEY([PrimaryPhoneId])
REFERENCES [dbo].[Phone] ([PhoneId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Phone]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_User]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_User1]
GO
ALTER TABLE [dbo].[Company_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Company_Activity_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[Company_Activity] CHECK CONSTRAINT [FK_Company_Activity_Activity]
GO
ALTER TABLE [dbo].[Company_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Company_Activity_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_Activity] CHECK CONSTRAINT [FK_Company_Activity_Company]
GO
ALTER TABLE [dbo].[Company_Address]  WITH CHECK ADD  CONSTRAINT [FK_Company_Address_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Company_Address] CHECK CONSTRAINT [FK_Company_Address_Address]
GO
ALTER TABLE [dbo].[Company_Address]  WITH CHECK ADD  CONSTRAINT [FK_Company_Address_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_Address] CHECK CONSTRAINT [FK_Company_Address_Company]
GO
ALTER TABLE [dbo].[Company_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Company_Attachment_Attachment] FOREIGN KEY([AttachmentId])
REFERENCES [dbo].[Attachment] ([AttachmentId])
GO
ALTER TABLE [dbo].[Company_Attachment] CHECK CONSTRAINT [FK_Company_Attachment_Attachment]
GO
ALTER TABLE [dbo].[Company_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Company_Attachment_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_Attachment] CHECK CONSTRAINT [FK_Company_Attachment_Company]
GO
ALTER TABLE [dbo].[Company_Call]  WITH CHECK ADD  CONSTRAINT [FK_Company_Call_Call] FOREIGN KEY([CallId])
REFERENCES [dbo].[Call] ([CallId])
GO
ALTER TABLE [dbo].[Company_Call] CHECK CONSTRAINT [FK_Company_Call_Call]
GO
ALTER TABLE [dbo].[Company_Call]  WITH CHECK ADD  CONSTRAINT [FK_Company_Call_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_Call] CHECK CONSTRAINT [FK_Company_Call_Company]
GO
ALTER TABLE [dbo].[Company_EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_Company_EmailAddress_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_EmailAddress] CHECK CONSTRAINT [FK_Company_EmailAddress_Company]
GO
ALTER TABLE [dbo].[Company_EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_Company_EmailAddress_EmailAddress] FOREIGN KEY([EmailAddressId])
REFERENCES [dbo].[EmailAddress] ([EmailAddressId])
GO
ALTER TABLE [dbo].[Company_EmailAddress] CHECK CONSTRAINT [FK_Company_EmailAddress_EmailAddress]
GO
ALTER TABLE [dbo].[Company_Note]  WITH CHECK ADD  CONSTRAINT [FK_Company_Note_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_Note] CHECK CONSTRAINT [FK_Company_Note_Company]
GO
ALTER TABLE [dbo].[Company_Note]  WITH CHECK ADD  CONSTRAINT [FK_Company_Note_Note] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([NoteId])
GO
ALTER TABLE [dbo].[Company_Note] CHECK CONSTRAINT [FK_Company_Note_Note]
GO
ALTER TABLE [dbo].[Company_Person]  WITH CHECK ADD  CONSTRAINT [FK_Company_Person_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_Person] CHECK CONSTRAINT [FK_Company_Person_Company]
GO
ALTER TABLE [dbo].[Company_Person]  WITH CHECK ADD  CONSTRAINT [FK_Company_Person_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Company_Person] CHECK CONSTRAINT [FK_Company_Person_Person]
GO
ALTER TABLE [dbo].[Company_Phone]  WITH CHECK ADD  CONSTRAINT [FK_Company_Phone_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_Phone] CHECK CONSTRAINT [FK_Company_Phone_Company]
GO
ALTER TABLE [dbo].[Company_Phone]  WITH CHECK ADD  CONSTRAINT [FK_Company_Phone_Phone] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phone] ([PhoneId])
GO
ALTER TABLE [dbo].[Company_Phone] CHECK CONSTRAINT [FK_Company_Phone_Phone]
GO
ALTER TABLE [dbo].[Company_Quote]  WITH CHECK ADD  CONSTRAINT [FK_Company_Quote_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_Quote] CHECK CONSTRAINT [FK_Company_Quote_Company]
GO
ALTER TABLE [dbo].[Company_Quote]  WITH CHECK ADD  CONSTRAINT [FK_Company_Quote_Quote] FOREIGN KEY([QuoteId])
REFERENCES [dbo].[Quote] ([QuoteId])
GO
ALTER TABLE [dbo].[Company_Quote] CHECK CONSTRAINT [FK_Company_Quote_Quote]
GO
ALTER TABLE [dbo].[Company_SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_Company_SalesOrder_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Company_SalesOrder] CHECK CONSTRAINT [FK_Company_SalesOrder_Company]
GO
ALTER TABLE [dbo].[Company_SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_Company_SalesOrder_SalesOrder] FOREIGN KEY([SalesOrderId])
REFERENCES [dbo].[SalesOrder] ([SalesOrderId])
GO
ALTER TABLE [dbo].[Company_SalesOrder] CHECK CONSTRAINT [FK_Company_SalesOrder_SalesOrder]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Employee] FOREIGN KEY([ManagerEmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Employee]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_User]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_User1]
GO
ALTER TABLE [dbo].[EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddress_EmailAddressType] FOREIGN KEY([EmailAddressTypeId])
REFERENCES [dbo].[EmailAddressType] ([EmailAddressTypeId])
GO
ALTER TABLE [dbo].[EmailAddress] CHECK CONSTRAINT [FK_EmailAddress_EmailAddressType]
GO
ALTER TABLE [dbo].[EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddress_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[EmailAddress] CHECK CONSTRAINT [FK_EmailAddress_User]
GO
ALTER TABLE [dbo].[EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddress_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[EmailAddress] CHECK CONSTRAINT [FK_EmailAddress_User1]
GO
ALTER TABLE [dbo].[EmailAddressType]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddressType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[EmailAddressType] CHECK CONSTRAINT [FK_EmailAddressType_User]
GO
ALTER TABLE [dbo].[EmailAddressType]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddressType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[EmailAddressType] CHECK CONSTRAINT [FK_EmailAddressType_User1]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Person]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_User]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_User1]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_SalesOrderHeader] FOREIGN KEY([SalesOrderId])
REFERENCES [dbo].[SalesOrder] ([SalesOrderId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_SalesOrderHeader]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User1]
GO
ALTER TABLE [dbo].[Invoice_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Activity_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[Invoice_Activity] CHECK CONSTRAINT [FK_Invoice_Activity_Activity]
GO
ALTER TABLE [dbo].[Invoice_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Activity_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[Invoice_Activity] CHECK CONSTRAINT [FK_Invoice_Activity_Invoice]
GO
ALTER TABLE [dbo].[Invoice_InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceLineItem_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[Invoice_InvoiceLineItem] CHECK CONSTRAINT [FK_Invoice_InvoiceLineItem_Invoice]
GO
ALTER TABLE [dbo].[Invoice_InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceLineItem_InvoiceLineItem] FOREIGN KEY([InvoiceLineItemId])
REFERENCES [dbo].[InvoiceLineItem] ([InvoiceLineItemId])
GO
ALTER TABLE [dbo].[Invoice_InvoiceLineItem] CHECK CONSTRAINT [FK_Invoice_InvoiceLineItem_InvoiceLineItem]
GO
ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLineItem_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [FK_InvoiceLineItem_User]
GO
ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLineItem_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [FK_InvoiceLineItem_User1]
GO
ALTER TABLE [dbo].[Lead]  WITH CHECK ADD  CONSTRAINT [FK_Lead_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Lead] CHECK CONSTRAINT [FK_Lead_User]
GO
ALTER TABLE [dbo].[Lead]  WITH CHECK ADD  CONSTRAINT [FK_Lead_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Lead] CHECK CONSTRAINT [FK_Lead_User1]
GO
ALTER TABLE [dbo].[LeadSource]  WITH CHECK ADD  CONSTRAINT [FK_LeadSource_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[LeadSource] CHECK CONSTRAINT [FK_LeadSource_User]
GO
ALTER TABLE [dbo].[LeadSource]  WITH CHECK ADD  CONSTRAINT [FK_LeadSource_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[LeadSource] CHECK CONSTRAINT [FK_LeadSource_User1]
GO
ALTER TABLE [dbo].[LeadStatus]  WITH CHECK ADD  CONSTRAINT [FK_LeadStatus_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[LeadStatus] CHECK CONSTRAINT [FK_LeadStatus_User]
GO
ALTER TABLE [dbo].[LeadStatus]  WITH CHECK ADD  CONSTRAINT [FK_LeadStatus_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[LeadStatus] CHECK CONSTRAINT [FK_LeadStatus_User1]
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_NoteType] FOREIGN KEY([NoteTypeId])
REFERENCES [dbo].[NoteType] ([NoteTypeId])
GO
ALTER TABLE [dbo].[Note] CHECK CONSTRAINT [FK_Note_NoteType]
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Note] CHECK CONSTRAINT [FK_Note_User]
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Note] CHECK CONSTRAINT [FK_Note_User1]
GO
ALTER TABLE [dbo].[NoteType]  WITH CHECK ADD  CONSTRAINT [FK_NoteType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[NoteType] CHECK CONSTRAINT [FK_NoteType_User]
GO
ALTER TABLE [dbo].[NoteType]  WITH CHECK ADD  CONSTRAINT [FK_NoteType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[NoteType] CHECK CONSTRAINT [FK_NoteType_User1]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Invoice]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_PaymentMethod] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethod] ([PaymentMethodId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_PaymentMethod]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_PaymentType] FOREIGN KEY([PaymentStatusId])
REFERENCES [dbo].[PaymentStatus] ([PaymentStatusId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_PaymentType]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_User]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_User1]
GO
ALTER TABLE [dbo].[PaymentLineItem]  WITH CHECK ADD  CONSTRAINT [FK_PaymentLineItem_InvoiceLineItem] FOREIGN KEY([InvoiceLineItemId])
REFERENCES [dbo].[InvoiceLineItem] ([InvoiceLineItemId])
GO
ALTER TABLE [dbo].[PaymentLineItem] CHECK CONSTRAINT [FK_PaymentLineItem_InvoiceLineItem]
GO
ALTER TABLE [dbo].[PaymentLineItem]  WITH CHECK ADD  CONSTRAINT [FK_PaymentLineItem_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([PaymentId])
GO
ALTER TABLE [dbo].[PaymentLineItem] CHECK CONSTRAINT [FK_PaymentLineItem_Payment]
GO
ALTER TABLE [dbo].[PaymentLineItem]  WITH CHECK ADD  CONSTRAINT [FK_PaymentLineItem_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PaymentLineItem] CHECK CONSTRAINT [FK_PaymentLineItem_User]
GO
ALTER TABLE [dbo].[PaymentLineItem]  WITH CHECK ADD  CONSTRAINT [FK_PaymentLineItem_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PaymentLineItem] CHECK CONSTRAINT [FK_PaymentLineItem_User1]
GO
ALTER TABLE [dbo].[PaymentMethod]  WITH CHECK ADD  CONSTRAINT [FK_PaymentMethod_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PaymentMethod] CHECK CONSTRAINT [FK_PaymentMethod_User]
GO
ALTER TABLE [dbo].[PaymentMethod]  WITH CHECK ADD  CONSTRAINT [FK_PaymentMethod_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PaymentMethod] CHECK CONSTRAINT [FK_PaymentMethod_User1]
GO
ALTER TABLE [dbo].[PaymentStatus]  WITH CHECK ADD  CONSTRAINT [FK_PaymentStatus_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PaymentStatus] CHECK CONSTRAINT [FK_PaymentStatus_User]
GO
ALTER TABLE [dbo].[PaymentStatus]  WITH CHECK ADD  CONSTRAINT [FK_PaymentStatus_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PaymentStatus] CHECK CONSTRAINT [FK_PaymentStatus_User1]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_User]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_User1]
GO
ALTER TABLE [dbo].[Person_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Person_Activity_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[Person_Activity] CHECK CONSTRAINT [FK_Person_Activity_Activity]
GO
ALTER TABLE [dbo].[Person_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Person_Activity_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_Activity] CHECK CONSTRAINT [FK_Person_Activity_Person]
GO
ALTER TABLE [dbo].[Person_Address]  WITH CHECK ADD  CONSTRAINT [FK_Person_Address_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Person_Address] CHECK CONSTRAINT [FK_Person_Address_Address]
GO
ALTER TABLE [dbo].[Person_Address]  WITH CHECK ADD  CONSTRAINT [FK_Person_Address_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_Address] CHECK CONSTRAINT [FK_Person_Address_Person]
GO
ALTER TABLE [dbo].[Person_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Person_Attachment_Attachment] FOREIGN KEY([AttachmentId])
REFERENCES [dbo].[Attachment] ([AttachmentId])
GO
ALTER TABLE [dbo].[Person_Attachment] CHECK CONSTRAINT [FK_Person_Attachment_Attachment]
GO
ALTER TABLE [dbo].[Person_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Person_Attachment_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_Attachment] CHECK CONSTRAINT [FK_Person_Attachment_Person]
GO
ALTER TABLE [dbo].[Person_Call]  WITH CHECK ADD  CONSTRAINT [FK_Person_Call_Call] FOREIGN KEY([CallId])
REFERENCES [dbo].[Call] ([CallId])
GO
ALTER TABLE [dbo].[Person_Call] CHECK CONSTRAINT [FK_Person_Call_Call]
GO
ALTER TABLE [dbo].[Person_Call]  WITH CHECK ADD  CONSTRAINT [FK_Person_Call_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_Call] CHECK CONSTRAINT [FK_Person_Call_Person]
GO
ALTER TABLE [dbo].[Person_EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_Person_EmailAddress_EmailAddress] FOREIGN KEY([EmailAddressId])
REFERENCES [dbo].[EmailAddress] ([EmailAddressId])
GO
ALTER TABLE [dbo].[Person_EmailAddress] CHECK CONSTRAINT [FK_Person_EmailAddress_EmailAddress]
GO
ALTER TABLE [dbo].[Person_EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_Person_EmailAddress_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_EmailAddress] CHECK CONSTRAINT [FK_Person_EmailAddress_Person]
GO
ALTER TABLE [dbo].[Person_Note]  WITH CHECK ADD  CONSTRAINT [FK_Person_Note_Note] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([NoteId])
GO
ALTER TABLE [dbo].[Person_Note] CHECK CONSTRAINT [FK_Person_Note_Note]
GO
ALTER TABLE [dbo].[Person_Note]  WITH CHECK ADD  CONSTRAINT [FK_Person_Note_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_Note] CHECK CONSTRAINT [FK_Person_Note_Person]
GO
ALTER TABLE [dbo].[Person_Phone]  WITH CHECK ADD  CONSTRAINT [FK_Person_Phone_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_Phone] CHECK CONSTRAINT [FK_Person_Phone_Person]
GO
ALTER TABLE [dbo].[Person_Phone]  WITH CHECK ADD  CONSTRAINT [FK_Person_Phone_Phone] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phone] ([PhoneId])
GO
ALTER TABLE [dbo].[Person_Phone] CHECK CONSTRAINT [FK_Person_Phone_Phone]
GO
ALTER TABLE [dbo].[Person_Quote]  WITH CHECK ADD  CONSTRAINT [FK_Person_Quote_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_Quote] CHECK CONSTRAINT [FK_Person_Quote_Person]
GO
ALTER TABLE [dbo].[Person_Quote]  WITH CHECK ADD  CONSTRAINT [FK_Person_Quote_Quote] FOREIGN KEY([QuoteId])
REFERENCES [dbo].[Quote] ([QuoteId])
GO
ALTER TABLE [dbo].[Person_Quote] CHECK CONSTRAINT [FK_Person_Quote_Quote]
GO
ALTER TABLE [dbo].[Person_SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_Person_SalesOrder_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[Person_SalesOrder] CHECK CONSTRAINT [FK_Person_SalesOrder_Person]
GO
ALTER TABLE [dbo].[Person_SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_Person_SalesOrder_SalesOrder] FOREIGN KEY([SalesOrderId])
REFERENCES [dbo].[SalesOrder] ([SalesOrderId])
GO
ALTER TABLE [dbo].[Person_SalesOrder] CHECK CONSTRAINT [FK_Person_SalesOrder_SalesOrder]
GO
ALTER TABLE [dbo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_PhoneType] FOREIGN KEY([PhoneTypeId])
REFERENCES [dbo].[PhoneType] ([PhoneTypeId])
GO
ALTER TABLE [dbo].[Phone] CHECK CONSTRAINT [FK_Phone_PhoneType]
GO
ALTER TABLE [dbo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Phone] CHECK CONSTRAINT [FK_Phone_User]
GO
ALTER TABLE [dbo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Phone] CHECK CONSTRAINT [FK_Phone_User1]
GO
ALTER TABLE [dbo].[PhoneType]  WITH CHECK ADD  CONSTRAINT [FK_PhoneType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PhoneType] CHECK CONSTRAINT [FK_PhoneType_User]
GO
ALTER TABLE [dbo].[PhoneType]  WITH CHECK ADD  CONSTRAINT [FK_PhoneType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PhoneType] CHECK CONSTRAINT [FK_PhoneType_User1]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductType] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductType] ([ProductTypeId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductType]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_User]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_User1]
GO
ALTER TABLE [dbo].[ProductGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ProductGroup] CHECK CONSTRAINT [FK_ProductGroup_User]
GO
ALTER TABLE [dbo].[ProductGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ProductGroup] CHECK CONSTRAINT [FK_ProductGroup_User1]
GO
ALTER TABLE [dbo].[ProductGroup_Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_Product_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductGroup_Product] CHECK CONSTRAINT [FK_ProductGroup_Product_Product]
GO
ALTER TABLE [dbo].[ProductGroup_Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_Product_ProductGroup] FOREIGN KEY([ProductGroupId])
REFERENCES [dbo].[ProductGroup] ([ProductGroupId])
GO
ALTER TABLE [dbo].[ProductGroup_Product] CHECK CONSTRAINT [FK_ProductGroup_Product_ProductGroup]
GO
ALTER TABLE [dbo].[ProductType]  WITH CHECK ADD  CONSTRAINT [FK_ProductType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ProductType] CHECK CONSTRAINT [FK_ProductType_User]
GO
ALTER TABLE [dbo].[ProductType]  WITH CHECK ADD  CONSTRAINT [FK_ProductType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ProductType] CHECK CONSTRAINT [FK_ProductType_User1]
GO
ALTER TABLE [dbo].[Quote]  WITH CHECK ADD  CONSTRAINT [FK_Quote_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Quote] CHECK CONSTRAINT [FK_Quote_User]
GO
ALTER TABLE [dbo].[Quote]  WITH CHECK ADD  CONSTRAINT [FK_Quote_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Quote] CHECK CONSTRAINT [FK_Quote_User1]
GO
ALTER TABLE [dbo].[Quote_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Quote_Activity_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[Quote_Activity] CHECK CONSTRAINT [FK_Quote_Activity_Activity]
GO
ALTER TABLE [dbo].[Quote_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Quote_Activity_Quote] FOREIGN KEY([QuoteId])
REFERENCES [dbo].[Quote] ([QuoteId])
GO
ALTER TABLE [dbo].[Quote_Activity] CHECK CONSTRAINT [FK_Quote_Activity_Quote]
GO
ALTER TABLE [dbo].[Quote_QuoteLineItem]  WITH CHECK ADD  CONSTRAINT [FK_Quote_QuoteLineItem_Quote] FOREIGN KEY([QuoteId])
REFERENCES [dbo].[Quote] ([QuoteId])
GO
ALTER TABLE [dbo].[Quote_QuoteLineItem] CHECK CONSTRAINT [FK_Quote_QuoteLineItem_Quote]
GO
ALTER TABLE [dbo].[Quote_QuoteLineItem]  WITH CHECK ADD  CONSTRAINT [FK_Quote_QuoteLineItem_QuoteLineItem] FOREIGN KEY([QuoteLineItemId])
REFERENCES [dbo].[QuoteLineItem] ([QuoteLineItemId])
GO
ALTER TABLE [dbo].[Quote_QuoteLineItem] CHECK CONSTRAINT [FK_Quote_QuoteLineItem_QuoteLineItem]
GO
ALTER TABLE [dbo].[QuoteLineItem]  WITH CHECK ADD  CONSTRAINT [FK_QuoteLineItem_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[QuoteLineItem] CHECK CONSTRAINT [FK_QuoteLineItem_User]
GO
ALTER TABLE [dbo].[QuoteLineItem]  WITH CHECK ADD  CONSTRAINT [FK_QuoteLineItem_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[QuoteLineItem] CHECK CONSTRAINT [FK_QuoteLineItem_User1]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_User]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_User1]
GO
ALTER TABLE [dbo].[SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_Quote] FOREIGN KEY([QuoteId])
REFERENCES [dbo].[Quote] ([QuoteId])
GO
ALTER TABLE [dbo].[SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_Quote]
GO
ALTER TABLE [dbo].[SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_SalesOrderStatus] FOREIGN KEY([SalesOrderStatusId])
REFERENCES [dbo].[SalesOrderStatus] ([SalesOrderStatusId])
GO
ALTER TABLE [dbo].[SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_SalesOrderStatus]
GO
ALTER TABLE [dbo].[SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_User]
GO
ALTER TABLE [dbo].[SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_User1]
GO
ALTER TABLE [dbo].[SalesOrder_Activity]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_Activity_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityId])
GO
ALTER TABLE [dbo].[SalesOrder_Activity] CHECK CONSTRAINT [FK_SalesOrder_Activity_Activity]
GO
ALTER TABLE [dbo].[SalesOrder_Activity]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_Activity_SalesOrder] FOREIGN KEY([SalesOrderId])
REFERENCES [dbo].[SalesOrder] ([SalesOrderId])
GO
ALTER TABLE [dbo].[SalesOrder_Activity] CHECK CONSTRAINT [FK_SalesOrder_Activity_SalesOrder]
GO
ALTER TABLE [dbo].[SalesOrder_SalesOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_SalesOrderLineItem_SalesOrder] FOREIGN KEY([SalesOrderId])
REFERENCES [dbo].[SalesOrder] ([SalesOrderId])
GO
ALTER TABLE [dbo].[SalesOrder_SalesOrderLineItem] CHECK CONSTRAINT [FK_SalesOrder_SalesOrderLineItem_SalesOrder]
GO
ALTER TABLE [dbo].[SalesOrder_SalesOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_SalesOrderLineItem_SalesOrderLineItem] FOREIGN KEY([SalesOrderLineItemId])
REFERENCES [dbo].[SalesOrderLineItem] ([SalesOrderLineItemId])
GO
ALTER TABLE [dbo].[SalesOrder_SalesOrderLineItem] CHECK CONSTRAINT [FK_SalesOrder_SalesOrderLineItem_SalesOrderLineItem]
GO
ALTER TABLE [dbo].[SalesOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLineItem_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesOrderLineItem] CHECK CONSTRAINT [FK_SalesOrderLineItem_User]
GO
ALTER TABLE [dbo].[SalesOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLineItem_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesOrderLineItem] CHECK CONSTRAINT [FK_SalesOrderLineItem_User1]
GO
ALTER TABLE [dbo].[SalesOrderLineItem_Product]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLineItem_Product_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[SalesOrderLineItem_Product] CHECK CONSTRAINT [FK_SalesOrderLineItem_Product_Product]
GO
ALTER TABLE [dbo].[SalesOrderLineItem_Product]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLineItem_Product_SalesOrderLineItem] FOREIGN KEY([SalesOrderLineItemId])
REFERENCES [dbo].[SalesOrderLineItem] ([SalesOrderLineItemId])
GO
ALTER TABLE [dbo].[SalesOrderLineItem_Product] CHECK CONSTRAINT [FK_SalesOrderLineItem_Product_SalesOrderLineItem]
GO
ALTER TABLE [dbo].[SalesOrderLineItem_Service]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLineItem_Service_SalesOrderLineItem] FOREIGN KEY([SalesOrderLineItemId])
REFERENCES [dbo].[SalesOrderLineItem] ([SalesOrderLineItemId])
GO
ALTER TABLE [dbo].[SalesOrderLineItem_Service] CHECK CONSTRAINT [FK_SalesOrderLineItem_Service_SalesOrderLineItem]
GO
ALTER TABLE [dbo].[SalesOrderLineItem_Service]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLineItem_Service_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([ServiceId])
GO
ALTER TABLE [dbo].[SalesOrderLineItem_Service] CHECK CONSTRAINT [FK_SalesOrderLineItem_Service_Service]
GO
ALTER TABLE [dbo].[SalesOrderStatus]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderStatus_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesOrderStatus] CHECK CONSTRAINT [FK_SalesOrderStatus_User]
GO
ALTER TABLE [dbo].[SalesOrderStatus]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderStatus_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesOrderStatus] CHECK CONSTRAINT [FK_SalesOrderStatus_User1]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceType] FOREIGN KEY([ServiceTypeId])
REFERENCES [dbo].[ServiceType] ([ServiceTypeId])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceType]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_User]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_User1]
GO
ALTER TABLE [dbo].[ServiceType]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ServiceType] CHECK CONSTRAINT [FK_ServiceType_User]
GO
ALTER TABLE [dbo].[ServiceType]  WITH CHECK ADD  CONSTRAINT [FK_ServiceType_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ServiceType] CHECK CONSTRAINT [FK_ServiceType_User1]
GO
ALTER TABLE [dbo].[ShipMethod]  WITH CHECK ADD  CONSTRAINT [FK_ShipMethod_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ShipMethod] CHECK CONSTRAINT [FK_ShipMethod_User]
GO
ALTER TABLE [dbo].[ShipMethod]  WITH CHECK ADD  CONSTRAINT [FK_ShipMethod_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ShipMethod] CHECK CONSTRAINT [FK_ShipMethod_User1]
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_State_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_State_User]
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_State_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_State_User1]
GO
ALTER TABLE [dbo].[TaxCode]  WITH CHECK ADD  CONSTRAINT [FK_TaxCode_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[TaxCode] CHECK CONSTRAINT [FK_TaxCode_User]
GO
ALTER TABLE [dbo].[TaxCode]  WITH CHECK ADD  CONSTRAINT [FK_TaxCode_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[TaxCode] CHECK CONSTRAINT [FK_TaxCode_User1]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_User]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_User1]
GO
ALTER TABLE [dbo].[Team_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Team_Employee_Employee1] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Team_Employee] CHECK CONSTRAINT [FK_Team_Employee_Employee1]
GO
ALTER TABLE [dbo].[Team_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Team_Employee_Team1] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[Team_Employee] CHECK CONSTRAINT [FK_Team_Employee_Team1]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Person]
GO
ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [FK_User_Role_Role]
GO
ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [FK_User_Role_User]
GO
ALTER TABLE [dbo].[Workflow]  WITH CHECK ADD  CONSTRAINT [FK_Workflow_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Workflow] CHECK CONSTRAINT [FK_Workflow_User]
GO
ALTER TABLE [dbo].[Workflow]  WITH CHECK ADD  CONSTRAINT [FK_Workflow_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Workflow] CHECK CONSTRAINT [FK_Workflow_User1]
GO
ALTER TABLE [dbo].[WorkflowDefinition]  WITH CHECK ADD  CONSTRAINT [FK_WorkflowDefinition_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[WorkflowDefinition] CHECK CONSTRAINT [FK_WorkflowDefinition_User]
GO
ALTER TABLE [dbo].[WorkflowDefinition]  WITH CHECK ADD  CONSTRAINT [FK_WorkflowDefinition_User1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[WorkflowDefinition] CHECK CONSTRAINT [FK_WorkflowDefinition_User1]
GO

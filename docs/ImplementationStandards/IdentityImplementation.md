# ASP.NET Core Identity Implementation

## Overview
This document outlines the implementation of ASP.NET Core Identity while preserving our existing User and Role tables. We'll extend the current schema to support all Identity features while maintaining backward compatibility.

## Required Identity Tables

### 1. AspNetUsers (Map to existing User table)
```sql
-- Add missing Identity columns to User table
ALTER TABLE [dbo].[User] ADD
    [NormalizedUserName] [nvarchar](256) NULL,
    [Email] [nvarchar](256) NULL,
    [NormalizedEmail] [nvarchar](256) NULL,
    [EmailConfirmed] [bit] NOT NULL DEFAULT(0),
    [PasswordHash] [nvarchar](max) NULL,
    [SecurityStamp] [nvarchar](max) NULL,
    [ConcurrencyStamp] [nvarchar](max) NULL,
    [PhoneNumber] [nvarchar](50) NULL,
    [PhoneNumberConfirmed] [bit] NOT NULL DEFAULT(0),
    [TwoFactorEnabled] [bit] NOT NULL DEFAULT(0),
    [LockoutEnd] [datetimeoffset](7) NULL,
    [LockoutEnabled] [bit] NOT NULL DEFAULT(0),
    [AccessFailedCount] [int] NOT NULL DEFAULT(0)

-- Add indexes for performance
CREATE NONCLUSTERED INDEX [IX_User_NormalizedUserName] ON [dbo].[User]
(
    [NormalizedUserName] ASC
) WHERE [NormalizedUserName] IS NOT NULL

CREATE NONCLUSTERED INDEX [IX_User_NormalizedEmail] ON [dbo].[User]
(
    [NormalizedEmail] ASC
) WHERE [NormalizedEmail] IS NOT NULL
```

### 2. AspNetRoles (Map to existing Role table)
```sql
-- Add missing Identity columns to Role table
ALTER TABLE [dbo].[Role] ADD
    [NormalizedName] [nvarchar](256) NULL,
    [ConcurrencyStamp] [nvarchar](max) NULL

-- Add index for performance
CREATE NONCLUSTERED INDEX [IX_Role_NormalizedName] ON [dbo].[Role]
(
    [NormalizedName] ASC
) WHERE [NormalizedName] IS NOT NULL
```

### 3. AspNetUserClaims
```sql
CREATE TABLE [dbo].[UserClaim]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserId] [uniqueidentifier] NOT NULL,
    [ClaimType] [nvarchar](max) NULL,
    [ClaimValue] [nvarchar](max) NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClaim_User] FOREIGN KEY([UserId]) 
        REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
)

CREATE NONCLUSTERED INDEX [IX_UserClaim_UserId] ON [dbo].[UserClaim]([UserId] ASC)
```

### 4. AspNetUserTokens
```sql
CREATE TABLE [dbo].[UserToken]
(
    [UserId] [uniqueidentifier] NOT NULL,
    [LoginProvider] [nvarchar](450) NOT NULL,
    [Name] [nvarchar](450) NOT NULL,
    [Value] [nvarchar](max) NULL,
    CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
    (
        [UserId] ASC,
        [LoginProvider] ASC,
        [Name] ASC
    ),
    CONSTRAINT [FK_UserToken_User] FOREIGN KEY([UserId])
        REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
)
```

### 5. AspNetUserLogins
```sql
CREATE TABLE [dbo].[UserLogin]
(
    [LoginProvider] [nvarchar](450) NOT NULL,
    [ProviderKey] [nvarchar](450) NOT NULL,
    [ProviderDisplayName] [nvarchar](max) NULL,
    [UserId] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
    (
        [LoginProvider] ASC,
        [ProviderKey] ASC
    ),
    CONSTRAINT [FK_UserLogin_User] FOREIGN KEY([UserId])
        REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
)

CREATE NONCLUSTERED INDEX [IX_UserLogin_UserId] ON [dbo].[UserLogin]([UserId] ASC)
```

### 6. AspNetRoleClaims
```sql
CREATE TABLE [dbo].[RoleClaim]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [RoleId] [uniqueidentifier] NOT NULL,
    [ClaimType] [nvarchar](max) NULL,
    [ClaimValue] [nvarchar](max) NULL,
    CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoleClaim_Role] FOREIGN KEY([RoleId])
        REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE
)

CREATE NONCLUSTERED INDEX [IX_RoleClaim_RoleId] ON [dbo].[RoleClaim]([RoleId] ASC)
```

### 7. AspNetUserRoles (Map to existing User_Role table if exists)
```sql
CREATE TABLE [dbo].[User_Role]
(
    [UserId] [uniqueidentifier] NOT NULL,
    [RoleId] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
    (
        [UserId] ASC,
        [RoleId] ASC
    ),
    CONSTRAINT [FK_User_Role_Role] FOREIGN KEY([RoleId])
        REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_User_Role_User] FOREIGN KEY([UserId])
        REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
)

CREATE NONCLUSTERED INDEX [IX_User_Role_RoleId] ON [dbo].[User_Role]([RoleId] ASC)
```

## Implementation Steps

1. **Infrastructure Layer Changes**

```csharp
public class ApplicationUser : IdentityUser<Guid>
{
    public Guid PersonId { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Guid ModifiedBy { get; set; }
    
    // Navigation property
    public virtual Person Person { get; set; }
}

public class ApplicationRole : IdentityRole<Guid>
{
    public string Description { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Guid ModifiedBy { get; set; }
}
```

2. **Identity Configuration**

```csharp
public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = 
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
```

3. **Store Implementation**

```csharp
public class CustomUserStore : UserStore<ApplicationUser, ApplicationRole, 
    ApplicationDbContext, Guid>
{
    public CustomUserStore(ApplicationDbContext context, 
        IdentityErrorDescriber describer = null) 
        : base(context, describer)
    {
    }

    public override async Task<IdentityResult> CreateAsync(
        ApplicationUser user, 
        CancellationToken cancellationToken = default)
    {
        // Custom implementation to handle our existing User table
        using var transaction = await Context.Database
            .BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await base.CreateAsync(user, cancellationToken);
            if (result.Succeeded)
            {
                // Additional custom logic
                await transaction.CommitAsync(cancellationToken);
            }
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
```

## Security Considerations

1. **Password Hashing**
   - Use ASP.NET Core Identity's built-in password hasher
   - Migrate existing password hashes if needed

2. **Claims-Based Authorization**
   - Implement policy-based authorization
   - Use claims for fine-grained permissions

3. **Two-Factor Authentication**
   - Enable 2FA with authenticator apps
   - Implement SMS-based 2FA (optional)

4. **External Authentication**
   - Configure OAuth/OpenID Connect providers
   - Map external logins to local accounts

## Migration Strategy

1. **Data Migration**
```sql
-- Example migration script for existing users
INSERT INTO [dbo].[UserClaim] (UserId, ClaimType, ClaimValue)
SELECT UserId, 'Permission', 'Legacy'
FROM [dbo].[User]
WHERE Active = 1
```

2. **Application Updates**
   - Update user management endpoints
   - Implement new Identity features gradually
   - Maintain backward compatibility

## Monitoring and Maintenance

1. **Security Monitoring**
   - Track failed login attempts
   - Monitor user activities
   - Audit security-related events

2. **Performance Optimization**
   - Index maintenance
   - Query optimization
   - Cache user claims

## Testing Requirements

1. **Integration Tests**
   - User registration
   - Authentication flows
   - Role-based access
   - Claims-based authorization

2. **Security Tests**
   - Password policy enforcement
   - Account lockout
   - Token validation
   - 2FA workflows

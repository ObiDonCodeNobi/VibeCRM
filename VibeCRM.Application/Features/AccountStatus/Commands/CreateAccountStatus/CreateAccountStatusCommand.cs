using MediatR;
using VibeCRM.Application.Features.AccountStatus.DTOs;

namespace VibeCRM.Application.Features.AccountStatus.Commands.CreateAccountStatus
{
    /// <summary>
    /// Command for creating a new account status in the system.
    /// Implements the CQRS command pattern for account status creation.
    /// </summary>
    public class CreateAccountStatusCommand : IRequest<AccountStatusDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account status.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the status name (e.g., "Active", "Inactive", "Prospect").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the description of the account status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting account statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the account status.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the account status.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        public CreateAccountStatusCommand() 
        { 
            Status = string.Empty; 
            Description = string.Empty; 
        }
    }
}
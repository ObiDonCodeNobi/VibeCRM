using MediatR;
using VibeCRM.Shared.DTOs.AccountStatus;

namespace VibeCRM.Application.Features.AccountStatus.Commands.UpdateAccountStatus
{
    /// <summary>
    /// Command for updating an existing account status in the system.
    /// Implements the CQRS command pattern for account status updates.
    /// </summary>
    public class UpdateAccountStatusCommand : IRequest<AccountStatusDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account status to update.
        /// </summary>
        public Guid Id { get; set; }

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
        /// Gets or sets the ID of the user who is modifying the account status.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        public UpdateAccountStatusCommand()
        {
            Status = string.Empty;
            Description = string.Empty;
        }
    }
}